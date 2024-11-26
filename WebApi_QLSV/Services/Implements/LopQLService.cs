using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Entities;
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
            var findLop = _context.LopQLs.Any(e => e.LopQLId == input.LopQLId);
            if (findLop) { throw new UserExceptions("Đã tồn tại mã lơp quả lí"); };
            var findNganh =
                _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId.ToUpper())
                ?? throw new UserExceptions("Không tồn tại nghành này");
            // tìm chủ nhiệm
            var findTeacher = _context.Teachers.FirstOrDefault(n => n.TeacherId == input.TeacherId);
            if (findTeacher == null)
            {
                throw new UserExceptions("Không tồn tại mã chủ nhiệm");
            };
            var findCN = _context.LopQLs.Any(n => n.TeacherId == input.TeacherId);
            if (findCN) { throw new UserExceptions("Đã là chủ nhiệm"); };


            var result = new LopQL
            {
                LopQLId = input.LopQLId.ToUpper(),
                TenLopQL = input.TenLopQL.ToUpper(),
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
            var findStudent = from stu in _context.Students where stu.LopQLId == classId select stu;
            if (findStudent == null)
            {
                throw new UserExceptions("Lớp chưa có sinh viên");
            }
            var findClass =
                _context.LopQLs.FirstOrDefault(l => l.LopQLId == classId)
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
            var findNganh = from n in _context.Nganhs
                            join l in _context.LopQLs on n.NganhId equals l.NganhId
                            select new
                            {
                                l.LopQLId,
                                l.TenLopQL,
                                l.NganhId,
                                n.TenNganh,
                                l.MaxStudent,
                                l.LopTruongId,
                                l.LopPhoId,
                                l.TeacherId,

                            };
            var group = findNganh.GroupBy(l => l.NganhId)
                .Select(l => new { nganhId = l.Key, lopQL = l.OrderBy(e => e.TenLopQL).Select(item1 => new LopQL
                {
                    LopQLId = item1.LopQLId,
                    TenLopQL = item1.TenLopQL,
                    NganhId = item1.NganhId,
                    LopPhoId = item1.LopPhoId,
                    LopTruongId = item1.LopTruongId,
                    MaxStudent = item1.MaxStudent,
                    TeacherId = item1.TeacherId,
                }).ToList() 
                }).ToList();
            var group2 = from n in _context.Nganhs
                         select new
                         {
                             n.NganhId,
                             n.TenNganh
                         };
            var listLopQL = new List<LopQLTheoNganhDtos>();
            foreach (var item in group2)
            {
                var newLopQL = new LopQLTheoNganhDtos();
                newLopQL.NganhId = item.NganhId;
                newLopQL.TenNganh = item.TenNganh;
                foreach (var item1 in group)
                {
                    if(item1.nganhId == item.NganhId)
                    {
                        newLopQL.lopQLs = item1.lopQL;
                    }
                }
                listLopQL.Add(newLopQL);
            }
            var query = listLopQL.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenNganh.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.lopQLs.Count).OrderByDescending(e => e.TenNganh).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public LopQL UpdateLopQL(UpdateLopQLDtos input)
        {
            var findLopQl = _context.LopQLs.FirstOrDefault(l => l.LopQLId == input.LopQLId);
            var findNganh = _context.Nganhs.FirstOrDefault(l => l.NganhId == input.NganhId)
                ?? throw new UserExceptions("Không tồn tại ngành");
            findLopQl.TenLopQL = input.TenLopQL;
            
            var AnyStudent = _context.Students.Any(s => s.LopQLId == input.LopQLId && s.StudentId == input.LopTruongId);
            if( !AnyStudent ) throw new UserExceptions($"Không tồn tại mã lớp trưởng: {input.LopTruongId}");
            
            var AnyStudent2 = _context.Students.Any(s => s.LopQLId == input.LopQLId && s.StudentId == input.LopPhoId);
            if (!AnyStudent2) throw new UserExceptions($"Không tồn tại mã lớp phó: {input.LopPhoId}");

            var AnyTeacher = _context.Teachers.Any(s => s.TeacherId == input.TeacherId);
            if (!AnyTeacher) throw new UserExceptions($"Không tồn tại mã giảng viên: {input.TeacherId}");

            var AnyCN = _context.LopQLs.Any(l => l.TeacherId == input.TeacherId);
            if (!AnyCN) throw new UserExceptions("Giảng viên này đã là chủ nhiệm");

            findLopQl.LopPhoId = input.LopPhoId;
            findLopQl.LopTruongId = input.LopTruongId;
            findLopQl.TeacherId = input.TeacherId;
            findLopQl.NganhId = input.NganhId;
            _context.LopQLs.Update(findLopQl);
            _context.SaveChanges();
            return findLopQl;
        }
        public void DeleteLopQL(string LopQLId)
        {
            var findLopQL = _context.LopQLs.FirstOrDefault( l => l.LopQLId == LopQLId);

            if (findLopQL == null)
            {
                throw new UserExceptions("Không tồn tại lớp");
            }
            else
            {
                var findStudent = _context.Students.Where(s => s.LopQLId == LopQLId).ToList();
                var findNganh = _context.Nganhs.FirstOrDefault(s => s.NganhId == findLopQL.NganhId);
                findNganh.SumClass = findNganh.SumClass - 1;
                _context.Nganhs.Update(findNganh);
                _context.Students.RemoveRange(findStudent);
            }
            _context.LopQLs.Remove(findLopQL);
            _context.SaveChanges();
        }
    }
}
