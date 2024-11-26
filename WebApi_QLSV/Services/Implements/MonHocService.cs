using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;
using static Azure.Core.HttpHeader;

namespace WebApi_QLSV.Services.Implements
{
    public class MonHocService : IMonHocService
    {
        private readonly ApplicationDbContext _context;
        public MonHocService(ApplicationDbContext context)
        {
            _context = context;
        }
        public MonHoc AddMonHoc(AddMonHocDtos input)
        {
            var findBoMon = _context.BoMons.FirstOrDefault(c => c.BoMonId == input.BoMonId)
                 ?? throw new UserExceptions("Không tồn tại bộ môn");
            var result = new MonHoc
            {
                MaMonHoc = input.MaMonHoc,
                TenMon = input.TenMon,
                SoTin = input.Sotin,
                BoMonId = input.BoMonId,
            };
            _context.MonHocs.Add(result);
            _context.SaveChanges();
            return result;
        }
        public PageResultDtos<MonHoc> GetAllMonHoc([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHoc>();

            var query = _context.MonHocs.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenMon.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query
                .OrderBy(e => e.TenMon)
                .Skip(input.Skip())
                .Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public PageResultDtos<MonHocTrongBoMonDtos> GetAllMonTrongBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHocTrongBoMonDtos>();
            var findBoMon = from bm in _context.BoMons
                            join mh in _context.MonHocs on bm.BoMonId equals mh.BoMonId
                            select new
                            {
                                mh.MaMonHoc,
                                mh.TenMon,
                                mh.SoTin,
                                mh.BoMonId,
                                bm.TenBoMon,
                            };
            var group = findBoMon.GroupBy(c => c.BoMonId).Select(l => new
            {
                boMonId = l.Key,
                monHoc = l.OrderBy(e => e.TenMon).ToList(),
            });
            var listMonHoc = new List<MonHocTrongBoMonDtos>();
            foreach (var item in group)
            {
                var newMonHoc = new MonHocTrongBoMonDtos();
                newMonHoc.BoMonId = item.boMonId;
                foreach (var item1 in item.monHoc)
                {
                    var mhs = new MonHoc
                    {
                        MaMonHoc = item1.MaMonHoc,
                        TenMon = item1.TenMon,
                        SoTin = item1.SoTin,
                        BoMonId = item1.BoMonId,
                    };
                    if (item1.BoMonId == item.boMonId)
                    {
                        newMonHoc.TenBoMon = item1.TenBoMon;
                        newMonHoc.monHocs.Add(mhs);
                    }
                }
                listMonHoc.Add(newMonHoc);
            }
            var query = listMonHoc.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.BoMonId.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.BoMonId).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<MonHocDetailDto> GetMonHocDetail([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<MonHocDetailDto>();
            var group = from bm in _context.BoMons
                        join mh in _context.MonHocs on bm.BoMonId equals mh.BoMonId
                        select new
                        {
                            bm.BoMonId,
                            bm.TenBoMon,
                            mh.MaMonHoc,
                            mh.TenMon,
                            mh.SoTin
                        };
            var group3 = from mh in _context.MonHocs
                         join t_m in _context.Teacher_MonHocs on mh.MaMonHoc equals t_m.MaMonHoc
                         join t in _context.Teachers on t_m.TeacherId equals t.TeacherId
                         select new
                         {
                             monHocId = mh.MaMonHoc,
                             teacherId = t.TeacherId,
                             tenGiangVien = t.TenGiangVien,
                             email = t.Email,
                             cccd = t.Cccd,
                             birthday = t.Birthday,
                             gioiTinh = t.GioiTinh,
                             queQUan = t.QueQuan,
                             image = t.Image
                         };
            var group4 = group3.GroupBy(e => e.monHocId).Select(g => new
            {
                maMonHoc = g.Key,
                teacher = g.OrderBy(e => e.tenGiangVien).Select(item1 => new TeacherDtos
                {
                    TeacherId = item1.teacherId,
                    TenGiangVien = item1.tenGiangVien,
                    Email = item1.email,
                    Cccd = item1.cccd,
                    Birthday = item1.birthday,
                    GioiTinh = item1.gioiTinh,
                    QueQUan = item1.queQUan,
                    Image = item1.image,
                }).ToList()
            }).ToList();
            var listMH = new List<MonHocDetailDto>();
            foreach (var item in group)
            {
                var mh = new MonHocDetailDto();


                mh.BoMonId = item.BoMonId;
                mh.MaMonHoc = item.MaMonHoc;
                mh.TenMon = item.TenMon;
                mh.SoTin = item.SoTin;
                foreach (var item1 in group4)
                {
                    if(item1.maMonHoc == item.MaMonHoc)
                    {
                        mh.TeacherDtos = item1.teacher;
                    }
                }
                listMH.Add(mh);

            }
            var query = listMH.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord)
                        || e.BoMonId.ToLower().Contains(input.KeyWord.ToLower())
);
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.BoMonId).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }

        public PageResultDtos<TeacherDtos> GetTeacherPhuTrach([FromQuery] string maMonHoc, FilterDtos input)
        {
            var result = new PageResultDtos<TeacherDtos>();
            var group3 = from mh in _context.MonHocs
                         join t_m in _context.Teacher_MonHocs on mh.MaMonHoc equals t_m.MaMonHoc
                         join t in _context.Teachers on t_m.TeacherId equals t.TeacherId
                         where mh.MaMonHoc == maMonHoc
                         select new TeacherDtos
                         {
                             TeacherId = t.TeacherId,
                             TenGiangVien = t.TenGiangVien,
                             Email = t.Email,
                             Cccd = t.Cccd,
                             Birthday = t.Birthday,
                             GioiTinh = t.GioiTinh,
                             QueQUan = t.QueQuan,
                             Image = t.Image,
                             BoMonId = t.BoMonId
                         };
            var query = group3.Where(e =>
                        string.IsNullOrEmpty(input.KeyWord)
                        || e.BoMonId.ToLower().Contains(input.KeyWord.ToLower())
                        );
            result.TotalItem = query.Count();
            query = query.OrderBy(e => e.TenGiangVien).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
        public MonHoc UpdateMonHoc(UpdateMonHoc input)
        {
            var findMonHoc = _context.MonHocs.FirstOrDefault(m => m.MaMonHoc == input.MaMonHoc)
                ?? throw new UserExceptions("Không tồn tại mã môn học");
            var findBoMon = _context.BoMons.FirstOrDefault(b => b.BoMonId == input.BoMonId)
                ?? throw new UserExceptions("Không tồn tại mã bộ môn");
            findMonHoc.TenMon = input.TenMon;
            findMonHoc.SoTin = input.SoTin;
            findBoMon.BoMonId = input.BoMonId;
            _context.MonHocs.Update(findMonHoc);
            _context.SaveChanges();
            return findMonHoc;
        }
        public void DeleteMonHoc(string MaMonHoc)
        {
            var findMonHoc = _context.MonHocs.FirstOrDefault(m => m.MaMonHoc == MaMonHoc)
                ?? throw new UserExceptions("Không tồn tại môn học");
            var findMQH = _context.Teacher_MonHocs.Where(m => m.MaMonHoc == MaMonHoc).ToList();
            _context.Teacher_MonHocs.RemoveRange(findMQH);
            _context.SaveChanges();
            _context.MonHocs.Remove(findMonHoc);
            _context.SaveChanges();
        }
        //public PageResultDtos<MonHocTrongNganhDtos> GetAllMonTrongNganh([FromQuery] FilterDtos input)
        //{
        //    var result = new PageResultDtos<MonHocTrongNganhDtos>();
        //    var group = _context.MonHocs.Join(_context.BoMons,
        //            MonHocs => MonHocs.BoMonId,
        //            boMons => boMons.BoMonId,
        //            (MonHocs, boMons) => new
        //            {
        //                maBoMon = MonHocs.BoMonId,
                        
        //                tenBoMon = boMons.TenBoMon,
        //                maMonHoc = MonHocs.MaMonHoc,
        //                tenMon = MonHocs.TenMon,
        //                soTins = MonHocs.SoTin,
        //            }
        //        ).GroupBy(g => g.maBoMon).Select(l => new
        //        {
        //            maBoMon = l.Key,
        //            monHoc = l.OrderBy(e => e.tenMon).ToList(),
        //        });
        //    var listMonHoc = new List<MonHocTrongNganhDtos>();
        //    foreach (var item in group)
        //    {
        //        var newMonHoc = new MonHocTrongNganhDtos();
        //        newMonHoc.MaBoMon = item.maBoMon;
        //        foreach (var item1 in item.monHoc)
        //        {
        //            newMonHoc.TenBoMon = item1.tenBoMon;
        //            var m = new MonHoc
        //            {
        //                MaMonHoc = item1.maMonHoc,
        //                TenMon = item1.tenMon,
        //                SoTin = item1.soTins,
        //                BoMonId = item1.maBoMon,

        //            };
        //            newMonHoc.monHocs.Add(m);
        //        }
        //        listMonHoc.Add(newMonHoc);
        //    }
        //    var query = listMonHoc.Where(e =>
        //        string.IsNullOrEmpty(input.KeyWord)
        //        || e.MaBoMon.ToLower().Contains(input.KeyWord.ToLower())
        //    );
        //    result.TotalItem = query.Count();
        //    query = query.OrderBy(e => e.MaBoMon).Skip(input.Skip()).Take(input.PageSize);

        //    result.Items = query.ToList();

        //    return result;
        //}

    }
}
