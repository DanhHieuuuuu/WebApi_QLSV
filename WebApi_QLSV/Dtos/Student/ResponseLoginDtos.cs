using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApi_QLSV.Dtos.Student
{
    public class ResponseLoginDtos
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Token { get; set; }
        public DateTime Birthday { get; set; }
        public string? QueQuan { get; set; }
        public string TenLopQL { get; set; }

    }
}
