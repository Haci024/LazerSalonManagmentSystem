using Microsoft.AspNetCore.Mvc;


namespace LazerBeautyFullProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KassaController : Controller
    {   
        
        public IActionResult BudgetPage()
        {
           
            return View();
        }
    }
}
