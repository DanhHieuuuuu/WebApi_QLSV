using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Entities;
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

        public Khoa AddKhoa([FromQuery] AddKhoaDtos input)
        {
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
                .OrderByDescending(e => e.KhoaId)
                .ThenByDescending(e => e.TruongKhoa)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
