namespace WebApi_QLSV.Dtos.Student
{
    public class StudentInClass
    {
        public string LopQLId { get; set; }
        public string TenLopQL { get; set; }
        public List<StudentDtos> StudentDtoss { get; set; } = new List<StudentDtos>();
    }
}
