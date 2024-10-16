using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface INganhService
    {
        Nganh AddNganh(AddNganhDtos input);
        void DeleteNganh(string nganhId);
        PageResultDtos<Nganh> GetAllNganh([FromQuery] FilterDtos input);
        List<NganhTheoKhoaDots> GetNganhTheoKhoa();
        Nganh UpdateNganh(UpdateNganhDtos input);
    }
}
