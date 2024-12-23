﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class ManagerService : IManagerService
    {
        private readonly ApplicationDbContext _context;
        private readonly Jwtsettings _jwtsettings;

        public ManagerService(ApplicationDbContext context, Jwtsettings jwtsettings)
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
                new Claim(ClaimTypes.Role, "Manager")
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
        public async Task<Manager> AddManager2([FromForm] AddManagerDtos2 input)
        {
            var findEmail = _context.Managers.FirstOrDefault(m => m.Email == input.Email);
            if (findEmail != null)
            {
                throw new UserExceptions("Trùng Email");
            }
            var findCccd = _context.Managers.FirstOrDefault(c => c.Cccd == input.Cccd);
            if (findCccd != null)
            {
                throw new UserExceptions("Đã Tồn tại CCCD");
            }
            var manager = new Manager
            {
                ManagerId = input.ManagerId,
                Email = input.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(input.Password),
                Username = input.Username,
                Birthday = input.Birthday,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                QueQuan = input.QueQUan,
            };
            if (input.Image.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", input.Image.FileName);
                using (var stream = System.IO.File.Create(path))
                {
                    await input.Image.CopyToAsync(stream);
                }
                manager.Image = "/images/" + input.Image.FileName;
            }
            else
            {
                throw new UserExceptions("Không có file");
            }
            _context.Managers.Add(manager);
            _context.SaveChanges();
            return manager;
        }
        public ResponseLoginManagerDtos LoginManager(Login input)
        {
            var manager =
                _context.Managers.SingleOrDefault(x => x.Email == input.Email)
                ?? throw new UserExceptions("Không tồn tại tài khoản");

            bool isValid = BCrypt.Net.BCrypt.Verify(input.Password, manager.Password);
            var token = Createtokens(manager.Username);
            if (isValid)
            {
                var sucess = new ResponseLoginManagerDtos
                {
                    ManagerId = manager.ManagerId,
                    Username = manager.Username,
                    Email = manager.Email,
                    Birthday=manager.Birthday,
                    Cccd = manager.Cccd,
                    GioiTinh = manager.GioiTinh,
                    QueQuan = manager.QueQuan,
                    UrlImage = manager.Image,
                    Token = token,
                };
                return sucess;
            }
            else
            {
                throw new UserExceptions("Không đúng mật khẩu");
            }
        }
        public PageResultDtos<ManagerDtos> GetAllManager([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<ManagerDtos>();
            var allManager =
                from mng in _context.Managers
                select new ManagerDtos
                {
                    ManagerId = mng.ManagerId,
                    Username = mng.Username,
                    Birthday = mng.Birthday,
                    Cccd = mng.Cccd,
                    Email = mng.Email,
                    GioiTinh = mng.GioiTinh,
                    QueQuan = mng.QueQuan,
                    Image = mng.Image
                };
            var query = allManager.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.Username.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query
                .OrderBy(e => e.Username)
                .ThenBy(e => e.Birthday)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public async Task<ManagerDtos> UpdateManager(
            [FromQuery] string managerId,
            [FromForm] UpdateManagerDtos input
        )
        {
            var findManager =
                _context.Managers.SingleOrDefault(s => s.ManagerId == managerId)
                ?? throw new UserExceptions("Không tồn tại quản lí");
            var checkCccd = _context.Managers.Count(s => s.Cccd == input.Cccd);
            if (
                input.Username == null
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

            findManager.Username = input.Username;
            findManager.Birthday = input.Birthday;
            findManager.QueQuan = input.QueQuan;
            findManager.Cccd = input.Cccd;
            findManager.GioiTinh = input.GioiTinh;
            if (input.Image == null)
            {
                findManager.Image = findManager.Image;
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
                    findManager.Image = "/images/" + input.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
            }
            _context.Managers.Update(findManager);
            _context.SaveChanges();
            var newManager = new ManagerDtos
            {
                ManagerId = findManager.ManagerId,
                Username = input.Username,
                Email = findManager.Email,
                Birthday = input.Birthday,
                QueQuan = input.QueQuan,
                Cccd = input.Cccd,
                GioiTinh = input.GioiTinh,
                Image = findManager.Image,
                
            };
            return newManager;
        }

    }
}
