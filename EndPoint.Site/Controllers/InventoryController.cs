using Microsoft.AspNetCore.Mvc;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Domain.Entities;

namespace EndPoint.Site.Controllers
{
    public class InventoryController : Controller
    {
        private readonly IMaterialManagmentService _materialManagmentService;
        public InventoryController(IMaterialManagmentService materialManagmentService)
        {
            _materialManagmentService = materialManagmentService;
        }

        public IActionResult Index()
        {
            return View(_materialManagmentService.Get_Inventories());
        }
        [HttpPost]
        public IActionResult Delete(int InventoryID)
        {
            return Json(_materialManagmentService.Delete_Inventory(InventoryID));
        }

        [HttpPost]
        public IActionResult Edit(int InventoryID, string Title , string Address)
        {
            return Json(_materialManagmentService.Update_Inventory(InventoryID, Title , Address));
        }

        [HttpPost]
        public IActionResult Add(string Title , string Address)
        {
            return Json(_materialManagmentService.Add_Inventory(Title , Address ));
        }
    }
}
