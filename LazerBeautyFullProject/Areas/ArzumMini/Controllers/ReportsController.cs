using Azure;
using Business.IServices;
using Data.Concrete;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    [Area("ArzumMini")]
    [Authorize]
    public class ReportsController : Controller
    {
        private readonly ILazerAppointmentService _lazerAppointmentService;
        private readonly AppDbContext _appDbContext;
        
        public ReportsController(ILazerAppointmentService lazerAppointmentService, AppDbContext db)
        {
            _lazerAppointmentService = lazerAppointmentService;           
            _appDbContext = db;
        }

        #region LazerReports
        [HttpGet]
        public IActionResult FilterLazerReports()
        {
            SelectFilterDTO selectFilterDTO = new SelectFilterDTO();

          selectFilterDTO.LazerMasters=_appDbContext.LazerMasters.Include(x=>x.LazerMasterFilial).ThenInclude(x=>x.LazerMaster).Where(x=>x.LazerMasterFilial.Any(x=>x.FilialId==1)).ToList();
           selectFilterDTO.Filials= _appDbContext.Filials.Include(x=>x.LazerMasterFilials).ThenInclude(x=>x.LazerMaster).Where(x=>x.Id!=4 && x.Id!=5).ToList();    
            selectFilterDTO.LazerCategories=_appDbContext.LazerCategories.Include(x=>x.MainCategory).Where(x=>x.MainCategoryId!=null && x.FilialId==1).ToList(); 

            return View(selectFilterDTO);
        }
        [HttpPost]
        public JsonResult LazerEarningCalculator(DateTime startDate, DateTime endDate,int lazeroloqId, int[] categoryId)
        {
            var lazerEarning = CalculateLazerMasterEarning(startDate, endDate,lazeroloqId,categoryId);
            var impulsCount=CalculateImpulsCount(startDate, endDate,lazeroloqId,categoryId);
            var SessionCount = CalculateSessionCount(startDate,endDate,lazeroloqId, categoryId);
            var TotalLazerEarning = CalculateLazerMasterEarningAll(startDate, endDate);
            var totalImpuls = CalculateImpulsCountAll(startDate, endDate);
            var AllSessions = CalculateSessionCountAll(startDate, endDate);
            var LazerMasterAllCategoryEarning = CalculateLazerMasterAllCategoryEarning(startDate,endDate,lazeroloqId);
            var LazerMasterTotalImpulsAllCategory = CalculateImpulsCountAllCategory(startDate,endDate,lazeroloqId);
            var LazerMasterAllCategorySession = CalculateLazerMasterALlSessions(startDate,endDate,lazeroloqId);
            var AllMasterCategoryEarning = CalculateAllMasterCategoriesEarning(startDate, endDate, categoryId);
            var AllMasterCategorySessionCount = CalculateAllMasterCategoriesSessionCount(startDate, endDate, categoryId);
            var AllMasterCategoryImpulsCount = CalculateAllMasterCategoriesImpulsCount(startDate, endDate, categoryId);

            var response = new
            {
                lazerMasterEarning = lazerEarning,
                impulsCount=impulsCount,
                sessionCount=SessionCount,
                totalSessions=AllSessions,
                totalLazerEarning=TotalLazerEarning,
                totalImpulsCount= totalImpuls,
                lazerMasterAllCategoryEarning=LazerMasterAllCategoryEarning,
                lazerMasterTotalImpulsAllCategory=LazerMasterTotalImpulsAllCategory,
                lazerMasterAllCategorySession=LazerMasterAllCategorySession,
                allMasterCategoryEarning=AllMasterCategoryEarning,
                allMasterCategorySessionCount=AllMasterCategorySessionCount,
                allMasterCategoryImpulsCount=AllMasterCategoryImpulsCount

            };
            return Json(response);
        }
        public decimal CalculateLazerMasterEarning(DateTime startDate, DateTime endDate, int lazeroloqId, int[] categoryId )
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId==lazeroloqId && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var LazerEarning = lazerAppointments.Sum(x => x.Price);
            return LazerEarning;
        }
        public int CalculateImpulsCount(DateTime startDate, DateTime endDate, int lazeroloqId, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var impuls = lazerAppointments.Sum(x => x.ImplusCount);
            return impuls;
        }
        public int CalculateSessionCount(DateTime startDate, DateTime endDate, int lazeroloqId, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var sessionCount = lazerAppointments.Count();
            return sessionCount;
        }

        public decimal CalculateLazerMasterAllCategoryEarning(DateTime startDate, DateTime endDate, int lazeroloqId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId).ToList();
            var LazerEarning = lazerAppointments.Sum(x => x.Price);
            return LazerEarning;
        }
        public int CalculateImpulsCountAllCategory(DateTime startDate, DateTime endDate, int lazeroloqId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId).ToList();
            var impuls = lazerAppointments.Sum(x => x.ImplusCount);
            return impuls;
        }
        public int CalculateLazerMasterALlSessions(DateTime startDate, DateTime endDate, int lazeroloqId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId).ToList();
            var sessionCount = lazerAppointments.Count();
            return sessionCount;
        }

        public decimal CalculateLazerMasterEarningAll(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1).ToList();
            var LazerEarning = lazerAppointments.Sum(x => x.Price);
            return LazerEarning;
        }
        public int CalculateImpulsCountAll(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1).ToList();
            var impuls = lazerAppointments.Sum(x => x.ImplusCount);
            return impuls;
        }
        public int CalculateSessionCountAll(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1).ToList();
            var sessionCount = lazerAppointments.Count();
            return sessionCount;
        }

        public decimal CalculateAllMasterCategoriesEarning(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var earning = lazerAppointments.Sum(x => x.Price);
            
            return earning;
        }
        public int CalculateAllMasterCategoriesSessionCount(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var session = lazerAppointments.Count();

            return session;
        }
        public int CalculateAllMasterCategoriesImpulsCount(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            var impuls = lazerAppointments.Sum(x=>x.ImplusCount);

            return impuls;
        }

        [HttpPost]
        public IActionResult SelectAllReports(DateTime startDate, DateTime endDate)
        {
           List<LazerAppointment> lazerAppointments= GetAllLazerReports(startDate, endDate);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LazerReports.cshtml",lazerAppointments);
        }
        [HttpPost]
        public IActionResult SelectLazerMasterAllReports(DateTime startDate, DateTime endDate,int lazeroloqId)
        {
            List<LazerAppointment> lazerAppointments = GetLazerMasterAllReports(startDate, endDate, lazeroloqId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LazerReports.cshtml", lazerAppointments);
        }
        [HttpPost]
        public IActionResult SelectAllMasterCategoryReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = SelectAllMasterCategoriesReports(startDate, endDate, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LazerReports.cshtml", lazerAppointments);
        }
        [HttpPost]
        public IActionResult SelectReportsWithAllFilter(DateTime startDate, DateTime endDate, int lazeroloqId, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = GetLazerReportsAllFilter(startDate, endDate, lazeroloqId, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LazerReports.cshtml", lazerAppointments);
        }
        

        public List<LazerAppointment> GetAllLazerReports(DateTime startDate,DateTime endDate) {
        
        List<LazerAppointment> GetallReports=_appDbContext.LazerAppointments.Include(x=>x.Customers).Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x=>x.IsCompleted==true && x.FilialId==1 && x.EndTime >= startDate && x.EndTime<=endDate).ToList();
            return GetallReports;
        }
        public List<LazerAppointment> GetLazerMasterAllReports(DateTime startDate, DateTime endDate,int lazeroloqId)
        {

            List<LazerAppointment> GetallReports = _appDbContext.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == true && x.FilialId==1 && x.LazerMasterId== lazeroloqId && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            return GetallReports;
        }
        public List<LazerAppointment> GetLazerReportsAllFilter(DateTime startDate,DateTime endDate,int lazeroloqId, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x=>x.Customers).Include(x=>x.LazerMaster).Include(x=>x.AppUser).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerMasterId == lazeroloqId && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            return lazerAppointments;
        }
        public List<LazerAppointment> SelectAllMasterCategoriesReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerAppointmentReports.Any(cr => categoryId.Contains(cr.LazerCategoryId))).ToList();
            return lazerAppointments;
        }
        #endregion
        #region ShopingReports
        [HttpGet]
        public IActionResult FilterShopingReports()
        {
            SelectFilterDTO selectFilterDTO = new SelectFilterDTO();

          
            selectFilterDTO.Stock = _appDbContext.Stock.Include(x => x.Filial).Where(x => x.FilialId==1).ToList();
            

            return View(selectFilterDTO);
        }
        [HttpPost]
        public JsonResult ShopingEarningCalculator(DateTime startDate, DateTime endDate, int[] productId)
        {
         var totalSellingMoney= CalculateTotalSellingMoney(startDate, endDate);
         var totalBuyingMoney=CalculateTotalBuyingMoney(startDate, endDate);
         var totalBenefitMoney=CalculateTotalBenefitMoney(startDate, endDate);
         var totalProductCount=CalculateTotalProductCount(startDate, endDate);
         var SellingMoney= CalculateProductSellingMoney(startDate, endDate,productId);
         var BuyingMoney=CalculateProductBuyingMoney(startDate, endDate,productId);
         var BenefitMoney=CalculateProductBenefitMoney(startDate,endDate,productId);
         var ProductCount = CalculateProductCount(startDate, endDate, productId);

            var response = new
            {
                sellingAllPrice = totalSellingMoney,
                buyingAllPrice = totalBuyingMoney,
                benefitAllMoney = totalBenefitMoney,
                allproductCount = totalProductCount,
                sellingPrice = SellingMoney,
                buyingPrice = BuyingMoney,
                benefitMoney = BenefitMoney,
                productCount= ProductCount,
            };
            return Json(response);
        }
        public decimal CalculateTotalSellingMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> income=_appDbContext.Incomes.Where(x=>x.FilialId==1 && x.IncomeDate>=startDate && x.IncomeDate<=endDate).ToList();
            var earning=income.Sum(x=>x.Price);
            return earning;
        }
        public decimal CalculateTotalBuyingMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate).ToList();
            var earning = income.Sum(x => x.BuyingPrice);
            return earning;
        }
        public decimal CalculateTotalBenefitMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate).ToList();
            var buyingPrice = income.Sum(x => x.BuyingPrice);
            var sellingPrice = income.Sum(x => x.Price);
            var benefitPrice = sellingPrice - buyingPrice;
            return benefitPrice;
        }
        public int CalculateTotalProductCount(DateTime startDate, DateTime endDate)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate).ToList();
            var product = income.Sum(x=>x.Count);
            return product;
        }
        public decimal CalculateProductSellingMoney(DateTime startDate, DateTime endDate,int[] productId)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate && productId.Contains(x.StockId)).ToList();
            var earning = income.Sum(x => x.Price);
            return earning;
        }
        public decimal CalculateProductBuyingMoney(DateTime startDate, DateTime endDate,int[] productId)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate && productId.Contains(x.StockId)).ToList();
            var earning = income.Sum(x => x.BuyingPrice);
            return earning;
        }
        public decimal CalculateProductBenefitMoney(DateTime startDate, DateTime endDate,int[] productId)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate && productId.Contains(x.StockId)).ToList();
            var buyingPrice = income.Sum(x => x.BuyingPrice);
            var sellingPrice = income.Sum(x => x.Price);
            var benefitPrice = sellingPrice - buyingPrice;
            return benefitPrice;
        }
        public int CalculateProductCount(DateTime startDate, DateTime endDate,int[] productId)
        {
            List<Income> income = _appDbContext.Incomes.Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate && productId.Contains(x.StockId)).ToList();
            var product = income.Sum(x => x.Count);
            return product;
        }
        [HttpPost]
        public IActionResult SelectAllShopingReports(DateTime startDate, DateTime endDate)
        {
            List<Income> income = GetAllShopingReports(startDate, endDate);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_ShopingReports.cshtml", income);
        }
        [HttpPost]
        public IActionResult SelectProductReports(DateTime startDate, DateTime endDate, int[] productId)
        {
            List<Income> income = GetProductReports(startDate, endDate, productId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_ShopingReports.cshtml", income);
        }
        public List<Income> GetProductReports(DateTime startDate, DateTime endDate, int[] productId)
        {

            List<Income> incomes = _appDbContext.Incomes.Include(x => x.AppUser).Include(x => x.Stock).Include(x => x.Filial).Where(x => x.FilialId == 1 && x.IncomeDate >= startDate && x.IncomeDate <= endDate && productId.Contains(x.StockId)).ToList();
            return incomes;
        }

        public List<Income> GetAllShopingReports(DateTime startDate, DateTime endDate)
        {

            List<Income> incomes = _appDbContext.Incomes.Include(x => x.AppUser).Include(x => x.Stock).Include(x => x.Filial).Where(x => x.IncomeDate >= startDate && x.IncomeDate <= endDate && x.FilialId == 1).ToList();
            return incomes;
        }
        #endregion
        #region LipuckaReports

        [HttpGet]
        public IActionResult FilterLipuckaReports()
        {
           SelectFilterDTO selectFilterDTO=new SelectFilterDTO();
            selectFilterDTO.LipuckaCategories=_appDbContext.LipuckaCategories.Include(x=>x.ChildCategories).Include(x=>x.MainCategory).Where(x=>x.MainCategoryId!=null && x.IsDeactive==false).ToList();
            selectFilterDTO.LazerMasters = _appDbContext.LazerMasters.Include(x=>x.LazerMasterFilial).ThenInclude(x=>x.Filial).Where(x => x.IsDeactive == false && x.LazerMasterFilial.Any(x => x.FilialId == 1)).ToList();
            return View(selectFilterDTO);
        }
        [HttpPost]
        public JsonResult CalculateLipuckaReports(DateTime startDate, DateTime endDate, int lipuckaMasterId ,int[] categoryId)
        {
            var totalEarning=TotalLipuckaEarning(startDate, endDate);
            var totalSession=TotalLipuckaSession(startDate, endDate);
            var masterLipuckaEarning = MasterLipuckaEarning(startDate, endDate, lipuckaMasterId);
            var masterLipuckaSession= MasterLipuckaSession(startDate, endDate, lipuckaMasterId);
            var categoryEarning=MasterCategoryEarning(startDate,endDate, categoryId);
            var categorySession=MasterCategorySession(startDate , endDate, categoryId);
            var allFilterEarning=LipuckaAllFilterEarning(startDate , endDate,lipuckaMasterId ,categoryId);
            var allFilterSession=LipuckaAllFilterSession(startDate , endDate,lipuckaMasterId ,categoryId);

            var response = new
            {
               totalEarning=totalEarning,
               totalSession=totalSession,
               masterLipuckaEarning=masterLipuckaEarning,
               masterLipuckaSession = masterLipuckaSession,
               categoryEarning=categoryEarning,
               categorySession=categorySession,
               allFilterEarning=allFilterEarning,
               allFilterSession=allFilterSession,
            };

            return Json(response);
        }
      
        public decimal TotalLipuckaEarning(DateTime startDate,DateTime endDate)
        {
            List<LipuckaAppointment> lipuckaAppointments=_appDbContext.LipuckaAppointments.Include(x=>x.AppUser).Include(x=>x.LazerMaster).Where(x=>x.FilialId==1 && x.IsCompleted==true && x.EndTime>=startDate && x.EndTime<=endDate).ToList();
            decimal earning = lipuckaAppointments.Sum(x=>x.Price);
            return earning;
        }
        public int TotalLipuckaSession(DateTime startDate, DateTime endDate)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = lipuckaAppointments.Count();
            return session;
        }
      
        public decimal MasterLipuckaEarning(DateTime startDate, DateTime endDate,int lipuckaMasterId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId==lipuckaMasterId && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = lipuckaAppointments.Sum(x => x.Price);
            return earning;
        }
        public int MasterLipuckaSession(DateTime startDate, DateTime endDate,int lipuckaMasterId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId==lipuckaMasterId && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = lipuckaAppointments.Count();
            
            return session;
        }

        public decimal MasterCategoryEarning(DateTime startDate, DateTime endDate, int[] CategoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LipuckaReports.Any(lc => CategoryId.Contains(lc.LipuckaCategoriesId)) && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = lipuckaAppointments.Sum(x => x.Price);
            return earning;
        }
        public int MasterCategorySession(DateTime startDate, DateTime endDate, int[]CategoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LipuckaReports.Any(lc=>CategoryId.Contains(lc.LipuckaCategoriesId)) && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = lipuckaAppointments.Count();

            return session;
        }
     
        public decimal LipuckaAllFilterEarning(DateTime startDate, DateTime endDate,int lipuckaMasterId ,int[] CategoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LipuckaReports.Any(lc => CategoryId.Contains(lc.LipuckaCategoriesId)) && x.LazerMasterId==lipuckaMasterId && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = lipuckaAppointments.Sum(x => x.Price);
            return earning;
        }
        public int LipuckaAllFilterSession(DateTime startDate, DateTime endDate,int lipuckaMasterId, int[] CategoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId==lipuckaMasterId && x.LipuckaReports.Any(lc => CategoryId.Contains(lc.LipuckaCategoriesId)) && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = lipuckaAppointments.Count();

            return session;
        }
       
        [HttpPost]
        public IActionResult SelectAllLipuckaReports(DateTime startDate, DateTime endDate)
        {
            List<LipuckaAppointment> lipuckaAppointments = GetAllLipuckaReports(startDate, endDate);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LipuckaReports.cshtml", lipuckaAppointments);
        }
        public List<LipuckaAppointment> GetAllLipuckaReports(DateTime startDate, DateTime endDate)
        {
            List<LipuckaAppointment> lipuckaAppointments=_appDbContext.LipuckaAppointments.Include(x=>x.LipuckaReports).ThenInclude(x=>x.LipuckaCategories).Include(x=>x.LazerMaster).Include(x=>x.Customer).Include(x=>x.AppUser).Where(x=>x.EndTime>=startDate && x.EndTime<=endDate && x.FilialId==1 && x.IsCompleted==true).ToList();

            return lipuckaAppointments;
        }
       
        [HttpPost]
        public IActionResult SelectLipuckaMasterReports(DateTime startDate, DateTime endDate, int lipuckaMasterId)
        {
            List<LipuckaAppointment> lipuckaAppointments = GetLipuckaMasterAllReports(startDate, endDate,lipuckaMasterId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LipuckaReports.cshtml", lipuckaAppointments);
        }
        public List<LipuckaAppointment> GetLipuckaMasterAllReports(DateTime startDate, DateTime endDate, int lipuckaMasterId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId==1 && x.LazerMasterId==lipuckaMasterId && x.IsCompleted==true).ToList();

            return lipuckaAppointments;
        }

        [HttpPost]
        public IActionResult SelectLipuckaCategoryReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = SelectLipuckaCategoriesReports(startDate, endDate, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LipuckaReports.cshtml", lipuckaAppointments);
        }
        public List<LipuckaAppointment> SelectLipuckaCategoriesReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LipuckaReports.Any(x => categoryId.Contains(x.LipuckaCategoriesId)) && x.IsCompleted == true).ToList();

            return lipuckaAppointments;
        }
       
        [HttpPost]
        public IActionResult SelectLipuckaReportsWithAllFilter(DateTime startDate, DateTime endDate,int lipuckaMasterId, int[] categoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = GetLipuckaReportsAllFilter(startDate, endDate, lipuckaMasterId, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_LipuckaReports.cshtml", lipuckaAppointments);
        }
        public List<LipuckaAppointment> GetLipuckaReportsAllFilter(DateTime startDate, DateTime endDate, int lipuckaMasterId, int[] categoryId)
        {
            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerMasterId==lipuckaMasterId && x.LipuckaReports.Any(x=>categoryId.Contains(x.LipuckaCategoriesId)) && x.IsCompleted == true).ToList();

            return lipuckaAppointments;
        }


        #endregion
        #region SpendingReports
        [HttpGet]
        public IActionResult FilterSpendingReports()
        {
            SelectFilterDTO DTO = new SelectFilterDTO();
            DTO.SpendCategory = _appDbContext.SpendCategories.Where(x => x.FilialId == 1 && x.Status == false).ToList();
            return View(DTO);
        }
        [HttpPost]
        public JsonResult CalculateSpendingReports(DateTime startDate, DateTime endDate, int categoryId)
        {
            var totalEarning = TotalSpendingMoney(startDate, endDate);
            var spendingPriceForCategory = TotalSpendingMoneyForCategory(startDate, endDate, categoryId);


            var response = new
            {
                spendingPriceForAll = totalEarning,
                spendingPriceForCategory = spendingPriceForCategory
            };

            return Json(response);
        }
        public decimal TotalSpendingMoney(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoneys = _appDbContext.OutMoney.Include(x => x.SpendCategory).Include(x => x.AppUser).Where(x => x.AddingDate >= startDate && endDate >= x.AddingDate && x.SpendCategory.FilialId == 1).ToList();

            decimal spendingMoney = outMoneys.Sum(x => x.Price);

            return spendingMoney;
        }
        public decimal TotalSpendingMoneyForCategory(DateTime startDate, DateTime endDate, int categoryId)
        {
            List<OutMoney> outMoneys = _appDbContext.OutMoney.Include(x => x.SpendCategory).Include(x => x.AppUser).Where(x => x.AddingDate >= startDate && endDate >= x.AddingDate && x.SpendCategory.FilialId == 1 && x.SpendCategoryId == categoryId).ToList();

            decimal spendingMoney = outMoneys.Sum(x => x.Price);

            return spendingMoney;
        }
        [HttpPost]
        public IActionResult SelectOutMoneyReports(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoney = GetAllSpendingReports(startDate, endDate);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_SpendingReports.cshtml", outMoney);
        }
        public List<OutMoney> GetAllSpendingReports(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x => x.SpendCategory).Include(x => x.AppUser).Where(x => x.AddingDate >= startDate && x.AddingDate <= endDate && x.SpendCategory.FilialId == 1).OrderBy(x => x.AddingDate).ToList();

            return outMoney;
        }
        [HttpPost]
        public IActionResult SelectOutMoneyReportsForCategory(DateTime startDate, DateTime endDate, int categoryId)
        {
            List<OutMoney> outMoney = GetSpendingReportsForCategory(startDate, endDate, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_SpendingReports.cshtml", outMoney);
        }
        public List<OutMoney> GetSpendingReportsForCategory(DateTime startDate, DateTime endDate, int categoryId)
        {
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x => x.SpendCategory).Include(x => x.AppUser).Where(x => x.AddingDate >= startDate && x.AddingDate <= endDate && x.SpendCategory.FilialId == 1 && x.SpendCategoryId == categoryId).OrderBy(x => x.AddingDate).ToList();

            return outMoney;
        }
        #endregion
        #region PirsinqReports

        [HttpGet]
        public IActionResult FilterPirsinqReports()
        {
            SelectFilterDTO selectFilterDTO = new SelectFilterDTO();
            selectFilterDTO.PirsinqCategory = _appDbContext.PirsinqCategories.Include(x => x.ChildCategory).Include(x => x.MainCategory).Where(x => x.MainCategoryId != null && x.IsDeactive == false).ToList();
            selectFilterDTO.LazerMasters = _appDbContext.LazerMasters.Include(x => x.LazerMasterFilial).ThenInclude(x => x.Filial).Where(x => x.IsDeactive == false && x.LazerMasterFilial.Any(x => x.FilialId == 1)).ToList();
            return View(selectFilterDTO);
        }
        [HttpPost]
        public JsonResult CalculatePirsinqReports(DateTime startDate, DateTime endDate, int pirsinqMasterId, int[] categoryId)
        {
            var totalEarning = TotalPirsinqEarning(startDate, endDate);
            var totalSession = TotalPirsinqSession(startDate, endDate);
            var masterPirsinqEarning = MasterPirsinqEarning(startDate, endDate, pirsinqMasterId);
            var masterPirsinqSession = MasterPirsinqSession(startDate, endDate, pirsinqMasterId);
            var categoryEarning = MasterPirsinqCategoryEarning(startDate, endDate, categoryId);
            var categorySession = MasterPirsinqCategorySession(startDate, endDate, categoryId);
            var allFilterEarning = PirsinqAllFilterEarning(startDate, endDate, pirsinqMasterId, categoryId);
            var allFilterSession = PirsinqAllFilterSession(startDate, endDate, pirsinqMasterId, categoryId);

            var response = new
            {
                totalEarning = totalEarning,
                totalSession = totalSession,
                masterPirsinqEarning = masterPirsinqEarning,
                masterPirsinqSession = masterPirsinqSession,
                categoryEarning = categoryEarning,
                categorySession = categorySession,
                allFilterEarning = allFilterEarning,
                allFilterSession = allFilterSession,
            };

            return Json(response);
        }

        public decimal TotalPirsinqEarning(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = pirsinqAppointments.Sum(x => x.Price);
            return earning;
        }
        public int TotalPirsinqSession(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = pirsinqAppointments.Count();
            return session;
        }

        public decimal MasterPirsinqEarning(DateTime startDate, DateTime endDate, int pirsinqMasterId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId == pirsinqMasterId && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = pirsinqAppointments.Sum(x => x.Price);
            return earning;
        }
        public int MasterPirsinqSession(DateTime startDate, DateTime endDate, int pirsinqMasterId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId == pirsinqMasterId && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = pirsinqAppointments.Count();

            return session;
        }

        public decimal MasterPirsinqCategoryEarning(DateTime startDate, DateTime endDate, int[] CategoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.PirsinqReports.Any(lc => CategoryId.Contains(lc.PirsinqCategoryId)) && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = pirsinqAppointments.Sum(x => x.Price);
            return earning;
        }
        public int MasterPirsinqCategorySession(DateTime startDate, DateTime endDate, int[] CategoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.PirsinqReports.Any(lc => CategoryId.Contains(lc.PirsinqCategoryId)) && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = pirsinqAppointments.Count();

            return session;
        }

        public decimal PirsinqAllFilterEarning(DateTime startDate, DateTime endDate, int pirsinqMasterId, int[] CategoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.PirsinqReports.Any(lc => CategoryId.Contains(lc.PirsinqCategoryId)) && x.LazerMasterId == pirsinqMasterId && x.IsCompleted == true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal earning = pirsinqAppointments.Sum(x => x.Price);
            return earning;
        }
        public int PirsinqAllFilterSession(DateTime startDate, DateTime endDate, int pirsinqMasterId, int[] CategoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.AppUser).Include(x => x.LazerMaster).Where(x => x.FilialId == 1 && x.LazerMasterId == pirsinqMasterId && x.PirsinqReports.Any(lc => CategoryId.Contains(lc.PirsinqCategoryId)) && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            int session = pirsinqAppointments.Count();

            return session;
        }

        [HttpPost]
        public IActionResult SelectAllPirsinqReports(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = GetAllPirsinqReports(startDate, endDate);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_PirsinqReports.cshtml", pirsinqAppointments);
        }
        public List<PirsinqAppointment> GetAllPirsinqReports(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.IsCompleted == true).ToList();

            return pirsinqAppointments;
        }

        [HttpPost]
        public IActionResult SelectPirsinqMasterReports(DateTime startDate, DateTime endDate, int pirsinqMasterId)
        {
            List<PirsinqAppointment> pirsinqAppointments = GetPirsinqMasterAllReports(startDate, endDate, pirsinqMasterId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_PirsinqReports.cshtml", pirsinqAppointments);
        }
        public List<PirsinqAppointment> GetPirsinqMasterAllReports(DateTime startDate, DateTime endDate, int pirsinqMasterId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerMasterId == pirsinqMasterId && x.IsCompleted == true).ToList();

            return pirsinqAppointments;
        }

        [HttpPost]
        public IActionResult SelectPirsinqCategoryReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = SelectPirsinqCategoriesReports(startDate, endDate, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_PirsinqReports.cshtml", pirsinqAppointments);
        }
        public List<PirsinqAppointment> SelectPirsinqCategoriesReports(DateTime startDate, DateTime endDate, int[] categoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.PirsinqReports.Any(x => categoryId.Contains(x.PirsinqCategoryId)) && x.IsCompleted == true).ToList();

            return pirsinqAppointments;
        }

        [HttpPost]
        public IActionResult SelectPirsinqReportsWithAllFilter(DateTime startDate, DateTime endDate, int pirsinqMasterId, int[] categoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = GetPirsinqReportsAllFilter(startDate, endDate, pirsinqMasterId, categoryId);

            return PartialView("~/Areas/ArzumMini/Views/Shared/_PirsinqReports.cshtml", pirsinqAppointments);
        }
        public List<PirsinqAppointment> GetPirsinqReportsAllFilter(DateTime startDate, DateTime endDate, int pirsinqMasterId, int[] categoryId)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.Customer).Include(x => x.AppUser).Where(x => x.EndTime >= startDate && x.EndTime <= endDate && x.FilialId == 1 && x.LazerMasterId == pirsinqMasterId && x.PirsinqReports.Any(x => categoryId.Contains(x.PirsinqCategoryId)) && x.IsCompleted == true).ToList();

            return pirsinqAppointments;
        }


        #endregion
    }

}





