using Business.IServices;
using Data.Concrete;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LazerBeautyFullProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperSupporter")]
    public class SolariumReportsController : Controller
    {
        private readonly ILazerAppointmentService _lazerAppointmentService;
        private readonly AppDbContext _appDbContext;
        public SolariumReportsController(ILazerAppointmentService lazerAppointmentService, AppDbContext db)
        {
            _lazerAppointmentService = lazerAppointmentService;
            _appDbContext = db;
        }

        [HttpGet]
        public async Task<IActionResult> FilterSolariumReports()
        {
            SelectFilterDTO filters = new SelectFilterDTO();
            filters.SolariumCategories = await _appDbContext.SolariumCategories.Include(x => x.ChildCategories).Include(x => x.MainCategory).Where(x => x.MainCategoryId != null).ToListAsync();

            return View(filters);
        }
        [HttpPost]
        public JsonResult GetSolariumPrice(DateTime startDate, DateTime endDate, int[] solariumCategoryId)
        {
            var CalculateTotalEarnings = CalculatePacketPrice(startDate, endDate, solariumCategoryId);
            var totalSessionCountByCategory = CalculateSessionCount(startDate, endDate, solariumCategoryId);
            var totalEarnings = CalculateAllPacket(startDate, endDate);
            var totalSessionCount = AllSessionCount(startDate, endDate);

            var response = new
            {

                totalEarningsByPacket = CalculateTotalEarnings,
                totalSessionCountByPacket = totalSessionCountByCategory,
                totalEarningsForAll = totalEarnings,
                totalSessionCount = totalSessionCount

            };

            return Json(response);
        }

        public decimal CalculatePacketPrice(DateTime startDate, DateTime endDate, int[] solariumCategoryIds)
        {
            IQueryable<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments
                .Include(x => x.SolariumCategories)
                .Where(x => x.BuyingDate >= startDate
                         && x.BuyingDate <= endDate
                         && solariumCategoryIds.Contains(x.SolariumCategoriesId));

            decimal totalPrice = solariumAppointments.Sum(x => x.Price);
            return totalPrice;
        }
        public int CalculateSessionCount(DateTime startDate, DateTime endDate, int[] solariumCategoryIds)
        {
            IQueryable<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments
                .Include(x => x.SolariumCategories)
                .Where(x => x.BuyingDate >= startDate
                         && x.BuyingDate <= endDate
                         && solariumCategoryIds.Contains(x.SolariumCategoriesId));

            int totalSessionCount = solariumAppointments.Count();
            return totalSessionCount;
        }
        public decimal CalculateAllPacket(DateTime startDate, DateTime endDate)
        {
            IQueryable<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments
               .Include(x => x.SolariumCategories)
               .Where(x => x.BuyingDate >= startDate
                        && x.BuyingDate <= endDate);

            decimal totalPrice = solariumAppointments.Sum(x => x.Price);
            return totalPrice;
        }
        public int AllSessionCount(DateTime startDate, DateTime endDate)
        {
            IQueryable<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments
               .Include(x => x.SolariumCategories)
               .Where(x => x.BuyingDate >= startDate
                        && x.BuyingDate <= endDate);

            int totalSession = solariumAppointments.Count();
            return totalSession;
        }

        [HttpPost]
        public IActionResult SelectSolariumReportsForAll(DateTime startDate, DateTime endDate)
        {
            List<SolariumAppointment> solariumAppointments = SelectSolariumReportsBetweenTwoDate(startDate, endDate);

                return PartialView("~/Areas/Admin/Views/Shared/_SolariumReports.cshtml", solariumAppointments);
            

        }
        [HttpPost]
        public IActionResult SelectSolariumReportsForCategory(DateTime startDate, DateTime endDate, int[] solariumCategoryId)
        {
            List<SolariumAppointment> solariumAppointments = SelectSolariumReportsBetweenTwoDate( startDate,  endDate, solariumCategoryId);

            return PartialView("~/Areas/Admin/Views/Shared/_SolariumReports.cshtml", solariumAppointments);


        }

        public List<SolariumAppointment> SelectSolariumReportsBetweenTwoDate(DateTime startDate,DateTime endDate, int[] solariumCategoryId)
        {
            List<SolariumAppointment> solariumAppointments=_appDbContext.SolariumAppointments.Include(x=>x.SolariumCategories).ThenInclude(x=>x.MainCategory).ThenInclude(x=>x.ChildCategories).Include(x=>x.AppUser).Include(x=>x.Customer).Include(x=>x.SolariumUsingList).Where(x=>x.FilialId==3 && x.BuyingDate>=startDate && x.BuyingDate <= endDate && solariumCategoryId.Contains(x.SolariumCategoriesId)).ToList();
          
            return solariumAppointments;
        }
        public List<SolariumAppointment> SelectSolariumReportsBetweenTwoDate(DateTime startDate, DateTime endDate)
        {
            List<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments.Include(x => x.SolariumCategories).Include(x => x.AppUser).Include(x => x.Customer).Include(x => x.SolariumUsingList).Include(x=>x.SolariumCategories).ThenInclude(x=>x.MainCategory).ThenInclude(x=>x.ChildCategories).Where(x => x.FilialId == 3 && x.BuyingDate >= startDate && x.BuyingDate <= endDate).ToList();

            return solariumAppointments;
        }

        [HttpGet]
        public IActionResult UpgradePriceList()
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Include(x => x.Customers).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x=>x.PriceUpdateDescription!=null).ToList();
           
         
            return View(lazerAppointments);
        }
  
       



    
    }





}





