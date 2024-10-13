using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhoaController : ControllerBase
    {
        private readonly IKhoaService _khoaService;
        public KhoaController(IKhoaService khoaService)
        {
            _khoaService = khoaService;
        }
        [HttpPost("/Add-khoa")]
        public IActionResult AddKhoa([FromBody] AddKhoaDtos input)
        {
            try
            {
                return Ok(_khoaService.AddKhoa(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-khoa")]
        public IActionResult GetAllKhoa([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_khoaService.GetAllKhoa(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-khoa-chi-tiet")]
        public IActionResult GetAllKhoaDetail([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_khoaService.GetKhoaDetail(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
