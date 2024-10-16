using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(Teacher))]
    public class Teacher
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TeacherId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TenGiangVien { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string Cccd { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string Password { get; set; }

        [
            Required(AllowEmptyStrings = false, ErrorMessage = "Vui lòng nhập email"),
            EmailAddress(ErrorMessage = "Email không đúng định dạng")
        ]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string QueQuan { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string BoMonId { get; set; }
        public string? Image { get; set; }

    }
}
