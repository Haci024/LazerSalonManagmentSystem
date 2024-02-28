using Business.IServices;
using Business.ValidationRules.AppUserValidation;
using Business.ValidationRules.CustomerValidator;
using Data.Concrete;
using DTO.DTOS.AppUserDto;
using DTO.DTOS.CustomerDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.ArzumBeauty.Controllers
{
    [Area("ArzumBeauty")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;

        public CustomerController(ICustomerService customerService, UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _customerService = customerService;
            _db = appDbContext;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> FemaleList()
        {
            

            List<Customer> Womans = await _customerService.GetFemaleList();

            return View(Womans);
        }
        [HttpGet]
        public async Task<IActionResult> MaleList()
        {

            List<Customer> Mans = await _customerService.MaleList();

            return View(Mans);
        }

        [HttpGet]
        public IActionResult AddCustomer()
        {
            AddCustomerDTO addCustomerDTO = new AddCustomerDTO();

            return View(addCustomerDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomer(AddCustomerDTO addCustomerDTO)
        {

            var validator = new CustomerValidator();
            var validationResult = validator.Validate(addCustomerDTO);
            bool IsExist = _db.Customers.Any(x => x.PhoneNumber == addCustomerDTO.PhoneNumber);
            var appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (IsExist)
            {
                ModelState.AddModelError("", "Bu nömrə daha öncə qeydiyyata alınıb");

                return View(addCustomerDTO);
            }
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(addCustomerDTO);
            }
            if (addCustomerDTO.IsFemale)
            {
                Customer customer = new Customer();
           
                customer.FullName = addCustomerDTO.FullName;
                customer.PhoneNumber = addCustomerDTO.PhoneNumber;
                customer.BirthDate = addCustomerDTO.BirthDate;
                customer.FilialId = 2;
                customer.Female = true;
                _customerService.Create(customer);
                return RedirectToAction("FemaleList", "Customer");
            }
            else
            {
                Customer customer = new Customer();
                customer.FullName = addCustomerDTO.FullName;
                customer.PhoneNumber = addCustomerDTO.PhoneNumber;
                customer.BirthDate = addCustomerDTO.BirthDate;
                customer.Female = false;
                customer.FilialId = 2;
                _customerService.Create(customer);
                return RedirectToAction("MaleList", "Customer");
            }

        }
   


        [HttpGet]
        public IActionResult UpdateCustomer(int CustomerId)
        {
            CustomerUpdateDTO customerUpdateDTO = new CustomerUpdateDTO();
            Customer customer = _customerService.GetById(CustomerId);
            customerUpdateDTO.PhoneNumber = customer.PhoneNumber;
            customerUpdateDTO.FullName = customer.FullName;
            customerUpdateDTO.BirthDate = customer.BirthDate;
            customerUpdateDTO.Female = customer.Female;
            return View(customerUpdateDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCustomer(int CustomerId, CustomerUpdateDTO customerUpdateDTO)
        {
            var validator = new UpdateCustomerValidator();
            var validationResult = validator.Validate(customerUpdateDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
                return View(customerUpdateDTO);
            }
            Customer customer = await _customerService.SelectedCustomer(CustomerId);
            customer.FullName = customerUpdateDTO.FullName;
            customer.BirthDate = customerUpdateDTO.BirthDate;
       
            customer.PhoneNumber = customerUpdateDTO.PhoneNumber;
        
            _customerService.Update(customer);
            if (customer.Female)
            {
                return RedirectToAction("FemaleList", "Customer");
            }
            return RedirectToAction("MaleList", "Customer");

        }
        [HttpGet]
        public async Task<IActionResult> DailyBirthDate()
        {
           
            List<Customer> customer = await _customerService.DailyBirthDate(2);

            return View(customer);
        }
        [HttpGet]
        public IActionResult CustomerHistory(int CustomerId)
        {

            CustomerUsingHistoryDTO customerUsingHistoryDTO = new CustomerUsingHistoryDTO();
            customerUsingHistoryDTO.LazerAppointmentsHistory = _db.LazerAppointments.Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x => x.Customers).Include(x => x.LazerAppointmentReports).ThenInclude(x => x.LazerCategory).Include(x => x.Filial).Where(x => x.CustomerId == CustomerId).ToList();
            customerUsingHistoryDTO.CosmetologyAppointments = _db.CosmetologyAppointments.Include(x => x.CosmetologyReports).ThenInclude(x => x.CosmetologyCategory).Include(x => x.Filial).Include(x => x.AppUser).Include(x => x.Customers).Where(x => x.CustomerId == CustomerId).ToList();
            customerUsingHistoryDTO.LipuckaAppointments = _db.LipuckaAppointments.Include(x => x.Customer).Include(x => x.AppUser).Include(x => x.Filial).Include(x => x.LipuckaReports).ThenInclude(x => x.LipuckaCategories).Where(x => x.CustomerId == CustomerId).ToList();
            customerUsingHistoryDTO.PirsinqAppointments = _db.PirsinqAppointments.Include(x => x.Customer).Include(x => x.AppUser).Include(x => x.Filial).Include(x => x.PirsinqReports).ThenInclude(x => x.PirsinqCategory).Where(x => x.CustomerId == CustomerId).ToList();

            customerUsingHistoryDTO.FullName = _db.Customers.Where(x => x.Id == CustomerId).Select(x => x.FullName).FirstOrDefault();
            customerUsingHistoryDTO.BirthDate = _db.Customers.Where(x => x.Id == CustomerId).Select(x => x.BirthDate).FirstOrDefault();
            return View(customerUsingHistoryDTO);
        }


    }
}
