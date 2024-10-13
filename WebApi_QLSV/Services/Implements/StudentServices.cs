using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
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
        private string Createtokens(string username)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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

        private string RemoveDiacritics(string text)
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
            var token = Createtokens(student.Username);
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
                    TenLopQL = student.LopQLId,
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
                _context.LopQLs.FirstOrDefault(n => n.LopQLId == input.TenLopQL.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại lớp {input.TenLopQL.ToUpper()}");
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
                LopQLId = input.TenLopQL.ToUpper(),
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
                select new StudentDtos
                {
                    StudentId = stu.StudentId,
                    Username = stu.Username,
                    Cccd = stu.Cccd,
                    Email = stu.Email,
                    TenLopQL = stu.LopQLId,
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

        public PageResultDtos<StudentInClass> GetAllStudentInClass([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<StudentInClass>();
            var group = _context
                .Students.GroupBy(l => l.LopQLId)
                .Select(l => new { lopQL = l.Key, student = l.OrderBy(e => e.Username).ToList() });
            var liststudent = new List<StudentInClass>();
            foreach (var item in group)
            {
                var newStudent = new StudentInClass();
                newStudent.Class = item.lopQL;
                foreach (var item1 in item.student)
                {
                    var stud = new StudentDtos
                    {
                        StudentId = item1.StudentId,
                        Username = item1.Username,
                        TenLopQL = item1.LopQLId,
                        Email = item1.Email,
                        Cccd = item1.Cccd,
                        QueQuan = item1.QueQuan,
                        Image = item1.Image,
                        GioiTinh = item1.GioiTinh,
                        NienKhoa = item1.NienKhoa,
                        Birthday = item1.Birthday,
                    };
                    if (item1.LopQLId == item.lopQL)
                    {
                        newStudent.StudentDtoss.Add(stud);
                    }
                }
                liststudent.Add(newStudent);
                Console.WriteLine(liststudent[0].Class);
            }
            var query = liststudent.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Class.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.Class).Skip(input.Skip()).Take(input.PageSize);

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

        public static string DiemChu(double? diem)
        {
            if (diem >= 8.5)
            {
                return "A";
            }
            else if (diem >= 8)
            {
                return "B+";
            }
            else if (diem >= 7)
            {
                return "B";
            }
            else if (diem >= 6.5)
            {
                return "C+";
            }
            else if (diem >= 5.5)
            {
                return "C";
            }
            else if (diem >= 5)
            {
                return "D+";
            }
            else if (diem >= 4)
            {
                return "D";
            }
            else if (diem < 4)
            {
                return "F";
            }
            else
            {
                return null;
            }
        }

        public static double DiemThang4(double? diem)
        {
            if (diem >= 8.5)
            {
                return 4;
            }
            else if (diem >= 8)
            {
                return 3.5;
            }
            else if (diem >= 7)
            {
                return 3;
            }
            else if (diem >= 6.5)
            {
                return 2.5;
            }
            else if (diem >= 5.5)
            {
                return 2;
            }
            else if (diem >= 5)
            {
                return 1.5;
            }
            else if (diem >= 4)
            {
                return 1;
            }
            else if (diem < 4)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public List<AddBangDiemDtos> GetBangDiem([FromQuery] string studentId)
        {
            var lopHP =
                from cls in _context.ClassStudents
                join lhp in _context.LopHPs on cls.LopHPId equals lhp.LopHPId
                join mh in _context.MonHocs on lhp.MonId equals mh.MaMonHoc
                where cls.StudentId == studentId
                select new BangDiemDtos
                {
                    KiHocNamHoc = lhp.KiHocNamHoc,
                    monId = lhp.MonId,
                    tenMon = mh.TenMon,
                    LopHPId = lhp.LopHPId,
                    LopHP = lhp.TenLopHP,
                    sotin = mh.SoTin,
                    DiemQT = cls.DiemMH,
                    DiemKT = cls.DiemKT,
                    DiemMH = cls.DiemMH,
                    DiemChu = DiemChu(cls.DiemMH),
                    DiemThang4 = DiemThang4(cls.DiemMH),
                    TienMonHoc = cls.TienMonHoc,
                    Nop = cls.Nop,
                };
            var group = lopHP
                .GroupBy(g => g.KiHocNamHoc)
                .Select(g => new
                {
                    KiHocNamHoc = g.Key,
                    BangDiem = g.OrderBy(o => o.tenMon).ToList(),
                })
                .ToList();
            List<AddBangDiemDtos> abd = new List<AddBangDiemDtos>();
            double? diemTB10 = 0;
            double? diemTichLuy10 = 0;
            int? tongTCKi = 0;
            int? tongTCTichLuy = 0;
            int? tongTCDat = 0;
            int? tienHocKi = 0;
            int? soTienNop = 0;
            int? congNo = 0;
            foreach (var item in group)
            {
                foreach (var item1 in item.BangDiem)
                {
                    double? diemMH = 0;
                    if (item1.DiemMH != null && item1.DiemMH >= 4)
                    {
                        diemMH = item1.DiemMH;
                        tongTCDat += item1.sotin;
                    }
                    else if (item1.DiemMH != null && item1.DiemMH < 4)
                    {
                        diemMH = item1.DiemMH;
                    }
                    else
                    {
                        diemMH = 0;
                    }
                    if (item1.Nop)
                    {
                        soTienNop += item1.TienMonHoc;
                    }
                    else
                    {
                        congNo += item1.TienMonHoc;
                    }
                    diemTB10 = diemTB10 + diemMH * item1.sotin;
                    diemTichLuy10 += diemMH * item1.sotin;
                    tongTCKi = tongTCKi + item1.sotin;
                    tienHocKi += item1.TienMonHoc;
                }
                tongTCTichLuy = tongTCTichLuy + tongTCKi;

                AddBangDiemDtos bd = new AddBangDiemDtos(
                    item.KiHocNamHoc,
                    diemTB10 / tongTCKi,
                    DiemThang4(diemTB10 / tongTCKi),
                    diemTichLuy10 / tongTCTichLuy,
                    DiemThang4(diemTichLuy10 / tongTCTichLuy),
                    tongTCKi,
                    tongTCTichLuy,
                    tongTCDat,
                    tienHocKi,
                    soTienNop,
                    congNo
                );
                soTienNop = 0;
                congNo = 0;
                tienHocKi = 0;
                diemTB10 = 0;
                tongTCKi = 0;
                bd.BangDiems = item.BangDiem;
                abd.Add(bd);
            }
            return abd;
        }

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
                || input.Email == null
                || input.QueQuan == null
                || input.Cccd == null
                || input.GioiTinh == null
            )
            {
                throw new UserExceptions("Chưa nhập đầy đủ thông tin");
            }
            if (checkCccd >= 2)
            {
                throw new UserExceptions("Đã tồn tại Căn cước công dân");
            }

            findStu.Username = input.Username;
            findStu.Email = input.Email;
            findStu.Birthday = input.Birthday;
            findStu.QueQuan = input.QueQuan;
            findStu.Cccd = input.Cccd;
            findStu.GioiTinh = input.GioiTinh;
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
            _context.Students.Update(findStu);
            _context.SaveChanges();
            var newStudent = new StudentDtos
            {
                StudentId = findStu.StudentId,
                Username = input.Username,
                Email = input.Email,
                Birthday = input.Birthday,
                QueQuan = input.QueQuan,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                TenLopQL = findStu.LopQLId,
                Image = findStu.Image,
                NienKhoa = findStu.NienKhoa,
            };
            return newStudent;
        }
    }
}
