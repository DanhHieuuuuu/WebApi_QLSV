using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Student
{
    public class UpdateStudentDtos
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public string? QueQuan { get; set; }
        public string? Cccd { get; set; }
        public bool? GioiTinh { get; set; }
        public IFormFile? Image { get; set; }
    }
}
