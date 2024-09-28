using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.Services.Interfaces.StudentAndClassFd
{
    public interface IClassStudentService
    {
        ClassStudent AddStudentInClass(AddStudentInLopHPDtos input);
        PageResultDtos<ClassStudent> GetAll([FromQuery] FilterDtos input);
    }
}
