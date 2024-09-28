using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("/Add-quan-li")]
        public IActionResult AddManager(AddManagerDtos input)
        {
            try
            {
                return Ok(_managerService.AddManager(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("/Login-manager")]
        public IActionResult LoginManager(Login input2)
        {
            try
            {
                return Ok(_managerService.LoginManager(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("/Get-all-quan-li")]
        public IActionResult GetAllManager([FromQuery] FilterDtos input3)
        {
            try
            {
                return Ok(_managerService.GetAllManager(input3));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
