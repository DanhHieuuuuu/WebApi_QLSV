namespace WebApi_QLSV.Dtos.Student
{
    public class StudentInClass
    {
        public string Class { get; set; }
        public List<StudentDtos> StudentDtoss { get; set; } = new List<StudentDtos>();
    }
}
