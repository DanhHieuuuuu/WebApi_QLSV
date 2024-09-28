using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.CTKhungFd;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class CTKhungService : ICTKhungService
    {
        private readonly ApplicationDbContext _context;

        public CTKhungService(ApplicationDbContext context)
        {
            _context = context;
        }

        public CTKhung AddCTKhung(AddCTKhungDtos input)
        {
            int tongtin = 0;
            for (int i = 0; i < input.MonHocs.Count(); i++)
            {
                var mon = new MonHoc
                {
                    MonId = input.MonHocs[i].MonId,
                    TenMon = input.MonHocs[i].TenMon,
                    KiHoc = input.MonHocs[i].KiHoc,
                    Sotin = input.MonHocs[i].Sotin,
                    CTKhungId = input.CTKhungId,
                };
                tongtin += input.MonHocs[i].Sotin;
                _context.MonHocs.Add(mon);
            }
            var result = new CTKhung { CTKhungId = input.CTKhungId, TongSoTin = tongtin };
            _context.CTKhungs.Add(result);
            _context.SaveChanges();
            return result;
        }

        public PageResultDtos<CTKhung> GetAllCTKhung([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<CTKhung>();

            var query = _context.CTKhungs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.CTKhungId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderByDescending(e => e.CTKhungId)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<MonHoc> GetAllMonHocInCTK([FromQuery] FilterDtos input, string id)
        {
            var result = new PageResultDtos<MonHoc>();
            var findCTK = _context.Nganhs.FirstOrDefault(n => n.NganhId == id);
            var monhoc =
                from mh in _context.MonHocs
                where mh.CTKhungId == findCTK.CTKhungId
                select new MonHoc
                {
                    KiHoc = mh.KiHoc,
                    MonId = mh.MonId,
                    Sotin = mh.Sotin,
                    TenMon = mh.TenMon,
                    CTKhungId = mh.CTKhungId,
                };
            var query = monhoc.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderByDescending(e => e.TenMon).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public List<MonInKiDtos> GetAllMonHocInKi(string id)
        {
            var findCTK =
                _context.Nganhs.FirstOrDefault(n => n.NganhId == id)
                ?? throw new UserExceptions("Không tìm thấy ngành");
            //var monhoc = from lhp in _context.LopHPs
            //             join mh in _context.MonHocs on lhp.MonId equals mh.MonId
            //             join d in _context.ClassStudents on lhp.LopHPId equals d.LopHPId
            //             where findCTK.CTKhungId == mh.CTKhungId && d.StudentId == studentId
            //             select new MonHocDtos
            //             {
            //                 kiHoc = mh.KiHoc,
            //                 MonId = mh.MonId,
            //                 Sotin = mh.Sotin,
            //                 TenMon = mh.TenMon,
            //                 DiemMH = d.DiemMH,

            //             };
            var monhoc =
                from mhs in _context.MonHocs
                where mhs.CTKhungId == findCTK.CTKhungId
                select new MonHocDtos
                {
                    kiHoc = mhs.KiHoc,
                    MonId = mhs.MonId,
                    Sotin = mhs.Sotin,
                    TenMon = mhs.TenMon,
                };
            var group = monhoc.GroupBy(e => e.kiHoc)
                        .Select(g => new
                        {
                            kihoc = g.Key,
                            MonHoc = g.ToList(),
                        }).ToList();
            List<MonInKiDtos> mikg = new List<MonInKiDtos>();

            foreach (var item in group)
            {
                MonInKiDtos mik = new MonInKiDtos(item.kihoc);

                mik.monHocs = item.MonHoc;
                mikg.Add(mik);
            }
            return mikg;
        }
    }
}
