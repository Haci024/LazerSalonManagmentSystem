using Business.IServices;
using Business.ValidationRules.CustomerValidator;
using Business.ValidationRules.LazerValidator;
using Data.Concrete;
using Data.DAL;
using DTO.DTOS.CustomerDTO;
using DTO.DTOS.LazerAppointmentDTO;
using Entity.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
   
    [Area("ArzumEstetic")]
    [Authorize]
    public class LazerAppointmentController : Controller
    {
        private readonly ILazerAppointmentService _appointmentService;
        private readonly AppDbContext _db;
        private readonly ICustomerService _customerService;
        private readonly ILazerAppointmentsReportService _lazerReports;
      
        private readonly UserManager<AppUser> _userManager;
        private readonly ILazerMasterService _lazerMasterService;
        public LazerAppointmentController(ILazerAppointmentService appointmentService, ILazerMasterService lazerMasterService, ILazerAppointmentsReportService lazerReports, AppDbContext db, ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _db = db;
            _appointmentService = appointmentService;
            _customerService = customerService;
            _lazerReports = lazerReports;
            _userManager = userManager;
            _lazerMasterService = lazerMasterService;
        }

        [HttpGet]
        public async Task<IActionResult> LazerMasterPage(int LazerMasterId)
        {
            LazerMaster lazerMaster = _lazerMasterService.GetById(LazerMasterId);
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.LazerMaster = lazerMaster.FullName;
            LazerMasterPageDTO lazerMasterPageDTO = new LazerMasterPageDTO();
            lazerMasterPageDTO.CustomerList =await _customerService.GetFemaleList();
            lazerMasterPageDTO.LazerAppointments = await _appointmentService.ReservationsForMaster(3,LazerMasterId);
            lazerMasterPageDTO.Injections = await _appointmentService.InjectionsForMaster(3, LazerMasterId);

            lazerMasterPageDTO.LazerMasterId = LazerMasterId;
            return View(lazerMasterPageDTO);
        }
        [HttpGet]
        public async Task<IActionResult> AddLazerAppointment(int LazerMasterId, bool Female, int CustomerId)
        {
            ViewBag.LazerMasterId = LazerMasterId;
            LazerMaster lazerMaster = _lazerMasterService.GetById(LazerMasterId);
            ViewBag.LazerMaster = lazerMaster.FullName;
            Customer customer = _customerService.GetById(CustomerId);
            LazerAppointmentDTO lazerAppointmentDTO = new LazerAppointmentDTO();
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (Female == true)
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 5).ToListAsync();
            }
            else
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 5).ToListAsync();
            }

            lazerAppointmentDTO.CustomerName = customer.FullName;
            lazerAppointmentDTO.LazerMasterName = lazerMaster.FullName;
            lazerAppointmentDTO.LazerMasterId = LazerMasterId;

            return View(lazerAppointmentDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddLazerAppointment(int LazerMasterId, bool Female, int CustomerId, LazerAppointmentDTO lazerAppointmentDTO)
        {
            ViewBag.LazerMasterId = LazerMasterId;
            ViewBag.LazerMaster = _db.LazerMasters.Where(x => x.Id == LazerMasterId).Select(x => x.FullName).FirstOrDefault();
            if (Female == true)
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 5).ToListAsync();
            }
            else
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 5).ToListAsync();
            }
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            var validator = new LazerValidator();
            var validationResult = validator.Validate(lazerAppointmentDTO);
            if (!validationResult.IsValid)
            {
                foreach (var validationRules in validationResult.Errors)
                {
                    ModelState.AddModelError("", validationRules.ErrorMessage);
                }
                return View(lazerAppointmentDTO);
            }
            LazerAppointment lazerAppointment = new LazerAppointment();
            lazerAppointment.FilialId =3;
            lazerAppointment.CustomerId = CustomerId;
            lazerAppointment.AppUserId = AppUser.Id;
            lazerAppointment.Price = lazerAppointmentDTO.Price;
            lazerAppointment.LazerMasterId = LazerMasterId;
            lazerAppointment.ReservationDate = lazerAppointmentDTO.ReservationDate;
            lazerAppointment.ImplusCount = 0;
          
            List<LazerAppointmentReports> reports = new List<LazerAppointmentReports>();
            foreach (int childcategories in lazerAppointmentDTO.lazerCategoriesId)
            {
                LazerAppointmentReports LazerReport = new LazerAppointmentReports
                {
                    LazerCategoryId = childcategories,
                };
                reports.Add(LazerReport);
            }

            lazerAppointment.LazerAppointmentReports = reports;
            _appointmentService.Create(lazerAppointment);
            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = LazerMasterId });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateReservationStatus(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
            Customer customer = _customerService.GetById(lazerAppointment.CustomerId);
            LazerMaster lazerMaster = _lazerMasterService.GetById(lazerAppointment.LazerMasterId);
            UpdateReservationDTO updateReservationDTO = new UpdateReservationDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            updateReservationDTO.IsStart = lazerAppointment.IsStart;
            updateReservationDTO.CustomerName = customer.FullName;
            updateReservationDTO.LazerMasterName = lazerMaster.FullName;
            updateReservationDTO.LazerMasterId = lazerAppointment.LazerMasterId;


            return View(updateReservationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationStatus(int AppointmentId, UpdateReservationDTO updateReservationDTO)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
            updateReservationDTO.LazerMasterId=lazerAppointment.LazerMasterId;
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var validator = new UpdateReservationValidator();
            var validationResult = validator.Validate(updateReservationDTO);
            if (!validationResult.IsValid)
            {
                foreach (var validationRules in validationResult.Errors)
                {
                    ModelState.AddModelError("", validationRules.ErrorMessage);
                }
                return View(updateReservationDTO);
            }

           
           
            updateReservationDTO.LazerMasterId = lazerAppointment.LazerMasterId;

            lazerAppointment.StartTime = updateReservationDTO.StartTime;

            if (updateReservationDTO.IsStart == false)
            {
                if (updateReservationDTO.Description == null)
                {
                    ModelState.AddModelError("", "İmtina olunma səbəbi boş ola bilməz!");

                    return View(updateReservationDTO);
                }
                else
                {
                    lazerAppointment.Decription = updateReservationDTO.Description;
                    lazerAppointment.StartTime = updateReservationDTO.StartTime;
                    lazerAppointment.IsStart = false;
                    lazerAppointment.IsCompleted = false;
                    lazerAppointment.IsDeleted = true;
                    _appointmentService.Update(lazerAppointment);
                    return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
                }
            }
            lazerAppointment.IsStart = true;

            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public async Task<IActionResult> CompletedReservationStatus(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
            CompletedReservationStatusDTO completedReservationStatusDTO = new CompletedReservationStatusDTO();
            completedReservationStatusDTO.EndTime = lazerAppointment.EndTime;

            completedReservationStatusDTO.CustomerName = lazerAppointment.Customers.FullName;
            completedReservationStatusDTO.LazerMasterName = lazerAppointment.LazerMaster.FullName;
            completedReservationStatusDTO.LazerMasterId = lazerAppointment.LazerMasterId;
            return View(completedReservationStatusDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CompletedReservationStatus(int AppointmentId, CompletedReservationStatusDTO completedReservationStatusDTO)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
            completedReservationStatusDTO.LazerMasterId = lazerAppointment.LazerMasterId;
            var validator = new CompletedReservationStatusValidator();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            var validationResult = validator.Validate(completedReservationStatusDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(completedReservationStatusDTO);
            }
      
            lazerAppointment.AppUserId = appUser.Id;
            lazerAppointment.EndTime = completedReservationStatusDTO.EndTime;
            lazerAppointment.NextSessionDate = lazerAppointment.EndTime.Value.AddDays(29);
            lazerAppointment.ImplusCount = completedReservationStatusDTO.ImpulsCount;
            if (completedReservationStatusDTO.IsCompleted)
            {
                lazerAppointment.IsCompleted = true;
                lazerAppointment.IsContiued = false;
                _appointmentService.Update(lazerAppointment);
                
                return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
            }
            lazerAppointment.IsCompleted = true;
            lazerAppointment.IsContiued = true;
            _appointmentService.Update(lazerAppointment);
          
            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]

        public async Task<IActionResult> UpdatePrice(int LazerId)
        {

            LazerPriceUpdateDTO lazerPriceUpdateDTO = new LazerPriceUpdateDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            LazerAppointment lazerAppointment = _appointmentService.GetById(LazerId);
            lazerPriceUpdateDTO.LazerMasterId = lazerAppointment.LazerMasterId;
            lazerPriceUpdateDTO.Price = lazerAppointment.Price;

            return View(lazerPriceUpdateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdatePrice(int LazerId, LazerPriceUpdateDTO lazerPriceUpdateDTO)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(LazerId);
            lazerPriceUpdateDTO.LazerMasterId = lazerAppointment.LazerMasterId;
            var validator = new UpdatePriceValidator();
            var validationResult = validator.Validate(lazerPriceUpdateDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);

                }
                return View(lazerPriceUpdateDTO);
            }
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            
            lazerAppointment.AppUserId = appUser.Id;

            lazerAppointment.PriceUpdateDescription = lazerPriceUpdateDTO.Description;
            lazerAppointment.Price = lazerPriceUpdateDTO.Price;

            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public async Task<IActionResult> RememberedMonthlySession()
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<LazerAppointment> lazerAppointments = await _appointmentService.NextSessionList(3);
            return View(lazerAppointments);
        }
        [HttpGet]
        public async Task<IActionResult> InCompleteSessionList()
        {
            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<LazerAppointment> lazerAppointments = await _appointmentService.InComepletedList(3);

            return View(lazerAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> AllReservations()
        {
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            List<LazerAppointment> lazerAppointments = await _appointmentService.AllReservations(3);

            return View(lazerAppointments);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateReservationTime(int AppointmentId)
        {

            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);

            UpdateReservationTimeDTO updateReservationTimeDTO = new UpdateReservationTimeDTO();
            updateReservationTimeDTO.NewStartTime = lazerAppointment.ReservationDate;

            return View(updateReservationTimeDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationTime(int AppointmentId, UpdateReservationTimeDTO updateReservationTimeDTO)
        {
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
           
            var validator = new UpdateReservationTimeValidator();
            var validationResult = validator.Validate(updateReservationTimeDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                    return View(updateReservationTimeDTO);
                }
            }
           
            UpdateReservationTimeDTO updateReservationDTO = new UpdateReservationTimeDTO();
            lazerAppointment.ReservationDate =(DateTime)updateReservationTimeDTO.NewStartTime;
            

            _appointmentService.Update(lazerAppointment);

            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public async Task<IActionResult> SesstionTime(int AppointmentId)
        {

            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);


            UpdateReservationTimeDTO updateReservationTimeDTO = new UpdateReservationTimeDTO();
            


            return View(updateReservationTimeDTO);
        }
        [HttpPost]
        public async Task<IActionResult> SesstionTime(int AppointmentId, UpdateReservationTimeDTO updateReservationTimeDTO)
        {
            var validator = new UpdateReservationTimeValidator();
            var validationResult = validator.Validate(updateReservationTimeDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                    return View(updateReservationTimeDTO);
                }
            }
            LazerAppointment lazerAppointment = await _appointmentService.SelectLazerAppointment(AppointmentId);
            UpdateReservationTimeDTO updateReservationDTO = new UpdateReservationTimeDTO();
            lazerAppointment.InCompleteStartTime = updateReservationTimeDTO.NewStartTime;


            _appointmentService.Update(lazerAppointment);

            return RedirectToAction("InCompleteSessionList", "LazerAppointment");
        }

        [HttpGet]
        public async Task<IActionResult> CompletelySecondSessionStart(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _appointmentService.CompletetedSecondSessionStart(AppointmentId);
            CompletelySecondSessionStartDTO korreksionDateDTO = new CompletelySecondSessionStartDTO();
            korreksionDateDTO.Customer = lazerAppointment.Customers.FullName;
            korreksionDateDTO.Lazeroloq = lazerAppointment.LazerMaster.FullName;


            return View(korreksionDateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CompletelySecondSessionStart(int AppointmentId, CompletelySecondSessionStartDTO korreksionDateDTO)
        {
            var validate = new CompletelySecondSessionStartValidator();
            var validationResult = validate.Validate(korreksionDateDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(validationResult);
            }
            LazerAppointment lazerAppointment = await _appointmentService.CompletetedSecondSessionStart(AppointmentId);
            lazerAppointment.IsContiued = lazerAppointment.IsContiued;
            lazerAppointment.InCompleteStartTime = korreksionDateDTO.StartDate2;

            lazerAppointment.StartForSecond = true;
            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("InCompleteSessionList");
        }

        [HttpGet]
        public async Task<IActionResult> CompletelySecondSessionEnd(int AppointmentId)
        {

            LazerAppointment lazerAppointment = await _appointmentService.CompletelySecondSessionEnd(AppointmentId);
            CompletelySecondSessionEndDTO korreksionDateDTO = new CompletelySecondSessionEndDTO();
            korreksionDateDTO.CustomerFullName = lazerAppointment.Customers.FullName;
            korreksionDateDTO.CustomerFullName = lazerAppointment.Customers.FullName;
            korreksionDateDTO.ImpulsCount = lazerAppointment.ImplusCount;
            korreksionDateDTO.Lazeroloq = lazerAppointment.LazerMaster.FullName;
            return View(korreksionDateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CompletelySecondSessionEnd(int AppointmentId, CompletelySecondSessionEndDTO korreksionDateDTO)
        {
            LazerAppointment lazerAppointment = await _appointmentService.CompletelySecondSessionEnd(AppointmentId);
            if (lazerAppointment.ImplusCount > korreksionDateDTO.ImpulsCount)
            {
                ModelState.AddModelError("", "Impuls sayı azaldıla bilməz!");

                return View(korreksionDateDTO);
            }
            if (korreksionDateDTO.InCompleteEnd == null)
            {
                ModelState.AddModelError("", "Çıxış saatı boş ola bilməz!");
                return View(korreksionDateDTO);
            }
            lazerAppointment.IsContiued = false;
            lazerAppointment.EndForSecond = true;
            lazerAppointment.ImplusCount = korreksionDateDTO.ImpulsCount;
            lazerAppointment.InCompleteEndTime = korreksionDateDTO.InCompleteEnd;
            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("InCompleteSessionList");
        }
    }
}
