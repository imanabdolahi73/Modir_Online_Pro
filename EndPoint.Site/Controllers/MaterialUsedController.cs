using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModirOnline.Application.Services.Material.Queries;
using ModirOnline.Application.Services.Product.Commands;

namespace EndPoint.Site.Controllers
{
    public class MaterialUsedController : Controller
    {
        private readonly IProductManagmentService _ProductManagmentService;
        private readonly IGetAllMaterialsService _getAllMaterialsService;
        public MaterialUsedController(
            IProductManagmentService materialManagmentService ,
            IGetAllMaterialsService getAllMaterialsService)
        {
            _ProductManagmentService = materialManagmentService;
            _getAllMaterialsService = getAllMaterialsService;
        }

        public IActionResult Index(int ProductID)
        {
            ViewBag.Materials = new SelectList(_getAllMaterialsService.Execute().Data, "Id", "Name");
            ViewBag.ProductID = ProductID;
            
            return View(_ProductManagmentService.Get_MaterialUsed(ProductID));
        }
        [HttpPost]
        public IActionResult Delete(int MaterialUsedID)
        {
            return Json(_ProductManagmentService.Delete_MaterialUsed(MaterialUsedID));
        }

        [HttpPost]
        public IActionResult Edit(Request_Edit_MaterialUsed_Dto request)
        {
            return Json(_ProductManagmentService.Update_MaterialUsed(request));
        }

        [HttpPost]
        public IActionResult Add(Request_Add_MaterialUsed_Dto request)
        {
            return Json(_ProductManagmentService.Add_MaterialUsed(request));
        }
    }
}
