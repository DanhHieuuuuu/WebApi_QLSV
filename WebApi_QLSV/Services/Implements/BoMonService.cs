using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.BoMonFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class BoMonService : IBoMonService
    {
        private readonly ApplicationDbContext _context;

        public BoMonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public BoMon AddBoMon(AddBoMonDtos input)
        {
            var findKhoa = _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId)
                ?? throw new UserExceptions("Không tồn tại khoa");
            var findBoMon = _context.BoMons.FirstOrDefault(k => k.BoMonId == input.BoMonId.ToUpper());
            if (findBoMon != null)
            {
                throw new UserExceptions("Đã tồn tại mã bộ môn");
            }

            var results = new BoMon
            {
                BoMonId = input.BoMonId.ToUpper(),
                TenBoMon = input.TenBoMon,
                TruongBoMon = input.TruongBoMonId,
                PhoBoMon = input.PhoBoMonId,
                NgayThanhLap = input.NgayThanhLap,
                SoLuongGV = 0,
                KhoaId = findKhoa.KhoaId,
            };
            _context.BoMons.Add(results);
            _context.SaveChanges();
            return results;
        }

        public PageResultDtos<BoMon> GetAllBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<BoMon>();

            var query = _context.BoMons.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenBoMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.TenBoMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public List<BoMon> GetBoMonTheoKhoa([FromQuery] string boMonId)
        {
            var findBoMon = _context.BoMons.FirstOrDefault( b => b.BoMonId == boMonId );
            var dsBoMon = from bm in _context.BoMons
                          where bm.KhoaId == findBoMon.KhoaId
                          select bm;

            var listBoMon = new List<BoMon>();
            foreach (var item in dsBoMon)
            {
                listBoMon.Add(item);

            }
            return listBoMon;
        }

        public BoMon UpdateBoMon(UpdateBoMonDtos input)
        {
            var findBoMon = _context.BoMons.FirstOrDefault(b => b.BoMonId == input.BoMonId)
                ?? throw new UserExceptions("Không tồn tại bộ môn");
            var findKhoa = _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId)
                ?? throw new UserExceptions("Không tồn tại khoa");
            findBoMon.TenBoMon = input.TenBoMon;
            findBoMon.TruongBoMon = input.TruongBoMonId;
            findBoMon.PhoBoMon = input.PhoBoMonId;
            findBoMon.NgayThanhLap = input.NgayThanhLap;
            findBoMon.BoMonId = input.BoMonId;
            _context.BoMons.Update(findBoMon);
            _context.SaveChanges();
            return findBoMon;
        }
        public void DeleteBoMon(string BoMonId)
        {
            var findBoMon = _context.BoMons.FirstOrDefault(b => b.BoMonId ==  BoMonId);
            if(findBoMon != null)
            {
                var findMonHoc = _context.MonHocs.Where(m => m.BoMonId == BoMonId).ToList();
                var findGiangVien = _context.Teachers.Where(t => t.BoMonId == BoMonId).ToList();
                _context.MonHocs.RemoveRange(findMonHoc);
                _context.Teachers.RemoveRange(findGiangVien);
            }
            _context.BoMons.Remove(findBoMon);
            _context.SaveChanges();
        }
    }
}
