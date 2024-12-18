using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi_QLSV.Dtos.Student
{
    public class ResponseLoginStudentDtos
    {
        public string StudentId { get; set; }   
        public string LopQLId { get; set; }
        public string TenLopQL {  get; set; }
        public string nganhId {  get; set; }
        public string nganh { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime Birthday { get; set; }
        public string? QueQuan { get; set; }
        public string? Cccd { get; set; }
        public bool? GioiTinh { get; set; }
        public string? UrlImage { get; set; }
        public string? Token { get; set; }
        public string Role { get; set; }

    }
}
