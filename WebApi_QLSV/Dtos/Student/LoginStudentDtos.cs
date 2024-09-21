using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Student
{
    public class LoginStudentDtos
    {
        public string StudentId { get; set; }
        public string Password { get; set; }
    }
}
