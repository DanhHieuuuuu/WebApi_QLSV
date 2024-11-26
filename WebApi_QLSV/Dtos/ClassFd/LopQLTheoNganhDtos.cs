using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.Dtos.ClassFd
{
    public class LopQLTheoNganhDtos
    {
        public string NganhId { get; set; }
        public string TenNganh { get; set; }
        public List<LopQL> lopQLs { get; set; } = new List<LopQL>();
    }
}
