using Business.IServices;
using Business.ValidationRules.LazerValidator;
using Data.Concrete;
using DTO.DTOS.LipuckaDTO;
using Entity.Concrete;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Validation.ValidationRules.LipuckaValidator;

namespace LazerBeautyFullProject.Areas.ArzumBeauty.Controllers
{
    [Area("ArzumBeauty")]
    [Authorize]
    public class LipuckaAppointmentController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILazerMasterService _master;
        private readonly ILipuckaAppointmentService _appointment;
        private readonly ICustomerService _customer;
     
        private readonly UserManager<AppUser> _userManager;
        public LipuckaAppointmentController(AppDbContext appDbContext,UserManager<AppUser> user,ICustomerService customerService,ILazerMasterService lazerMasterService,ILipuckaAppointmentService appointment)
        {

            _db = appDbContext;
            _master = lazerMasterService;
            _appointment = appointment;
            _customer= customerService;
            _userManager = user;
           

        }
        [HttpGet]
        public IActionResult LipuckaMasterPage(int LipuckaMasterId)
        {
            LazerMaster lazerMaster = _master.GetById(LipuckaMasterId);
            LipuckaMasterPageDTO masterPageDTO = new LipuckaMasterPageDTO();
            masterPageDTO.LipuckaMasterId = LipuckaMasterId;
            ViewBag.LipuckaMaster = lazerMaster.FullName;
            masterPageDTO.Customers = _db.Customers.Include(x => x.Filial).Where(x=>x.IsDeactive==false).ToList();
            masterPageDTO.ReservationList=_db.LipuckaAppointments.Include(x=>x.LipuckaReports).ThenInclude(x=>x.LipuckaCategories).Include(x=>x.LazerMaster).Include(x=>x.AppUser).Where(x=>x.IsCompleted==false && x.FilialId==2).ToList();
            masterPageDTO.InjectionList=_db.LipuckaAppointments.Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).Include(x => x.LazerMaster).Include(x => x.AppUser).Where(x => x.IsDeleted == true && x.FilialId == 2).ToList();

            return View(masterPageDTO);
        }
        [HttpGet]
        public IActionResult AddLipuckaAppointment(int CustomerId,int LipuckaMasterId,bool Female)
        {
            ViewBag.LipuckaMasterId=LipuckaMasterId;
            AddNewAppointmentDTO addNewAppointmentDTO = new AddNewAppointmentDTO();
           
            Customer customer=_customer.GetById(CustomerId);
            LazerMaster master=_master.GetById(LipuckaMasterId);       
            addNewAppointmentDTO.LipuckaMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            if (Female==true)
            {
                addNewAppointmentDTO.LipuckaCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId==4).ToList();
            }
            else
            {
                addNewAppointmentDTO.LipuckaCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 3).ToList();
            }
         
            
            return View(addNewAppointmentDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLipuckaAppointment(int CustomerId, int LipuckaMasterId,bool Female, AddNewAppointmentDTO addNewAppointmentDTO)
        {

            ViewBag.LipuckaMaster = _db.LazerMasters.Where(x => x.Id == LipuckaMasterId).Select(x => x.FullName);
           ViewBag.LipuckaMasterId= LipuckaMasterId;
            if (Female == true)
            {
                addNewAppointmentDTO.LipuckaCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 4).ToList();
            }
            else
            {
                addNewAppointmentDTO.LipuckaCategories = _db.LipuckaCategories.Include(x => x.MainCategory).Include(x => x.ChildCategories).Where(x => x.IsDeactive == false && x.MainCategoryId != null && x.MainCategoryId == 3).ToList();
            }
            AddLipuckaAppointmentValidator validationRules = new AddLipuckaAppointmentValidator();
           
            var validationResult = validationRules.Validate(addNewAppointmentDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addNewAppointmentDTO);
            }
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            LazerMaster master = _master.GetById(LipuckaMasterId);
            Customer customer = _customer.GetById(CustomerId);

            addNewAppointmentDTO.LipuckaMaster = master.FullName;
            addNewAppointmentDTO.Customer = customer.FullName;
            LipuckaAppointment lipuckaAppointment = new LipuckaAppointment();
            lipuckaAppointment.CustomerId = CustomerId;
            lipuckaAppointment.LazerMasterId= LipuckaMasterId;
            lipuckaAppointment.FilialId = 2;
            lipuckaAppointment.Price = addNewAppointmentDTO.Price;
            lipuckaAppointment.StartTime = addNewAppointmentDTO.StartDate;
            lipuckaAppointment.AppUserId = appUser.Id;
            lipuckaAppointment.IsStart= true;
            List<LipuckaReports> lipuckaReports = new List<LipuckaReports>();
            foreach (int  categoryId in addNewAppointmentDTO.LipuckaCategoryIds)
            {
                LipuckaReports lipuckaReport = new LipuckaReports()
                {
                    LipuckaCategoriesId = categoryId
                };
                lipuckaReports.Add(lipuckaReport);

            }
            lipuckaAppointment.LipuckaReports= lipuckaReports;
            _appointment.Create(lipuckaAppointment);        
            return RedirectToAction("LipuckaMasterPage","LipuckaAppointment",new { LipuckaMasterId = master.Id});
        }
        [HttpGet]
        public IActionResult CompleteLipuckaSession(int AppointmentId)
        {
           
            LipuckaAppointment appointment = _appointment.GetById(AppointmentId);
            LazerMaster master=_master.GetById(appointment.LazerMasterId);
            Customer customer = _customer.GetById(appointment.CustomerId);
            ViewBag.LipuckaMaster = master.FullName;
            
            CompleteLipuckaAppointmentDTO completeSession=new CompleteLipuckaAppointmentDTO();
            completeSession.LipuckaMasterId = master.Id;
            completeSession.Customer = customer.FullName;
            completeSession.Master = master.FullName;
            completeSession.Price=appointment.Price;
            
            

            return View(completeSession);
        }
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteLipuckaSession(int AppointmentId, CompleteLipuckaAppointmentDTO completeSession)
        {
            LipuckaAppointment appointment = _appointment.GetById(AppointmentId);
            ViewBag.LipuckaMaster = _db.LazerMasters.Where(x => x.Id == appointment.LazerMasterId).Select(x => x.FullName);
            ViewBag.LipuckaMasterId = appointment.LazerMasterId;
            completeSession.LipuckaMasterId=appointment.Id;
            var completeSessionValidator = new CompleteSessionValidator();
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
            return RedirectToAction("LipuckaMasterPage", "LipuckaAppointment",new {LipuckaMasterId=appointment.LazerMasterId});
        }
        [HttpGet]
        public IActionResult UpdateMoney(int AppointmentId)
        {
            LipuckaPriceUpdateDTO appointmentDTO = new LipuckaPriceUpdateDTO();
            LipuckaAppointment lipuckaAppointment=_appointment.GetById(AppointmentId);
            appointmentDTO.Price = lipuckaAppointment.Price;
            appointmentDTO.LipuckaMasterId = lipuckaAppointment.LazerMasterId;
            return View(appointmentDTO);
        }
        [HttpPost]
        public IActionResult UpdateMoney(int AppointmentId, LipuckaPriceUpdateDTO updateReservation)
        {
            LipuckaAppointment appointment = _appointment.GetById(AppointmentId);
            updateReservation.LipuckaMasterId = appointment.LazerMasterId;
            var validationResult=new UpdateLipuckaPriceValidator();
            var validation=validationResult.Validate(updateReservation);
            if (!validation.IsValid)
            {
                foreach (var item in validation.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(updateReservation);
            }
            appointment.Price=updateReservation.Price;
            appointment.Description = updateReservation.Description;
            _appointment.Update(appointment);
            return RedirectToAction("LipuckaMasterPage", "LipuckaAppointment",new {LipuckaMasterId=appointment.LazerMasterId });
        }
    }
}
