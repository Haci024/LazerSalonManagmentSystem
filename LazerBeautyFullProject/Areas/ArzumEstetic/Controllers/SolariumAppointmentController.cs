using Business.IServices;
using Business.ValidationRules.CustomerValidator;
using Data.Concrete;
using DTO.DTOS.CustomerDTO;
using DTO.DTOS.SolariumDTO;
using Entity.Concrete;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Runtime.CompilerServices;
using Validation.ValidationRules.SolariumValidator;

namespace LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
    [Area("ArzumEstetic")]
    [Authorize]
    public class SolariumAppointmentController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICustomerService _customerService;
        private readonly ISolariumAppointmentService _solariumAppointmentService;
        private readonly ISolariumUsingListService _solariumUsingListService;
        private readonly UserManager<AppUser> _userManager;
        private readonly TimeHelper _TimeHelper;

        public SolariumAppointmentController(AppDbContext db, ICustomerService customerService,UserManager<AppUser> appUser, ISolariumAppointmentService solariumAppointmentService, ISolariumUsingListService solariumUsingListService)
        {
            _appDbContext = db;
            _customerService = customerService;
            _solariumAppointmentService = solariumAppointmentService;
            _solariumUsingListService = solariumUsingListService;
            _userManager = appUser;
            
            _TimeHelper = new TimeHelper();

        }
        [HttpGet]
        public IActionResult SolariumAppointmentList()
        {
            SolariumPageDTO solariumPageDTO = new SolariumPageDTO();
            solariumPageDTO.ActivePackets = _appDbContext.SolariumAppointments.Include(x => x.AppUser).Include(x => x.Customer).Include(x => x.SolariumCategories).Include(x => x.AppUser).Where(x=>x.IsCompleted ==false && x.IsTimeOut==false && x.IsDeleted==false).ToList();
            solariumPageDTO.Customers=_appDbContext.Customers.Include(x=>x.Filial).Where(x=>x.IsDeactive==false).ToList();
            solariumPageDTO.SuccessfullyPackets=_appDbContext.SolariumAppointments.Include(x => x.Customer).Include(x => x.SolariumCategories).Include(x => x.AppUser).Where(x=>x.IsCompleted==true).ToList();
            solariumPageDTO.TimeOutPackets = _appDbContext.SolariumAppointments.Include(x => x.Customer).Include(x=>x.AppUser).Include(x => x.SolariumCategories).Include(x => x.AppUser).Include(x => x.Filial).Where(x=>x.IsTimeOut==true).ToList();
            solariumPageDTO.InjectionList = _appDbContext.SolariumAppointments.Include(x => x.Customer).Include(x=>x.AppUser).Include(x => x.SolariumCategories).Include(x => x.AppUser).Include(x => x.Filial).Where(x=>x.IsDeleted==true).ToList();

            return View(solariumPageDTO);
        }
        [HttpGet]
        public IActionResult DeleteAppointment(int AppointmentId)
        {
              SolariumAppointment solariumAppointment=_solariumAppointmentService.GetById(AppointmentId);
                solariumAppointment.IsTimeOut = true;
               _solariumAppointmentService.Update(solariumAppointment);

            return RedirectToAction("SolariumAppointmentList", "SolariumAppointment");
        }
        [HttpGet]
        public IActionResult AddSolariumAppointment(int CustomerId)
        {

            SolariumAddDTO solariumAddDTO = new SolariumAddDTO();
            Customer customer=_customerService.GetById(CustomerId);
            solariumAddDTO.SolariumCategories = _appDbContext.SolariumCategories.Include(x => x.ChildCategories).Where(x=>x.IsDeactive==false).ToList();
            solariumAddDTO.CustomerName = customer.FullName;



            return View(solariumAddDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSolariumAppointment(int CustomerId,SolariumAddDTO solariumAddDTO)
        {
            solariumAddDTO.SolariumCategories =await _appDbContext.SolariumCategories.Include(x => x.ChildCategories).Where(x=>x.IsDeactive==false).ToListAsync();
            AppUser AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var validator = new SolariumValidator();
            var validationResult = validator.Validate(solariumAddDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(solariumAddDTO);
            }
            SolariumAppointment solariumAppointment = new SolariumAppointment();
            if (solariumAppointment.CustomerId == null)
            {
                ModelState.AddModelError("", "Müştəri mövcud deyil!");
                return View(solariumAddDTO);
            }
            solariumAppointment.MinuteLimit = solariumAddDTO.MinuteLimit;
            solariumAppointment.RemainingMinute = solariumAddDTO.MinuteLimit;
            solariumAppointment.CustomerId = CustomerId;
            solariumAppointment.Price = solariumAddDTO.Price;
            solariumAppointment.BuyingDate = _TimeHelper.GetAzerbaijanTime();
            solariumAppointment.FilialId = 3;
            solariumAppointment.AppUserId = AppUser.Id;
            solariumAppointment.IsCompleted = false;
            solariumAppointment.ReturnMoney = 0;
            solariumAppointment.IsDeleted = false;
            
            
            
          
            solariumAppointment.SolariumCategoriesId = _appDbContext.SolariumCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.Id == solariumAddDTO.ChildCategoryId).Select(x => x.Id).FirstOrDefault();
            int? solariumPacketDays =_appDbContext.SolariumCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.Id == solariumAddDTO.ChildCategoryId).Select(x => x.UsingPeriod).FirstOrDefault();
            solariumAppointment.RemainingTime = solariumAppointment.BuyingDate.AddDays((double)solariumPacketDays);
            _solariumAppointmentService.Create(solariumAppointment);
          
            return RedirectToAction("SolariumAppointmentList", "SolariumAppointment");
        }
        public IActionResult MonthlySessionList(int CustomerId, int SolariumId)
        {
            List<SolariumUsingList> solariumUsingLists = _appDbContext.SolariumUsingLists.Include(x=>x.AppUser).Include(x => x.SolariumAppointment).Include(x=>x.SolariumAppointment).ThenInclude(x=>x.AppUser).Where(x => x.SolariumAppointment.Id == SolariumId).ToList();
            ViewBag.CustomerName = _appDbContext.Customers.Where(x => x.Id == CustomerId).Select(x => x.FullName).FirstOrDefault();

            return View(solariumUsingLists);
        }
        [HttpGet]
        public IActionResult AddSolariumUsingSession(int CustomerId, int SolariumId)
        {
            ViewBag.CustomerName = _appDbContext.Customers.Where(x => x.Id == CustomerId).Select(x => x.FullName).FirstOrDefault();
            UsingListAddAppointmentDTO Session = new UsingListAddAppointmentDTO();
            Session.RemainingMinute = _appDbContext.SolariumAppointments.Where(x => x.Id == SolariumId).Select(x => x.RemainingMinute).FirstOrDefault();
            return View(Session);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSolariumUsingSession(int CustomerId, int SolariumId, UsingListAddAppointmentDTO Session)
        {
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            SolariumAppointment solariumAppointment=_solariumAppointmentService.GetById(SolariumId);
            Session.RemainingMinute=solariumAppointment.RemainingMinute;
            var validator = new UsingListValidator();
            var validationResult = validator.Validate(Session);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(Session);
            }
            SolariumUsingList solariumUsingList = new SolariumUsingList();
           
            if (solariumAppointment.RemainingMinute < Session.UsingMinute)
            {
                ModelState.AddModelError("", "İstifadə müddəti qalan müddətdən çox  ola blməz!");
                return View(Session);
            }
            solariumAppointment.UsingMinute = solariumAppointment.UsingMinute + Session.UsingMinute;
            solariumAppointment.RemainingMinute = solariumAppointment.RemainingMinute - Session.UsingMinute;
            solariumUsingList.UsingMinute = Session.UsingMinute;
            solariumUsingList.RemainingMinute = solariumAppointment.RemainingMinute;
            solariumUsingList.SolariumAppointmentId = solariumAppointment.Id;
            solariumUsingList.AppUserId =appUser.Id;
            solariumUsingList.UsingDate = _TimeHelper.ConvertToAzerbaijanTime(DateTime.Now);
            if (solariumAppointment.RemainingMinute==0)
            {
                solariumAppointment.IsCompleted = true;
                _solariumAppointmentService.Update(solariumAppointment);
                _solariumUsingListService.Create(solariumUsingList);


                return RedirectToAction("SolariumAppointmentList", "SolariumAppointment");
            }
            _solariumAppointmentService.Update(solariumAppointment);
            _solariumUsingListService.Create(solariumUsingList);


            return RedirectToAction("SolariumAppointmentList", "SolariumAppointment");
        }
  
    

        public JsonResult GetChildCategories(int id)
        {
            var childCategories = _appDbContext.SolariumCategories
                .Where(x => x.MainCategoryId == id)
                .Select(x => new { Id = x.Id, Name = x.Name })
                .ToList();

            return Json(childCategories);
        }
        [HttpGet]
        public IActionResult GetSolariumData(DateTime startDate, DateTime endDate)
        {
            try
            {
                var data = _appDbContext.SolariumAppointments
                    .Where(appointment => appointment.BuyingDate >= startDate && appointment.BuyingDate <= endDate)
                    .Select(appointment => new
                    {
                        appointment.BuyingDate,
                        appointment.Customer.FullName,
                        appointment.Customer.PhoneNumber,
                        appointment.Price

                    })
                    .ToList();
                decimal totalPrice = data.Sum(item => item.Price);

                return Json(new { Data = data, TotalPrice = totalPrice });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpGet]
        public IActionResult InjectionPacket(int AppointmentId) {
        
            SolariumAppointment solariumAppointment=_solariumAppointmentService.GetById(AppointmentId);
           InjectionPacketDTO injectionPacketDTO = new InjectionPacketDTO();
            injectionPacketDTO.PacketValue = solariumAppointment.Price;
            injectionPacketDTO.ReturnMoney = 0;
           solariumAppointment.Description = injectionPacketDTO.Description;

            return View(injectionPacketDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> InjectionPacket(int AppointmentId,InjectionPacketDTO injectionPacketDTO)
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            SolariumAppointment solariumAppointment=_solariumAppointmentService.GetById(AppointmentId);
            
            if (injectionPacketDTO.Description==null)
            {
                ModelState.AddModelError("", "Açıqlama olmadan paketdən imtina edilə bilməz!");
               
                return View(injectionPacketDTO);
            }
            solariumAppointment.Price = solariumAppointment.Price- injectionPacketDTO.ReturnMoney;
            solariumAppointment.ReturnMoney=injectionPacketDTO.ReturnMoney;
            solariumAppointment.Description=injectionPacketDTO.Description;
            solariumAppointment.IsDeleted=true;
            solariumAppointment.AppUserId = AppUser.Id;
            _solariumAppointmentService.Update(solariumAppointment);

            return RedirectToAction("SolariumAppointmentList", "SolariumAppointment");
        }

    }
}
