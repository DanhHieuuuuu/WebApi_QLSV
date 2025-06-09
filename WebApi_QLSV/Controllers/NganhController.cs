using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.NganhFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpGet("/Get-all-nganh-theo-khoa")]
        public IActionResult GetAllNganhTheoKhoa()
        {
            try
            {
                return Ok(_nganhService.GetNganhTheoKhoa());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpPut("/Update-nganh")]
        public IActionResult UpdateNganh(UpdateNganhDtos input)
        {
            try
            {
                return Ok(_nganhService.UpdateNganh(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpDelete("/Delete-nganh")]
        public IActionResult DeleteNganh([FromQuery] string nganhId)
        {
            try
            {
                _nganhService.DeleteNganh(nganhId);
                return Ok("Xóa thành công ngành");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
