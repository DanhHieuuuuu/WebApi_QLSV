using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IStudentServices
    {
        ResponseLoginStudentDtos LoginStudent(Login input);
        PageResultDtos<StudentDtos> GetAllStudent([FromQuery] FilterDtos input);
        //List<MonHocDtos> DiemMonHoc(string studentId, string nganhId);
        List<AddBangDiemDtos> GetBangDiem(string studentId);
        PageResultDtos<StudentInClass> GetAllStudentInClass([FromQuery] FilterDtos input);
        Task<Student> AddStudent2(AddStudentDtos2 input);
        Task<StudentDtos> UpdateStudent([FromQuery] string studentId, [FromForm] UpdateStudentDtos input);
        void DeleteStudent(string studentId);
        //PageResultDtos<StudentInClassDtos> GetAllStudentInLopHP([FromQuery] FilterDtos input, [FromQuery] string ClassName);
    }
}
