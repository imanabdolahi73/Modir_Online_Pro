using Microsoft.AspNetCore.Mvc;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Common;
using ModirOnline.Domain.Entities;

namespace EndPoint.Site.Controllers
{
    public class MaterialCategoryController : Controller
    {
        private readonly IMaterialManagmentService _materialManagmentService;
        public MaterialCategoryController(IMaterialManagmentService materialManagmentService)
        {
            _materialManagmentService = materialManagmentService;
        }

        public IActionResult Index()
        {
            return View(_materialManagmentService.Get_MaterialCategories());
        }
        [HttpPost]
        public IActionResult Delete(int MaterialCategoryID)
        {
            return Json(_materialManagmentService.Delete_MaterialCategory(MaterialCategoryID));
        }

        [HttpPost]
        public IActionResult Edit(int CategoryID, string Title)
        {
            return Json(_materialManagmentService.Update_MaterialCategory(CategoryID, Title));
        }

        [HttpPost]
        public IActionResult Add(string Title)
        {
            return Json(_materialManagmentService.Add_MaterialCategory(Title));
        }

    }
}
