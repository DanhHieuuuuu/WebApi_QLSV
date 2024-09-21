using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.ClassFd
{
    public class AddLopQLDtos
    {
        public string TenLopQL { get; set; }
        public string? NganhId { get; set; }
        public int MaxStudent { get; set; }
        public string? LopTruong { get; set; }
        public string? LopPho { get; set; }
        public string ChuNhiem { get; set; }

    }
}
