using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IManagerService
    {
        Task<Manager> AddManager2([FromForm] AddManagerDtos2 input);
        PageResultDtos<ManagerDtos> GetAllManager([FromQuery] FilterDtos input);
        ResponseLoginManagerDtos LoginManager(Login input);
    }
}
