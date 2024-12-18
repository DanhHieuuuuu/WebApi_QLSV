
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentServices _studentServices;
        private readonly ApplicationDbContext _context;

        public StudentController(IStudentServices userServices, ApplicationDbContext context)
        {
            _studentServices = userServices;
            _context = context;
        }


        //[Authorize(Roles = "Manager")]
        [HttpPost("/Add-student")]
        public async Task<IActionResult> AddStudent2([FromForm] AddStudentDtos2 input3)
        {
            try
            {
                var student = await _studentServices.AddStudent2(input3);
                return Ok(student);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Login-student")]
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

        [HttpPost("/login")]
        public IActionResult Login(Login input1)
        {
            try
            {
                return Ok(_studentServices.Login(input1));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/change-password")]
        public IActionResult ChangePassword([FromBody] Login input2, string role)
        {
            try
            {
                _studentServices.ChangePassword(input2.Email, input2.Password, role);
                return Ok("Thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/get-student-by-id")]
        public IActionResult GetAllStudentById([FromQuery] FilterDtos input, List<string> studentId)
        {
            try
            {
                return Ok(_studentServices.GetAllStudentById(input, studentId));
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

        [HttpGet("/Get-all-student-and-find-by-name")]
        public IActionResult GetAllStudentAndFindByNames([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_studentServices.GetAllAndFindStudentByName(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/Get-all-student-3")]
        public IActionResult GetAllStudentInClass([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_studentServices.GetAllStudentInClass(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpGet("/Get-diem-mon-hoc")]
        //public IActionResult GetDiemMonHoc(string studentId, string nganhId)
        //{
        //    try
        //    {
        //        return Ok(_studentServices.DiemMonHoc(studentId, nganhId));
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        //[HttpGet("/Get-bang-diem")]
        //public IActionResult GetBangDiem(string studentId)
        //{
        //    try
        //    {
        //        return Ok(_studentServices.GetBangDiem(studentId));
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [Authorize]
        [HttpPut("/Update-student")]
        public async Task<IActionResult> UpdateStudent([FromQuery] string studentId ,[FromForm] UpdateStudentDtos input)
        {
            try
            {
                var updateStu = await _studentServices.UpdateStudent(studentId, input);
                return Ok(updateStu);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("/Update-image-student")]
        public async Task<IActionResult> UpdateImageStudent([FromForm] AddImageDtos input3)
        {
            try
            {
                var findStudent =
                    _context.Students.FirstOrDefault(s => s.StudentId == input3.Id)
                    ?? throw new UserExceptions("Không tìm thấy sinh viên");
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
                    findStudent.Image = "/images/" + input3.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
                _context.Students.Update(findStudent);
                _context.SaveChanges();
                return Ok(findStudent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpDelete("/Delete-student")]
        public IActionResult DeleteStudent([FromQuery] string studentId)
        {
            try
            {
                _studentServices.DeleteStudent(studentId);
                return Ok("Đã xóa sinh viên");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
