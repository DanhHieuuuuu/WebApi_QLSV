using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities.ClassFd
{
    [Table(nameof(LopHP))]
    public class LopHP
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? LopHPId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TenLopHP { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string KiHocNamHoc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        [MaxLength(100)]
        public int MaxStudent { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? TeacherId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string BlockId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string MonId { get; set; }

        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
    }
}
