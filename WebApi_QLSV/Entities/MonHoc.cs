using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Entities
{
    [Table(nameof(MonHoc))]
    public class MonHoc
    {
        [Key]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string MaMonHoc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TenMon { get; set; }
        public int SoTin {  get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string BoMonId { get; set; }
    }
}
