using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Material.Queries
{
    
    public interface IGetAllMaterialCategoriesService
    {
        ResultDto<List<AllMaterialCategoriesDto>> Execute();
    }


    public class GetAllMaterialCategoriesService : IGetAllMaterialCategoriesService
    {
        private readonly IModirOnlineDbContext _context;

        public GetAllMaterialCategoriesService(IModirOnlineDbContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllMaterialCategoriesDto>> Execute()
        {
            var categories = _context
                .MaterialCategories
                .ToList()
                .Select(p => new AllMaterialCategoriesDto
                {
                    Id = p.MaterialCategoryId,
                    Name = p.Title,
                }
                ).ToList();

            return new ResultDto<List<AllMaterialCategoriesDto>>
            {
                Data = categories,
                IsSuccess = false,
                Message = "",
            };
        }
    }

    public class AllMaterialCategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
