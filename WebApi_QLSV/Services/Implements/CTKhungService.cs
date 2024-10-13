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
        private readonly IStudentServices _studentServices;
        private static int index;

        public CTKhungService(ApplicationDbContext context, IStudentServices studentServices)
        {
            _context = context;
            _studentServices = studentServices;
        }

        //public CTKhung AddCTKhung(AddCTKhungDtos input)
        //{
        //    int tongtin = 0;
        //    for (int i = 0; i < input.MonHocs.Count(); i++)
        //    {
        //        var mon = new MonHoc
        //        {
        //            MaHocPhan = input.MonHocs[i].MonId,
        //            TenMon = input.MonHocs[i].TenMon,
        //            KiHoc = input.MonHocs[i].KiHoc,
        //            Sotin = input.MonHocs[i].Sotin,
        //            CTKhungId = input.CTKhungId,
        //        };
        //        tongtin += input.MonHocs[i].Sotin;
        //        _context.MonHocs.Add(mon);
        //    }
        //    var result = new CTKhung { CTKhungId = input.CTKhungId, TongSoTin = tongtin };
        //    _context.CTKhungs.Add(result);
        //    _context.SaveChanges();
        //    return result;
        //}

//        public PageResultDtos<CTKhung> GetAllCTKhung([FromQuery] FilterDtos input)
//        {
//            var result = new PageResultDtos<CTKhung>();

//            var query = _context.CTKhungs.Where(e =>
//                string.IsNullOrEmpty(input.KeyWord)
//                || e.CTKhungId.ToLower().Contains(input.KeyWord.ToLower())
//            );
//            result.TotalItem = query.Count();

//            query = query
//                .OrderByDescending(e => e.CTKhungId)
//                .Skip(input.Skip())
//                .Take(input.PageSize);

//            result.Items = query.ToList();

//            return result;
//        }

//        public PageResultDtos<MonHoc> GetAllMonHocInCTK([FromQuery] FilterDtos input)
//        {
//            var result = new PageResultDtos<MonHoc>();
//            //var findCTK = _context.Nganhs.FirstOrDefault(n => n.NganhId == id);
//            //var monhoc =
//            //    from mh in _context.MonHocs
//            //    where mh.CTKhungId == findCTK.CTKhungId
//            //    select new MonHoc
//            //    {
//            //        KiHoc = mh.KiHoc,
//            //        MonId = mh.MonId,
//            //        Sotin = mh.Sotin,
//            //        TenMon = mh.TenMon,
//            //        CTKhungId = mh.CTKhungId,
//            //    };
//            var query = _context.MonHocs.Where(e =>
//                string.IsNullOrEmpty(input.KeyWord)
//                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
//            );
//            result.TotalItem = query.Count();

//            query = query.OrderBy(e => e.TenMon).Skip(input.Skip()).Take(input.PageSize);

//            result.Items = query.ToList();

//            return result;
//        }

//        public List<MonInKiDtos> GetAllMonHocInKi(string nganhId, string studentId)
//        {
//            var findCTK =
//                _context.Nganhs.FirstOrDefault(n => n.NganhId == nganhId)
//                ?? throw new UserExceptions("Không tìm thấy ngành");
//            var monhoc =
//                from mh in _context.MonHocs
//                where findCTK.CTKhungId == mh.CTKhungId
//                select new MonHocDtos
//                {
//                    kiHoc = mh.KiHoc,
//                    MonId = mh.MonId, 
//                    TenMon = mh.TenMon,
//                    Sotin = mh.Sotin,
//                };

//            var diemMh = _studentServices.DiemMonHoc(studentId, nganhId);

//            List<MonInKiDtos> mikg = new List<MonInKiDtos>();

//            var group = monhoc
//                .GroupBy(e => e.kiHoc)
//                .Select(g => new { kihoc = g.Key, MonHoc = g.OrderBy(o => o.TenMon).ToList() })
//                .ToList();
//            foreach (var item in group)
//            {
//                foreach (var item2 in item.MonHoc)
//                {
//                    foreach (var item3 in diemMh)
//                    {
//                        if (item2.MonId == item3.MonId)
//                        {
//                            item2.DiemKT = item3.DiemKT;
//                            item2.DiemQT = item3.DiemQT;
//                            item2.DiemMH = item3.DiemMH;
//                            break;
//                        }
//                    }
//                }
//                MonInKiDtos mik = new MonInKiDtos(item.kihoc);
//                mik.MonHocs = item.MonHoc;
//                mikg.Add(mik);
//            }
//            return mikg;
//        }
    }
}
