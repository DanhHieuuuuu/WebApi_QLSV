using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.BoMonFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IBoMonService
    {
        BoMon AddBoMon(AddBoMonDtos input);
        void DeleteBoMon(string BoMonId);
        PageResultDtos<BoMon> GetAllBoMon([FromQuery] FilterDtos input);
        List<BoMonTheoKhoaDtos> GetBoMonTheoKhoa();
        BoMon UpdateBoMon(UpdateBoMonDtos input);
    }
}
