using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Student
{
    public class StudentDtos
    {
        public string? StudentId { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }
        public DateTime Birthday { get; set; } = DateTime.Now;

        public string? TenLopQL { get; set; }

        public string? QueQuan { get; set; }
        public int NienKhoa { get; set; }
        public string? Cccd { get; set; }
        public bool? GioiTinh { get; set; }
        public string? Image {  get; set; }
    }
}
