using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.BoMonFd
{
    public class BoMonTheoKhoaDtos
    {
        public string KhoaId { get; set; }
        public List<BoMon> BoMons { get; set; }
    }
}
