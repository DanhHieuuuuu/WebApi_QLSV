using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.BlockFd
{
    public class AddBlockDtos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string BlockId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string TenBlock { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string KiHocId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string NamHoc { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
    }
}
