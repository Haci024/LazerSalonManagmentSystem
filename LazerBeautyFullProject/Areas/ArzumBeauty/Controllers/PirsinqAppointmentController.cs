using Business.IServices;
using Business.ValidationRules.LazerValidator;
using Data.Concrete;
using DTO.DTOS.LipuckaDTO;
using DTO.DTOS.PirsinqDTO;
using Entity.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Validation.ValidationRules.CosmetologyValidator;
using Validation.ValidationRules.LipuckaValidator;
using Validation.ValidationRules.PirsinqValidator;

namespace LazerBeautyFullProject.Areas.ArzumBeauty.Controllers
{
    [Area("ArzumBeauty")]
    [Authorize]
    public class PirsinqAppointmentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILazerMasterService _master;
        private readonly IPirsinqAppointmentService _appointment;
        private readonly ICustomerService _customer;
        private readonly IPirsinqCategoriesService _Categories;
    
        private readonly UserManager<AppUser> _userManager;
        public PirsinqAppointmentController(AppDbContext appDbContext,UserManager<AppUser> user, IPirsinqCategoriesService categoryService, ICustomerService customerService, ILazerMasterService lazerMasterService, IPirsinqAppointmentService appointment)
        {

            _db = appDbContext;
            _master = lazerMasterService;
            _appointment = appointment;
            _customer = customerService;
            _Categories = categoryService;
            _userManager = user;
           

        }
        [HttpGet]
        public async Task<IActionResult> PirsinqMasterPage(int PirsinqMasterId)
        {
            LazerMaster lazerMaster = _master.GetById(PirsinqMasterId);
            PirsinqMasterPageDTO masterPageDTO = new PirsinqMasterPageDTO();
            masterPageDTO.PirsinqMasterId = lazerMaster.Id;
            ViewBag.PirsinqMaster = lazerMaster.FullName;
            masterPageDTO.Customers = await _customer.GetActiveCustomerList();
            masterPageDTO.ReservationList =await _db.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.AppUser).Where(x => x.IsCompleted == false && x.FilialId == 2).ToListAsync();
            masterPageDTO.InjectionList =await _db.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.AppUser).Where(x => x.IsDeactive == true && x.FilialId == 2).ToListAsync();

            return View(masterPageDTO);
        }
        [HttpGet]
        public async  Task<IActionResult> AddPirsinqAppointment(int CustomerId, int PirsinqMasterId)
        {
            AddPirsinqAppointmentDTO addNewAppointmentDTO = new AddPirsinqAppointmentDTO();
            addNewAppointmentDTO.PirsinqMasterId = PirsinqMasterId;
            Customer customer = _customer.GetById(CustomerId);
            LazerMaster master = _master.GetById(PirsinqMasterId);
            addNewAppointmentDTO.PirsinqMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            if (customer.Female)
            {
                addNewAppointmentDTO.PirsinqCategories = await _Categories.GetFemaleCategoryList();
            }
            else
            {
                addNewAppointmentDTO.PirsinqCategories =await  _Categories.GetMaleCategoryList();
            }


            return View(addNewAppointmentDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPirsinqAppointment(int CustomerId, int PirsinqMasterId, AddPirsinqAppointmentDTO addNewAppointmentDTO)
        {

            ViewBag.PirsinqMaster = _db.LazerMasters.Where(x => x.Id == PirsinqMasterId).Select(x => x.FullName);
            ViewBag.PirsinqMasterId = PirsinqMasterId; LazerMaster master = _master.GetById(PirsinqMasterId);
            Customer customer = _customer.GetById(CustomerId);
            if (customer.Female)
            {
                addNewAppointmentDTO.PirsinqCategories =await _Categories.GetFemaleCategoryList();
            }
            else
            {
                addNewAppointmentDTO.PirsinqCategories = await _Categories.GetMaleCategoryList();
            }
            AddPirsinqAppointmentValidator validationRules = new AddPirsinqAppointmentValidator();

            var validationResult = validationRules.Validate(addNewAppointmentDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addNewAppointmentDTO);
            }
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
          

            addNewAppointmentDTO.PirsinqMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            PirsinqAppointment PirsinqAppointment = new PirsinqAppointment();
            PirsinqAppointment.CustomerId = CustomerId;
            PirsinqAppointment.LazerMasterId = PirsinqMasterId;
            PirsinqAppointment.FilialId = 2;
            PirsinqAppointment.Price = addNewAppointmentDTO.Price;
            PirsinqAppointment.StartTime = addNewAppointmentDTO.StartDate;
            PirsinqAppointment.AppUserId = appUser.Id;
            PirsinqAppointment.IsStart = true;
            List<PirsinqReports> PirsinqReports = new List<PirsinqReports>();
            foreach (int categoryId in addNewAppointmentDTO.PirsinqCategoryIds)
            {
                PirsinqReports lipuckaReport = new PirsinqReports()
                {
                    PirsinqCategoryId = categoryId
                };
                PirsinqReports.Add(lipuckaReport);

            }
            PirsinqAppointment.PirsinqReports = PirsinqReports;
            _appointment.Create(PirsinqAppointment);
            return RedirectToAction("PirsinqMasterPage", "PirsinqAppointment", new { PirsinqMasterId = master.Id });
        }
        [HttpGet]
        public IActionResult CompletePirsinqSession(int AppointmentId)
        {

            PirsinqAppointment appointment = _appointment.GetById(AppointmentId);
            LazerMaster master = _master.GetById(appointment.LazerMasterId);
            Customer customer = _customer.GetById(appointment.CustomerId);
            ViewBag.PirsinqMaster = master.FullName;

            CompletePirsinqAppointmentDTO completeSession = new CompletePirsinqAppointmentDTO();
            completeSession.PirsinqMasterId = master.Id;
            completeSession.Customer = customer.FullName;
            completeSession.Master = master.FullName;
            completeSession.Price = appointment.Price;



            return View(completeSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompletePirsinqSession(int AppointmentId, CompletePirsinqAppointmentDTO completeSession)
        {
            PirsinqAppointment appointment = _appointment.GetById(AppointmentId);
            ViewBag.LipuckaMaster = _db.LazerMasters.Where(x => x.Id == appointment.LazerMasterId).Select(x => x.FullName);
            ViewBag.LipuckaMasterId = appointment.LazerMasterId;
            completeSession.PirsinqMasterId = appointment.Id;
            var completeSessionValidator = new CompletePirsinqAppointmentValidator();
            var validationResult = completeSessionValidator.Validate(completeSession);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(completeSession);
            }
            appointment.IsCompleted = true;

            appointment.EndTime = completeSession.EndTime;
            appointment.Price = completeSession.Price;

            appointment.IsCompleted = true;
            _appointment.Update(appointment);
            return RedirectToAction("PirsinqMasterPage", "PirsinqAppointment", new { PirsinqMasterId = appointment.LazerMasterId });
        }
        [HttpGet]
        public IActionResult UpdateMoney(int AppointmentId)
        {
            PirsinqPriceUpdateDTO appointmentDTO = new PirsinqPriceUpdateDTO();
            PirsinqAppointment PirsinqAppointment = _appointment.GetById(AppointmentId);
            appointmentDTO.Price = PirsinqAppointment.Price;
            appointmentDTO.PirsinqMasterId = PirsinqAppointment.LazerMasterId;
            return View(appointmentDTO);
        }
        [HttpPost]
        public IActionResult UpdateMoney(int AppointmentId, PirsinqPriceUpdateDTO updateReservation)
        {
            PirsinqAppointment appointment = _appointment.GetById(AppointmentId);
            updateReservation.PirsinqMasterId = appointment.LazerMasterId;
            var validationResult = new UpdatePirsinqPriceValidator();
            var validation = validationResult.Validate(updateReservation);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(updateReservation);
            }
            appointment.Price = updateReservation.Price;
            appointment.Description = updateReservation.Description;
            _appointment.Update(appointment);
            return RedirectToAction("PirsinqMasterPage", "PirsinqAppointment", new { PirsinqMasterId = appointment.LazerMasterId });
        }
    }
}