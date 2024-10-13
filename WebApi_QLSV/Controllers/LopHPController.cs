using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LopHPController : ControllerBase
    {
        private readonly ILopHPService _hpService;
        public LopHPController(ILopHPService hpService)
        {
            _hpService = hpService;
        }
        //[HttpPost("/Add-lop-hoc-phan")]
        //public IActionResult AddLopHP(AddLopHPDtos input)
        //{
        //    try
        //    {
        //        return Ok(_hpService.AddLopHP(input));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("/Get-all-lop-hoc-phan")]
        //public IActionResult GetLopHP([FromQuery] FilterDtos input2)
        //{
        //    try
        //    {
        //        return Ok(_hpService.GetAllLopHP(input2));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
