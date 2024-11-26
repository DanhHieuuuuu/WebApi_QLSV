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
        void AddTeachertoMonHoc( List<string> listTeacher, string maMonHoc);
        void DeleteTeacher(string teacherId);
        PageResultDtos<TeacherDtos> GetAll([FromQuery] FilterDtos input);
        List<TeacherDtos> GetAllTeacherInBoMon( string? boMonId);
        List<TeacherDtos> GetAllTeacherInKhoa(string? khoaId);
        PageResultDtos<MonHoc> GetAllTeacherMonHoc([FromQuery] FilterDtos input, string teacherId);
        PageResultDtos<TeacherInBoMon> GetAllTeacherPerBoMon([FromQuery] FilterDtos input);
        StudentInLopQLDtos GetStudentInLopQl([FromQuery] string teacherId);
        PageResultDtos<TeacherDtos> GetTeacherById([FromQuery] FilterDtos input, List<string> teacherId);
        ResponseLoginTeacherDtos LoginTeacher(Login input);
        void RemoveTeacherToMonHoc([FromQuery] string teacherId, string maMonHoc);
        Task<TeacherDtos> UpdateTeacher([FromQuery] string teacherId, [FromForm] UpdateTeacherDtos input);
    }
}
