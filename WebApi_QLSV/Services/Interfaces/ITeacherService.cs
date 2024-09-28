using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface ITeacherService
    {
        Teacher AddTeacher(AddTeacherDtos input);
        PageResultDtos<TeacherDtos> GetAll([FromQuery] FilterDtos input);
        TeacherDtos LoginTeacher(Login input);
    }
}
