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

namespace LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
    [Area("ArzumEstetic")]
    [Authorize]
    public class PirsinqAppointmentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILazerMasterService _master;
        private readonly IPirsinqAppointmentService _appointment;
        private readonly ICustomerService _customer;
        private readonly TimeHelper _timeHelper;
       
        private readonly UserManager<AppUser> _userManager;
        public PirsinqAppointmentController(AppDbContext appDbContext, UserManager<AppUser> user, ICustomerService customerService, ILazerMasterService lazerMasterService, IPirsinqAppointmentService appointment)
        {

            _db = appDbContext;
            _master = lazerMasterService;
            _appointment = appointment;
            _customer = customerService;
            _timeHelper = new TimeHelper();
            _userManager = user;
            

        }
        [HttpGet]
        public IActionResult PirsinqMasterPage(int PirsinqMasterId)
        {
            LazerMaster lazerMaster = _master.GetById(PirsinqMasterId);
            PirsinqMasterPageDTO masterPageDTO = new PirsinqMasterPageDTO();
            masterPageDTO.PirsinqMasterId = lazerMaster.Id;
            ViewBag.PirsinqMaster = lazerMaster.FullName;
            masterPageDTO.Customers = _db.Customers.Include(x => x.Filial).Where(x => x.IsDeactive == false).ToList();
            masterPageDTO.ReservationList = _db.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.AppUser).Where(x => x.IsCompleted == false && x.FilialId == 3).ToList();
            masterPageDTO.InjectionList = _db.PirsinqAppointments.Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Include(x => x.LazerMaster).Include(x => x.AppUser).Where(x => x.IsDeactive == true && x.FilialId == 3).ToList();

            return View(masterPageDTO);
        }
        [HttpGet]
        public IActionResult AddPirsinqAppointment(int CustomerId, int PirsinqMasterId, bool Female)
        {
            AddPirsinqAppointmentDTO addNewAppointmentDTO = new AddPirsinqAppointmentDTO();
            addNewAppointmentDTO.PirsinqMasterId = PirsinqMasterId;
            Customer customer = _customer.GetById(CustomerId);
            LazerMaster master = _master.GetById(PirsinqMasterId);
            addNewAppointmentDTO.PirsinqMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            if (Female == true)
            {
                addNewAppointmentDTO.PirsinqCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 1).ToList();
            }
            else
            {
                addNewAppointmentDTO.PirsinqCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 1).ToList();
            }


            return View(addNewAppointmentDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPirsinqAppointment(int CustomerId, int PirsinqMasterId, bool Female, AddPirsinqAppointmentDTO addNewAppointmentDTO)
        {

            ViewBag.PirsinqMaster = _db.LazerMasters.Where(x => x.Id == PirsinqMasterId).Select(x => x.FullName);
            ViewBag.PirsinqMasterId = PirsinqMasterId;
            if (Female == true)
            {
                addNewAppointmentDTO.PirsinqCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 1).ToList();
            }
            else
            {
                addNewAppointmentDTO.PirsinqCategories = _db.PirsinqCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 1).ToList();
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
            LazerMaster master = _master.GetById(PirsinqMasterId);
            Customer customer = _customer.GetById(CustomerId);

            addNewAppointmentDTO.PirsinqMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            PirsinqAppointment PirsinqAppointment = new PirsinqAppointment();
            PirsinqAppointment.CustomerId = CustomerId;
            PirsinqAppointment.LazerMasterId = PirsinqMasterId;
            PirsinqAppointment.FilialId = 3;
            PirsinqAppointment.Price = addNewAppointmentDTO.Price;
            PirsinqAppointment.StartTime = _timeHelper.ConvertToAzerbaijanTime(addNewAppointmentDTO.StartDate);
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

            appointment.EndTime = _timeHelper.ConvertToAzerbaijanTime((DateTime)completeSession.EndTime);
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