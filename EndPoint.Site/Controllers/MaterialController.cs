using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModirOnline.Application.Services.Material.Commands;
using ModirOnline.Application.Services.Material.Queries;
using ModirOnline.Common;
using ModirOnline.Domain.Entities;
using ModirOnline.Persistence.Contexts;
using System.Collections.Generic;

namespace EndPoint.Site.Controllers
{
    public class MaterialController : Controller
    {
        private readonly IMaterialManagmentService _materialManagmentService;
        private readonly IGetAllMaterialCategoriesService _getAllMaterialCategoriesService;
        private readonly IGetAllInventoriesService _getAllInventoriesService;
        public MaterialController( 
            IMaterialManagmentService materialManagmentService , 
            IGetAllMaterialCategoriesService getAllMaterialCategoriesService ,
            IGetAllInventoriesService getAllInventoriesService)
        {
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
        public IActionResult Add(Request_Add_Material_Dto request)
        {
            return Json(_materialManagmentService.Add_Material(request));
        }

        [HttpPost]
        public IActionResult ChangeVisible(int MaterialID)
        {
            return Json(_materialManagmentService.Change_MaterialVisible(MaterialID));
        }

        [HttpPost]
        public IActionResult Delete(int MaterialID)
        {
            return Json(_materialManagmentService.Delete_Material(MaterialID));
        }

        [HttpPost]
        public IActionResult Edit(int MaterialID , string Title)
        {
            return Json(_materialManagmentService.Update_Material(MaterialID , Title));
        }

    }
}
