using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using ModirOnline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Product.Commands
{
    public interface IProductManagmentService
    {
        //Category
        ResultDto Add_Category(string Title);
        ResultDto Delete_Category(int CategoryID);
        ResultDto Update_Category(int CategoryID, string Title);
        ResultDto<List<Category>> Get_Categories();

    }
    public class ProductManagmentService : IProductManagmentService
    {
        //Injection
        private readonly IModirOnlineDbContext _Context;
        public ProductManagmentService(IModirOnlineDbContext context)
        {
            _Context = context;
        }

        //Category
        public ResultDto Add_Category(string Title)
        {
            try
            {
                Category myCategory = new Category
                {
                    Title = Title
                };

                _Context.Categories.Add(myCategory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_Category)
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.ServerException
                };
            }

        }

        public ResultDto Delete_Category(int CategoryID)
        {
            try
            {
                Category myCategory = _Context.Categories
                    .Where(p => p.CategoryId == CategoryID)
                    .First();

                _Context.Categories.Remove(myCategory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_Category)
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.RemoveException
                };
            }

        }

        public ResultDto Update_Category(int CategoryID, string Title)
        {
            try
            {
                Category myCategory = _Context.Categories
                    .Where(p => p.CategoryId == CategoryID)
                    .First();

                myCategory.Title = Title;
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Category)
                };
            }
            catch
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = Alert.NotFound
                };
            }

        }

        public ResultDto<List<Category>> Get_Categories()
        {
            try
            {
                if (_Context.Categories.Count() == 0)
                {
                    return new ResultDto<List<Category>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    List<Category> myCategories = _Context.Categories.ToList();

                    return new ResultDto<List<Category>>
                    {
                        Data = myCategories,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<Category>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }
        }

    }

}
