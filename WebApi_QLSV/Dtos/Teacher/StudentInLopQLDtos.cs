using WebApi_QLSV.Dtos.Student;

namespace WebApi_QLSV.Dtos.Teacher
{
    public class StudentInLopQLDtos
    {
        public string LopQlId { get; set; }
        public string TenLopQl { get; set; }
        public string LopTruongId { get; set; }
        public string LopPhoId { get; set; }
        public int MaxStudent {  get; set; }
        public List<StudentDtos> StudentDtoss { get; set; } = new List<StudentDtos>();

    }
}
