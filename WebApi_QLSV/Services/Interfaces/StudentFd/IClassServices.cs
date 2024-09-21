using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassStudent;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.Services.Interfaces.StudentFd
{
    public interface IClassServices
    {
        LopHP AddClass(AddClassDtos input);
        PageResultDtos<LopHP> GetAll([FromQuery] FilterDtos input);
    }
}
