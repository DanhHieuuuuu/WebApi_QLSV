using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class AddMonHocDtos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string MonId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TenMon { get; set; }
        public int Sotin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string KiHoc { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string CTKhungId { get; set; }
    }
}
