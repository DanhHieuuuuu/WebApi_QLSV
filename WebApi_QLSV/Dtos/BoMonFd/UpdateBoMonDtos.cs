using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.BoMonFd
{
    public class UpdateBoMonDtos
    {
        public string BoMonId { get; set; }
        public string TenBoMon { get; set; }
        public string? TruongBoMonId { get; set; }
        public DateTime? NgayThanhLap { get; set; }
        public string? PhoBoMonId { get; set; }
        public string? KhoaId { get; set; }
    }
}
