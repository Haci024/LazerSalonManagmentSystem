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
    
    [Area("ArzumMini")]
    [Authorize]
    public class OutMoneyController : Controller
    {
       
        private readonly IOutMoneyService _outMoneyService;
        private readonly ISpendCategoryService _spendCategoryService;
        private readonly IKassaService _kassaService;
        private readonly AppDbContext _db;
        private readonly TimeHelper _timeHelper;
        private readonly UserManager<AppUser> _userManager;
        public OutMoneyController(IOutMoneyService OutMoneyService,AppDbContext db,IKassaService kassaService,ISpendCategoryService spendCategoryService,UserManager<AppUser> userManager )
        {
            _outMoneyService = OutMoneyService;
            _db = db;
            _timeHelper = new TimeHelper();
            _userManager = userManager;
            _spendCategoryService=spendCategoryService;
            _kassaService=kassaService;



        }
        [HttpGet]
        public IActionResult OutMoneyList()
        {
            List<OutMoney> outMoneys =_db.OutMoney.Include(x=>x.AppUser).Include(x=>x.SpendCategory).Where(x=>x.SpendCategory.FilialId==1).ToList();
           
            return View(outMoneys);
        }
        [HttpGet]
        public async Task<IActionResult> AddOutMoney()
        {

            OutMoneyAddDTO outMoneyAddDTO = new OutMoneyAddDTO();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            outMoneyAddDTO.SpendCategories = _db.SpendCategories.Include(x => x.Filial).Where(x => x.FilialId == 1 && x.Status == false).ToList();
           

            return View(outMoneyAddDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOutMoney(OutMoneyAddDTO outMoneyAddDTO)
        {
            outMoneyAddDTO.SpendCategories = _db.SpendCategories.Include(x => x.Filial).Where(x => x.FilialId == 1 && x.Status == false).ToList();
            var validator = new OutMoneyValidator(_kassaService);
            var validationResult = validator.Validate(outMoneyAddDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);

                    return View(outMoneyAddDTO);
                }
            }
            bool autoDate = _db.SpendCategories.Where(x => x.Id == outMoneyAddDTO.SpendCategoryId).Select(x => x.AutoDate).FirstOrDefault();
            OutMoney outMoney = new OutMoney();
            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);
            outMoney.Description = outMoneyAddDTO.Description;

            outMoney.Price = outMoneyAddDTO.Price;

            outMoney.AppUserId = appUser.Id;
            outMoney.SpendCategoryId = outMoneyAddDTO.SpendCategoryId;
            if (autoDate == true)
            {

                outMoney.AddingDate = _timeHelper.GetAzerbaijanTime();
            }
            else
            {
                if (outMoneyAddDTO.AddingDate == null)
                {
                    ModelState.AddModelError("", "Bu əməliyyat sistemə qeyd olunan zaman mütləq tarix daxil edilməlidir!");
                    return View(outMoneyAddDTO);
                }

                outMoney.AddingDate = (DateTime)outMoneyAddDTO.AddingDate;



            }
            _outMoneyService.Create(outMoney);
            return RedirectToAction("OutMoneyList", "OutMoney");

        }
        [HttpGet]
        public async Task<IActionResult> UpdateOutMoney(int Id)
        {

            UpdateOutMoneyDTO updateOutMoneyDTO = new UpdateOutMoneyDTO();
           
          OutMoney outMoney =_outMoneyService.GetById(Id);
            AppUser appUser =await _userManager.FindByNameAsync(User.Identity.Name);
            updateOutMoneyDTO.SpendCategories = _db.SpendCategories.Where(x => x.FilialId == 1 && x.Status == false).ToList();
            updateOutMoneyDTO.Description = outMoney.Description;
            updateOutMoneyDTO.Price= outMoney.Price;

            return View(updateOutMoneyDTO);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateOutMoney(int Id, UpdateOutMoneyDTO updateOutMoneyDTO)
        {
            updateOutMoneyDTO.SpendCategories = _db.SpendCategories.Where(x=>x.FilialId==1 &&  x.Status==false).ToList();
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
           

            AppUser appUser = await _userManager.FindByNameAsync(User.Identity.Name);

            OutMoney outMoney = _outMoneyService.GetById(Id);
            outMoney.AppUserId = appUser.Id;
            outMoney.AddingDate = _timeHelper.GetAzerbaijanTime();;
            outMoney.Description = updateOutMoneyDTO.Description;
            if (updateOutMoneyDTO.Price < outMoney.Price)
            { 
                outMoney.Price = updateOutMoneyDTO.Price;
                _outMoneyService.Update(outMoney);
               
                return RedirectToAction("OutMoneyList");

            }
            else
            {
             
                outMoney.Price = updateOutMoneyDTO.Price;
                _outMoneyService.Update(outMoney);
                
                return RedirectToAction("OutMoneyList");
            }



        }
        [HttpGet]
        public IActionResult SpendCategoryList()
        {
            List<SpendCategory> spendCategories = _db.SpendCategories.Where(x => x.FilialId == 1).ToList();
            return View(spendCategories);
        }
        [HttpGet]
        public IActionResult AddSpendCategory()
        {
            AddSpendCategoryDTO spendCategoryDTO = new AddSpendCategoryDTO();

            return View(spendCategoryDTO);
        }
        [HttpPost]
        public IActionResult AddSpendCategory(AddSpendCategoryDTO spendCategoryDTO)
        {
            SpendCategory spendCategory = new SpendCategory();
            bool SpendCategoryExists = _db.SpendCategories.Any(x => x.Category == spendCategoryDTO.Category && x.FilialId == 1);
            if (SpendCategoryExists)
            {
                ModelState.AddModelError("", "Bu kategoriya artıq mövcuddur!");
                return View(spendCategoryDTO);
            }
            if (spendCategoryDTO.Category==null)
            {
                ModelState.AddModelError("", "Kategoriya daxil edilməyib!");
              
                return View(spendCategoryDTO);
            }
            spendCategory.Category = spendCategoryDTO.Category;
            spendCategory.Status = false;
            spendCategory.FilialId = 1;
            spendCategory.AutoDate = spendCategoryDTO.AutoDate;
            _spendCategoryService.Create(spendCategory);
            return RedirectToAction("SpendCategoryList");
        }
        [HttpGet]
        public IActionResult UpdateSpendCategory(int Id)
        {
            SpendCategory spendCategory = _spendCategoryService.GetById(Id);
            AddSpendCategoryDTO addSpendCategoryDTO = new AddSpendCategoryDTO();
            addSpendCategoryDTO.Category = spendCategory.Category;


            return View(addSpendCategoryDTO);
        }
        [HttpPost]
        public IActionResult UpdateSpendCategory(int Id, AddSpendCategoryDTO spendCategoryDTO)
        {

            SpendCategory spendCategory = _spendCategoryService.GetById(Id);
            spendCategory.Category = spendCategoryDTO.Category;
            _spendCategoryService.Update(spendCategory);
            return RedirectToAction("SpendCategoryList");
        }
        [HttpGet]
        public IActionResult SpendCategoryOFF(int Id)
        {
            SpendCategory category = _spendCategoryService.GetById(Id);
            if (category.Status == false)
            {
                category.Status = true;
                _spendCategoryService.Update(category);

                return RedirectToAction("SpendCategoryList");
            }
            else
            {
                category.Status = false;
                _spendCategoryService.Update(category);
                return RedirectToAction("SpendCategoryList");
            }


        }
    }
}
