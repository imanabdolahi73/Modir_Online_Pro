using Microsoft.EntityFrameworkCore;
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

        //Product
        ResultDto Add_Product(Request_Add_Product_Dto request);
        ResultDto Delete_Product(int ProductID);
        ResultDto Update_Product(Request_Edit_Product_Dto request);
        ResultDto<List<Domain.Entities.Product>> Get_Products(int? CategoryID);
        ResultDto Change_ProductVisible(int ProductID);
        
        //Material Used
        ResultDto Add_MaterialUsed(Request_Add_MaterialUsed_Dto request);
        ResultDto Delete_MaterialUsed(int MaterialUsedID);
        ResultDto Update_MaterialUsed(Request_Edit_MaterialUsed_Dto request);
        ResultDto<List<MaterialUsed>> Get_MaterialUsed(int ProductID);
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

        //Product
        public ResultDto Add_Product(Request_Add_Product_Dto request)
        {
            try
            {
                Domain.Entities.Product myProduct = new Domain.Entities.Product
                {
                    Title = request.Title,
                    Price = request.Price,
                    CategoryId = request.CategoryID,
                    Visible = 1,
                };

                _Context.Products.Add(myProduct);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_Product)
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

        public ResultDto Delete_Product(int ProductID)
        {
            try
            {
                Domain.Entities.Product myProduct = _Context.Products
                    .Where(p => p.ProductId == ProductID)
                    .First();

                List<MaterialUsed> myMaterialUsed = _Context.MaterialUseds
                    .Where(p => p.ProductId == ProductID)
                    .ToList();

                foreach (MaterialUsed item in myMaterialUsed)
                {
                    _Context.MaterialUseds.Remove(item);
                    _Context.SaveChanges();
                }

                _Context.Products.Remove(myProduct);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_Product)
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

        public ResultDto Update_Product(Request_Edit_Product_Dto request)
        {
            try
            {
                Domain.Entities.Product myProduct = _Context.Products
                    .Where(p => p.ProductId == request.ProductID)
                    .First();

                myProduct.Title = request.Title;
                myProduct.Price = request.Price;
                
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Product)
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

        public ResultDto<List<Domain.Entities.Product>> Get_Products(int? CategoryID)
        {
            List<Domain.Entities.Product> Products;
            if (CategoryID is null)
            {
                Products = _Context.Products
                .Include(p => p.Category)
                .ToList();

            }
            else
            {
                Products = _Context.Products
                .Where(p => p.CategoryId == CategoryID)
                .Include(p => p.Category)
                .ToList();

            }

            try
            {
                if (Products.Count() == 0)
                {
                    return new ResultDto<List<Domain.Entities.Product>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    return new ResultDto<List<Domain.Entities.Product>>
                    {
                        Data = Products,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<Domain.Entities.Product>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }

        }

        public ResultDto Change_ProductVisible(int ProductID)
        {
            try
            {
                Domain.Entities.Product myProduct = _Context.Products
                    .Where(p => p.ProductId == ProductID)
                    .First();

                myProduct.Visible *= -1;

                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Product)
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

        //Material Used
        public ResultDto Add_MaterialUsed(Request_Add_MaterialUsed_Dto request) 
        {
            try
            {
                Domain.Entities.Material myMaterial = _Context.Materials
                    .Where(p => p.MaterialId == request.MaterialID)
                    .First();

                MaterialUsed myMaterialUsed = new MaterialUsed
                {
                    MaterialId = request.MaterialID,
                    Title = request.Amount + " " + myMaterial.Unit + " " + myMaterial.Title,
                    Amount = request.Amount,
                    ProductId = request.ProductID,
                };

                _Context.MaterialUseds.Add(myMaterialUsed);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_MaterialUsed)
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

        public ResultDto Delete_MaterialUsed(int MaterialUsedID)
        {
            try
            {
                MaterialUsed myMaterialUsed = _Context.MaterialUseds
                    .Where(p => p.MaterialUsedId == MaterialUsedID)
                    .First();

                _Context.MaterialUseds.Remove(myMaterialUsed);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_MaterialUsed)
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

        public ResultDto Update_MaterialUsed(Request_Edit_MaterialUsed_Dto request)
        {
            try
            {
                
                MaterialUsed myMaterialUsed = _Context.MaterialUseds
                    .Where(p => p.MaterialUsedId == request.MaterialUsedID)
                    .First();

                Domain.Entities.Material myMaterial = _Context.Materials
                    .Where(p => p.MaterialId == myMaterialUsed.MaterialId)
                    .First();

                myMaterialUsed.Amount = request.Amount;
                myMaterialUsed.Title = request.Amount + " " + myMaterial.Unit + " " + myMaterial.Title;

                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_MaterialUsed)
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

        public ResultDto<List<MaterialUsed>> Get_MaterialUsed(int ProductID)
        {
            try
            {
                List<MaterialUsed> myMaterialUsed = _Context.MaterialUseds
                    .Where(p=>p.ProductId == ProductID)
                    .ToList();

                if (myMaterialUsed.Count() == 0)
                {
                    return new ResultDto<List<MaterialUsed>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    
                    return new ResultDto<List<MaterialUsed>>
                    {
                        Data = myMaterialUsed,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<MaterialUsed>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }
        }
    }

    public class Request_Add_Product_Dto
    {
        public string Title { get; set; }

        public int CategoryID { get; set; }

        public long Price { get; set; }
    }

    public class Request_Edit_Product_Dto
    {
        public int ProductID { get; set; }

        public string Title { get; set; }

        public long Price { get; set; }
    }

    public class Request_Add_MaterialUsed_Dto
    {
        public int ProductID { get; set; }
        public int MaterialID { get; set; }
        public int Amount { get; set; }
    }

    public class Request_Edit_MaterialUsed_Dto
    {
        public int MaterialUsedID { get; set; }
        public int Amount { get; set; }
    }
}
