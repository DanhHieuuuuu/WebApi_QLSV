using Microsoft.AspNetCore.Mvc;

namespace WebApi_QLSV.Dtos.KhoaFd
{
    public class AddKhoaDtos
    {
        [FromQuery(Name = "Mã khoa")]
        public string KhoaId { get; set; }
        [FromQuery(Name = "Tên khoa")]
        public string TenKhoa { get; set; }
        [FromQuery(Name = "Ngày thành lập")]
        public DateTime NgayThanhLap {  get; set; } 

        [FromQuery(Name = "Tên trưởng khoa")]
        public string TruongKhoa { get; set; }
        [FromQuery(Name = "Tên phó khoa")]
        public string PhoKhoa { get; set; }
    }
}
