using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Student
{
    public class AddStudentDtos
    {
        public string? Username { get; set; }

        public DateTime Birthday { get; set; } = DateTime.Now;

        public string TenLopQL { get; set; }

        public string? QueQuan { get; set; }

        public string? Cccd { get; set; }

        public bool? GioiTinh { get; set; }
    }
}
