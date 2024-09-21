using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassStudent;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;
using WebApi_QLSV.Services.Interfaces.StudentFd;

namespace WebApi_QLSV.Services.Implements.StudentAndClassFd
{
    public class ClassService : IClassServices
    {
        private readonly ApplicationDbContext _context;
        public ClassService(ApplicationDbContext context)
        {
            _context = context;
        }
        public LopHP AddClass(AddClassDtos input)
        {
            int Count = _context.LopHPs.Count(u => u.NganhId == input.NganhId);
            int Year = DateTime.Now.Year;
            string ClassName = (Year - 1955).ToString() + input.NganhId + (Count + 1).ToString();
            var ClassStudent = new LopHP
            {
                NganhId = input.NganhId,
                TenLopHP = ClassName,
                MaxStudent = input.MaxStudent,
            };
            _context.LopHPs.Add(ClassStudent);
            _context.SaveChanges();
            return ClassStudent;

        }
        public PageResultDtos<LopHP> GetAll([FromQuery] FilterDtos input) 
        {
            var result = new PageResultDtos<LopHP>();

            var query = _context.LopHPs.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord) ||
                        e.TenLopHP.ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();

            query = query
                    .OrderByDescending(e => e.TenLopHP)
                    .ThenByDescending(e => e.NganhId)
                    .Skip(input.Skip())
                    .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

    }
}
