using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(Khoa))]
    public class Khoa
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string KhoaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? TenKhoa { get; set; }

        public string? TruongKhoa { get; set; }

        public string? PhoKhoa { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public DateTime? NgayThanhLap { get; set; }
    }
}
