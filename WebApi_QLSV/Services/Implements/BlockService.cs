using Microsoft.AspNetCore.Mvc;
using WebApi_QLSV.DbContexts;
using WebApi_QLSV.Dtos.BlockFd;
using WebApi_QLSV.Dtos.Common;
using WebApi_QLSV.Entities;
using WebApi_QLSV.Services.Interfaces;

namespace WebApi_QLSV.Services.Implements
{
    public class BlockService : IBlockService
    {
        private readonly ApplicationDbContext _context;

        public BlockService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Block AddBlock(AddBlockDtos input)
        {
            var block = new Block
            {
                BlockId = input.BlockId,
                TenBlock = input.TenBlock,
                KiHocId = input.KiHocId,
                NamHoc = input.NamHoc,
                KetThuc = input.KetThuc,
                BatDau = input.BatDau,
            };
            _context.Blocks.Add(block);
            _context.SaveChanges();
            return block;
        }
        public PageResultDtos<Block> GetAllBoMon([FromQuery] FilterDtos input)
        {
            var result = new PageResultDtos<Block>();

            var query = _context.Blocks.Where(e =>
                string.IsNullOrEmpty(input.KeyWord)
                || e.TenBlock.ToLower().Contains(input.KeyWord.ToLower())
            );
            result.TotalItem = query.Count();

            query = query.OrderBy(e => e.NamHoc).ThenBy( e => e.KiHocId).Skip(input.Skip()).Take(input.PageSize);

            result.Items = query.ToList();

            return result;
        }
    }
}
