using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.ClassFd
{
    public class UpdateLopQLDtos
    {
        public string LopQLId { get; set; }

        public string TenLopQL { get; set; }

        public string? NganhId { get; set; }

        public string? LopTruongId { get; set; }

        public string? LopPhoId { get; set; }

        public string TeacherId { get; set; }
    }
}
