using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.CauHoiFd
{
    public class AddCauHoiDtos
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string CauHoiId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public string NoiDungCauHoi { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public bool Role { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Không được bỏ trống")]
        public int MaxDiem { get; set; }
    }
}
