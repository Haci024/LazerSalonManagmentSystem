using Business.IServices;
using Data.Concrete;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    [Authorize]
    [Area("ArzumMini")]
    public class ReportsController : Controller
    {
        private readonly ILazerAppointmentService _lazerAppointmentService;
        private readonly AppDbContext _appDbContext;
        public ReportsController(ILazerAppointmentService lazerAppointmentService, AppDbContext db)
        {
            _lazerAppointmentService = lazerAppointmentService;           
            _appDbContext = db;
        }
        [HttpGet]
        public IActionResult LazerReportsEllada()
        {
            ViewBag.Ellada = _appDbContext.LazerMasters.Where(x => x.Id == 1).Select(x => x.FullName).FirstOrDefault();
         
            List<LazerAppointment> lazerAppointments = _lazerAppointmentService.DailyLazerAppointmentReports();
            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult LazerReportsMedine()
        {
          
            ViewBag.Mədinə = _appDbContext.LazerMasters.Where(x => x.Id == 2).Select(x => x.FullName).FirstOrDefault();
            List<LazerAppointment> lazerAppointments = _lazerAppointmentService.DailyLazerAppointmentReports();
            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult FilterLazerReports()
        {
            List<LazerAppointment> lazerAppointments=_appDbContext.LazerAppointments.Include(x=>x.Customers).Include(x=>x.AppUser).Include(x=>x.LazerMaster).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).ToList();
          ViewBag.LazerMasters=_appDbContext.LazerMasters.ToList();
            return View(lazerAppointments);
        }
        
        [HttpPost]
        public IActionResult SelectMasterInfo1(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> appointmentsInDateRangeForMaster1 = SelectDataBetweenTwoDate(1, startDate, endDate);
           
            return PartialView("~/Views/Shared/_LoadLazerMaster1.cshtml", appointmentsInDateRangeForMaster1);
        }
        [HttpPost]
        public IActionResult SelectMasterInfo2(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> appointmentsInDateRangeForMaster2 = SelectDataBetweenTwoDate(2, startDate, endDate);

            return PartialView("~/Views/Shared/_LoadLazerMaster2.cshtml", appointmentsInDateRangeForMaster2);
        }
        [HttpPost]
        public IActionResult SelectInComeMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> incomes = _appDbContext.Incomes.Include(x=>x.AppUser).Where(x => x.IncomeDate >= startDate && x.IncomeDate <= endDate).ToList();

            return PartialView("~/Views/Shared/_IncomeMoneyList.cshtml", incomes);
        }
        [HttpPost]
        public IActionResult SelectOutMoney(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x=>x.AppUser).Where(x=>x.CreateTime >= startDate && x.CreateTime <= endDate).ToList();

            return PartialView("~/Views/Shared/_OutMoneyList.cshtml", outMoney);
        }
        public List<LazerAppointment> SelectDataBetweenTwoDate(int LazerMasterId, DateTime startDate, DateTime endDate)
        {

            List<LazerAppointment> appointmentsInDateRangeForMaster = _appDbContext.LazerAppointments
                .Include(x => x.Customers)
                .Include(x => x.LazerMaster)
                .Include(x => x.LazerAppointmentReports)
                .ThenInclude(x => x.LazerCategory)
                .Include(x=>x.AppUser)
                .Where(x => x.LazerMasterId == LazerMasterId && x.EndTime >= startDate && x.EndTime <= endDate && x.IsCompleted==true).ToList();


            return appointmentsInDateRangeForMaster;
        }
        [HttpPost]
        public JsonResult CalculateTotalPrice(DateTime startDate, DateTime endDate)
        {
            var totalPrice = CalculatePriceBetweenDates(startDate, endDate);
            var totalPriceForMaster1 = CalculateTotalPriceForMaster(1,startDate, endDate);
            var totalPriceForMaster2 = CalculateTotalPriceForMaster(2,startDate, endDate);
            var totalIncomeMoney= CalculatePriceBetweenDatesIncome(startDate, endDate);
            var totalOutMoney=CalculatePriceBetweenDatesOutMoney(startDate, endDate);
            var totalSellingForResepsionist1=CalculatePriceBetweenDatesRecepsion("kqndkdair3m324dvnokju89ke", startDate, endDate);
            var totalSellingForResepsionist2 = CalculatePriceBetweenDatesRecepsion("s1kfir3m324dv089nkke", startDate, endDate);
            var totalImpulsCountForMaster1 = CalculateImpuls(1,startDate, endDate);
            var totalImpulsCountForMaster2 = CalculateImpuls(2, startDate, endDate);
            var result = new 
            {
                TotalPrice = totalPrice,
                TotalPriceForMaster1 = totalPriceForMaster1,
                TotalPriceForMaster2 = totalPriceForMaster2,
                TotalIncomeMoney=totalIncomeMoney,
                TotalOutMoney=totalOutMoney,
                TotalSellingForResepsionist1= totalSellingForResepsionist1,
                TotalSellingForResepsionist2= totalSellingForResepsionist2,
                totalImpulsCount1= totalImpulsCountForMaster1,
                totalImpulsCount2 = totalImpulsCountForMaster2,

            };
       

            return Json(result);
        }
        public decimal CalculatePriceBetweenDates(DateTime startDate, DateTime endDate)
        {
          
            IQueryable<LazerAppointment> appointmentsInDateRangeForMaster = _appDbContext.LazerAppointments
                .Include(x => x.Customers)
                .Include(x => x.LazerMaster).
              
  Include(x => x.LazerAppointmentReports)
                .ThenInclude(x => x.LazerCategory)
                .Where(  x=>x.EndTime >= startDate && x.EndTime <= endDate);

            decimal totalPrice = appointmentsInDateRangeForMaster.Sum(x => x.Price);
            return totalPrice;
        }
        public decimal CalculatePriceBetweenDatesIncome(DateTime startDate, DateTime endDate)
        {

            IQueryable<Income> IncomeMoneyBetweenTwoDates = _appDbContext.Incomes.Where(x=>x.IncomeDate >= startDate && x.IncomeDate <= endDate);

            decimal totalPrice = IncomeMoneyBetweenTwoDates.Sum(x => x.Price);
            return totalPrice;
        }
        public decimal CalculatePriceBetweenDatesRecepsion(string Id,DateTime startDate, DateTime endDate)
        {

            IQueryable<Income> IncomeMoneyBetweenTwoDates = _appDbContext.Incomes.Include(x=>x.AppUser).Where(x=>x.AppUserId == Id  && x.IncomeDate >= startDate  &&x.IncomeDate <= endDate);

            decimal totalPrice = IncomeMoneyBetweenTwoDates.Sum(x => x.Price);
            return totalPrice;
        }
        public decimal CalculatePriceBetweenDatesOutMoney(DateTime startDate, DateTime endDate)
        {

            IQueryable<OutMoney> OutMoneyBetweenTwoDates = _appDbContext.OutMoney.Where(x=>x.CreateTime >= startDate && x.CreateTime <= endDate);

            decimal totalPrice = OutMoneyBetweenTwoDates.Sum(x => x.Price);
            return totalPrice;
        }
        public decimal CalculateTotalPriceForMaster(int masterId, DateTime startDate, DateTime endDate)
        {
           

            IQueryable<LazerAppointment> appointmentsInDateRangeForMaster = _appDbContext.LazerAppointments
                .Where(x =>x.LazerMasterId == masterId && x.EndTime >= startDate && x.EndTime <= endDate);

            decimal totalPriceForMaster = appointmentsInDateRangeForMaster.Sum(x => x.Price);
            return totalPriceForMaster;
        }
        public int CalculateImpuls(int LazerMasterId,DateTime startDate, DateTime endDate)
        {
            IQueryable<LazerAppointment> appointmentImpuls = _appDbContext.LazerAppointments
               .Include(x=>x.LazerMaster).Where(x=>x.LazerMasterId==LazerMasterId && x.EndTime >= startDate && x.EndTime <= endDate);

            int totalImpulsForMaster = appointmentImpuls.Sum(x => x.ImplusCount);
            return totalImpulsForMaster;
        }
        [HttpGet]
        public IActionResult UpgradePriceList()
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Include(x => x.Customers).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x=>x.PriceUpdateDescription!=null).ToList();
           
         
            return View(lazerAppointments);
        }


    }

}

