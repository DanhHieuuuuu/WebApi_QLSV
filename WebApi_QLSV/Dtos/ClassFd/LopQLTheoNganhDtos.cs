using WebApi_QLSV.Entities.ClassFd;

namespace WebApi_QLSV.Dtos.ClassFd
{
    public class LopQLTheoNganhDtos
    {
        public string Nganhs { get; set; }
        public List<LopQL> lopQLs { get; set; } = new List<LopQL>();
    }
}
