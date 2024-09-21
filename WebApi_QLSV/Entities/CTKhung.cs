using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(CTKhung))]
    public class CTKhung
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string CTKhungId { get; set; }

        public int TongSoTin { get; set; }
    }
}
