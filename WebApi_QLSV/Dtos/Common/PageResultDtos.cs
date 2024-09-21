namespace WebApi_QLSV.Dtos.Common
{
    public class PageResultDtos<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalItem { get; set; }

    }
}
