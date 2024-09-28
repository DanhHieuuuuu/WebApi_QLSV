using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IManagerService
    {
        Manager AddManager(AddManagerDtos input);
        PageResultDtos<ManagerDtos> GetAllManager([FromQuery] FilterDtos input);
        ManagerDtos LoginManager(Login input);
    }
}
