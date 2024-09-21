﻿using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.ClassFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers.Class
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassQLController : ControllerBase
    {
        private readonly ILopQLService _lopQLService ;
        public ClassQLController(ILopQLService lopQLService)
        {
            _lopQLService = lopQLService ;
        }
        [HttpPost("/Add-lop-quan-li")]
        public IActionResult AddLopQL(AddLopQLDtos input)
        {
            try
            {
                return Ok(_lopQLService.AddLopQL(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-Class")]
        public IActionResult GetAllLopQL([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_lopQLService.GetAllLopQL(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}