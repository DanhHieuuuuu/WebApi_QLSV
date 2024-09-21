using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassStudent;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces.StudentFd;

namespace WebApi_QLSV.Controllers.Class
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassStudentController : ControllerBase
    {
        private readonly IClassStudentService _classStudentService;
        public ClassStudentController(IClassStudentService classStudentService)
        {
            _classStudentService = classStudentService;
        }
        [HttpPost("/Add-student-in-class")]
        public IActionResult AddStudentInClass(AddStudentInClassDtos input)
        {
            try
            {
                return Ok(_classStudentService.AddStudentInClass(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("/Get-all-student-class")]
        public IActionResult GetAllStudentInClass([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_classStudentService.GetAll(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
