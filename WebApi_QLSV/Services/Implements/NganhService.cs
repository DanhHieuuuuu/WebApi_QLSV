using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class NganhService : INganhService
    {
        private readonly ApplicationDbContext _context;

        public NganhService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Nganh AddNganh(AddNganhDtos input)
        {
            var findCTK =
                _context.CTKhungs.FirstOrDefault(n => n.CTKhungId == input.CTKhungId.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại ID CTK: {input.CTKhungId}");
            var findKHoa =
                _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại Id khoa: {input.KhoaId}");
            var result = new Nganh
            {
                NganhId = input.NganhId.ToUpper(),
                TenNganh = input.TenNganh,
                CTKhungId = input.CTKhungId.ToUpper(),
                InfoNganh = input.NganhId,
                KhoaId = input.KhoaId.ToUpper(),
                SumClass = 0,
            };
            _context.Nganhs.Add(result);
            _context.SaveChanges();
            return result;
        }

        public PageResultDtos<Nganh> GetAllNganh([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<Nganh>();

            var query = _context.Nganhs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenNganh.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(e => e.TenNganh)
                .ThenByDescending(e => e.SumClass)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
