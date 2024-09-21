using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers.Nganh
{
    [Route("api/[controller]")]
    [ApiController]
    public class NganhController : ControllerBase
    {
        private readonly INganhService _nganhService;
        public NganhController(INganhService nganhService)
        {
            _nganhService = nganhService;
        }
        [HttpPost("/Add-nganh")]
        public IActionResult AddNganh(AddNganhDtos input)
        {
            try
            {
                return Ok(_nganhService.AddNganh(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-nganh")]
        public IActionResult GetAllNganh([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_nganhService.GetAllNganh(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
