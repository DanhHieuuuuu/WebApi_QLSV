using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class StudentServices : IStudentServices
    {
        private readonly ApplicationDbContext _context;
        private readonly Jwtsettings _jwtsettings;

        public StudentServices(ApplicationDbContext context, Jwtsettings jwtsettings)
        {
            _context = context;
            _jwtsettings = jwtsettings;
        }
        private string Createtokens(string username, string role, DateTime birthday, bool? gioiTinh, string id)
        {
            var claims = new[]
            {
                new Claim("UserName", username),
                new Claim("Role", role),
                new Claim("Birthday", birthday.ToString()),
                new Claim("Giới tính", gioiTinh.ToString()),
                new Claim("ID", id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtsettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtsettings.Issuer,
                audience: _jwtsettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtsettings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string RemoveDiacritics(string text)
        {
            string[] accentedChars = new string[]
            {
                "á",
                "à",
                "ả",
                "ã",
                "ạ",
                "ă",
                "ắ",
                "ằ",
                "ẳ",
                "ẵ",
                "ặ",
                "â",
                "ấ",
                "ầ",
                "ẩ",
                "ẫ",
                "ậ",
                "é",
                "è",
                "ẻ",
                "ẽ",
                "ẹ",
                "ê",
                "ế",
                "ề",
                "ể",
                "ễ",
                "ệ",
                "í",
                "ì",
                "ỉ",
                "ĩ",
                "ị",
                "ó",
                "ò",
                "ỏ",
                "õ",
                "ọ",
                "ô",
                "ố",
                "ồ",
                "ổ",
                "ỗ",
                "ộ",
                "ơ",
                "ớ",
                "ờ",
                "ở",
                "ỡ",
                "ợ",
                "ú",
                "ù",
                "ủ",
                "ũ",
                "ụ",
                "ư",
                "ứ",
                "ừ",
                "ử",
                "ữ",
                "ự",
                "ý",
                "ỳ",
                "ỷ",
                "ỹ",
                "ỵ",
                "đ",
            };

            string[] unaccentedChars = new string[]
            {
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "a",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "e",
                "i",
                "i",
                "i",
                "i",
                "i",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "o",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "u",
                "y",
                "y",
                "y",
                "y",
                "y",
                "d",
            };

            for (int i = 0; i < accentedChars.Length; i++)
            {
                text = text.Replace(accentedChars[i], unaccentedChars[i]);
                text = text.Replace(accentedChars[i].ToUpper(), unaccentedChars[i].ToUpper());
            }

            return text;
        }

        public ResponseLoginStudentDtos LoginStudent(Login input)
        {
            var student =
                _context.Students.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");

            bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, student.Password);
            var token = Createtokens(student.Username, student.Role, student.Birthday, student.GioiTinh, student.StudentId);
            var findNganh = _context.LopQLs.FirstOrDefault(l => l.LopQLId == student.LopQLId);
            var nganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == findNganh.NganhId);
            if (isValid)
            {
                var sucess = new ResponseLoginStudentDtos
                {
                    Username = student.Username,
                    Email = student.Email,
                    Token = token,
                    Birthday = student.Birthday,
                    QueQuan = student.QueQuan,
                    LopQLId = student.LopQLId,
                    TenLopQL = findNganh.TenLopQL,
                    Cccd = student.Cccd,
                    GioiTinh = student.GioiTinh,
                    StudentId = student.StudentId,
                    nganh = nganh.TenNganh,
                    nganhId = nganh.NganhId,
                    UrlImage = student.Image,
                };
                return sucess;
            }
            else
            {
                throw new UserExceptions("Không đúng mật khẩu");
            }
        }

        public async Task<Student> AddStudent2(AddStudentDtos2 input)
        {
            var stu = new Student();
            string IdStudent;
            do
            {
                int randomNumber = Random.Shared.Next(10000); // Số nguyên từ 0 đến 99
                int Year = DateTime.Now.Year;
                IdStudent = randomNumber.ToString() + (Year - 1955).ToString();
                stu = _context.Students.FirstOrDefault(x => x.StudentId == IdStudent);
            } while (stu != null);

            string[] lastname = input.Username.Split(' ');
            string name = RemoveDiacritics(lastname[lastname.Length - 1]).ToLower();

            string Email = name + IdStudent + "@huce.edu.vn";
            string Password = name + IdStudent;
            var findLopQL =
                _context.LopQLs.FirstOrDefault(n => n.LopQLId == input.LopQLId.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại lớp {input.LopQLId.ToUpper()}");
            findLopQL.MaxStudent = findLopQL.MaxStudent + 1;
            var findCccd = _context.Students.FirstOrDefault(c => c.Cccd == input.Cccd);
            if (findCccd != null)
            {
                throw new UserExceptions("Đã Tồn tại CCCD");
            }
            var student = new Student
            {
                StudentId = IdStudent,
                Username = input.Username,
                Email = Email,
                Password = BCrypt.Net.BCrypt.HashPassword(Password),
                Birthday = input.Birthday,
                QueQuan = input.QueQuan,
                LopQLId = input.LopQLId.ToUpper(),
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
            };
            if (input.Image.Length > 0)
            {
                var path = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot",
                    "Images",
                    input.Image.FileName
                );
                using (var stream = System.IO.File.Create(path))
                {
                    await input.Image.CopyToAsync(stream);
                }
                student.Image = "/images/" + input.Image.FileName;
            }
            else
            {
                throw new UserExceptions("Không có file");
            }
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        public PageResultDtos<StudentDtos> GetAllStudent([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<StudentDtos>();
            var allStudent =
                from stu in _context.Students
                join cls in _context.LopQLs on stu.LopQLId equals cls.LopQLId
                select new StudentDtos
                {
                    StudentId = stu.StudentId,
                    Username = stu.Username,
                    Cccd = stu.Cccd,
                    Email = stu.Email,
                    LopQLId = cls.LopQLId,
                    TenLopQL = cls.TenLopQL,
                    GioiTinh = stu.GioiTinh,
                    Birthday = stu.Birthday,
                    QueQuan = stu.QueQuan,
                    NienKhoa = stu.NienKhoa,
                    Image = stu.Image,
                };
            var query = allStudent.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.LopQLId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query
                .OrderByDescending(e => e.Username)
                .ThenByDescending(e => e.StudentId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<StudentInClass> GetAllStudentInClass([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<StudentInClass>();
            var group2 = from s in _context.Students
                         join l in _context.LopQLs on s.LopQLId equals l.LopQLId
                         select new
                         {
                             l.LopQLId,
                             l.TenLopQL,
                             s.StudentId,
                             s.Username,
                             s.Email,
                             s.Cccd,
                             s.QueQuan,
                             s.Image,
                             s.GioiTinh,
                             s.NienKhoa,
                             s.Birthday,
                         };
            var group1 = group2.GroupBy(e => e.LopQLId).Select
                         (g => new
                         {
                             lopqlId = g.Key,
                             student = g.OrderBy(s => s.Username).Select(item2 => new StudentDtos
                             {
                                 StudentId = item2.StudentId,
                                 Username = item2.Username,
                                 LopQLId = item2.LopQLId,
                                 TenLopQL = item2.TenLopQL,
                                 Email = item2.Email,
                                 Cccd = item2.Cccd,
                                 QueQuan = item2.QueQuan,
                                 Image = item2.Image,
                                 GioiTinh = item2.GioiTinh,
                                 NienKhoa = item2.NienKhoa,
                                 Birthday = item2.Birthday,
                             }),
                         }
                        ).ToList();

            var group = from l in _context.LopQLs
                        select new
                        {
                            l.LopQLId,
                            l.TenLopQL
                        };
            var liststudent = new List<StudentInClass>();
            foreach (var item in group)
            {
                var newStudent = new StudentInClass();
                newStudent.LopQLId = item.LopQLId;
                newStudent.TenLopQL = item.TenLopQL;
                foreach (var item1 in group1)
                {
                    if(item1.lopqlId == item.LopQLId)
                    {
                       newStudent.StudentDtoss = item1.student.ToList();
                    }
                }
                liststudent.Add(newStudent);
            }
            var query = liststudent.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenLopQL.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.StudentDtoss.Count).OrderBy(e => e.TenLopQL).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<StudentDtos> GetAllAndFindStudentByName([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<StudentDtos>();
            var allStudent =
                from stu in _context.Students
                join cls in _context.LopQLs on stu.LopQLId equals cls.LopQLId
                select new StudentDtos
                {
                    StudentId = stu.StudentId,
                    Username = stu.Username,
                    Cccd = stu.Cccd,
                    Email = stu.Email,
                    LopQLId = cls.LopQLId,
                    TenLopQL = cls.TenLopQL,
                    GioiTinh = stu.GioiTinh,
                    Birthday = stu.Birthday,
                    QueQuan = stu.QueQuan,
                    NienKhoa = stu.NienKhoa,
                    Image = stu.Image,
                };
            var query = allStudent.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Username.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query
                .OrderByDescending(e => e.Username)
                .ThenByDescending(e => e.StudentId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        //public List<MonHocDtos> DiemMonHoc(string studentId, string nganhId)
        //{
        //    var findCTK =
        //        _context.Nganhs.FirstOrDefault(n => n.NganhId == nganhId)
        //        ?? throw new UserExceptions("Không tìm thấy ngành");
        //    var monhoc = (
        //        from m in _context.MonHocs
        //        join l in _context.LopHPs on m.MaHocPhan equals l.MonId into lophp
        //        from lm in lophp.DefaultIfEmpty()
        //        join cl in _context.ClassStudents on lm.LopHPId equals cl.LopHPId into diem
        //        from dg in diem.DefaultIfEmpty()

        //        where dg.StudentId == studentId && m.CTKhungId == findCTK.CTKhungId
        //        select new MonHocDtos
        //        {
        //            kiHoc = m.KiHoc,
        //            MonId = m.MonId,
        //            Sotin = m.Sotin,
        //            TenMon = m.TenMon,
        //            DiemMH = dg.DiemMH,
        //            DiemQT = dg.DiemQT,
        //            DiemKT = dg.DiemKT,
        //        }
        //    ).OrderBy(m => m.TenMon);
        //    return monhoc.ToList();
        //}


        public async Task<StudentDtos> UpdateStudent(
            [FromQuery] string studentId,
            [FromForm] UpdateStudentDtos input
        )
        {
            var findStu =
                _context.Students.SingleOrDefault(s => s.StudentId == studentId)
                ?? throw new UserExceptions("Không tồn tại sinh viên");
            var checkCccd = _context.Students.Count(s => s.Cccd == input.Cccd);
            if (
                input.Username == null
                || input.QueQuan == null
                || input.Cccd == null
                || input.LopQLId == null
                || input.GioiTinh == null
            )
            {
                throw new UserExceptions("Chưa nhập đầy đủ thông tin");
            }
            if (checkCccd >= 2)
            {
                throw new UserExceptions("Đã tồn tại Căn cước công dân");
            }
            var findLopQL = _context.LopQLs.SingleOrDefault(s => s.LopQLId == findStu.LopQLId);
            findLopQL.MaxStudent = findLopQL.MaxStudent - 1;

            var findNewLopQl = _context.LopQLs.FirstOrDefault(s => s.LopQLId == input.LopQLId);
            findNewLopQl.MaxStudent = findNewLopQl.MaxStudent + 1;

            findStu.Username = input.Username;
            findStu.Birthday = input.Birthday;
            findStu.QueQuan = input.QueQuan;
            findStu.Cccd = input.Cccd;
            findStu.LopQLId = input.LopQLId;
            findStu.GioiTinh = input.GioiTinh;
            if( input.Image == null)
            {
                findStu.Image = findStu.Image;
            }
            else
            {
                if (input.Image.Length > 0)
                {
                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        input.Image.FileName
                    );
                    using (var stream = System.IO.File.Create(path))
                    {
                        await input.Image.CopyToAsync(stream);
                    }
                    findStu.Image = "/images/" + input.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
            }
            _context.Students.Update(findStu);
            _context.LopQLs.Update(findLopQL);
            _context.LopQLs.Update(findNewLopQl);
            _context.SaveChanges();
            var newStudent = new StudentDtos
            {
                StudentId = findStu.StudentId,
                Username = input.Username,
                Email = findStu.Email,
                Birthday = input.Birthday,
                QueQuan = input.QueQuan,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                LopQLId = findLopQL.LopQLId,
                TenLopQL = findLopQL.TenLopQL,
                Image = findStu.Image,
                NienKhoa = findStu.NienKhoa,
            };
            return newStudent;
        }
        public void DeleteStudent(string studentId)
        {
            var findStudent = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            if (findStudent != null)
            {
                var findLopQL = _context.LopQLs.FirstOrDefault(l => l.LopQLId == findStudent.LopQLId);
                findLopQL.MaxStudent = findLopQL.MaxStudent - 1;
                _context.LopQLs.Update(findLopQL);
                _context.Students.Remove(findStudent);
                _context.SaveChanges();
            }
            else
            {
                throw new UserExceptions("Không tồn tại sinh viên");
            }
        }

        public PageResultDtos<StudentDtos> GetAllStudentById([FromQuery] FilterDtos input, List<string> studentId)
        {
            var result = new PageResultDtos<StudentDtos>();

            var listStudent = new List<StudentDtos>();
            foreach (var item in studentId)
            {
                var stu = _context.Students.FirstOrDefault( s => s.StudentId == item);
                var cls = _context.LopQLs.FirstOrDefault(l => l.LopQLId == stu.LopQLId);
                var student = new StudentDtos
                {
                    StudentId = stu.StudentId,
                    Username = stu.Username,
                    Cccd = stu.Cccd,
                    Email = stu.Email,
                    LopQLId = cls.LopQLId,
                    TenLopQL = cls.TenLopQL,
                    GioiTinh = stu.GioiTinh,
                    Birthday = stu.Birthday,
                    QueQuan = stu.QueQuan,
                    NienKhoa = stu.NienKhoa,
                    Image = stu.Image,
                };
                listStudent.Add(student);
            }
            var query = listStudent.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord)
                        || e.Username.ToLower().Contains(input.KeyWord.ToLower())
);
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.Username).OrderBy(e => e.TenLopQL).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public Object Login (Login input )
        {
            var findStu = _context.Students.FirstOrDefault(x => x.Email == input.Email);
            var findTea = _context.Teachers.FirstOrDefault(x => x.Email == input.Email);
            var findMana = _context.Managers.FirstOrDefault(x => x.Email == input.Email);

            if( findStu == null && findTea == null && findMana == null)
            {
                throw new UserExceptions("Không tồn tại tài khoản");
            }

            if( findStu != null)
            {
                var student =
                _context.Students.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");

                bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, student.Password);
                var token = Createtokens(student.Username, student.Role, student.Birthday, student.GioiTinh, student.StudentId);
                var findNganh = _context.LopQLs.FirstOrDefault(l => l.LopQLId == student.LopQLId);
                var nganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == findNganh.NganhId);
                if (isValid)
                {
                    var sucess = new ResponseLoginStudentDtos
                    {
                        Username = student.Username,
                        Email = student.Email,
                        Token = token,
                        Birthday = student.Birthday,
                        QueQuan = student.QueQuan,
                        LopQLId = student.LopQLId,
                        TenLopQL = findNganh.TenLopQL,
                        Cccd = student.Cccd,
                        GioiTinh = student.GioiTinh,
                        StudentId = student.StudentId,
                        nganh = nganh.TenNganh,
                        nganhId = nganh.NganhId,
                        UrlImage = student.Image,
                        Role = student.Role,
                    };
                    return sucess;
                }
                else
                {
                    throw new UserExceptions("Không đúng mật khẩu");
                }
            }
            else if( findTea != null)
            {
                var teacher =
                _context.Teachers.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");
                var findBM = _context.BoMons.FirstOrDefault(x => x.BoMonId == teacher.BoMonId);
                bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, teacher.Password);
                var token = Createtokens(teacher.TenGiangVien, teacher.Role, teacher.Birthday, teacher.GioiTinh, teacher.TeacherId);

                if (isValid)
                {
                    var sucess = new ResponseLoginTeacherDtos
                    {
                        TeacherId = teacher.TeacherId,
                        TenGiangVien = teacher.TenGiangVien,
                        Email = teacher.Email,
                        TenBoMon = findBM.TenBoMon,
                        BoMonId = teacher.BoMonId,
                        Cccd = teacher.Cccd,
                        Birthday = teacher.Birthday,
                        GioiTinh = teacher.GioiTinh,
                        QueQuan = teacher.QueQuan,
                        UrlImage = teacher.Image,
                        Token = token,
                        Role = teacher.Role,
                    };
                    return sucess;
                }
                else
                {
                    throw new UserExceptions("Không đúng mật khẩu");
                }
            }
            else
            {
                var manager =
                _context.Managers.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");

                bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, manager.Password);
                var token = Createtokens(manager.Username, manager.Role, manager.Birthday, manager.GioiTinh, manager.ManagerId);
                if (isValid)
                {
                    var sucess = new ResponseLoginManagerDtos
                    {
                        ManagerId = manager.ManagerId,
                        Username = manager.Username,
                        Email = manager.Email,
                        Birthday = manager.Birthday,
                        Cccd = manager.Cccd,
                        GioiTinh = manager.GioiTinh,
                        QueQuan = manager.QueQuan,
                        UrlImage = manager.Image,
                        Token = token,
                        Role = manager.Role
                    };
                    return sucess;
                }
                else
                {
                    throw new UserExceptions("Không đúng mật khẩu");
                }
            }
        }

        public void ChangePassword(string email, string Password, string role)
        {
            if (role == "Student")
            {
                var findStu = _context.Students.FirstOrDefault(x => x.Email == email);
                findStu.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                _context.Students.Update(findStu); 
            }
            if (role == "Teacher")
            {
                var findTea = _context.Teachers.FirstOrDefault(x => x.Email == email);
                findTea.Password = BCrypt.Net.BCrypt.HashPassword(Password);
                _context.Teachers.Update(findTea);
            }
            _context.SaveChanges();
        }

        public StudentDtos FindStudent(string studentId)
        {
            var find = _context.Students.FirstOrDefault(s => s.StudentId == studentId);
            var findLop = _context.LopQLs.FirstOrDefault(l => l.LopQLId == find.LopQLId);
            var student = new StudentDtos
            {
                StudentId = find.StudentId,
                Username = find.Username,
                Cccd = find.Cccd,
                Email = find.Email,
                LopQLId = find.LopQLId,
                TenLopQL = findLop.TenLopQL,
                GioiTinh = find.GioiTinh,
                Birthday = find.Birthday,
                QueQuan = find.QueQuan,
                NienKhoa = find.NienKhoa,
                Image = find.Image,
            };
            return student;
        }
    }
}
