using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.BlockFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlockController : ControllerBase
    {
        private readonly IBlockService _blockService;
        public BlockController(IBlockService blockService)
        {
            _blockService = blockService;
        }
        [HttpPost("/Add-block")]
        public IActionResult AddBlock(AddBlockDtos input)
        {
            try
            {
                return Ok(_blockService.AddBlock(input));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("/Get-all-block")]
        public IActionResult GetAllBlock([FromQuery] FilterDtos input2)
        {
            try
            {
                return Ok(_blockService.GetAllBoMon(input2));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
