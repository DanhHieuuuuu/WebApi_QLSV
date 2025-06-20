﻿using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpPost("/Add-teacher-to-mon-hoc")]
        public IActionResult AddTeachertoMonHoc( List<string> listTeacher, string maMonHoc)
        {
            try
            {
                _service.AddTeachertoMonHoc(listTeacher, maMonHoc);
                return Ok("Thêm thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/get-teacher-by-id")]
        public IActionResult GetAllTeacherById([FromQuery] FilterDtos input, List<string> teacherId)
        {
            try
            {
                return Ok(_service.GetTeacherById(input, teacherId));
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

        [HttpGet("/Get-all-teacher-moi-bo-mon")]
        public IActionResult GetAllTeacherPerBoMon([FromQuery] FilterDtos inpu4)
        {
            try
            {
                return Ok(_service.GetAllTeacherPerBoMon(inpu4));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-lop-chu-nhiem")]
        public IActionResult GetLopChuNhiem([FromQuery] string teacherId)
        {
            try
            {
                return Ok(_service.GetStudentInLopQl(teacherId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-all-teacher-in-bo-mon")]
        public IActionResult GetAllTeacherInBoMon(string? boMonId)
        {
            try
            {
                return Ok(_service.GetAllTeacherInBoMon(boMonId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-all-teacher-in-khoa")]
        public IActionResult getAllTeacherInKhoa(string? khoaId)
        {
            try
            {
                return Ok(_service.GetAllTeacherInKhoa(khoaId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/get-all-teacher-mon-hoc")]
        public IActionResult GetAllTeacherMonHoc([FromQuery] FilterDtos input, string teacherId)
        {
            try
            {
                return Ok(_service.GetAllTeacherMonHoc(input, teacherId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/find-teacher")]
        public IActionResult FindTeacher(string teacherId)
        {
            try
            {
                return Ok(_service.FindTeacherById(teacherId));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager,Teacher" })]
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpDelete("/Delete-teacher")]
        public IActionResult DeleteTeacher([FromQuery] string teacherId)
        {
            try
            {
                _service.DeleteTeacher(teacherId);
                return Ok("Đã xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpDelete("/Delete-teacher-khoi-mon-hoc")]
        public IActionResult DeleteTeacherToMonHoc([FromQuery] string teacherId, string maMonHoc)
        {
            try
            {
                _service.RemoveTeacherToMonHoc(teacherId, maMonHoc);
                return Ok("Đã xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
