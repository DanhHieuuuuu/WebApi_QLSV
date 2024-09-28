using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces.StudentAndClassFd;

namespace WebApi_QLSV.Services.Implements
{
    public class ClassStudentService : IClassStudentService
    {
        private readonly ApplicationDbContext _context;

        public ClassStudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ClassStudent AddStudentInClass(AddStudentInLopHPDtos input)
        {
            var student =
                _context.Students.FirstOrDefault(s => s.StudentId == input.StudentId)
                ?? throw new Exception("Không tìm thấy sinh viên");
            var cl =
                _context.LopHPs.FirstOrDefault(c => c.LopHPId == input.LopHPId)
                ?? throw new Exception("Không tìm thấy lớp");

            int count = _context.ClassStudents.Count(u => u.LopHPId == input.LopHPId);
            if (count >= cl.MaxStudent)
            {
                throw new UserExceptions("Đã đủ học sinh");
            }
            else
            {
                var findMon = _context.MonHocs.FirstOrDefault(m => m.MonId == cl.MonId);
                var tienhoc = 455000 * findMon.Sotin;
                var StudentCl = new ClassStudent
                {
                    LopHPId = input.LopHPId,
                    StudentId = input.StudentId,
                    DiemQT = input.DiemQT,
                    DiemKT = input.DiemKT,
                    DiemMH = input.DiemMH,
                    TienMonHoc = tienhoc,
                };
                _context.ClassStudents.Add(StudentCl);
                _context.SaveChanges();
                return StudentCl;
            }
        }

        public PageResultDtos<ClassStudent> GetAll([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<ClassStudent>();

            var query = _context.ClassStudents.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.LopHPId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.LopHPId)
                //.ThenBy(e => e.Id)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
