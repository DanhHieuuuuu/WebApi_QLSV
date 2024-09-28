using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Entities.ClassFd;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class LopHPService : ILopHPService
    {
        private readonly ApplicationDbContext _context;

        public LopHPService(ApplicationDbContext context)
        {
            _context = context;
        }
        public LopHP AddLopHP(AddLopHPDtos input)
        {
            var findNganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId)
                ?? throw new UserExceptions($"Không tồn tại mã ngành: {input.NganhId}");

            var findMon = _context.MonHocs.FirstOrDefault(m => m.CTKhungId == findNganh.CTKhungId && m.MonId == input.MonId)
                ?? throw new UserExceptions($"Không tồn tại mã môn học: {input.MonId} trong ngành: {input.NganhId}");
            var findBlock = _context.MonHocs.FirstOrDefault( m => m.MonId == input.MonId )
                ?? throw new UserExceptions($"Không tồn tại mã block: {input.BlockId}");
            var findTeacher = _context.Teachers.FirstOrDefault(t => t.TeacherId == input.TeacherId)
                ?? throw new UserExceptions($"Không tồn tại giảng viên có mã: {input.TeacherId}");
            var lop = new LopHP
            {
                LopHPId = input.LopHPId.ToUpper(),
                TenLopHP = input.TenLopHP.ToUpper(),
                MaxStudent = input.MaxStudent,
                NganhId = input.NganhId,
                BlockId = input.BlockId,
                MonId = input.MonId,
                TeacherId = input.TeacherId,
                BatDau = input.BatDau,
                KetThuc = input.KetThuc
            };
            _context.LopHPs.Add( lop );
            _context.SaveChanges();
            return lop;
        }
        public PageResultDtos<LopHP> GetAllLopHP([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<LopHP>();

            var query = _context.LopHPs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenLopHP.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.TenLopHP).ThenBy(e => e.LopHPId).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
