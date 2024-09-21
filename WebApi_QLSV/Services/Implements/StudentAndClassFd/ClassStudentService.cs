using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassStudent;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces.StudentFd;

namespace WebApi_QLSV.Services.Implements.StudentAndClassFd
{
    public class ClassStudentService : IClassStudentService
    {
        private readonly ApplicationDbContext _context;
        public ClassStudentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public ClassStudent AddStudentInClass(AddStudentInClassDtos input)
        {
            throw new NotImplementedException();
        }

        /*public ClassStudent AddStudentInClass(AddStudentInClassDtos input)
        {
            var student = _context.Students.FirstOrDefault(s => s.StudentId == input.StudentId)
                ?? throw new Exception("Không tìm thấy sinh viên");
            var cl = _context.LopHPs.FirstOrDefault(c => c.ClassName == input.ClassName)
                ?? throw new Exception("Không tìm thấy lớp");
            if (student?.Major == cl?.Major)
            {
                var stucl = _context.ClassStudents.FirstOrDefault(s => s.StudentId == student.StudentId);
                if (stucl == null)
                {
                    int count = _context.ClassStudents.Count(u => u.ClassName == input.ClassName);
                    if (count >= 40)
                    {
                        throw new UserExceptions("Đã đủ học sinh");
                    }
                    else
                    {
                        var StudentCl = new ClassStudent
                        {
                            ClassName = input.ClassName,
                            StudentId = input.StudentId,
                        };
                        _context.ClassStudents.Add(StudentCl);
                        _context.SaveChanges();
                        return StudentCl;
                    }
                }
                else
                {
                    throw new UserExceptions("Đã được thêm vào lớp");
                }

            }
            else
            {
                throw new UserExceptions("Không trùng mã nghành");
            }
        }
        */
        public PageResultDtos<ClassStudent> GetAll([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<ClassStudent>();

            var query = _context.ClassStudents.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord) ||
                        e.LopHPId.ToLower().Contains(input.KeyWord.ToLower()));
            result.TotalItem = query.Count();

            query = query
                    .OrderByDescending(e => e.LopHPId)
                    //.ThenBy(e => e.Id)
                    .Skip(input.Skip())
                    .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

    }
}
