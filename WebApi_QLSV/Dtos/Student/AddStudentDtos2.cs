namespace WebApi_QLSV.Dtos.Student
{
    public class AddStudentDtos2
    {
        public string? Username { get; set; }

        public DateTime Birthday { get; set; } = DateTime.Now;

        public string LopQLId { get; set; }

        public string? QueQuan { get; set; }

        public string? Cccd { get; set; }

        public bool? GioiTinh { get; set; }
        public IFormFile Image { get; set; }

    }
}
