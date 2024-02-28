﻿using Business.IServices;
using Business.ValidationRules.LazerValidator;
using Data.Concrete;
using DTO.DTOS.CosmetologyDTO;
using DTO.DTOS.LazerAppointmentDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Validation.ValidationRules.CosmetologyValidator;

namespace LazerBeautyFullProject.Areas.ArzumEstetic.Controllers
{
    [Area("ArzumEstetic")]
    [Authorize]
    public class CosmetologyController : Controller
    {
        private readonly ICosmetologyAppointmentService _cosmetologyAppointmentService;
        private readonly ICosmetologService _cosmetologService;
        private readonly AppDbContext _appDbContext;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICustomerService _customerService;
      

        public CosmetologyController(ICosmetologyAppointmentService cosmetologyAppointmentService, ICustomerService customerService, UserManager<AppUser> userManager, AppDbContext dbContext, ICosmetologService cosmetologService)
        {
            _cosmetologyAppointmentService = cosmetologyAppointmentService;
            _cosmetologService = cosmetologService;
            _customerService = customerService;
            _appDbContext = dbContext;
            _userManager = userManager;
            
        }

        public async Task<IActionResult> CosmetologPage(int CosmetologId)
        {

            Cosmetologs cosmetologs = _cosmetologService.GetById(CosmetologId);
            CosmetologPageDTO cosmetologPageDTO = new CosmetologPageDTO();
            ViewBag.Cosmetolog = cosmetologs.FullName;
            cosmetologPageDTO.CosmetologId = CosmetologId;
            cosmetologPageDTO.Customers = _appDbContext.Customers.Include(x => x.Filial).Where(x => x.IsDeactive == false).ToList();
            cosmetologPageDTO.CosmetologyAppointments = _appDbContext.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.AppUser).Include(x => x.Cosmetolog).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Where(x => x.IsCompleted == false && x.IsStart == true && x.FilialId == 3 && x.CosmetologId== CosmetologId).ToList();
            cosmetologPageDTO.InJectionAppointments = _appDbContext.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.AppUser).Include(x => x.Cosmetolog).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Where(x => x.IsStart == false && x.FilialId == 3 && x.CosmetologId==CosmetologId).ToList();
            ViewBag.CosmetologName = cosmetologs.FullName;
            return View(cosmetologPageDTO);
        }
        [HttpGet]
        public IActionResult AddNewAppointment(int CosmetologId, int CustomerId)
        {
            Cosmetologs cosmetologs = _cosmetologService.GetById(CosmetologId);
            Customer customer = _customerService.GetById(CustomerId);
            AddNewSessionDTO addNewSessionDTO = new AddNewSessionDTO();
            addNewSessionDTO.CosmetologName = cosmetologs.FullName;
            addNewSessionDTO.CustomerName = customer.FullName;
            addNewSessionDTO.CosmetologId = CosmetologId;
            addNewSessionDTO.CosmetologyCategories = _appDbContext.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId != null).ToList();

            return View(addNewSessionDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNewAppointment(int CosmetologId, int CustomerId, AddNewSessionDTO addNewSessionDTO)
        {
            var validator = new AddAppointmentValidator();
            addNewSessionDTO.CosmetologyCategories = _appDbContext.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId != null).ToList();
            var validationResult = validator.Validate(addNewSessionDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(addNewSessionDTO);
            }


            var AppUser = await _userManager.FindByNameAsync(User.Identity.Name);

            Cosmetologs cosmetologs = _cosmetologService.GetById(CosmetologId);
            Customer customer = _customerService.GetById(CustomerId);

            CosmetologyAppointment appointment = new CosmetologyAppointment();
            appointment.StartTime = DateTime.Now;
            appointment.IsStart = true;
            appointment.AppUserId = AppUser.Id;
            appointment.Price = addNewSessionDTO.Price;
            appointment.CosmetologId = CosmetologId;
            appointment.CustomerId = customer.Id;
            appointment.FilialId = 3;
            appointment.CosmetologyDescription = addNewSessionDTO.Description;
            List<CosmetologyReport> cosmetologyReports = new List<CosmetologyReport>();
            foreach (int childcategoriesId in addNewSessionDTO.CategoriesId)
            {
                CosmetologyReport cosmetologyCategory = new CosmetologyReport
                {
                    CosmetologyCategoryId = childcategoriesId,
                };
                cosmetologyReports.Add(cosmetologyCategory);
            }

            appointment.CosmetologyReports = cosmetologyReports;
            _cosmetologyAppointmentService.Create(appointment);

            return RedirectToAction("CosmetologPage", "Cosmetology", new { CosmetologId = appointment.CosmetologId });
        }
        [HttpGet]
        public async Task<IActionResult> UpdateAppointment(int AppointmentId)
        {
            CosmetologyAppointment cosmetologyAppointment = await _appDbContext.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.Cosmetolog).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).FirstOrDefaultAsync(x => x.Id == AppointmentId);
            AddNewSessionDTO addNewSessionDTO = new AddNewSessionDTO();
            addNewSessionDTO.CustomerName = cosmetologyAppointment.Customers.FullName;
            addNewSessionDTO.CosmetologName = cosmetologyAppointment.Cosmetolog.FullName;
            addNewSessionDTO.Price = cosmetologyAppointment.Price;
            addNewSessionDTO.Description = cosmetologyAppointment.CosmetologyDescription;

            addNewSessionDTO.CosmetologId = cosmetologyAppointment.CosmetologId;
            addNewSessionDTO.CosmetologyCategories = _appDbContext.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory)
                .Where(x => x.MainCategoryId != null).ToList();



            return View(addNewSessionDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAppointment(int AppointmentId, AddNewSessionDTO addNewSessionDTO)
        {
            addNewSessionDTO.CosmetologyCategories = _appDbContext.CosmetologyCategories.Include(x => x.MainCategory).Include(x => x.ChildCategory).Where(x => x.MainCategoryId != null).ToList();
            if (addNewSessionDTO.CategoriesId == null)
            {
                ModelState.AddModelError("", "Kategoriya seçməyi unutdunuz!");

                return View(addNewSessionDTO);
            }
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            CosmetologyAppointment cosmetologyAppointment = await _appDbContext.CosmetologyAppointments.Include(x => x.AppUser).Include(x => x.Customers).Include(x => x.Cosmetolog).Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).FirstOrDefaultAsync(x => x.Id == AppointmentId);
            cosmetologyAppointment.AppUserId = appUser.Id;
            cosmetologyAppointment.CosmetologyDescription = addNewSessionDTO.Description;
            cosmetologyAppointment.Price = addNewSessionDTO.Price;
            List<CosmetologyReport> cosmetologyReports = new List<CosmetologyReport>();
            foreach (int childcategoriesId in addNewSessionDTO.CategoriesId)
            {
                CosmetologyReport cosmetologyCategory = new CosmetologyReport
                {
                    CosmetologyCategoryId = childcategoriesId,
                };
                cosmetologyReports.Add(cosmetologyCategory);
            }

            cosmetologyAppointment.CosmetologyReports = cosmetologyReports;
            _cosmetologyAppointmentService.Update(cosmetologyAppointment);

            return RedirectToAction("CosmetologPage", "Cosmetology", new { CosmetologId = cosmetologyAppointment.CosmetologId });
        }
        [HttpGet]
        public IActionResult CompleteAppointment(int AppointmentId)
        {

            CompleteSessionDTO completeSessionDTO = new CompleteSessionDTO();

            CosmetologyAppointment cosmetologyAppointment = _appDbContext.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.Cosmetolog).Include(x => x.Filial).Where(x => x.Id == AppointmentId).FirstOrDefault();
            Customer customer = _customerService.GetById(cosmetologyAppointment.CustomerId);
            completeSessionDTO.CosmetologId = cosmetologyAppointment.CosmetologId;
            Cosmetologs cosmetologs = _cosmetologService.GetById(cosmetologyAppointment.CosmetologId);
            completeSessionDTO.Description = cosmetologyAppointment.CosmetologyDescription;

            completeSessionDTO.Customer = customer.FullName;
            completeSessionDTO.Master = cosmetologs.FullName;
            completeSessionDTO.Price = cosmetologyAppointment.Price;

            return View(completeSessionDTO);
        }
        [HttpPost]
        public async Task<IActionResult> CompleteAppointment(int AppointmentId, CompleteSessionDTO completeSessionDTO)
        {

            CosmetologyAppointment cosmetologyAppointment = await _appDbContext.CosmetologyAppointments.Include(x => x.Customers).Include(x => x.Cosmetolog).Include(x => x.Filial).Where(x => x.Id == AppointmentId).FirstOrDefaultAsync();
            var validator = new CompleteCosmetologyValidator();
           
            var validationResult = validator.Validate(completeSessionDTO);
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(completeSessionDTO);
            }
            Customer customer = _customerService.GetById(cosmetologyAppointment.CustomerId);
            Cosmetologs cosmetologs = _cosmetologService.GetById(cosmetologyAppointment.CosmetologId);
            cosmetologyAppointment.Price = completeSessionDTO.Price;
            cosmetologyAppointment.CosmetologyDescription = completeSessionDTO.Description;
            cosmetologyAppointment.OutTime = completeSessionDTO.OutDate;
            cosmetologyAppointment.IsCompleted = true;
            _cosmetologyAppointmentService.Update(cosmetologyAppointment);
            return RedirectToAction("CosmetologPage", "Cosmetology", new { CosmetologId = cosmetologyAppointment.CosmetologId });
        }
   
    }
}
