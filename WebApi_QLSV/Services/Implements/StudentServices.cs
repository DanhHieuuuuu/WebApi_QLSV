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
    public ResponseLoginDtos LoginStudent(Login input)
        {
            var student =
                _context.Students.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");

            bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, student.Password);
            var token = Createtokens(student.Username);
            var findNganh = _context.LopQLs.FirstOrDefault(l => l.TenLopQL == student.TenLopQL);
            var nganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == findNganh.NganhId);
            if (isValid)
            {
                var sucess = new ResponseLoginDtos
                {
                    Username = student.Username,
                    Email = student.Email,
                    Token = token,
                    Birthday = student.Birthday,
                    QueQuan = student.QueQuan,
                    TenLopQL = student.TenLopQL,
                    Cccd = student.Cccd,
                    GioiTinh = student.GioiTinh,
                    StudentId = student.StudentId,
                    nganh = nganh.TenNganh,
                    nganhId = nganh.NganhId,
                };
                return sucess;
            }
            else
            {
                throw new UserExceptions("Không đúng mật khẩu");
            }
        }

        public Student AddStudent(AddStudentDtos input)
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
                _context.LopQLs.FirstOrDefault(n => n.TenLopQL == input.TenLopQL.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại lớp {input.TenLopQL.ToUpper()}");
            var findCccd = _context.Students.FirstOrDefault(c => c.Cccd == input.Cccd);
            if (findCccd != null)
            {
                throw new UserExceptions("Đã Tồn tại CCCD");
            }
            var sum = _context.Students.Count(s => s.TenLopQL == input.TenLopQL.ToUpper());
            if (sum >= findLopQL.MaxStudent)
            {
                throw new UserExceptions("Lớp đã đủ số lượng");
            }
            var student = new Student
            {
                StudentId = IdStudent,
                Username = input.Username,
                Email = Email,
                Password = BCrypt.Net.BCrypt.HashPassword(Password),
                Birthday = input.Birthday,
                QueQuan = input.QueQuan,
                TenLopQL = input.TenLopQL.ToUpper(),
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
            };
            _context.Students.Add(student);
            _context.SaveChanges();
            return student;
        }

        //public PageResultDtos<StudentInClassDtos> GetAllStudentInLopHP(
        //    [FromQuery] FilterDtos input,
        //    [FromQuery] string ClassName
        //)
        //{
        //    var result = new PageResultDtos<StudentInClassDtos>();
        //    var findStudent =
        //        from stu in _context.Students
        //        join stuinclass in _context.ClassStudents
        //            on stu.StudentId equals stuinclass.StudentId
        //        where stuinclass.LopHPId == ClassName
        //        select new StudentInClassDtos
        //        {
        //            Username = stu.Username,
        //            Email = stu.Email,
        //            Birthday = stu.Birthday,
        //            StudentId = stu.StudentId,
        //            QueQuan = stu.QueQuan,
        //        };
        //    var query = findStudent.Where(e =>
        //        string.IsNullOrEmpty(input.KeyWord)
        //        || e.Username.ToLower().Contains(input.KeyWord.ToLower())
        //    );
        //    result.TotalItem = query.Count();

        //    query = query
        //        .OrderByDescending(e => e.Username)
        //        .ThenByDescending(e => e.StudentId)
        //        .Skip(input.Skip())
        //        .Take(input.PageSize);

        //    result.Items = query.ToList();

        //    return result;
        //}

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
                    TenLopQL = stu.TenLopQL,
                    GioiTinh = stu.GioiTinh,
                    Birthday = stu.Birthday,
                    QueQuan = stu.QueQuan,
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
    }
}
