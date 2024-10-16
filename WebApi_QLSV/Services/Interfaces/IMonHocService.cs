using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IMonHocService
    {
        MonHoc AddMonHoc(AddMonHocDtos input);
        void DeleteMonHoc(string MaMonHoc);
        PageResultDtos<MonHoc> GetAllMonHoc([FromQuery] FilterDtos input);
        PageResultDtos<MonHocTrongNganhDtos> GetAllMonTrongNganh([FromQuery] FilterDtos input);
        MonHoc UpdateMonHoc(UpdateMonHoc input);
    }
}
