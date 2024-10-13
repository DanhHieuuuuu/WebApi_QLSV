using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities.ClassFd
{
    [Table(nameof(LopQL))]
    public class LopQL
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string LopQLId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string? NganhId { get; set; }

        [Required]
        [MaxLength(100)]
        public int MaxStudent { get; set; }
        public string? LopTruongId { get; set; }
        public string? LopPhoId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TeacherId { get; set; }
    }
}
