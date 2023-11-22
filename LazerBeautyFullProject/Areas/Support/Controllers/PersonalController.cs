using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    public class PersonalController : Controller
    {
        [HttpGet]
        public IActionResult OperatorList()
        {
        
            return View();
        }
        [HttpGet]
        public IActionResult AddOperator()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddOperator(int Id) {
        
        
        return View();
        }
        [HttpGet]
        public IActionResult EditOperator() {
        
        
        return View();
         }
        [HttpPost]
        public IActionResult EditOperator(int Id)
        {
         
            return View();
        }
  

    }
}
