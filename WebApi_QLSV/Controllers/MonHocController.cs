using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.MonHocFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonHocController : ControllerBase
    {
        private readonly IMonHocService _monHocService;
        public MonHocController(IMonHocService monHocService)
        {
            _monHocService = monHocService;
        }
        [HttpPost("/Add-Mon-Hoc")]
        public IActionResult AddMonHoc(AddMonHocDtos input)
        {
            try
            {
                return Ok(_monHocService.AddMonHoc(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-mon-hoc")]
        public IActionResult GetMonHoc([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_monHocService.GetAllMonHoc(input3));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-Mon-Hoc-Theo-Bo-Mon")]
        public IActionResult GetAllMonHocTrongBoMon([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_monHocService.GetAllMonTrongNganh(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/Update-mon-hoc")]
        public IActionResult UpdateMonHoc(UpdateMonHoc input)
        {
            try
            {
                return Ok(_monHocService.UpdateMonHoc(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("/Delete-mon-hoc")]
        public IActionResult DeleteMonHoc([FromQuery] string maMonHoc)
        {
            try
            {
                _monHocService.DeleteMonHoc(maMonHoc);
                return Ok("Đã xóa thành công");
            }
            catch( Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
