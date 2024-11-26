namespace WebApi_QLSV.Dtos.ManagerFd
{
    public class UpdateManagerDtos
    {
        public string Username { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        public string Cccd { get; set; }
        public string QueQuan { get; set; }
        public IFormFile? Image { get; set; }
    }
}
