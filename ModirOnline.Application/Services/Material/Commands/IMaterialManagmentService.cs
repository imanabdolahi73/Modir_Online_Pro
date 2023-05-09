using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using ModirOnline.Domain.Entities;

namespace ModirOnline.Application.Services.Material.Commands
{
    public interface IMaterialManagmentService
    {
        //Material Category
        ResultDto Add_MaterialCategory(string Title);
        ResultDto Delete_MaterialCategory(int MaterialCategoryID);
        ResultDto Update_MaterialCategory(int MaterialCategoryID , string Title);
        ResultDto<List<MaterialCategory>> Get_MaterialCategories();

        //Inventory
        ResultDto Add_Inventory(string Title , string Address);
        ResultDto Delete_Inventory(int InventoryID);
        ResultDto Update_Inventory(int InventoryID, string Title, string Address);
        ResultDto<List<Inventory>> Get_Inventories();

        //Inventory Amount
        ResultDto Add_InventoryAmount(int InventoryID , int MaterialID , int MaxAmount);
        ResultDto Delete_InventoryAmount(int InventoryAmountID);
        ResultDto Update_MaxAmount(int InventoryAmountID , int MaxAmount);

        //Material
        ResultDto Add_Material(Request_Add_Material_Dto request);
        ResultDto Change_MaterialVisible(int MaterialID);
        ResultDto Delete_Material(int MaterialID);
        ResultDto Update_Material(int MaterialID , string Title);
        ResultDto<List<Domain.Entities.Material>> Get_Materials(int? MaterialCategoryID);

    }
    public class MaterialManagmentService : IMaterialManagmentService
    {
        //Injection
        private readonly IModirOnlineDbContext _Context;
        public MaterialManagmentService(IModirOnlineDbContext context)
        {
            _Context = context;
        }


        //Material Category
        public ResultDto Add_MaterialCategory(string Title)
        {
            try
            {
                MaterialCategory myCategory = new MaterialCategory
                {
                    Title = Title
                };

                _Context.MaterialCategories.Add(myCategory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_MaterialCategory)
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

        public ResultDto Delete_MaterialCategory(int MaterialCategoryID)
        {
            try
            {
                MaterialCategory myCategory = _Context.MaterialCategories
                    .Where(p => p.MaterialCategoryId == MaterialCategoryID)
                    .First();

                _Context.MaterialCategories.Remove(myCategory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_MaterialCategory)
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

        public ResultDto Update_MaterialCategory(int MaterialCategoryID , string Title)
        {
            try
            {
                MaterialCategory myCategory = _Context.MaterialCategories
                    .Where(p => p.MaterialCategoryId == MaterialCategoryID)
                    .First();

                myCategory.Title = Title;
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_MaterialCategory)
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

        public ResultDto<List<MaterialCategory>> Get_MaterialCategories()
        {
            try
            {
                if (_Context.MaterialCategories.Count() == 0)
                {
                    return new ResultDto<List<MaterialCategory>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    List<MaterialCategory> myCategories = _Context.MaterialCategories.ToList();

                    return new ResultDto<List<MaterialCategory>>
                    {
                        Data = myCategories,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<MaterialCategory>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }
        }


        //Inventory
        public ResultDto Add_Inventory(string Title , string Address)
        {
            try
            {
                Inventory myInventory = new Inventory
                {
                    Title = Title,
                    Address = Address
                };

                _Context.Inventories.Add(myInventory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_Inventory)
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

        public ResultDto Delete_Inventory(int InventoryID)
        {
            try
            {
                Inventory myInventory = _Context.Inventories
                    .Where(p => p.InventoryId == InventoryID)
                    .First();

                _Context.Inventories.Remove(myInventory);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_Inventory)
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

        public ResultDto Update_Inventory(int InventoryID, string Title, string Address)
        {
            try
            {
                Inventory myInventory = _Context.Inventories
                    .Where(p => p.InventoryId == InventoryID)
                    .First();

                myInventory.Title = Title;
                myInventory.Address = Address;
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Inventory)
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

        public ResultDto<List<Inventory>> Get_Inventories()
        {
            try
            {
                if (_Context.Inventories.Count() == 0)
                {
                    return new ResultDto<List<Inventory>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    List<Inventory> myInventories = _Context.Inventories.ToList();

                    return new ResultDto<List<Inventory>>
                    {
                        Data = myInventories,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<Inventory>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }
        }


        //Inventory Amount
        public ResultDto Add_InventoryAmount(int InventoryID, int MaterialID, int MaxAmount)
        {
            try
            {
                int Exsit = _Context.InventoryAmounts
                    .Where(p => p.InventoryId == InventoryID && p.MaterialId == MaterialID)
                    .Count();
                if (Exsit == 0)
                {
                    InventoryAmount myInventoryAmount = new InventoryAmount
                    {
                        MaterialId = MaterialID,
                        InventoryId = InventoryID,
                        MaxAmount = MaxAmount,
                        AveragePrice = 0,
                        LastPrice = 0,
                        Amount = 0,
                    };

                    _Context.InventoryAmounts.Add(myInventoryAmount);
                    _Context.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = Alert.GetInsertAlert(Alert.Entity_InventoryAmount)
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "مواد اولیه اولیه در انبار موجود است"
                    };
                }
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

        public ResultDto Delete_InventoryAmount(int InventoryAmountID)
        {
            try
            {
                InventoryAmount myInventoryAmount = _Context.InventoryAmounts
                    .Where(p => p.InventoryAmountId == InventoryAmountID)
                    .First();

                _Context.InventoryAmounts.Remove(myInventoryAmount);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_InventoryAmount)
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

        public ResultDto Update_MaxAmount(int InventoryAmountID , int MaxAmount)
        {
            try
            {
                InventoryAmount myInventoryAmount = _Context.InventoryAmounts
                    .Where(p => p.InventoryAmountId == InventoryAmountID)
                    .First();

                myInventoryAmount.MaxAmount = MaxAmount;

                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_InventoryAmount)
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


        //Material
        public ResultDto Add_Material(Request_Add_Material_Dto request)
        {
            try
            {
                Domain.Entities.Material myMaterial = new Domain.Entities.Material();

                myMaterial.MaterialCategoryId = request.MaterialCategoryID;
                myMaterial.Title = request.Title;
                myMaterial.Unit = request.Unit;
                myMaterial.Visible = 1;

                _Context.Materials.Add(myMaterial);
                _Context.SaveChanges();

                
                Add_InventoryAmount(request.InventoryID, myMaterial.MaterialId, request.MaxAmount);
                
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(Alert.Entity_Material)
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

        public ResultDto Change_MaterialVisible(int MaterialID)
        {
            try
            {
                Domain.Entities.Material myMaterial = _Context.Materials
                    .Where(p => p.MaterialId == MaterialID)
                    .First();

                myMaterial.Visible *= -1;

                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Material)
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

        public ResultDto Delete_Material(int MaterialID)
        {
            try
            {
                Domain.Entities.Material myMaterial = _Context.Materials
                    .Where(p => p.MaterialId == MaterialID)
                    .First();

                List<InventoryAmount> inventories = _Context.InventoryAmounts
                    .Where(p => p.MaterialId == MaterialID)
                    .ToList();

                foreach (InventoryAmount item in inventories)
                {
                    _Context.InventoryAmounts.Remove(item);
                    _Context.SaveChanges();
                }

                _Context.Materials.Remove(myMaterial);
                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetDeleteAlert(Alert.Entity_Material)
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
        public ResultDto Update_Material(int MaterialID, string Title)
        {
            try
            {
                Domain.Entities.Material myMaterial = _Context.Materials
                    .Where(p => p.MaterialId == MaterialID)
                    .First();

                myMaterial.Title = Title;

                _Context.SaveChanges();
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetUpdateAlert(Alert.Entity_Material)
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

        public ResultDto<List<Domain.Entities.Material>> Get_Materials(int? MaterialCategoryID)
        {
            List<Domain.Entities.Material> Materials;
            if (MaterialCategoryID is null)
            {
                Materials = _Context.Materials
                .Include(p => p.MaterialCategory)
                .ToList();

            }
            else
            {
                Materials = _Context.Materials
                .Where(p => p.MaterialCategoryId == MaterialCategoryID)
                .Include(p => p.MaterialCategory)
                .ToList();

            }

            try
            {
                if (Materials.Count() == 0)
                {
                    return new ResultDto<List<Domain.Entities.Material>>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = Alert.NotFound

                    };
                }
                else
                {
                    return new ResultDto<List<Domain.Entities.Material>>
                    {
                        Data = Materials,
                        IsSuccess = true,
                        Message = Alert.Success

                    };
                }

            }
            catch
            {
                return new ResultDto<List<Domain.Entities.Material>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = Alert.ServerException

                };
            }

        }

    }

    //Requset For Add Material
    public class Request_Add_Material_Dto
    {
        public int MaterialCategoryID { get; set; }
        public string Title { get; set; }
        public string Unit { get; set; }
        public int InventoryID { get; set; }

        public int MaxAmount { get; set; }

    }
    
}
