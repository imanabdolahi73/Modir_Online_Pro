using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModirOnline.Application.Services.Product.Commands;
using ModirOnline.Application.Services.Product.Queries;

namespace EndPoint.Site.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductManagmentService _productManagmentService;
        private readonly IGetAllCategoriesService _getAllCategoriesService;
        public ProductController(
            IProductManagmentService productManagmentService,
            IGetAllCategoriesService getAllCategoriesService)
        {
            _productManagmentService = productManagmentService;
            _getAllCategoriesService = getAllCategoriesService;
            
        }

        public IActionResult Index(int? CategoryID)
        {
            ViewBag.Categories = new SelectList(_getAllCategoriesService.Execute().Data, "Id", "Name");
            
            return View(_productManagmentService.Get_Products(CategoryID));
        }

        [HttpPost]
        public IActionResult Add(Request_Add_Product_Dto request)
        {
            return Json(_productManagmentService.Add_Product(request));
        }

        [HttpPost]
        public IActionResult ChangeVisible(int ProductID)
        {
            return Json(_productManagmentService.Change_ProductVisible(ProductID));
        }

        [HttpPost]
        public IActionResult Delete(int ProductID)
        {
            return Json(_productManagmentService.Delete_Product(ProductID));
        }

        [HttpPost]
        public IActionResult Edit(Request_Edit_Product_Dto request)
        {
            return Json(_productManagmentService.Update_Product(request));
        }

    }
}
