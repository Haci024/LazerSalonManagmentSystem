using Business.IServices;

using Business.ValidationRules.OutMoneyValidator;
using Data.Concrete;
using DTO.DTOS.InComeDTO;
using DTO.DTOS.OutMoneyDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    [Authorize]
    [Area("ArzumMini")]
    public class OutMoneyController : Controller
    {
       
        private readonly IOutMoneyService _outMoneyService;
        private readonly AppDbContext _db;
        private readonly UserManager<AppUser> _userManager;
        public OutMoneyController(IOutMoneyService OutMoneyService,AppDbContext db,UserManager<AppUser> userManager )
        {
            _outMoneyService = OutMoneyService;
            _db = db;
            _userManager = userManager;
            
        }
        [HttpGet]
        public IActionResult OutMoneyList()
        {
            List<OutMoney> outMoneys =_db.OutMoney.Include(x=>x.AppUser).ToList();
           
            return View(outMoneys);
        }
        [HttpGet]
        public async Task<IActionResult> AddOutMoney() {

            OutMoneyAddDTO outMoneyAddDTO= new OutMoneyAddDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(outMoneyAddDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOutMoney(OutMoneyAddDTO outMoneyAddDTO)
        {
          
            var validator = new OutMoneyValidator();
            var validationResult = validator.Validate(outMoneyAddDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                   
                    return View(outMoneyAddDTO);
                }
            }
            OutMoney outMoney = new OutMoney();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            Kassa kassa = _db.Budget.FirstOrDefault();
            kassa.Budget = kassa.Budget - outMoneyAddDTO.Price;
            outMoney.Description = outMoneyAddDTO.Description;
            outMoney.Product = outMoneyAddDTO.Product;
            outMoney.Price = outMoneyAddDTO.Price;
            outMoney.CreateTime = DateTime.Now;
            outMoney.AppUserId = appUser.Id;
              
            _outMoneyService.Create(outMoney);
            _db.Update(kassa);
            _db.SaveChanges();
            return RedirectToAction("OutMoneyList","OutMoney");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateOutMoney(int Id)
        {

            UpdateOutMoneyDTO updateOutMoneyDTO = new UpdateOutMoneyDTO();
           
          OutMoney outMoney =_outMoneyService.GetById(Id);
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            updateOutMoneyDTO.Description = outMoney.Description;
            updateOutMoneyDTO.Price= outMoney.Price;
            updateOutMoneyDTO.Product = outMoney.Product;

            return View(updateOutMoneyDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOutMoney(int Id,UpdateOutMoneyDTO updateOutMoneyDTO)
        {
            var validator = new UpdateOutMoneyValidator();
            var validationResult = validator.Validate(updateOutMoneyDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);


                }
                return View(updateOutMoneyDTO);
            }
            Kassa budget = _db.Budget.FirstOrDefault();
           
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            OutMoney outMoney = _outMoneyService.GetById(Id);
            budget.Budget = ((budget.Budget + outMoney.Price) - updateOutMoneyDTO.Price);
           outMoney.AppUserId = appUser.Id;
           
            
            outMoney.Product = updateOutMoneyDTO.Product;
            outMoney.Price = updateOutMoneyDTO.Price;
            outMoney.CreateTime = DateTime.Now;
            outMoney.Description=updateOutMoneyDTO.Description;
            _outMoneyService.Update(outMoney);
            _db.Update(budget);
            _db.SaveChanges();
            return RedirectToAction("OutMoneyList");
        }
    }
}
