using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class LopQLService : ILopQLService
    {
        private readonly ApplicationDbContext _context;

        public LopQLService(ApplicationDbContext context)
        {
            _context = context;
        }

        public LopQL AddLopQL(AddLopQLDtos input)
        {
            var findNganh =
                _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId.ToUpper())
                ?? throw new UserExceptions("Không tồn tại nghành này");
            var result = new LopQL
            {
                TenLopQL = input.TenLopQL.ToUpper(),
                ChuNhiem = input.ChuNhiem,
                LopTruong = input.LopTruong,
                LopPho = input.LopPho,
                MaxStudent = input.MaxStudent,
                NganhId = input.NganhId.ToUpper(),
            };
            _context.LopQLs.Add(result);
            var nganhs = _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId);
            int sum = _context.LopQLs.Where(l => l.NganhId == nganhs.NganhId).Count() + 1;

            nganhs.SumClass = sum;
            _context.SaveChanges();

            return result;
        }

        public PageResultDtos<LopQL> GetAllLopQL([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<LopQL>();

            var query = _context.LopQLs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenLopQL.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(e => e.TenLopQL)
                .ThenByDescending(e => e.ChuNhiem)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
