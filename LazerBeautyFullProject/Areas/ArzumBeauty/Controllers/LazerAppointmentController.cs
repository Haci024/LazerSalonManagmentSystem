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

namespace LazerBeautyFullProject.Areas.ArzumBeauty.Controllers
{
   
    [Area("ArzumBeauty")]
    public class LazerAppointmentController : Controller
    {
        private readonly ILazerAppointmentService _appointmentService;
        private readonly AppDbContext _db;
        private readonly ICustomerService _customerService;
        private readonly ILazerAppointmentsReportService _lazerReports;
        private readonly UserManager<AppUser> _userManager;
        public LazerAppointmentController(ILazerAppointmentService appointmentService, ILazerAppointmentsReportService lazerReports, AppDbContext db, ICustomerService customerService, UserManager<AppUser> userManager)
        {
            _db = db;
            _appointmentService = appointmentService;
            _customerService = customerService;
            _lazerReports = lazerReports;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult LazerMasterPage(int LazerMasterId)
        {
            ViewBag.LazerMaster = _db.LazerMasters.Where(x => x.Id == LazerMasterId).Select(x => x.FullName).FirstOrDefault();
            LazerMasterPageDTO lazerMasterPageDTO = new LazerMasterPageDTO();
            lazerMasterPageDTO.CustomerList = _customerService.GetList();
            lazerMasterPageDTO.LazerAppointments = _appointmentService.GetAllSuccecfullyAppointments();
            lazerMasterPageDTO.LazerMasterId = LazerMasterId;
            return View(lazerMasterPageDTO);
        }
        [HttpGet]
        public async Task<IActionResult> AddLazerAppointment(int LazerMasterId, bool Female, int CustomerId)
        {
            ViewBag.LazerMasterId = LazerMasterId;
            ViewBag.LazerMaster = _db.LazerMasters.Where(x => x.Id == LazerMasterId).Select(x => x.FullName).FirstOrDefault();
            LazerAppointmentDTO lazerAppointmentDTO = new LazerAppointmentDTO();
            AppUser AppUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (Female==true)
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId==1).ToListAsync();
            }
            else
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId ==15).ToListAsync();
            }
           
            lazerAppointmentDTO.CustomerName = await _db.Customers.Where(c => c.Id == CustomerId).Select(x => x.FullName).FirstOrDefaultAsync();
            lazerAppointmentDTO.LazerMasterName = _db.LazerMasters.Where(x => x.Id == LazerMasterId).Select(x => x.FullName).FirstOrDefault();
            lazerAppointmentDTO.LazerMasterId = LazerMasterId;
            List<Customer> customers = _customerService.GetList();
            return View(lazerAppointmentDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddLazerAppointment(int LazerMasterId, bool Female, int CustomerId, LazerAppointmentDTO lazerAppointmentDTO)
        {
            ViewBag.LazerMasterId = LazerMasterId;
            ViewBag.LazerMaster = _db.LazerMasters.Where(x => x.Id == LazerMasterId).Select(x => x.FullName).FirstOrDefault();
            if (Female==true)
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 1).ToListAsync();
            }
            else
            {
                lazerAppointmentDTO.ChildCategory = await _db.LazerCategories.Include(x => x.MainCategory).ThenInclude(x => x.ChildCategories).Where(x => x.MainCategoryId == 15).ToListAsync();
            }
            AppUser AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

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
            lazerAppointment.FilialId = AppUser.FilialId;
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
            UpdateReservationDTO updateReservationDTO = new UpdateReservationDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            updateReservationDTO.IsStart = lazerAppointment.IsStart;
            updateReservationDTO.CustomerName = _db.Customers.Where(x => x.FullName == lazerAppointment.Customers.FullName).Select(x => x.FullName).FirstOrDefault();
            updateReservationDTO.LazerMasterName = _db.LazerMasters.Where(x => x.FullName == lazerAppointment.LazerMaster.FullName).Select(x => x.FullName).FirstOrDefault();
            updateReservationDTO.LazerMasterId = lazerAppointment.LazerMasterId;
            
            
            return View(updateReservationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReservationStatus(int AppointmentId, UpdateReservationDTO updateReservationDTO)
        {
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

            LazerAppointment lazerAppointment =await _appointmentService.SelectLazerAppointment(AppointmentId);
            lazerAppointment.AppUserId = appUser.Id;
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
                    lazerAppointment.IsDeleted = true;
                    _appointmentService.Update(lazerAppointment);
                    return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
                }
            }
            lazerAppointment.IsStart = true;

            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("LazerMasterPage", "LazerAppointment",new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public async Task<IActionResult> CompletedReservationStatus(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
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
            LazerAppointment lazerAppointment = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).FirstOrDefault(x => x.Id == AppointmentId);
            lazerAppointment.AppUserId = appUser.Id;
            lazerAppointment.EndTime = completedReservationStatusDTO.EndTime;
            lazerAppointment.NextSessionDate = lazerAppointment.EndTime.Value.AddDays(30);
            lazerAppointment.ImplusCount = completedReservationStatusDTO.ImpulsCount;

            Kassa budget = _db.Budget.FirstOrDefault();
            budget.Budget = budget.Budget + lazerAppointment.Price;
            if (completedReservationStatusDTO.IsCompleted)
            {
                lazerAppointment.IsCompleted = true;
                lazerAppointment.IsContiued = false;
                _appointmentService.Update(lazerAppointment);
                _db.Update(budget);
                _db.SaveChanges();
                return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
            }
            lazerAppointment.IsCompleted = true;
            lazerAppointment.IsContiued = true;
            _appointmentService.Update(lazerAppointment);
            _db.Update(budget);
            _db.SaveChanges();
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
            LazerAppointment lazerAppointment = _appointmentService.GetById(LazerId);
            lazerAppointment.AppUserId = appUser.Id;

            lazerAppointment.PriceUpdateDescription = lazerPriceUpdateDTO.Desciption;
            lazerAppointment.Price = lazerPriceUpdateDTO.Price;

            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public IActionResult KorreksionDateList() {

            List<LazerAppointment> lazerAppointments = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.EndTime.Value.Day == DateTime.Today.Day && x.EndTime.Value.Year == DateTime.Today.Year).ToList();

            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult SuccessfullyAppointments()
        {
            List<LazerAppointment> lazerAppointments = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).ToList();
            return View(lazerAppointments);
        }
   

        [HttpGet]
        public IActionResult RememberedMonthlySession()
        {
            DateTime today = DateTime.Today.Date;
            List<LazerAppointment> lazerAppointments = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.NextSessionDate.Value.Date == today).ToList();
            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult InCompleteSessionList()
        {
            List<LazerAppointment> lazerAppointments = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Where(x => x.IsContiued == true).ToList();

            return View(lazerAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> AllReservations()
        {
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            List<LazerAppointment> lazerAppointments= _appointmentService.AllReservations(appUser);
           
            return View(lazerAppointments);
        }
        [HttpGet]
        public IActionResult UpdateReservationTime(int AppointmentId)
        {

            LazerAppointment lazerAppointment = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefault();

            UpdateReservationTimeDTO updateReservationTimeDTO = new UpdateReservationTimeDTO();
            updateReservationTimeDTO.NewStartTime = lazerAppointment.ReservationDate;
            


            return View(updateReservationTimeDTO);
        }
        [HttpPost]
        public IActionResult UpdateReservationTime(int AppointmentId,UpdateReservationTimeDTO updateReservationTimeDTO)
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
            LazerAppointment lazerAppointment = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefault();
            UpdateReservationTimeDTO updateReservationDTO = new UpdateReservationTimeDTO();
            lazerAppointment.ReservationDate = updateReservationTimeDTO.NewStartTime;
            
          
            _appointmentService.Update(lazerAppointment);

            return RedirectToAction("LazerMasterPage", "LazerAppointment", new { LazerMasterId = lazerAppointment.LazerMasterId });
        }
        [HttpGet]
        public IActionResult SesstionTime(int AppointmentId)
        {

            LazerAppointment lazerAppointment = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefault();

           
            UpdateReservationTimeDTO updateReservationTimeDTO=new UpdateReservationTimeDTO();



            return View(updateReservationTimeDTO);
        }
        [HttpPost]
        public IActionResult SesstionTime(int AppointmentId, UpdateReservationTimeDTO updateReservationTimeDTO)
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
            LazerAppointment lazerAppointment = _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefault();
            UpdateReservationTimeDTO updateReservationDTO = new UpdateReservationTimeDTO();
            lazerAppointment.InCompleteStartTime =updateReservationTimeDTO.NewStartTime;


            _appointmentService.Update(lazerAppointment);

            return RedirectToAction("InCompleteSessionList", "LazerAppointment");
        }

        [HttpGet]
        public async Task<IActionResult> CompletelySecondSessionStart(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
        CompletelySecondSessionStartDTO korreksionDateDTO = new CompletelySecondSessionStartDTO();
            korreksionDateDTO.Customer = lazerAppointment.Customers.FullName;
            korreksionDateDTO.Lazeroloq = lazerAppointment.LazerMaster.FullName;

            
            return View(korreksionDateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CompletelySecondSessionStart(int AppointmentId,CompletelySecondSessionStartDTO korreksionDateDTO)
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
            LazerAppointment lazerAppointment = await _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
            lazerAppointment.IsContiued=lazerAppointment.IsContiued;
            lazerAppointment.InCompleteStartTime = korreksionDateDTO.StartDate2;
            
            lazerAppointment.StartForSecond = true;
            _appointmentService.Update(lazerAppointment);
            return RedirectToAction("InCompleteSessionList");
        }
      
        [HttpGet]
        public async Task<IActionResult> CompletelySecondSessionEnd(int AppointmentId)
        {
            LazerAppointment lazerAppointment = await _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
            CompletelySecondSessionEndDTO korreksionDateDTO = new CompletelySecondSessionEndDTO();
            korreksionDateDTO.CustomerFullName = lazerAppointment.Customers.FullName;
            korreksionDateDTO.CustomerFullName=lazerAppointment.Customers.FullName;
            korreksionDateDTO.ImpulsCount = lazerAppointment.ImplusCount;
            korreksionDateDTO.Lazeroloq = lazerAppointment.LazerMaster.FullName;
            return View(korreksionDateDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CompletelySecondSessionEnd(int AppointmentId, CompletelySecondSessionEndDTO korreksionDateDTO)
        {
            LazerAppointment lazerAppointment = await _db.LazerAppointments.Include(x => x.Customers).Include(x => x.LazerMaster).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
            if (lazerAppointment.ImplusCount>korreksionDateDTO.ImpulsCount)
            {
                ModelState.AddModelError("","Impuls sayı azaldıla bilməz!");
               
                return View(korreksionDateDTO);
            }
            if (korreksionDateDTO.InCompleteEnd==null)
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
