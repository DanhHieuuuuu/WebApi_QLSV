using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.ManagerFd
{
    public class AddManagerDtos
    {  
        public string ManagerId { get; set; }     
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public bool GioiTinh { get; set; }
        public string Cccd { get; set; }
    }
}
