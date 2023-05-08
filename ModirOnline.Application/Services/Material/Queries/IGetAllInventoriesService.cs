using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Material.Queries
{
    
    public interface IGetAllInventoriesService
    {
        ResultDto<List<AllInventoriesDto>> Execute();
    }
    public class GetAllInventoriesService : IGetAllInventoriesService
    {
        private readonly IModirOnlineDbContext _context;

        public GetAllInventoriesService(IModirOnlineDbContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllInventoriesDto>> Execute()
        {
            var inventories = _context
                .Inventories
                .ToList()
                .Select(p => new AllInventoriesDto
                {
                    Id = p.InventoryId,
                    Name = p.Title,
                }
                ).ToList();

            return new ResultDto<List<AllInventoriesDto>>
            {
                Data = inventories,
                IsSuccess = false,
                Message = "",
            };
        }
    }

    public class AllInventoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
