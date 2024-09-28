using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.Dtos.BlockFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;

namespace WebApi_QLSV.Services.Interfaces
{
    public interface IBlockService
    {
        Block AddBlock(AddBlockDtos input);
        PageResultDtos<Block> GetAllBoMon([FromQuery] FilterDtos input);
    }
}
