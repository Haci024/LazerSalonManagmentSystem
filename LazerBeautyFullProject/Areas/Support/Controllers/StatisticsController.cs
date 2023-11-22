using Microsoft.AspNetCore.Mvc;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
