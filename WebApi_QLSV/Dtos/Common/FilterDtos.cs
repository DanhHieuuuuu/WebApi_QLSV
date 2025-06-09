using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi_QLSV.Dtos.Common
{
    public class FilterDtos
    {
        /// <summary>
        /// số lượng của 1 trang 
        /// </summary>
        [FromQuery(Name = "PageIndex")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// size của trang 
        /// </summary>
        [FromQuery(Name = "PageSize")]
        public int PageSize { get; set; } = 8;

        private string _keyWord;
        [FromQuery(Name = "KeyWord")]
        public string? KeyWord
        {
            get => _keyWord;
            set => _keyWord = value?.Trim();
        }

        public int Skip()
        {
            return (PageIndex - 1) * PageSize;
        }

    }
}
