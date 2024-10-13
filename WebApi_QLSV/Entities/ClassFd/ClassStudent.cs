using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities.ClassFd
{
    [Table(nameof(ClassStudent))]
    public class ClassStudent
    {
        public string StudentId { get; set; }
        public string LopHPId { get; set; }
        public double? DiemQT {  get; set; }
        public double? DiemKT { get; set; }
        public double? DiemMH {  get; set; }
        public int TienMonHoc { get; set; }
        public bool Nop {  get; set; }
    }
}
