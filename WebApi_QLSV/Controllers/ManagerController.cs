using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Dtos.ManagerFd;
using WebApi_QLSV.Dtos.Student;
using WebApi_QLSV.Dtos.Teacher;
using WebApi_QLSV.Exceptions;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        public readonly IManagerService _managerService;
        private readonly ApplicationDbContext _context;
        public ManagerController(IManagerService managerService, ApplicationDbContext applicationDbContext)
        {
            _managerService = managerService;
            _context = applicationDbContext;
        }

        [HttpPost("/Add-quan-li")]
        public async Task<IActionResult> AddManager2([FromForm] AddManagerDtos2 input5)
        {
            try
            {
                var manager = await _managerService.AddManager2(input5);
                return Ok(manager);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("/update-image-manager")]
        public async Task<IActionResult> UpdateImageManager([FromForm] AddImageDtos input3)
        {
            try
            {
                var findManager =
                    _context.Managers.FirstOrDefault(s => s.ManagerId == input3.Id)
                    ?? throw new UserExceptions("Không tìm thấy quản lí");
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
                    findManager.Image = "/images/" + input3.Image.FileName;
                }
                else
                {
                    throw new UserExceptions("Không có file");
                }
                _context.Managers.Update(findManager);
                _context.SaveChanges();
                return Ok(findManager);
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
