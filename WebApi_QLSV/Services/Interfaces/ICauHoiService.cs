using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.CauHoiFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface ICauHoiService
    {
        CauHoi AddCauHoi(AddCauHoiDtos input);
        PageResultDtos<CauHoi> GetAllCauHoi([FromQuery] FilterDtos input);
    }
}
