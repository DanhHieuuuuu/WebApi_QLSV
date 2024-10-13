using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class KhoaService : IKhoaService
    {
        private readonly ApplicationDbContext _context;

        public KhoaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Khoa AddKhoa([FromBody] AddKhoaDtos input)
        {
            var findKhoa = _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId);
            if(findKhoa != null)
            {
                throw new UserExceptions("Trung mã khoa");
            };
            var result = new Khoa
            {
                KhoaId = input.KhoaId.ToUpper(),
                TenKhoa = input.TenKhoa,
                TruongKhoa = input.TruongKhoa,
                PhoKhoa = input.PhoKhoa,
            };
            _context.Khoas.Add(result);
            _context.SaveChanges();
            return result;
        }

        public PageResultDtos<Khoa> GetAllKhoa([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<Khoa>();

            var query = _context.Khoas.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.KhoaId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.KhoaId)
                .ThenByDescending(e => e.TruongKhoa)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public PageResultDtos<KhoaDetailsDtos> GetKhoaDetail([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<KhoaDetailsDtos>();
            var group1 = _context.Nganhs.GroupBy(e => e.KhoaId).Select(
                g => new
                {
                    khoaId = g.Key,
                    nganh = g.OrderBy(e => e.TenNganh).ToList(),
                }
                );
            var group2 = _context.BoMons.GroupBy(e => e.KhoaId).Select(
                g => new
                {
                    khoaId = g.Key,
                    boMon = g.OrderBy(e => e.TenBoMon).ToList(),
                }
                );
            var group3 = from k in _context.Khoas
                         select new Khoa
                         {
                             KhoaId = k.KhoaId,
                             TenKhoa = k.TenKhoa,
                             PhoKhoa = k.PhoKhoa,
                             TruongKhoa = k.TruongKhoa,
                         };
            var listkhoa = new List<KhoaDetailsDtos>();
            foreach (var item in group3.ToList())
            {
                var khoaDetail = new KhoaDetailsDtos();
                khoaDetail.Khoa = item;
                foreach (var item1 in group1)
                {
                    if (item.KhoaId == item1.khoaId)
                    {
                        khoaDetail.nganhs = item1.nganh;
                    }
                }
                foreach (var item2 in group2)
                {
                    if (item.KhoaId == item2.khoaId)
                    {
                        khoaDetail.boMons = item2.boMon;
                    }
                }
                listkhoa.Add( khoaDetail );
            }
            var query = listkhoa.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Khoa.TenKhoa.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.Khoa.TenKhoa)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    };
}
