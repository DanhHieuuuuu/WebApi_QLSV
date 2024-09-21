using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface INganhService
    {
        Nganh AddNganh(AddNganhDtos input);
        PageResultDtos<Nganh> GetAllNganh([FromQuery] FilterDtos input);
    }
}
