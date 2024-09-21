using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.CTKhungFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class CTKhungService : ICTKhungService
    {
        private readonly ApplicationDbContext _context;

        public CTKhungService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CTKhung AddCTKhung(AddCTKhungDtos input)
        {
            int tongtin = 0;
            for (int i = 0; i < input.MonHocs.Count(); i++)
            {
                var mon = new MonHoc
                {
                    MonId = input.MonHocs[i].MonId,
                    TenMon = input.MonHocs[i].TenMon,
                    KiHoc = input.MonHocs[i].KiHoc,
                    Sotin = input.MonHocs[i].Sotin,
                    CTKhungId = input.CTKhungId,
                };
                tongtin += input.MonHocs[i].Sotin;
                _context.MonHocs.Add(mon);
            }
            var result = new CTKhung { CTKhungId = input.CTKhungId, TongSoTin = tongtin };
            _context.CTKhungs.Add(result);
            _context.SaveChanges();
            return result;
        }

        public PageResultDtos<CTKhung> GetAllCTKhung([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<CTKhung>();

            var query = _context.CTKhungs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.CTKhungId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(e => e.CTKhungId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<MonHoc> GetAllMonHocInCTK([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHoc>();

            var query = _context.MonHocs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderByDescending(e => e.TenMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
