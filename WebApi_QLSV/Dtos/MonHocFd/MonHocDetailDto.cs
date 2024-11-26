using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonHocDetailDto
    {
        public string MaMonHoc { get; set; }
        public string TenMon { get; set; }
        public int SoTin { get; set; }
        public string BoMonId { get; set; }

        public List<TeacherDtos>? TeacherDtos { get; set; } = new List<TeacherDtos>();
    }
}
