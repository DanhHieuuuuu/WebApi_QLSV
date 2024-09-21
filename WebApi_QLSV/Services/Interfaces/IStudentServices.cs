using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IStudentServices
    {
        Student AddStudent(AddStudentDtos input);
        ResponseLoginDtos LoginStudent(LoginStudentDtos input);
        PageResultDtos<StudentDtos> GetAllStudent([FromQuery] FilterDtos input);
        //PageResultDtos<StudentInClassDtos> GetAllStudentInLopHP([FromQuery] FilterDtos input, [FromQuery] string ClassName);
    }
}
