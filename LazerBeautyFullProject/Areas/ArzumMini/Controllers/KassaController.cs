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

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    
    [Area("ArzumMini")]
    [Authorize]
    public class KassaController : Controller
    {
        private readonly TimeHelper _timeHelper;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IKassaService _kassaService;
        private readonly IKassaActionListService _kassaActionListService;
        public KassaController(AppDbContext db,UserManager<AppUser> appUser, IKassaActionListService kassaActionListService, IKassaService kassaService)
        {
            _appDbContext = db;
            _kassaActionListService = kassaActionListService;
            _timeHelper = new TimeHelper();
            _userManager = appUser;
            _kassaService = kassaService;
        }
        public IActionResult BudgetPage()
        {
           
            DailyReportDTO dailyReportDTO = new DailyReportDTO();
            DailyReportDTO dailyReportDTO1 = _kassaService.GetDailyReport(1,dailyReportDTO);
            dailyReportDTO1.Budget = _kassaService.Budget(1);
            
            return View(dailyReportDTO1);
        }
        [HttpPost]
        public JsonResult CalculateForArzumMiniPrice(DateTime startDate, DateTime endDate)
        {
            var lazerEarning = CalculateLazerEarning(startDate, endDate);
            var incomeMoney=CalculateIncomeMoney(startDate,endDate);
            var outMoney=CalculateOutMoney(startDate,endDate);
            var impulsCount=CalculateImpulsCount(startDate,endDate);
            var pirsinqEarning = CalculatePirsinqEarning(startDate, endDate);
            var LipuckaEarning = CalculateLipuckaEarning(startDate, endDate);


            var TotalEarning = lazerEarning+incomeMoney+pirsinqEarning+ LipuckaEarning - outMoney;

            var response = new
            {
                lazerEarning = lazerEarning,
                incomeMoney = incomeMoney,
                outMoney = outMoney,
                totalEarning =TotalEarning,
                impulsCount= impulsCount,
                pirsinqEarning= pirsinqEarning,
                lipuckaEarning= LipuckaEarning,


            };
            return Json(response);
        }
        public decimal CalculateLazerEarning(DateTime startDate,DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId==1).ToList();
            var LazerEarning = lazerAppointments.Sum(x=>x.Price);
            return LazerEarning;
        }
        public int CalculateImpulsCount(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1).ToList();
            var impuls = lazerAppointments.Sum(x => x.ImplusCount);
            return impuls;
        }

        public decimal CalculatePirsinqEarning(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1).ToList();
            var pirsinqEarning = pirsinqAppointments.Sum(x => x.Price);
            return pirsinqEarning;
        }
        public decimal CalculateLipuckaEarning(DateTime startDate, DateTime endDate)
        {
            List<LipuckaAppointment> LipuckaAppointments = _appDbContext.LipuckaAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 1).ToList();
            var lipuckaEarning = LipuckaAppointments.Sum(x => x.Price);
            return lipuckaEarning;
        }
        public decimal CalculateIncomeMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> incomes = _appDbContext.Incomes.Where(x => x.FilialId==1 && x.IncomeDate > startDate && x.IncomeDate < endDate).ToList();
            var incomeEarning = incomes.Sum(x => x.Price);
            return incomeEarning;
        }
        public decimal CalculateOutMoney(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x=>x.SpendCategory).Where(x => x.SpendCategory.FilialId == 1 && x.AddingDate > startDate && x.AddingDate < endDate).ToList();
            var sellingEarning = outMoney.Sum(x => x.Price);
            return sellingEarning;
        }
        [HttpGet]
        public async Task<IActionResult> AddMoneyToBudget()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
           
        
            
            return View(kassaActionsDTO);
        }
     
        [HttpGet]
        public async Task<IActionResult> OutMoneyForOwner()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
          
            kassaActionsDTO.Budget =_kassaService.Budget(1);
            return View(kassaActionsDTO);
        }
      
        [HttpPost]     
        public async  Task<IActionResult> OutMoneyForOwner(KassaActionsDTO kassaActionsListDTO)
        {
           
            kassaActionsListDTO.Budget = _kassaService.Budget(1); ;
            var validator = new KassaActionListValidator();
            var validationResult=validator.Validate(kassaActionsListDTO);
            if(!validationResult.IsValid) {
                foreach(var item in validationResult.Errors)
                {
                    ModelState.AddModelError("",item.ErrorMessage); 
                }
                return View(kassaActionsListDTO);
            }
            AppUser appUser=await _userManager.FindByNameAsync(User.Identity.Name);
           
            if (kassaActionsListDTO.OutMoney>kassaActionsListDTO.Budget)
            {
                ModelState.AddModelError("","Kassadan çıxarmaq istədiyiniz məbləğ kassadakı məbləğdən çoxdur!");
                
                return View(kassaActionsListDTO);
            }

            KassaActionList kassaActionList= new KassaActionList();
            kassaActionList.LastOutMoneyDate = _timeHelper.ConvertToAzerbaijanTime(DateTime.Now);
            kassaActionList.AppUserId = appUser.Id;
            kassaActionList.OutMoneyQuantity=kassaActionsListDTO.OutMoney;
            kassaActionList.Description = kassaActionsListDTO.Description;
            kassaActionList.Status = false;
            kassaActionList.FilialId = 1;
            _kassaActionListService.Create(kassaActionList);
            return RedirectToAction("KassaActionList");
        }
        [HttpGet]
        public IActionResult KassaActionList()
        {
            List<KassaActionList> kassaActionLists= _appDbContext.KassaActionLists.Include(x=>x.AppUser).Include(x=>x.Filial).Where(x=>x.FilialId==1).ToList();  
           
           return View(kassaActionLists);
        }

        [HttpGet]
        public IActionResult AddMoneyToKassa()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
          
            kassaActionsDTO.OutMoney = 0;
            kassaActionsDTO.Budget = _kassaService.Budget(1);



            return View(kassaActionsDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoneyToKassa(KassaActionsDTO kassaActionsDTO)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
          
            kassaActionsDTO.Budget = _kassaService.Budget(1);
        
            KassaActionList kassaActionList = new KassaActionList();
            kassaActionList.LastOutMoneyDate =_timeHelper.ConvertToAzerbaijanTime(DateTime.Now);
            kassaActionList.AppUserId = appUser.Id;
            kassaActionList.FilialId=1;
            kassaActionList.Description = kassaActionsDTO.Description;
            kassaActionList.OutMoneyQuantity = kassaActionsDTO.OutMoney;
            kassaActionList.Status = true;
            _kassaActionListService.Create(kassaActionList);
            
            return RedirectToAction("KassaActionList");
        }


    }
}
