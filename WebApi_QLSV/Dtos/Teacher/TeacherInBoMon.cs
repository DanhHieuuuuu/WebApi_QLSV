using WebApi_QLSV.Dtos.Student;

namespace WebApi_QLSV.Dtos.Teacher
{
    public class TeacherInBoMon
    {
        public string BoMon { get; set; }
        public List<TeacherDtos> TeacherDtoss { get; set; } = new List<TeacherDtos>();
    }
}
