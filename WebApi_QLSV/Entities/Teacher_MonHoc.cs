using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(Teacher_MonHoc))]

    public class Teacher_MonHoc
    {
        public string TeacherId { get; set; }
        public string MaMonHoc { get; set; }

    }
}
