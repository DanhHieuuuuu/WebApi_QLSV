using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _service;
        private readonly ApplicationDbContext _context;

        public TeacherController(ITeacherService service, ApplicationDbContext applicationDbContext)
        {
            _service = service;
            _context = applicationDbContext;
        }

        [HttpPost("/Add-teacher")]
        public async Task<IActionResult> AddTeacher2([FromForm] AddTeacherDtos2 input4)
        {
            try
            {
                var teacher = await _service.AddTeacher2(input4);
                return Ok(teacher);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("/update-image-teacher")]
        public async Task<IActionResult> UpdateImageTeacher([FromForm] AddImageDtos input3)
        {
            try
            {
                var findTeacher =
                    _context.Teachers.FirstOrDefault(s => s.TeacherId == input3.Id)
                    ?? throw new UserExceptions("Không tìm thấy giảng viên");
                if (input3.Image.Length > 0)
                {
                    var path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "Images",
                        input3.Image.FileName
                    );
                    using (var stream = System.IO.File.Create(path))
                    {
                        await input3.Image.CopyToAsync(stream);
                    }
                    findTeacher.Image = "/images/" + input3.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
                _context.Teachers.Update(findTeacher);
                _context.SaveChanges();
                return Ok(findTeacher);
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
            catch (Exception ex)
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

        [HttpGet("/Get-all-teacher-in-bo-mon")]
        public IActionResult GetAllTeacherInBoMon([FromQuery] FilterDtos inpu4)
        {
            try
            {
                return Ok(_service.GetAllTeacherInBoMon(inpu4));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/Update-teacher")]
        public async Task<IActionResult> UpdateTeacher([FromQuery] string teacherId, [FromForm] UpdateTeacherDtos input)
        {
            try
            {
                var teacher = await _service.UpdateTeacher(teacherId, input);
                return Ok(teacher);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
