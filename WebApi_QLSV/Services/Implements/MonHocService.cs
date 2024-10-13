using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;
using static Azure.Core.HttpHeader;

namespace WebApi_QLSV.Services.Implements
{
    public class MonHocService : IMonHocService
    {
        private readonly ApplicationDbContext _context;
        public MonHocService(ApplicationDbContext context)
        {
            _context = context;
        }
        public MonHoc AddMonHoc(AddMonHocDtos input)
        {
            var findBoMon = _context.BoMons.FirstOrDefault(c => c.BoMonId == input.BoMonId)
                 ?? throw new UserExceptions("Không tồn tại bộ môn");
           var result = new MonHoc
           {
               MaMonHoc = input.MaMonHoc,
               TenMon = input.TenMon,
               SoTin = input.Sotin,
               BoMonId = input.BoMonId,
           };
            _context.MonHocs.Add(result);
            _context.SaveChanges();
            return result;
        }
        public PageResultDtos<MonHoc> GetAllMonHoc([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHoc>();

            var query = _context.MonHocs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.TenMon)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public PageResultDtos<MonHocTrongNganhDtos> GetAllMonTrongNganh([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHocTrongNganhDtos>();
            var group = _context.MonHocs.GroupBy(c => c.BoMonId).Select(l => new
            {
                boMonId = l.Key,
                monHoc = l.OrderBy(e => e.TenMon).ToList(),
            });
            var listMonHoc = new List<MonHocTrongNganhDtos>();
            foreach (var item in group)
            {
                var newMonHoc = new MonHocTrongNganhDtos();
                newMonHoc.MaBoMon = item.boMonId;
                newMonHoc.monHocs = item.monHoc;
                listMonHoc.Add(newMonHoc);
            }
            var query = listMonHoc.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.MaBoMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.MaBoMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        //public PageResultDtos<MonHocTrongNganhDtos> GetAllMonTrongNganh([FromQuery] FilterDtos input)
        //{
        //    var result = new PageResultDtos<MonHocTrongNganhDtos>();
        //    var group = _context.MonHocs.Join(_context.BoMons,
        //            MonHocs => MonHocs.BoMonId,
        //            boMons => boMons.BoMonId,
        //            (MonHocs, boMons) => new
        //            {
        //                maBoMon = MonHocs.BoMonId,
                        
        //                tenBoMon = boMons.TenBoMon,
        //                maMonHoc = MonHocs.MaMonHoc,
        //                tenMon = MonHocs.TenMon,
        //                soTins = MonHocs.SoTin,
        //            }
        //        ).GroupBy(g => g.maBoMon).Select(l => new
        //        {
        //            maBoMon = l.Key,
        //            monHoc = l.OrderBy(e => e.tenMon).ToList(),
        //        });
        //    var listMonHoc = new List<MonHocTrongNganhDtos>();
        //    foreach (var item in group)
        //    {
        //        var newMonHoc = new MonHocTrongNganhDtos();
        //        newMonHoc.MaBoMon = item.maBoMon;
        //        foreach (var item1 in item.monHoc)
        //        {
        //            newMonHoc.TenBoMon = item1.tenBoMon;
        //            var m = new MonHoc
        //            {
        //                MaMonHoc = item1.maMonHoc,
        //                TenMon = item1.tenMon,
        //                SoTin = item1.soTins,
        //                BoMonId = item1.maBoMon,

        //            };
        //            newMonHoc.monHocs.Add(m);
        //        }
        //        listMonHoc.Add(newMonHoc);
        //    }
        //    var query = listMonHoc.Where(e =>
        //        string.IsNullOrEmpty(input.KeyWord)
        //        || e.MaBoMon.ToLower().Contains(input.KeyWord.ToLower())
        //    );
        //    result.TotalItem = query.Count();
        //    query = query.OrderBy(e => e.MaBoMon).Skip(input.Skip()).Take(input.PageSize);

        //    result.Items = query.ToList();

        //    return result;
        //}
    }
}
