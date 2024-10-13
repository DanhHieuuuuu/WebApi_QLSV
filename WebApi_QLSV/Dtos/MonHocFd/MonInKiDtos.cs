using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonInKiDtos
    {
        public string id { get; set; }
        public List<MonHocDtos> MonHocs { get; set; }
        public MonInKiDtos(string kh)
        {
            id = kh;
            MonHocs = new List<MonHocDtos>();
        }
    }
}