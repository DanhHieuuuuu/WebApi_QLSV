namespace WebApi_QLSV.Dtos.Teacher
{
    public class ResponseLoginTeacherDtos
    {
        public string TeacherId { get; set; }
        public string TenGiangVien { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        public string Cccd { get; set; }
        public string TenBoMon { get; set; }
        public string BoMonId { get; set; }
        public string QueQuan { get; set; }
        public string UrlImage { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
    }
}
