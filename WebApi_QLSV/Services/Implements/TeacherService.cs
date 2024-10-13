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

            bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, teacher.Password);
            var token = Createtokens(teacher.TenGiangVien);

            if (isValid)
            {
                var sucess = new ResponseLoginTeacherDtos
                {
                    TeacherId = teacher.TeacherId,
                    TenGiangVien = teacher.TenGiangVien,
                    Email = teacher.Email,
                    BoMonId = teacher.BoMonId,
                    Cccd = teacher.Cccd,
                    Birthday = teacher.Birthday,
                    GioiTinh = teacher.GioiTinh,
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
                        Image = teacher.Image,
                    };
                var query = allTeacher.Where(e =>
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

        public PageResultDtos<TeacherInBoMon> GetAllTeacherInBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<TeacherInBoMon>();
            var group = _context
                .Teachers.GroupBy(t => t.BoMonId)
                .Select(t => new
                {
                    boMon = t.Key,
                    teacher = t.OrderBy(o => o.TenGiangVien).ToList(),
                });
            var listTeacher = new List<TeacherInBoMon>();
            foreach (var item in group)
            {
                var newteacher = new TeacherInBoMon();
                newteacher.BoMon = item.boMon;
                foreach (var item1 in item.teacher)
                {
                    var tea = new TeacherDtos
                    {
                        TeacherId = item1.TeacherId,
                        TenGiangVien = item1.TenGiangVien,
                        Email = item1.Email,
                        Cccd = item1.Cccd,
                        Birthday = item1.Birthday,
                        GioiTinh = item1.GioiTinh,
                    };
                    newteacher.TeacherDtoss.Add(tea);
                }
                listTeacher.Add(newteacher);
            }
            var query = listTeacher.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.BoMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.BoMon).Skip(input.Skip()).Take(input.PageSize);

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
                || input.Email == null
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
            if (checkBoMon == null)
            {
                throw new UserExceptions("Không tồn tại bộ môn Id");
            }
            else
            {
                checkBoMon.SoLuongGV += 1;
                var findBoMon = _context.BoMons.SingleOrDefault(b => b.BoMonId == findTecher.BoMonId);
                findBoMon.SoLuongGV -= 1;
            }
            findTecher.TenGiangVien = input.TenGiangVien;
            findTecher.Email = input.Email;
            findTecher.Birthday = input.Birthday;
            findTecher.Cccd = input.Cccd;
            findTecher.GioiTinh = input.GioiTinh;
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
            _context.Teachers.Update(findTecher);
            _context.SaveChanges();
            var newTeacher = new TeacherDtos
            {
                TeacherId = findTecher.TeacherId,
                TenGiangVien = input.TenGiangVien,
                Email = input.Email,
                Birthday = input.Birthday,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                Image = findTecher.Image,
                BoMonId = input.BoMonId,
            };
            return newTeacher;
        }
    }
}
