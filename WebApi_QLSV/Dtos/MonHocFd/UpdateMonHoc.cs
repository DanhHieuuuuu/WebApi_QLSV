using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class UpdateMonHoc
    {
        public string MaMonHoc { get; set; }

        public string TenMon { get; set; }

        public int SoTin { get; set; }

        public string BoMonId { get; set; }
    }
}
