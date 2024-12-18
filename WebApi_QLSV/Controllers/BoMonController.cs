using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.BoMonFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoMonController : ControllerBase
    {
        private readonly IBoMonService _bomonService;

        public BoMonController(IBoMonService bomonService)
        {
            _bomonService = bomonService;
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("/Add-bo-mon")]
        public IActionResult AddBoMon(AddBoMonDtos input)
        {
            try
            {
                return Ok(_bomonService.AddBoMon(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("/Get-all-bo-mon")]
        public IActionResult GetAllBoMon([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_bomonService.GetAllBoMon(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("/Get-all-bo-mon-theo-khoa")]
        public IActionResult GetAllBoMonTheoKhoa([FromQuery] string boMonId)
        {
            try
            {
                return Ok(_bomonService.GetBoMonTheoKhoa(boMonId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("/Update-bo-mon")]
        public IActionResult UpdateBoMon(UpdateBoMonDtos input)
        {
            try
            {
                return Ok(_bomonService.UpdateBoMon(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("/Delete-bo-mon")]
        public IActionResult DeleteBoMon([FromQuery] string BoMonId)
        {
            try
            {
                _bomonService.DeleteBoMon(BoMonId);
                return Ok("Đã xóa bộ môn");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
