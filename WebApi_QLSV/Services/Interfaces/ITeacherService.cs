using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<Teacher> AddTeacher2([FromForm] AddTeacherDtos2 input);
        PageResultDtos<TeacherDtos> GetAll([FromQuery] FilterDtos input);
        PageResultDtos<TeacherInBoMon> GetAllTeacherInBoMon([FromQuery] FilterDtos input);
        ResponseLoginTeacherDtos LoginTeacher(Login input);
        Task<TeacherDtos> UpdateTeacher([FromQuery] string teacherId, [FromForm] UpdateTeacherDtos input);
    }
}
