using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.NganhFd
{
    public class NganhTheoKhoaDots
    {
        public string KhoaId { get; set; }
        public string TenKhoa {  get; set; }
        public List<Nganh>? Nganhs { get; set; } = new List<Nganh>();
    }
}
