using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers.Teacher
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;
        public TeacherController(ITeacherService service)
        {
            _service = service;
        }
        [HttpPost("/Add-teacher")]
        public IActionResult AddTeacher(AddTeacherDtos input)
        {
            try
            {
                return Ok(_service.AddTeacher(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        [HttpGet("/Get-all-teacher")]
        public IActionResult GetAll(FilterDtos input2) 
        {
            try
            {
                return Ok(_service.GetAll(input2));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        }
        
    }
}
