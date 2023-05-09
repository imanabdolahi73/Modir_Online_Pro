
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Application.Services.Material.Queries;
using ModirOnline.Application.Services.Product.Commands;

namespace EndPoint.Site.Controllers
{
    public class InOutMaterialController : Controller
    {
        private readonly IInOutMaterialService _inOutMaterialService;
        private readonly IMaterialManagmentService _materialManagmentService;
        private readonly IGetAllMaterialCategoriesService _getAllMaterialCategoriesService;
        private readonly IGetAllInventoriesService _getAllInventoriesService;
        public InOutMaterialController(
            IInOutMaterialService inOutMaterialService,
            IMaterialManagmentService materialManagmentService,
            IGetAllMaterialCategoriesService getAllMaterialCategoriesService,
            IGetAllInventoriesService getAllInventoriesService)
        {
            _inOutMaterialService = inOutMaterialService;
            _materialManagmentService = materialManagmentService;
            _getAllMaterialCategoriesService = getAllMaterialCategoriesService;
            _getAllInventoriesService = getAllInventoriesService;
        }
        public IActionResult Index(int? MaterialCategoryID)
        {
            ViewBag.Categories = new SelectList(_getAllMaterialCategoriesService.Execute().Data, "Id", "Name");
            ViewBag.Inventories = new SelectList(_getAllInventoriesService.Execute().Data, "Id", "Name");

            return View(_materialManagmentService.Get_Materials(MaterialCategoryID));
        }

        [HttpPost]
        public IActionResult Add(Request_InOut_Material_Dto request)
        {
            request.EmployeeID = 1;
            return Json(_inOutMaterialService.Add(request));
        }


    }
}
