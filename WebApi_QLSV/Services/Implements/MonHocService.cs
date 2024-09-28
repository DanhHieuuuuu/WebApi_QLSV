using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Services.Interfaces;

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
           var findCTk = _context.CTKhungs.FirstOrDefault( c => c.CTKhungId == input.CTKhungId);
           var restult = new MonHoc
           {
               MonId = input.MonId,
               TenMon = input.TenMon,
               Sotin = input.Sotin,
               CTKhungId = input.CTKhungId,
               KiHoc = input.KiHoc
           };
            return restult;
        }
    }
}
