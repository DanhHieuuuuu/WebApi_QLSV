using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.CauHoiFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauHoiController : ControllerBase
    {
        private readonly ICauHoiService _cauHoiService;
        public CauHoiController(ICauHoiService cauHoiService)
        {
            _cauHoiService = cauHoiService;
        }
        [HttpPost("/Add-cau-hoi")]
        public IActionResult AddCauHoi(AddCauHoiDtos input)
        {
            try
            {
                return Ok(_cauHoiService.AddCauHoi(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/get-all-cau-hoi")]
        public IActionResult GetCauHoi([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_cauHoiService.GetAllCauHoi(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
