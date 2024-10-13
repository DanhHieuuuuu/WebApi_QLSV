using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.KhoaFd
{
    public class KhoaDetailsDtos
    {
        public Khoa Khoa { get; set; }
        public List<Nganh> nganhs { get; set; }
        public List<BoMon> boMons { get; set; }

    }
}
