using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IMonHocService
    {
        MonHoc AddMonHoc(AddMonHocDtos input);
        void DeleteMonHoc(string MaMonHoc);
        PageResultDtos<MonHoc> GetAllMonHoc([FromQuery] FilterDtos input);
        PageResultDtos<MonHocTrongBoMonDtos> GetAllMonTrongBoMon([FromQuery] FilterDtos input);
        PageResultDtos<MonHocDetailDto> GetMonHocDetail([FromQuery] FilterDtos input);
        PageResultDtos<TeacherDtos> GetTeacherPhuTrach([FromQuery] string maMonHoc, FilterDtos input);
        MonHoc UpdateMonHoc(UpdateMonHoc input);
    }
}
