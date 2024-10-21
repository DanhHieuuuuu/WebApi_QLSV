using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopQLController : ControllerBase
    {
        private readonly ILopQLService _lopQLService;

        public LopQLController(ILopQLService lopQLService)
        {
            _lopQLService = lopQLService;
        }

        [HttpPost("/Add-lop-quan-li")]
        public IActionResult AddLopQL(AddLopQLDtos input)
        {
            try
            {
                return Ok(_lopQLService.AddLopQL(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Get-all-Class")]
        public IActionResult GetAllLopQL([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_lopQLService.GetAllLopQL(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Get-all-lopQL-theo-nganh")]
        public IActionResult GetAllLopQLTheoNganh([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_lopQLService.getAllLopQLTheoNganh(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/Update-lop-quan-li")]
        public IActionResult UpdateLopQL(UpdateLopQLDtos input)
        {
            try
            {
                return Ok(_lopQLService.UpdateLopQL(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/Delete-lop-quan-li")]
        public IActionResult DeleteLopQL([FromQuery] string lopQLId)
        {
            try
            {
                _lopQLService.DeleteLopQL(lopQLId);
                return Ok("Đã xóa lớp quản lí và sinh viên trong lớp đấy");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
