using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonHocTrongNganhDtos
    {
        public string? MaBoMon { get; set; }
        public string? TenBoMon { get; set; }
        public List<MonHoc>? monHocs { get; set; }
    }
}
