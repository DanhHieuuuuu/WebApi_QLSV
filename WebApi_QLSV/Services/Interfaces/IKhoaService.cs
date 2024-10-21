using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IKhoaService
    {
        Khoa AddKhoa([FromBody] AddKhoaDtos input);
        void DeleteKhoa(string khoaId);
        PageResultDtos<Khoa> GetAllKhoa([FromQuery] FilterDtos input);
        PageResultDtos<KhoaDetailsDtos> GetKhoaDetail([FromQuery] FilterDtos input);
        Khoa UpdateKhoa(UpdateKhoaDtos input);
    }
}
