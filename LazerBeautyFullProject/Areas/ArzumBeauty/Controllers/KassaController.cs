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

namespace LazerBeautyFullProject.Areas.ArzumBeauty.Controllers
{

    [Area("ArzumBeauty")]
    [Authorize]
    public class KassaController : Controller
    {
        private readonly TimeHelper _timeHelper;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IKassaActionListService _kassaActionListService;
        private readonly IKassaService _kassaService;
        public KassaController(AppDbContext db, UserManager<AppUser> appUser,IKassaService kassaService ,IKassaActionListService kassaActionListService)
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
            DailyReportDTO dailyReportDTO1 = _kassaService.GetDailyReport(2, dailyReportDTO);
            dailyReportDTO1.Budget = _kassaService.Budget(2);
          
            return View(dailyReportDTO1);
        }
        [HttpPost]
        public JsonResult CalculateTotalPrice(DateTime startDate, DateTime endDate)
        {
            var lazerEarning = CalculateLazerEarning(startDate, endDate);
            var Cosmetology = CalculateCosmetologyEarning(startDate, endDate);
            var incomeMoney = CalculateIncomeMoney(startDate, endDate);
            var outMoney = CalculateOutMoney(startDate, endDate);
            var impulsCount = CalculateImpulsCount(startDate, endDate);
            var pirsinqEarning = CalculatePirsinqEarning(startDate, endDate);
            var LipuckaEarning = CalculateLipuckaEarning(startDate, endDate);

            var TotalEarning = lazerEarning + incomeMoney + pirsinqEarning + LipuckaEarning + Cosmetology - outMoney;

            var response = new
            {
                lazerEarning = lazerEarning,
                incomeMoney = incomeMoney,
                outMoney = outMoney,
                totalEarning = TotalEarning,
                impulsCount = impulsCount,
                pirsinqEarning = pirsinqEarning,
                lipuckaEarning = LipuckaEarning,
                cosmetology = Cosmetology,
            };
            return Json(response);
        }
        public decimal CalculateLazerEarning(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 2).ToList();
            var Earning = lazerAppointments.Sum(x => x.Price);
            return Earning;
        }
        public int CalculateImpulsCount(DateTime startDate, DateTime endDate)
        {
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 2).ToList();
            var impuls = lazerAppointments.Sum(x => x.ImplusCount);
            return impuls;
        }
        public decimal CalculatePirsinqEarning(DateTime startDate, DateTime endDate)
        {
            List<PirsinqAppointment> pirsinqAppointments = _appDbContext.PirsinqAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 2).ToList();
            var Earning = pirsinqAppointments.Sum(x => x.Price);
            return Earning;
        }
        public decimal CalculateLipuckaEarning(DateTime startDate, DateTime endDate)
        {
            List<LipuckaAppointment> LipuckaAppointments = _appDbContext.LipuckaAppointments.Where(x => x.IsCompleted == true && x.EndTime > startDate && x.EndTime < endDate && x.FilialId == 2).ToList();
            var Earning = LipuckaAppointments.Sum(x => x.Price);
            return Earning;
        }
        public decimal CalculateIncomeMoney(DateTime startDate, DateTime endDate)
        {
            List<Income> incomes = _appDbContext.Incomes.Where(x => x.FilialId == 2 && x.IncomeDate > startDate && x.IncomeDate < endDate).ToList();
            var Earning = incomes.Sum(x => x.Price);
            return Earning;
        }
        public decimal CalculateOutMoney(DateTime startDate, DateTime endDate)
        {
            List<OutMoney> outMoney = _appDbContext.OutMoney.Include(x=>x.SpendCategory).Where(x => x.SpendCategory.FilialId == 2 && x.AddingDate > startDate && x.AddingDate < endDate).ToList();
            var Earning = outMoney.Sum(x => x.Price);
            return Earning;
        }
        public decimal CalculateCosmetologyEarning(DateTime startDate, DateTime endDate)
        {
            List<CosmetologyAppointment> appointments = _appDbContext.CosmetologyAppointments.Where(x => x.FilialId == 2 && x.OutTime > startDate && x.OutTime < endDate).ToList();
            var Earning = appointments.Sum(x => x.Price);
            return Earning;
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
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
          
            kassaActionsDTO.Budget = _kassaService.Budget(2);
            return View(kassaActionsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> OutMoneyForOwner(KassaActionsDTO kassaActionsListDTO)
        {

            kassaActionsListDTO.Budget = _kassaService.Budget(2);
            var validator = new KassaActionListValidator();
            var validationResult = validator.Validate(kassaActionsListDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(kassaActionsListDTO);
            }
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            if (kassaActionsListDTO.OutMoney > kassaActionsListDTO.Budget)
            {
                ModelState.AddModelError("", "Kassadan çıxarmaq istədiyiniz məbləğ kassadakı məbləğdən çoxdur!");

                return View(kassaActionsListDTO);
            }

            KassaActionList kassaActionList = new KassaActionList();
            kassaActionList.LastOutMoneyDate =(DateTime)kassaActionsListDTO.ProcessDate;
            kassaActionList.AppUserId = appUser.Id;
            kassaActionList.OutMoneyQuantity = kassaActionsListDTO.OutMoney;
            kassaActionList.Description = kassaActionsListDTO.Description;
            kassaActionList.Status = false;
            kassaActionList.FilialId = 2;
            _kassaActionListService.Create(kassaActionList);
            return RedirectToAction("KassaActionList");
        }
        [HttpGet]
        public IActionResult KassaActionList()
        {
            List<KassaActionList> kassaActionLists = _appDbContext.KassaActionLists.Include(x => x.AppUser).Include(x => x.Filial).Where(x => x.FilialId == 2).ToList();

            return View(kassaActionLists);
        }

        [HttpGet]
        public IActionResult AddMoneyToKassa()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();

            kassaActionsDTO.Budget = _kassaService.Budget(2);
            kassaActionsDTO.OutMoney = 0;

            return View(kassaActionsDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMoneyToKassa(KassaActionsDTO kassaActionsDTO)
        {
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
    
            kassaActionsDTO.Budget = _kassaService.Budget(2);
            if (kassaActionsDTO.Description==null)
            {
                ModelState.AddModelError("","Açıqlama qeyd etmədən pul əlavə edilə bilməz!");
                return View(kassaActionsDTO);
            }
            KassaActionList kassaActionList = new KassaActionList();
            kassaActionList.LastOutMoneyDate =(DateTime)kassaActionsDTO.ProcessDate;
            kassaActionList.AppUserId = appUser.Id;
            kassaActionList.FilialId = 2;
            kassaActionList.Description = kassaActionsDTO.Description;
            kassaActionList.OutMoneyQuantity = kassaActionsDTO.OutMoney;
            kassaActionList.Status = true;
            _kassaActionListService.Create(kassaActionList);

            return RedirectToAction("KassaActionList");
        }


    }
}
