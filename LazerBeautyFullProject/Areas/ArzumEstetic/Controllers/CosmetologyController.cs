using Microsoft.AspNetCore.Mvc;

namespace LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
    [Area("ArzumEstetic")]
    public class CosmetologyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
