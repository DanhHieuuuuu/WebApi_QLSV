using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
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
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("/Login-teacher")]
        public IActionResult LogionTeacher(Login input2)
        {
            try
            {
                return Ok(_service.LoginTeacher(input2));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-teacher")]
        public IActionResult GetAll(FilterDtos input3)
        {
            try
            {
                return Ok(_service.GetAll(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
