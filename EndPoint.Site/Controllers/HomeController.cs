using Microsoft.AspNetCore.Mvc;

namespace EndPoint.Site.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
