using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.CTKhungFd;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface ICTKhungService
    {
        CTKhung AddCTKhung(AddCTKhungDtos input);
        PageResultDtos<CTKhung> GetAllCTKhung([FromQuery] FilterDtos input);
        PageResultDtos<MonHoc> GetAllMonHocInCTK([FromQuery] FilterDtos input, string id);
        List<MonInKiDtos> GetAllMonHocInKi(string nganhId);
    }
}
