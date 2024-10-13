using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
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
            // tìm chủ nhiệm
            var findTeacher = _context.Teachers.FirstOrDefault(n => n.TeacherId == input.TeacherId);
            if (findTeacher == null)
            {
                throw new UserExceptions("Không tồn tại mã chủ nhiệm");
            };
            var result = new LopQL
            {
                LopQLId = input.TenLopQL.ToUpper(),
                TeacherId = input.TeacherId,
                MaxStudent = 0,
                NganhId = input.NganhId.ToUpper(),
            };
            _context.LopQLs.Add(result);
            var nganhs = _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId);
            int sum = _context.LopQLs.Where(l => l.NganhId == nganhs.NganhId).Count() + 1;

            nganhs.SumClass = sum;
            _context.SaveChanges();

            return result;
        }
        public void AddLopTruongLopPho([FromQuery] AddLTLP input, string classId)
        {
            var findStudent = from stu in _context.Students
                              where stu.LopQLId == classId
                              select stu;
            if (findStudent == null)
            {
                throw new UserExceptions("Lớp chưa có sinh viên");
            }
            var findClass = _context.LopQLs.FirstOrDefault(l => l.LopQLId == classId)
                ?? throw new UserExceptions($"Unable to find class {classId}");
            findClass.LopTruongId = input.LopTruongId;
            findClass.LopPhoId = input.LopPhoId;
            _context.LopQLs.Update(findClass);
            _context.SaveChanges();
        }
        public PageResultDtos<LopQL> GetAllLopQL([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<LopQL>();

            var query = _context.LopQLs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.LopQLId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(e => e.LopQLId)
                .ThenByDescending(e => e.TeacherId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public PageResultDtos<LopQLTheoNganhDtos> getAllLopQLTheoNganh([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<LopQLTheoNganhDtos>();
            var group = _context
                .LopQLs.GroupBy(l => l.NganhId)
                .Select(l => new { nganh = l.Key, lopQL = l.OrderBy(e => e.LopQLId).ToList() });
            var listLopQL = new List<LopQLTheoNganhDtos>();
            foreach (var item in group)
            {
                var newLopQL = new LopQLTheoNganhDtos();
                newLopQL.Nganhs = item.nganh;
                newLopQL.lopQLs = item.lopQL;
                listLopQL.Add(newLopQL);
            }
            var query = listLopQL.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Nganhs.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.Nganhs).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
