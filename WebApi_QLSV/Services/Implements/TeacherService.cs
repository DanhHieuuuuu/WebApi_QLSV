using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebApi_QLSV.Services.Implements
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly Jwtsettings _jwtsettings;

        public TeacherService(ApplicationDbContext context, Jwtsettings jwtsettings)
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
                new Claim(ClaimTypes.Role, "Teacher")

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

        public async Task<Teacher> AddTeacher2([FromForm] AddTeacherDtos2 input)
        {
            var tea = new Teacher();
            string IdTeacher;
            do
            {
                int randomNumber = Random.Shared.Next(10000);
                IdTeacher = randomNumber.ToString();
                tea = _context.Teachers.FirstOrDefault(t => t.TeacherId == IdTeacher);
            } while (tea != null);
            string[] name = input.TenGiangVien.Split(' ');

            string lastNameAbbreviation = name[0].ToLower().Substring(0, 1);
            for (int i = 1; i < name.Length - 1; i++)
            {
                lastNameAbbreviation = lastNameAbbreviation + name[i].ToLower().Substring(0, 1);
            }

            // Lấy tên (phần cuối cùng của họ tên)
            string firstName = name[name.Length - 1]; // "Hiếu"

            // Chuyển tên sang dạng không dấu
            string normalizedFirstName = RemoveDiacritics(firstName).ToLower(); // "hieu"

            // Kết hợp tên không dấu và họ viết tắt
            string username = normalizedFirstName + lastNameAbbreviation;

            string Email = username + IdTeacher + "@huce.edu.vn";
            string Password = username + IdTeacher;
            var findBoMon =
                _context.BoMons.FirstOrDefault(b => b.BoMonId == input.BoMonId)
                ?? throw new UserExceptions($"Không tìm thấy mã bộ môn: {input.BoMonId}");
            var findCccd = _context.Teachers.FirstOrDefault(c => c.Cccd == input.Cccd);
            if (findCccd != null)
            {
                throw new UserExceptions("Đã Tồn tại CCCD");
            }
            findBoMon.SoLuongGV = findBoMon.SoLuongGV + 1;
            var teacher = new Teacher
            {
                TeacherId = IdTeacher,
                TenGiangVien = input.TenGiangVien,
                Email = Email,
                Cccd = input.Cccd,
                Birthday = input.Birthday,
                GioiTinh = input.GioiTinh,
                QueQuan = input.QueQuan,
                BoMonId = input.BoMonId,
                Password = BCrypt.Net.BCrypt.HashPassword(Password),
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
                teacher.Image = "/images/" + input.Image.FileName;
            }
            else
            {
                throw new UserExceptions("Không có file");
            }
            _context.Teachers.Add(teacher);
            _context.SaveChanges();
            return teacher;
        }

        public ResponseLoginTeacherDtos LoginTeacher(Login input)
        {
            var teacher =
                _context.Teachers.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");
            var findBM = _context.BoMons.FirstOrDefault(x => x.BoMonId == teacher.BoMonId);
            bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, teacher.Password);
            var token = Createtokens(teacher.TenGiangVien);

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
                };
                return sucess;
            }
            else
            {
                throw new UserExceptions("Không đúng mật khẩu");
            }
        }

        public PageResultDtos<TeacherDtos> GetAll([FromQuery] FilterDtos input)
        {
            {
                var result = new PageResultDtos<TeacherDtos>();
                var allTeacher =
                    from teacher in _context.Teachers
                    select new TeacherDtos
                    {
                        TeacherId = teacher.TeacherId,
                        TenGiangVien = teacher.TenGiangVien,
                        Email = teacher.Email,
                        BoMonId = teacher.BoMonId,
                        Cccd = teacher.Cccd,
                        Birthday = teacher.Birthday,
                        GioiTinh = teacher.GioiTinh,
                        QueQUan = teacher.QueQuan,
                        Image = teacher.Image,
                    };
                var query = allTeacher.Where(e =>
                    string.IsNullOrEmpty(input.KeyWord)
                    || e.BoMonId.ToLower().Contains(input.KeyWord.ToLower())
                );
                result.TotalItem = query.Count();

                query = query
                    .OrderBy(e => e.TenGiangVien)
                    .ThenBy(e => e.BoMonId)
                    .Skip(input.Skip())
                    .Take(input.PageSize);

                result.Items = query.ToList();

                return result;
            }
        }

        public List<TeacherDtos> GetAllTeacherInBoMon( string? boMonId)
        {
            {
                var allTeacher =
                    from teacher in _context.Teachers
                    where teacher.BoMonId == boMonId
                    select new TeacherDtos
                    {
                        TeacherId = teacher.TeacherId,
                        TenGiangVien = teacher.TenGiangVien,
                        Email = teacher.Email,
                        BoMonId = teacher.BoMonId,
                        Cccd = teacher.Cccd,
                        Birthday = teacher.Birthday,
                        GioiTinh = teacher.GioiTinh,
                        QueQUan = teacher.QueQuan,
                        Image = teacher.Image,
                    };


                allTeacher = allTeacher
                    .OrderBy(e => e.TenGiangVien)
                    .ThenBy(e => e.BoMonId);

                return allTeacher.ToList();
            }
        }
        public List<TeacherDtos> GetAllTeacherInKhoa(string? khoaId)
        {
            {
                var allTeacher =
                    from k in _context.Khoas
                    join b in _context.BoMons on k.KhoaId equals b.KhoaId
                    join teacher in _context.Teachers on b.BoMonId equals teacher.BoMonId
                    where k.KhoaId == khoaId
                    select new TeacherDtos
                    {
                        TeacherId = teacher.TeacherId,
                        TenGiangVien = teacher.TenGiangVien,
                        Email = teacher.Email,
                        BoMonId = teacher.BoMonId,
                        Cccd = teacher.Cccd,
                        Birthday = teacher.Birthday,
                        GioiTinh = teacher.GioiTinh,
                        QueQUan = teacher.QueQuan,
                        Image = teacher.Image,
                    };


                allTeacher = allTeacher
                    .OrderBy(e => e.TenGiangVien)
                    .ThenBy(e => e.BoMonId);

                return allTeacher.ToList();
            }
        }

        public PageResultDtos<TeacherInBoMon> GetAllTeacherPerBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<TeacherInBoMon>();
            var group2 = from t in _context.Teachers
                         join bm in _context.BoMons on t.BoMonId equals bm.BoMonId
                         select new
                         {
                             bomon = bm.BoMonId,
                             tenBoMon = bm.TenBoMon,
                             teacherId = t.TeacherId,
                             tenGiangVien = t.TenGiangVien,
                             email = t.Email,
                             cccd = t.Cccd,
                             birthday = t.Birthday,
                             gioiTinh = t.GioiTinh,
                             queQUan = t.QueQuan,
                             image = t.Image
                         };

            var group1 = group2.GroupBy(t => t.bomon)
                .Select(t => new
                {
                    boMon = t.Key,
                    teacher = t.OrderBy(o => o.tenGiangVien).Select(item1 => new TeacherDtos
                    {
                        TeacherId = item1.teacherId,
                        TenGiangVien = item1.tenGiangVien,
                        Email = item1.email,
                        Cccd = item1.cccd,
                        Birthday = item1.birthday,
                        GioiTinh = item1.gioiTinh,
                        QueQUan = item1.queQUan,
                        Image = item1.image,
                        BoMonId = item1.bomon
                    }).ToList(),
                }).ToList();
            var group = from bm in _context.BoMons
                        select new
                        {
                            bm.BoMonId,
                            bm.TenBoMon
                        };
            var listTeacher = new List<TeacherInBoMon>();
            foreach (var item in group)
            {
                var newteacher = new TeacherInBoMon();
                newteacher.BoMon = item.BoMonId;
                newteacher.TenBoMon = item.TenBoMon;
                foreach (var item1 in group1)
                {
                    if(item1.boMon == item.BoMonId)
                    {   
                        newteacher.TeacherDtoss = item1.teacher;
                    }
                }
                listTeacher.Add(newteacher);
            }
            var query = listTeacher.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.BoMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderByDescending(e => e.TeacherDtoss.Count).OrderBy(e => e.BoMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public async Task<TeacherDtos> UpdateTeacher(
            [FromQuery] string teacherId,
            [FromForm] UpdateTeacherDtos input
        )
        {
            var findTecher =
                _context.Teachers.SingleOrDefault(s => s.TeacherId == teacherId)
                ?? throw new UserExceptions("Không tồn tại giảng viên");
            var checkCccd = _context.Teachers.Count(s => s.Cccd == input.Cccd);
            var checkBoMon = _context.BoMons.FirstOrDefault(t => t.BoMonId == input.BoMonId);
            
            if (
                input.TenGiangVien == null
                || input.Cccd == null
                || input.GioiTinh == null
                || input.QueQuan == null
            )
            {
                throw new UserExceptions("Chưa nhập đầy đủ thông tin");
            }
            if (checkCccd >= 2)
            {
                throw new UserExceptions("Đã tồn tại Căn cước công dân");
            }
            if (checkBoMon == null)
            {
                throw new UserExceptions("Không tồn tại bộ môn Id");
            }
            else
            {
                checkBoMon.SoLuongGV += 1;
                var findBoMon = _context.BoMons.SingleOrDefault(b => b.BoMonId == findTecher.BoMonId);
                findBoMon.SoLuongGV -= 1;
                _context.BoMons.Update(findBoMon);

            }
            findTecher.TenGiangVien = input.TenGiangVien;
            findTecher.Birthday = input.Birthday;
            findTecher.Cccd = input.Cccd;
            findTecher.GioiTinh = input.GioiTinh;
            findTecher.QueQuan = input.QueQuan;
            findTecher.BoMonId = input.BoMonId;
            if(input.Image == null)
            {
                findTecher.Image = findTecher.Image;
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
                    findTecher.Image = "/images/" + input.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
            }
            _context.BoMons.Update(checkBoMon);
            _context.Teachers.Update(findTecher);
            _context.SaveChanges();
            var newTeacher = new TeacherDtos
            {
                TeacherId = findTecher.TeacherId,
                TenGiangVien = input.TenGiangVien,
                Email = findTecher.Email,
                Birthday = input.Birthday,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                QueQUan = input.QueQuan,
                Image = findTecher.Image,
                BoMonId = input.BoMonId,
            };
            return newTeacher;
        }

        public void DeleteTeacher(string teacherId)
        {
            var findTeacher = _context.Teachers.FirstOrDefault( t => t.TeacherId == teacherId)
                ?? throw new UserExceptions("Không tồn tại giảng viên");
            var findBoMon = _context.BoMons.FirstOrDefault( t => t.BoMonId == findTeacher.BoMonId);
            findBoMon.SoLuongGV = findBoMon.SoLuongGV - 1;
            _context.BoMons.Update(findBoMon);
            var findMQH = _context.Teacher_MonHocs.Where(t => t.TeacherId == teacherId).ToList();

            _context.Teacher_MonHocs.RemoveRange(findMQH);
            _context.SaveChanges();

            _context.Teachers.Remove(findTeacher);
            _context.SaveChanges();
        }

        public void AddTeachertoMonHoc( List<string> listTeacher, string maMonHoc)
        {
            var findMonHoc = _context.MonHocs.FirstOrDefault(m => m.MaMonHoc == maMonHoc)
                ?? throw new UserExceptions("Không tồn tại mã môn học");

            
            foreach (var item in listTeacher)
            {
                var findteacher = _context.Teachers.Any(t => t.TeacherId == item);
                if (!findteacher) { throw new UserExceptions("Không tồn tại mã giảng viên"); };

                var checkTeacher = _context.Teachers.Any(t => t.BoMonId == findMonHoc.BoMonId);
                if (!checkTeacher) { throw new UserExceptions("Không tồn tại mã giảng viên"); };
                var result = new Teacher_MonHoc
                {
                    MaMonHoc = maMonHoc,
                    TeacherId = item
                };
                _context.Teacher_MonHocs.Add(result);
            }
            _context.SaveChanges();
        }
        public void RemoveTeacherToMonHoc([FromQuery] string teacherId, string maMonHoc)
        {
            var findTeac = _context.Teachers.Any(t => t.TeacherId == teacherId);
            if (!findTeac) { throw new UserExceptions("Không tồn tại mã giảng viên"); };
            var findMH = _context.MonHocs.Any(m => m.MaMonHoc == maMonHoc);
            if (!findMH) { throw new UserExceptions("Không tồn tại mã môn học"); };
            var ttm = new Teacher_MonHoc
            {
                MaMonHoc = maMonHoc,
                TeacherId = teacherId,
            };
            _context.Teacher_MonHocs.Remove(ttm);
            _context.SaveChanges();
        }

        public StudentInLopQLDtos GetStudentInLopQl([FromQuery] string teacherId)
        {
            var result = new StudentInLopQLDtos();

            var findLop = _context.LopQLs.FirstOrDefault(s => s.TeacherId == teacherId);
            if (findLop == null) { return result; }

            var findStud = from s in _context.Students
                           where s.LopQLId == findLop.LopQLId
                           select new StudentDtos
                           {
                               StudentId = s.StudentId,
                               Username = s.Username,
                               Email = s.Email,
                               Cccd = s.Cccd,
                               QueQuan = s.QueQuan,
                               Image = s.Image,
                               GioiTinh = s.GioiTinh,
                               NienKhoa = s.NienKhoa,
                               Birthday = s.Birthday,
                           };

            result.LopQlId = findLop.LopQLId;
            result.TenLopQl = findLop.TenLopQL;
            result.LopTruongId = findLop.LopTruongId;
            result.LopPhoId = findLop.LopPhoId;
            result.MaxStudent = findLop.MaxStudent;
            result.StudentDtoss = findStud.ToList();

            return result;

        }

        public PageResultDtos<MonHoc> GetAllTeacherMonHoc([FromQuery] FilterDtos input, string teacherId)
        {
            var result = new PageResultDtos<MonHoc>();
            var findMh = from mh in _context.MonHocs
                            join tmh in _context.Teacher_MonHocs on mh.MaMonHoc equals tmh.MaMonHoc
                            where tmh.TeacherId == teacherId
                            select mh;
            var query = findMh.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderByDescending(e => e.TenMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public PageResultDtos<TeacherDtos> GetTeacherById([FromQuery] FilterDtos input, List<string> teacherId)
        {
            var result = new PageResultDtos<TeacherDtos>();
            var listTeacher = new List<TeacherDtos>();
            foreach (var item in teacherId)
            {
                var findteacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == item);
                var teacher = new TeacherDtos
                              {
                                  TeacherId = findteacher.TeacherId,
                                  TenGiangVien = findteacher.TenGiangVien,
                                  Birthday = findteacher.Birthday,
                                  BoMonId = findteacher.BoMonId,
                                  Cccd = findteacher.Cccd,
                                  Email = findteacher.Email,
                                  GioiTinh = findteacher.GioiTinh,
                                  QueQUan = findteacher.QueQuan,
                                  Image = findteacher.Image
                              };
                listTeacher.Add(teacher);
            }
            var query = listTeacher.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord)
                        || e.TenGiangVien.ToLower().Contains(input.KeyWord.ToLower())
);
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.TenGiangVien)
                .ThenBy(e => e.BoMonId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();
            return result;
        }
    
    }
}
