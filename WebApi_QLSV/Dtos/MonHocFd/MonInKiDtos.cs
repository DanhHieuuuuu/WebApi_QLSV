using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Dtos.MonHocFd
{
    public class MonInKiDtos
    {
        public string id { get; set; }
        public List<MonHocDtos> monHocs { get; set; }
        public MonInKiDtos(string kh)
        {
            id = kh;
            monHocs = new List<MonHocDtos>();
        }
    }
}
