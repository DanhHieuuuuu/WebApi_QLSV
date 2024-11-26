using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonHocTrongBoMonDtos
    {
        public string? BoMonId { get; set; }
        public string? TenBoMon { get; set; }
        public List<MonHoc>? monHocs { get; set; } = new List<MonHoc>();
    }
}
