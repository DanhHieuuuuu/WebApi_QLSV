﻿using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class NganhService : INganhService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILopQLService _lopQLService;
        public NganhService(ApplicationDbContext context, ILopQLService lopQLService)
        {
            _context = context;
            _lopQLService = lopQLService;
        }

        public Nganh AddNganh(AddNganhDtos input)
        {
            var findKHoa =
                _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId.ToUpper())
                ?? throw new UserExceptions($"Không tồn tại Id khoa: {input.KhoaId}");
            var findNgang = _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId.ToUpper());
            if (findNgang != null)
            {
                throw new UserExceptions("Đã tồn tại mã ngành");
            }
            var result = new Nganh
            {
                NganhId = input.NganhId.ToUpper(),
                TenNganh = input.TenNganh,
                NgayThanhLap = input.NgayThanhLap,
                TruongNganh = input.TruongNganhId,
                PhoNganh = input.PhoNganhId,
                KhoaId = input.KhoaId.ToUpper(),
                SumClass = 0,
            };
            _context.Nganhs.Add(result);
            _context.SaveChanges();
            return result;
        }

        public PageResultDtos<Nganh> GetAllNganh([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<Nganh>();

            var query = _context.Nganhs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.KhoaId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.TenNganh)
                .ThenByDescending(e => e.SumClass)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public List<NganhTheoKhoaDots> GetNganhTheoKhoa()
        {
            var findNganhs = from k in _context.Khoas
                             join n in _context.Nganhs on k.KhoaId equals n.KhoaId
                             select new
                             {
                                 n.NganhId,
                                 n.TenNganh,
                                 n.NgayThanhLap,
                                 n.TruongNganh,
                                 n.PhoNganh,
                                 n.KhoaId,
                                 k.TenKhoa,
                                 n.SumClass,

                             };
            var group = findNganhs.GroupBy(e => e.KhoaId).Select(
                g => new
                {
                    khoaId = g.Key,
                    nganh = g.OrderBy(e => e.TenNganh).ToList(),
                }
                );
            var listNganh = new List<NganhTheoKhoaDots>();
            foreach (var item in group)
            {
                var newNganh = new NganhTheoKhoaDots();
                newNganh.KhoaId = item.khoaId;
                foreach (var item1 in item.nganh)
                {
                    var nganh2 = new Nganh
                    {
                        NganhId = item1.NganhId,
                        TenNganh = item1.TenNganh,
                        NgayThanhLap = item1.NgayThanhLap,
                        TruongNganh = item1.TruongNganh,
                        PhoNganh = item1.PhoNganh,
                        KhoaId = item1.KhoaId,
                        SumClass = item1.SumClass,
                    };
                    if (item1.KhoaId == item.khoaId)
                    {
                        newNganh.Nganhs.Add(nganh2);
                        newNganh.TenKhoa = item1.TenKhoa;
                    }
                }
                listNganh.Add(newNganh);
            }
            return listNganh;
        }
        public Nganh UpdateNganh(UpdateNganhDtos input)
        {
            var findNganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == input.NganhId)
                ?? throw new UserExceptions("Không tìm thấy ngành");
            var findKhoa = _context.Khoas.FirstOrDefault(k => k.KhoaId == input.KhoaId)
                ?? throw new UserExceptions("Không tìm thấy khoa");
                        var findTea = _context.Teachers.Any(k => k.TeacherId == input.TruongNganhId);
            findNganh.TenNganh = input.TenNganh;
            findNganh.TruongNganh = input.TruongNganhId;
            findNganh.PhoNganh = input.PhoNganhId;
            findNganh.NgayThanhLap = input.NgayThanhLap;
            findNganh.KhoaId = input.KhoaId;
            _context.Nganhs.Update(findNganh);
            _context.SaveChanges();
            return findNganh;
        }
        public void DeleteNganh(string nganhId)
        {
            var findNganh = _context.Nganhs.FirstOrDefault(n => n.NganhId == nganhId)
                ?? throw new UserExceptions("Không tồn tại ngành");
            var findLopQL = _context.LopQLs.Where(l => l.NganhId == nganhId).ToList();
            foreach (var item in findLopQL)
            {
                _lopQLService.DeleteLopQL(item.LopQLId);
            }
            _context.Nganhs.Remove(findNganh);
            _context.SaveChanges();
        }
    }
}
