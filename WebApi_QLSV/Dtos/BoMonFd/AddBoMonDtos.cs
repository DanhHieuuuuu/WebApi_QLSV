using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.BoMonFd
{
    public class AddBoMonDtos
    {
        public string BoMonId { get; set; }
        public string TenBoMon { get; set; }
        public string? TruongBoMon { get; set; }
        public string? PhoBoMon { get; set; }
        public string KhoaId { get; set; }
    }
}
