using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.KhoaFd
{
    public class UpdateKhoaDtos
    {
        public string KhoaId { get; set; }

        public string? TenKhoa { get; set; }

        public string? TruongKhoa { get; set; }

        public string? PhoKhoa { get; set; }

        public DateTime? NgayThanhLap { get; set; }
    }
}
