using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazerBeautyFullProject.Areas.ArzumMini.ViewComponents
{
    public class ArzumMiniViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ArzumMiniViewComponent(UserManager<AppUser> userManager,AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IViewComponentResult Invoke()
        {

            ViewBag.LazerMasters = _db.LazerMasters.Include(x=>x.LazerMasterFilial).ThenInclude(x=>x.LazerMaster).Where(x=>x.LazerMasterFilial.Any(x=>x.FilialId== 1) && x.IsDeactive==false).ToList();

            return View();
        }
    }
}
