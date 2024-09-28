using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        public StudentController(IStudentServices userServices)
        {
            _studentServices = userServices;
        }
        [HttpPost("/Add-student")]
        public IActionResult AddStudent(AddStudentDtos input)
        {
            try
            {
                return Ok(_studentServices.AddStudent(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Login")]
        public IActionResult LoginUser(Login input1)
        {
            try
            {
                return Ok(_studentServices.LoginStudent(input1));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-student-2")]
        public IActionResult GetAllStudent2([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_studentServices.GetAllStudent(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
