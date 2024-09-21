using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers.Khoa
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
        public IActionResult AddKhoa([FromQuery] AddKhoaDtos input)
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
    }
}
