using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.CauHoiFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class CauHoiService : ICauHoiService
    {
        private readonly ApplicationDbContext _dbContext;

        public CauHoiService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public CauHoi AddCauHoi(AddCauHoiDtos input)
        {
            var result = new CauHoi
            {
                CauHoiId = input.CauHoiId,
                NoiDungCauHoi = input.NoiDungCauHoi,
                Role = input.Role,
                MaxDiem = input.MaxDiem,
            };
            _dbContext.CauHois.Add(result);
            _dbContext.SaveChanges();
            return result;
        }

        public PageResultDtos<CauHoi> GetAllCauHoi([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<CauHoi>();

            var query = _dbContext.CauHois.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Role.ToString().ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.NoiDungCauHoi).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
