using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities.ClassFd
{
    [Table(nameof(ClassStudent))]
    public class ClassStudent
    {
        public string StudentId { get; set; }
        public string LopHPId { get; set; }
        public int Diem {  get; set; }
    }
}
