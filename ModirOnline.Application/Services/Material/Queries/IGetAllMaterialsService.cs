using Microsoft.EntityFrameworkCore;
using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Material.Queries
{
    public interface IGetAllMaterialsService
    {
        ResultDto<List<AllMaterialsDto>> Execute();
    }


    public class GetAllMaterialsService : IGetAllMaterialsService
    {
        private readonly IModirOnlineDbContext _context;

        public GetAllMaterialsService(IModirOnlineDbContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllMaterialsDto>> Execute()
        {
            var materials = _context
                .Materials
                .Include(p=> p.MaterialCategory)
                .ToList()
                .OrderBy(p=>p.MaterialCategoryId)
                .Select(p => new AllMaterialsDto
                
                {
                    Id = p.MaterialId,
                    Name = p.MaterialCategory.Title + " " + p.Title,
                }
                ).ToList();

            return new ResultDto<List<AllMaterialsDto>>
            {
                Data = materials,
                IsSuccess = false,
                Message = "",
            };
        }
    }

    public class AllMaterialsDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
