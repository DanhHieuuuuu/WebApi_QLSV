using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.CTKhungFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CTKhungController : ControllerBase
    {
        private readonly ICTKhungService _ctkhungService;
        public CTKhungController(ICTKhungService ctkhungService)
        {
            _ctkhungService = ctkhungService;
        }
        //[HttpPost("/Add-chuong-trinh-khung")]
        //public IActionResult AddCTKhung(AddCTKhungDtos input)
        //{
        //    try
        //    {
        //        return Ok(_ctkhungService.AddCTKhung(input));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("/Get-all-chuong-trinh-khung")]
        //public IActionResult GetAllCTKhung([FromQuery] FilterDtos input2)
        //{
        //    try
        //    {
        //        return Ok(_ctkhungService.GetAllCTKhung(input2));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("/Get-all-mon-hoc-chuong-trinh-khung")]
        //public IActionResult GetAllMonHocInCTK([FromQuery] FilterDtos input3)
        //{
        //    try
        //    {
        //        return Ok(_ctkhungService.GetAllMonHocInCTK(input3));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("/get-all-mon-hoc-trong-ki")]
        //public IActionResult GetAllMonHocInKi( string nganhid, string studentId)
        //{
        //    try
        //    {
        //        return Ok(_ctkhungService.GetAllMonHocInKi(nganhid, studentId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
    }
}
