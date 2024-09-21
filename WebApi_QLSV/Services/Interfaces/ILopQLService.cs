using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface ILopQLService
    {
        LopQL AddLopQL(AddLopQLDtos input);
        PageResultDtos<LopQL> GetAllLopQL([FromQuery] FilterDtos input);
    }
}
