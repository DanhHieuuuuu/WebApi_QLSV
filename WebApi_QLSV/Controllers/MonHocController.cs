using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [HttpPost("/get-mon-hoc-by-Id-2")]
        public IActionResult GetMonHocById2([FromQuery] FilterDtos input, [FromBody] List<string> maMonHocs)
        {
            try
            {
                return Ok(_monHocService.GetMonHocById2(input, maMonHocs));
            }
            catch(Exception ex)
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
                return Ok(_monHocService.GetAllMonTrongBoMon(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Get-all-mon-hoc-chi-tiet")]
        public IActionResult GetAllMonHocChiTiet([FromQuery] FilterDtos input4)
        {
            try
            {
                return Ok(_monHocService.GetMonHocDetail(input4));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Get-mon-hoc-by-Id")]
        public IActionResult GetMonHocById([FromQuery] FilterDtos input5)
        {
            try
            {
                return Ok(_monHocService.GetMonHocById(input5));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("/Get-teacher-phu-trach")]
        public IActionResult GetTeacherPhuTrach([FromQuery] FilterDtos input4, string maMonHoc)
        {
            try
            {
                return Ok(_monHocService.GetTeacherPhuTrach(maMonHoc, input4));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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
