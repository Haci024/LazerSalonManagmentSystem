using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
  
    [Area("Support")]
    [Authorize(Roles ="SuperSupporter,Supporter")]
    public class StatisticsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
