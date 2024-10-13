namespace WebApi_QLSV.Dtos.ManagerFd
{
    public class ResponseLoginManagerDtos
    {
        public string ManagerId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        public string Cccd { get; set; }
        public string UrlImage { get; set; }
        public string Token { get; set; }
    }
}
