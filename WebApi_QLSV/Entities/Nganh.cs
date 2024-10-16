using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(Nganh))]
    public class Nganh
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string NganhId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? TenNganh { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public DateTime NgayThanhLap { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TruongNganh { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string PhoNganh { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string KhoaId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public int SumClass { get; set; }
    }
}
