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

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    [Area("ArzumMini")]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;

        public CustomerController(ICustomerService customerService,UserManager<AppUser> userManager ,AppDbContext appDbContext)
        {
            _customerService = customerService;
            _db = appDbContext;
            _userManager = userManager;
        }
        
        [HttpGet]
        public IActionResult FemaleList()
        {
            
            List<Customer> Womans = _customerService.GetFemaleList();

            return View(Womans);
        }
        [HttpGet]
        public IActionResult MaleList()
        {

            List<Customer> Mans = _customerService.MaleList();

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
        public IActionResult AddCustomer(AddCustomerDTO addCustomerDTO)
        {
            var validator = new CustomerValidator();
            var validationResult = validator.Validate(addCustomerDTO);
            bool IsExist = _db.Customers.Any(x => x.PhoneNumber == addCustomerDTO.PhoneNumber);
            if (IsExist)
            {
                ModelState.AddModelError("","Bu nömrə daha öncə qeydiyyata alınıb");
              
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
                _customerService.Create(customer);
                return RedirectToAction("MaleList", "Customer");
            }
          
        }
        public IActionResult GetMatchingCustomers(string searchTerm)
        {
            var matchingCustomers = GetMatchingCustomersFromDatabase(searchTerm); 
            return PartialView("Views/Shared/_CustomerInfoPartial.cshtml", matchingCustomers);
        }

        private List<Customer> GetMatchingCustomersFromDatabase(string searchTerm)
        {     
            var searchCustomer = _db.Customers.Where(c => c.FullName.Contains(searchTerm) || c.PhoneNumber.ToString().Contains(searchTerm)).ToList();

            return searchCustomer;
        }

        [HttpGet]
        public IActionResult UpdateCustomer(int CustomerId)
        {
            CustomerUpdateDTO customerUpdateDTO = new CustomerUpdateDTO();
            Customer customer = _customerService.GetById(CustomerId);
            customerUpdateDTO.PhoneNumber = customer.PhoneNumber;
            customerUpdateDTO.FullName = customer.FullName;
            customerUpdateDTO.BirthDate = customer.BirthDate;
            return View(customerUpdateDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCustomer(int CustomerId, CustomerUpdateDTO customerUpdateDTO)
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
            Customer customer = _customerService.SelectedCustomer(CustomerId);
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
        public IActionResult DailyBirthDate()
        {
            DateTime date = DateTime.Today;
            List<Customer> customer = _db.Customers.Where(x => x.BirthDate.Month == date.Month && x.BirthDate.Day == date.Day).ToList();

            return View(customer);
        }
        [HttpGet]
        public IActionResult CustomerHistory(int CustomerId) {

            List<LazerAppointment> lazerAppointment = _db.LazerAppointments.Include(x => x.LazerMaster).Include(x => x.AppUser).Include(x=>x.Customers).Include(x=>x.LazerAppointmentReports).ThenInclude(x=>x.LazerCategory).
                Where(x=>x.CustomerId==CustomerId).ToList();
            ViewBag.Customer = _db.Customers.Where(x => x.Id == CustomerId).Select(x=>x.FullName).FirstOrDefault();
            ViewBag.Female=_db.Customers.Where(x=>x.Id==CustomerId).Select(x => x.Female).FirstOrDefault();
        return View(lazerAppointment);
        }

    }
}
