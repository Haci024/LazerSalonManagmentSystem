using Microsoft.AspNetCore.Mvc;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    public class LazerMasterController : Controller
    {
        [HttpGet]   
        public IActionResult LazeroloqList()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddLazeroloq() {

        
            return View();
        }
        [HttpPost]
        public IActionResult AddLazeroloq(int Id)
        {
         
            return  View();
        }
        [HttpGet]
        public IActionResult EditLazeroloq()
        {
         
            return View();
        }
        [HttpPost]
        public IActionResult EditLazerolog(int Id)
        {

            return View();
        }
        [HttpGet]
        public IActionResult DeleteLazeroloq(int id)
        {
         
            return View();
        }
        
    }
}
