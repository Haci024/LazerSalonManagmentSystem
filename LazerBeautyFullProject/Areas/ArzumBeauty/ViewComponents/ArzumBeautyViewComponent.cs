using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LazerBeautyFullProject.Areas.ArzumBeauty.ViewComponents
{
    public class ArzumBeautyViewComponent: ViewComponent
    {
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public ArzumBeautyViewComponent(UserManager<AppUser> userManager,AppDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }
        public IViewComponentResult Invoke()
        {
            ViewBag.LazerMasters=_db.LazerMasters.Include(x=>x.LazerMasterFilial).ThenInclude(x=>x.Filial).Where(x=>x.LazerMasterFilial.Any(x=>x.FilialId==2) && x.IsDeactive==false).ToList();
            ViewBag.BodyShapingMasters = _db.BodyShapingMasters.Where(x => x.FilialId == 2 && x.IsDeactive == false).ToList();
            ViewBag.Cosmetologs = _db.Cosmetologs.Where(x => x.CosmetologsFilial.Any(x=>x.FilialId == 2)&& x.IsDeactive == false).ToList();


            return View();
        }
    }
}
