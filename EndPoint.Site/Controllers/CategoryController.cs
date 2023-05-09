using Microsoft.AspNetCore.Mvc;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Application.Services.Product.Commands;

namespace EndPoint.Site.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProductManagmentService _materialManagmentService;
        public CategoryController(IProductManagmentService materialManagmentService)
        {
            _materialManagmentService = materialManagmentService;
        }

        public IActionResult Index()
        {
            return View(_materialManagmentService.Get_Categories());
        }
        [HttpPost]
        public IActionResult Delete(int CategoryID)
        {
            return Json(_materialManagmentService.Delete_Category(CategoryID));
        }

        [HttpPost]
        public IActionResult Edit(int CategoryID, string Title)
        {
            return Json(_materialManagmentService.Update_Category(CategoryID, Title));
        }

        [HttpPost]
        public IActionResult Add(string Title)
        {
            return Json(_materialManagmentService.Add_Category(Title));
        }

    }

}
