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
    [Authorize]
    [Area("ArzumMini")]
    public class KassaController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly IKassaService _kassaService;
        private readonly IKassaActionListService _kassaActionListService;
        public KassaController(AppDbContext db,IKassaService kassaService,UserManager<AppUser> appUser,IKassaActionListService kassaActionListService)
        {
            _appDbContext = db;
            _kassaService= kassaService;
            _kassaActionListService =kassaActionListService;
            _userManager = appUser;
        }
        public IActionResult BudgetPage()
        {
            Kassa budget=_appDbContext.Budget.FirstOrDefault();
            DailyReportDTO dailyReportDTO = new DailyReportDTO();
           
            List<LazerAppointment> lazerAppointments = _appDbContext.LazerAppointments.Where(x=>x.EndTime.Value.Day==DateTime.Today.Day).ToList();
            List<Income> incomes = _appDbContext.Incomes.Where(x => x.IncomeDate.Day == DateTime.Today.Day).ToList();
            List<OutMoney> outMoneys = _appDbContext.OutMoney.Where(x=>x.CreateTime.Date.Day==DateTime.Today.Day).ToList();
            
           
            dailyReportDTO.Budget = budget.Budget;
            dailyReportDTO.DailyTotalOutMoney = outMoneys.Sum(x => x.Price);
            dailyReportDTO.ProceedsFromSales = incomes.Sum(x => x.Price);
            dailyReportDTO.DailyTotalInComeMoney = incomes.Sum(x => x.Price)+lazerAppointments.Sum(x=>x.Price);
            dailyReportDTO.DailyBenefitMoney=(lazerAppointments.Sum(x => x.Price) + incomes.Sum(x => x.Price)) - outMoneys.Sum(x => x.Price);
            dailyReportDTO.Master1DailyEarnings=lazerAppointments.Where(x=>x.LazerMasterId==1).Sum(x=>x.Price);
            dailyReportDTO.Master2DailyEarnings=lazerAppointments.Where(x => x.LazerMasterId == 2).Sum(x => x.Price);
            dailyReportDTO.ImplusCount = lazerAppointments.Sum(x => x.ImplusCount);
            
            return View(dailyReportDTO);
        }
        [HttpGet]
        public async Task<IActionResult> OutMoneyForOwner()
        {
            KassaActionsDTO kassaActionsDTO = new KassaActionsDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            Kassa kassa = _appDbContext.Budget.FirstOrDefault();
            kassaActionsDTO.Budget=kassa.Budget;
            return View(kassaActionsDTO);
        }
      
        [HttpPost]     
        public async  Task<IActionResult> OutMoneyForOwner(KassaActionsDTO kassaActionsListDTO)
        {   
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
            Kassa Budget = _appDbContext.Budget.FirstOrDefault();
            if (kassaActionsListDTO.OutMoney>Budget.Budget)
            {
                ModelState.AddModelError("","Kassadan çıxarmaq istədiyiniz məbləğ kassadakı məbləğdən çoxdur!");
                
                return View(kassaActionsListDTO);
            }

            KassaActionList kassaActionList= new KassaActionList();
            kassaActionList.LastOutMoneyDate = DateTime.Now;
            kassaActionList.AppUserId = appUser.Id;
            kassaActionList.KassaId= Budget.Id;
            kassaActionList.OutMoneyQuantity=kassaActionsListDTO.OutMoney;
            Budget.Budget = Budget.Budget - kassaActionList.OutMoneyQuantity;
           
            _kassaService.Update(Budget);
            _kassaActionListService.Create(kassaActionList);
            
            return RedirectToAction("KassaActionList");
        }
        public IActionResult KassaActionList()
        {
            List<KassaActionList> kassaActionLists= _appDbContext.KassaActionLists.Include(x=>x.AppUser).Include(x=>x.Kassa).ToList();  
            return View(kassaActionLists);
        }


    }
}
