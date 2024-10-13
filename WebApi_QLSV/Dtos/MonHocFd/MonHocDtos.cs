using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonHocDtos
    {
        public string kiHoc { get; set; }
        public string MonId { get; set; }
        public string TenMon { get; set; }
        public int Sotin { get; set; }
        public double? DiemQT { get; set; }
        public double? DiemKT { get; set; }
        public double? DiemMH { get; set; }
    }
}
