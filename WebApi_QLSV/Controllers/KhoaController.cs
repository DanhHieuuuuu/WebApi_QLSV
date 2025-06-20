﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.KhoaFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpPost("/Add-khoa")]
        public IActionResult AddKhoa([FromBody] AddKhoaDtos input)
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

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
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

        //[Authorize(Roles = "Manager")]
        [HttpGet("/Get-all-khoa-chi-tiet")]
        public IActionResult GetAllKhoaDetail([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_khoaService.GetKhoaDetail(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpPut("/Update-khoa")]
        public IActionResult UpdateKhoa(UpdateKhoaDtos input)
        {
            try
            {
                return Ok(_khoaService.UpdateKhoa(input));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [TypeFilter(typeof(AuthorizationFilter), Arguments = new Object[] { "Manager" })]
        [HttpDelete("/Delete-khoa")]
        public IActionResult DeleteKhoa(string khoaId)
        {
            try
            {
                _khoaService.DeleteKhoa(khoaId);
                return Ok("Đã xóa thành công");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
