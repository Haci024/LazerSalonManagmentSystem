using Azure;
using Business.IServices;
using Business.ValidationRules.KassaValidator;
using Data.Concrete;
using DTO.DTOS.KassaActionsDTO;
using DTO.DTOS.ReportDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;

namespace LazerBeautyFullProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Supporter,SuperSupporter")]
    public class TotalKassaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IKassaActionListService _kassaActionListService;
        private readonly IKassaService _kassaService;
       

        public TotalKassaController(AppDbContext db,UserManager<AppUser> userManager,IKassaService kassaService ,IKassaActionListService kassaActionListService)
        {
            _appDbContext = db;
            _kassaActionListService = kassaActionListService;
            _userManager = userManager;
            _kassaService = kassaService;

        }
        [HttpGet]
        public IActionResult BudgetPage()
        {
            SelectFilterDTO selectFilterDTO = new SelectFilterDTO();
            selectFilterDTO.ArzumMiniBudget = _kassaService.Budget(1);
            selectFilterDTO.ArzumBeautyBudget = _kassaService.Budget(2);
            selectFilterDTO.ArzumEsteticBudget = _kassaService.Budget(3);
          
            selectFilterDTO.Filials = _appDbContext.Filials.Where(x => x.Id != 4 && x.Id != 5).ToList();
            
            return View(selectFilterDTO);
        }
        [HttpPost]
        public JsonResult CalculateNetPrice(DateTime startDate, DateTime endDate, int filialId)
        {
            var LazerEarning = CalculateLazerEarning(startDate, endDate, filialId);
            var LazerEarningAll = CalculateLazerEarningAll(startDate, endDate);
            var CosmetologyEarning = CalculateCosmetologyEarning(startDate, endDate, filialId);
            var CosmetologyEarningAll = CalculateCosmetologyEarningAll(startDate, endDate);
            var SolariumEarning = CalculateSolariumEarning(startDate, endDate, filialId);
            var SolariumEarningAll = CalculateSolariumEarningAll(startDate, endDate);
            var BodyShapingEarning = CalculateBodyShapingEarning(startDate, endDate, filialId);
            var BodyShapingEarningAll = CalculateBodyShapingEarningAll(startDate, endDate);

            var OutMoneyForFilial=CalculateTotalOutMoneyForFilial(startDate, endDate, filialId);
            var OutMoneyForAll = CalculateTotalOutMoneyAll(startDate, endDate);
            var ShopForAll= CalculateTotaSellingMoneyAll(startDate, endDate);
            var PirsinqForFilial=CalculatePirsinqEarningForFilial(startDate, endDate, filialId);
            var PirsinqForAll=CalculatePirsinqEarningAll(startDate, endDate);
            var ShopForFilial = CalculateTotalSellingMoneyForFilial(startDate, endDate, filialId);
            var lipuckaForFilial=CalculateLipuckaEarningForFilial(startDate,endDate, filialId);
            var lipuckaForAll = CalculateLipuckaEarningAll(startDate, endDate);


            var TotalEarningFilial = LazerEarning +lipuckaForFilial+PirsinqForFilial+ CosmetologyEarning + SolariumEarning + BodyShapingEarning + ShopForFilial - OutMoneyForFilial;
            var TotalEarningAll = LazerEarningAll + CosmetologyEarningAll +lipuckaForAll+PirsinqForAll+ SolariumEarningAll + BodyShapingEarningAll + ShopForAll - OutMoneyForAll;
            var response = new
            {
                totalLazerEarningForFilial = LazerEarning,
                totalLazerEarningAll=LazerEarningAll,
                pirsinqEarningAll=PirsinqForAll,
                pirsinqForFilial=PirsinqForFilial,
                totalCosmetologyEarningForFilial = CosmetologyEarning,
                totalCosmetologyEarningAll=CosmetologyEarningAll,
                totalSolariumEarningForFilial = SolariumEarning,
                totalSolariumEarningAll = SolariumEarningAll,
                totalBodyShapingEarningForFilial = BodyShapingEarning,
                totalBodyShapingEarningAll = BodyShapingEarningAll,
                totalIncomeForFilial = ShopForFilial,
                totalIncomeForAll = ShopForAll,
                totalOutMoneyForFilial=OutMoneyForFilial,
                totalOutMoneyForAll=OutMoneyForAll,
                totalEarningForFilial = TotalEarningFilial,
                totalEarningAll = TotalEarningAll,
                totalLipuckaEarningForFilial=lipuckaForFilial,
                totalLipuckaForAll=lipuckaForAll,
            };
            return Json(response);
        }

        public decimal CalculateLazerEarning(DateTime startDate, DateTime endDate, int FilialId)
        {       
                List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Include(x => x.Filial).Where(x =>x.IsCompleted == true &&  x.FilialId==FilialId && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
                decimal totalEarning = lazerAppointments.Sum(x => x.Price);
                return totalEarning;
            
        }
        public decimal CalculateLazerEarningAll(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Include(x => x.Filial).Where(x => x.IsCompleted==true && x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal totalEarning = lazerAppointments.Sum(x => x.Price);
            return totalEarning;

        }
        public decimal CalculateCosmetologyEarning(DateTime startDate, DateTime endDate, int FilialId)
        {
          
                List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments.Include(x=>x.Filial).Include(x => x.Cosmetolog).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Where(x=>x.IsCompleted == true && x.FilialId==FilialId && x.OutTime > startDate && x.OutTime < endDate).ToList();
                decimal totalEarning = cosmetologyAppointments.Sum(x => x.Price);
                return totalEarning;
        }
        public decimal CalculateCosmetologyEarningAll(DateTime startDate, DateTime endDate)
        {

            List<CosmetologyAppointment> cosmetologyAppointments = _appDbContext.CosmetologyAppointments.Include(x => x.Cosmetolog).Include(x=>x.Filial).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Where(x => x.IsCompleted == true && x.OutTime >= startDate && x.OutTime <= endDate).ToList();
            decimal totalEarning = cosmetologyAppointments.Sum(x => x.Price);
            return totalEarning;


        }
        public decimal CalculateSolariumEarning(DateTime startDate, DateTime endDate, int FilialId)
        {
          
                List<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments.Include(x=>x.Filial).Include(x => x.SolariumUsingList).Include(x => x.SolariumCategories).Where(x =>x.FilialId==FilialId && x.BuyingDate >= startDate && x.BuyingDate <= endDate).ToList();
                decimal totalEarning = solariumAppointments.Sum(x => x.Price);
                return totalEarning;
   

        }


        public decimal CalculateSolariumEarningAll(DateTime startDate, DateTime endDate)
        {

            List<SolariumAppointment> solariumAppointments = _appDbContext.SolariumAppointments.Include(x=>x.Filial).Include(x => x.SolariumUsingList).Include(x => x.SolariumCategories).Where(x => x.BuyingDate >= startDate && x.BuyingDate <= endDate).ToList();
            decimal totalEarning = solariumAppointments.Sum(x => x.Price);
            return totalEarning;


        }
        public decimal CalculateBodyShapingEarning(DateTime startDate, DateTime endDate, int FilialId)
        {
        
                List<BodyshapingAppointment> BodyShapingAppointments = _appDbContext.BodyShapingAppointments.Include(x=>x.Filial).Include(x => x.BodyShapingPacketReports).ThenInclude(x => x.BodyShapingPackets).Where(x =>x.FilialId==FilialId && startDate <= x.BuyingDate && x.BuyingDate <= endDate).ToList();
                decimal totalEarning = BodyShapingAppointments.Sum(x => x.Price);
                return totalEarning;
     
       

        }

        public decimal CalculatePirsinqEarningAll(DateTime startDate, DateTime endDate)
        {

            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x=>x.Filial).Where(x =>x.EndTime>=startDate && x.EndTime <= endDate).ToList();
            decimal totalEarning = pirsinqAppointments.Sum(x => x.Price);
            return totalEarning;


        }
        public decimal CalculatePirsinqEarningForFilial(DateTime startDate, DateTime endDate, int FilialId)
        {

            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Include(x=>x.Filial).Where(x => x.FilialId == FilialId && startDate <= x.EndTime && x.EndTime <= endDate).ToList();
            decimal totalEarning = pirsinqAppointments.Sum(x => x.Price);
            return totalEarning;



        }
        public decimal CalculateLipuckaEarningAll(DateTime startDate, DateTime endDate)
        {

            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Where(x => x.EndTime >= startDate && x.EndTime <= endDate).ToList();
            decimal totalEarning = lipuckaAppointments.Sum(x => x.Price);
            return totalEarning;


        }
        public decimal CalculateLipuckaEarningForFilial(DateTime startDate, DateTime endDate, int FilialId)
        {

            List<LipuckaAppointment> lipuckaAppointments = _appDbContext.LipuckaAppointments.Include(x => x.Filial).Where(x => x.FilialId == FilialId && startDate <= x.EndTime && x.EndTime <= endDate).ToList();
            decimal totalEarning = lipuckaAppointments.Sum(x => x.Price);
            return totalEarning;



        }
        public decimal CalculateBodyShapingEarningAll(DateTime startDate, DateTime endDate)
        {

            List<BodyshapingAppointment> BodyShapingAppointments = _appDbContext.BodyShapingAppointments.Include(x=>x.Filial).Include(x => x.BodyShapingPacketReports).ThenInclude(x => x.BodyShapingPackets).Where(x => x.BuyingDate >= startDate && x.BuyingDate <= endDate).ToList();
            decimal totalEarning = BodyShapingAppointments.Sum(x => x.Price);
            return totalEarning;



        }
        //public decimal CalculateNetEarning(DateTime startDate, DateTime endDate, int FilialId)
        //{


        //        List<Income> Incomes = _appDbContext.Incomes.Include(x => x.Stock).Where(x => x.FilialId == FilialId && startDate<=x.IncomeDate &&  x.IncomeDate <= endDate).ToList();
        //        decimal earningForSelling = 0;
        //        foreach (var income in Incomes)
        //        {

        //            decimal selling = income.Count * (income.Price - income.BuyingPrice);


        //            earningForSelling += selling;
        //        }   

        //    decimal netEarning = earningForSelling;

        //        return netEarning;

        //}
        //public decimal CalculateNetEarningAll(DateTime startDate, DateTime endDate)
        //{


        //    List<Income> Incomes = _appDbContext.Incomes.Include(x => x.Stock).Where(x => x.IncomeDate >= startDate && x.IncomeDate <= endDate).ToList();
        //    decimal earningForSelling = 0;
        //    foreach (var income in Incomes)
        //    {

        //        decimal selling = income.Count * (income.Price - income.BuyingPrice);


        //        earningForSelling += selling;
        //    }


        //    decimal netEarning = earningForSelling;


        //    return netEarning;

        //}
        public decimal CalculateTotalOutMoneyForFilial(DateTime startDate, DateTime endDate, int FilialId)
        {

          
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x=>x.SpendCategory).Where(x => x.SpendCategory.FilialId==FilialId && x.AddingDate >= startDate && x.AddingDate <= endDate).ToList();
            decimal totalOutMoney = outMoney.Sum(x => x.Price);
         

            return totalOutMoney;
        }
        public decimal CalculateTotalOutMoneyAll(DateTime startDate, DateTime endDate)
        {
         
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x=>x.SpendCategory).Where(x => x.AddingDate >= startDate && x.AddingDate <= endDate).ToList();
            decimal totalOutMoney = outMoney.Sum(x => x.Price);
            

            return totalOutMoney;
        }
        public decimal CalculateTotaSellingMoneyAll(DateTime startDate, DateTime endDate)
        {
            List<Income> income = _appDbContext.Incomes.Include(x => x.Filial).Where(x=>x.IncomeDate>= startDate && endDate<=x.IncomeDate).ToList();

           var total= income.Sum(x => x.Price);

            return total;
        }
        public decimal CalculateTotalSellingMoneyForFilial(DateTime startDate, DateTime endDate, int FilialId)
        {


            List<Income> income = _appDbContext.Incomes.Include(x=>x.Filial).Where(x => x.IncomeDate >= startDate && endDate <= x.IncomeDate && x.FilialId== FilialId).ToList();

            var total = income.Sum(x => x.Price);

            return total;
        }
        #region KassEmeliyyatları
        [HttpGet]
        public async Task<IActionResult> OutMoneyForOwner()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
          
          
            return View(kassaActionsDTO);
        }

        public IActionResult KassaActionList()
        {
            List<KassaActionList> kassaActionLists = _appDbContext.KassaActionLists.Include(x => x.AppUser).Include(x => x.Filial).Where(x=>x.FilialId == 2).ToList();
            return View(kassaActionLists);
        }
        [HttpGet]
        public IActionResult AddMoneyToKassa()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
          
            return View(kassaActionsDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoneyToKassa(KassaActionsDTO kassaActionsDTO)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
           
            KassaActionList kassaActionList = new KassaActionList();
            kassaActionList.LastOutMoneyDate = DateTime.Now;
            kassaActionList.AppUserId = appUser.Id;
            
            kassaActionList.OutMoneyQuantity = kassaActionsDTO.OutMoney;
            kassaActionList.Status = true;


            
            _kassaActionListService.Create(kassaActionList);

            return RedirectToAction("KassaActionList");
        }
        #endregion
    }

}

