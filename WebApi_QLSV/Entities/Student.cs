using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(Student))]
    public class Student
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string StudentId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? Username { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? Password { get; set; }

        [
            Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email"),
            EmailAddress(ErrorMessage = "Email không đúng định dạng")
        ]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Birthday { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public int NienKhoa { get; set; } = DateTime.Now.Year;

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string LopQLId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? QueQuan { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? Cccd { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public bool? GioiTinh { get; set; }
        public string? Image { get; set; }
        public string Role { get; set; } = "Student";
    }
}
