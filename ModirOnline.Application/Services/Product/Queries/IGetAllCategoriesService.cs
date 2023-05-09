using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Product.Queries
{
    
    public interface IGetAllCategoriesService
    {
        ResultDto<List<AllCategoriesDto>> Execute();
    }


    public class GetAllCategoriesService : IGetAllCategoriesService
    {
        private readonly IModirOnlineDbContext _context;

        public GetAllCategoriesService(IModirOnlineDbContext context)
        {
            _context = context;
        }

        public ResultDto<List<AllCategoriesDto>> Execute()
        {
            var categories = _context
                .Categories
                .ToList()
                .Select(p => new AllCategoriesDto
                {
                    Id = p.CategoryId,
                    Name = p.Title,
                }
                ).ToList();

            return new ResultDto<List<AllCategoriesDto>>
            {
                Data = categories,
                IsSuccess = false,
                Message = "",
            };
        }
    }

    public class AllCategoriesDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

}
