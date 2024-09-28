using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.ClassFd
{
    public class AddLopHPDtos
    {
        public string? LopHPId { get; set; }
        public string TenLopHP { get; set; }
        public int MaxStudent { get; set; }
        public string? TeacherId { get; set; }
        public string? NganhId { get; set; }
        public string BlockId { get; set; }
        public string MonId { get; set; }
        public DateTime BatDau { get; set; }
        public DateTime KetThuc { get; set; }
    }
}
