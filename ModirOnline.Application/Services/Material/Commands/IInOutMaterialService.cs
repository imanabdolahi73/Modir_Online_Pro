using ModirOnline.Application.Interfaces.Contexts;
using ModirOnline.Common;
using ModirOnline.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModirOnline.Application.Services.Material.Commands
{
    public interface IInOutMaterialService
    {
        ResultDto Add(Request_InOut_Material_Dto request);
    }
    public class InOutMaterialService : IInOutMaterialService
    {
        private readonly IModirOnlineDbContext _context;
        private readonly IMaterialManagmentService _materialManagmentService;
        public InOutMaterialService(IModirOnlineDbContext context , IMaterialManagmentService materialManagmentService) 
        {
            _context = context;
            _materialManagmentService = materialManagmentService;
        }

        public ResultDto Add(Request_InOut_Material_Dto request)
        {
            try
            {
                InOutMaterial myInOut = new InOutMaterial();
                if (request.Type == "خروج")
                {
                    InventoryAmount inventoryAmount;

                    inventoryAmount = _context.InventoryAmounts
                            .Where(p => p.InventoryId == request.InventoryID && p.MaterialId == request.MaterialID)
                            .First();

                    if (inventoryAmount == null)
                    {
                        return new ResultDto
                        {
                            IsSuccess = false,
                            Message = Alert.NotFoundInventoryAmount,
                        };
                    }
                    else
                    {
                        myInOut.Price = inventoryAmount.AveragePrice;
                    }
                }
                else
                {
                    myInOut.Price = request.Price;
                }

                myInOut.Amount = request.Amount;
                myInOut.EmployeeId = request.EmployeeID;
                myInOut.MaterialId = request.MaterialID;
                myInOut.InventoryId = request.InventoryID;
                myInOut.Title = Get_InOutTitle(request);
                myInOut.Type = request.Type;
                myInOut.DateTime = DateTime.Now;

                _context.InOutMaterials.Add(myInOut);
                _context.SaveChanges();

                Update_InventoryAmount(request);

                return new ResultDto
                {
                    IsSuccess = true,
                    Message = Alert.GetInsertAlert(myInOut.Title),
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

        private string Get_InOutTitle(Request_InOut_Material_Dto request)
        {
            Inventory myInventory = _context.Inventories
                .Where(p => p.InventoryId == request.InventoryID)
                .First();
            if (request.Type == "ورود")
            {
                return request.Type + " به " + myInventory.Title + " (" + request.Title + ")";
            }
            else
            {
                return request.Type + " از " + myInventory.Title + " (" + request.Title + ")";
            }
            
        }

        private void Update_InventoryAmount(Request_InOut_Material_Dto request)
        {
            InventoryAmount inventoryAmount;
            
            inventoryAmount = _context.InventoryAmounts
                    .Where(p => p.InventoryId == request.InventoryID && p.MaterialId == request.MaterialID)
                    .First();
            
            if (inventoryAmount == null)
            {
                _materialManagmentService.Add_InventoryAmount(request.InventoryID, request.MaterialID, request.Amount);

                inventoryAmount = _context.InventoryAmounts
                    .Where(p => p.InventoryId == request.InventoryID && p.MaterialId == request.MaterialID)
                    .First();
            }
            if (request.Type == "ورود")
            {
                int AveragePrice = ((int)(inventoryAmount.Amount * inventoryAmount.AveragePrice) + (request.Amount * request.Price)) / ((int)inventoryAmount.Amount + request.Amount);

                inventoryAmount.LastPrice = request.Price;
                inventoryAmount.AveragePrice = AveragePrice;
                inventoryAmount.Amount += request.Amount;

                if (inventoryAmount.MaxAmount < inventoryAmount.Amount)
                {
                    inventoryAmount.MaxAmount = inventoryAmount.Amount;
                }
            }
            else
            {
                inventoryAmount.Amount -= request.Amount;
            }

            _context.SaveChanges();
        }
    }

    public class Request_InOut_Material_Dto
    {
        public int MaterialID { get; set; }
        public int InventoryID { get; set; }
        public int Amount { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public int EmployeeID { get; set; }
        public int Price { get; set; }

    }
}
