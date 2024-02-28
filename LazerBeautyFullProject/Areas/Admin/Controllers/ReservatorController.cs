using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LazerBeautyFullProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="SuperSupporter,Admin")] 
    public class ReservatorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
