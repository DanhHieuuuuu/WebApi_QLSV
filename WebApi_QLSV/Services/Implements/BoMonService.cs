using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.BoMonFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class BoMonService : IBoMonService
    {
        private readonly ApplicationDbContext _context;

        public BoMonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BoMon AddBoMon(AddBoMonDtos input)
        {
            var results = new BoMon
            {
                BoMonId = input.BoMonId,
                TenBoMon = input.TenBoMon,
                TruongBoMon = input.TruongBoMon,
                PhoBoMon = input.PhoBoMon,
                SoLuongGV = input.SoLuongGV,
            };
            _context.BoMons.Add(results);
            _context.SaveChanges();
            return results;
        }

        public PageResultDtos<BoMon> GetAllBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<BoMon>();

            var query = _context.BoMons.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenBoMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.TenBoMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
