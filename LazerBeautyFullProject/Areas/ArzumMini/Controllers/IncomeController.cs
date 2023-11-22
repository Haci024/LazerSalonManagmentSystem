using Business.IServices;
using Business.ValidationRules.CustomerValidator;
using Business.ValidationRules.IncomeValidator;
using Business.ValidationRules.StockValidator;
using Data.Concrete;
using DTO.DTOS.CustomerDTO;
using DTO.DTOS.InComeDTO;
using DTO.DTOS.StockDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LazerBeautyFullProject.Areas.ArzumMini.Controllers
{
    [Area("ArzumMini")]
    [Authorize]
    
    public class IncomeController : Controller
    {
        private readonly InComeService _InComeService;
        private readonly UserManager<AppUser> _UserManager;
        private readonly AppDbContext _db;
        private readonly IStockService _stockService;
        public IncomeController(InComeService inComeService,IStockService stockService,AppDbContext db,UserManager<AppUser> userManager)
        {
            _InComeService = inComeService;
            _db = db;
            _stockService= stockService;
            _UserManager = userManager;
        }
        [HttpGet]
        public IActionResult IncomeMoneyList()
        {

            List<Income> incomeList = _db.Incomes.Include(x => x.AppUser).Include(x=>x.Stock).ToList();
          
            return View(incomeList);
        }
        [HttpGet]
        public IActionResult AddInComeMoney( )
        {
    InComeAddDTO inComeAddDTO = new InComeAddDTO();
            inComeAddDTO.Stocks = _db.Stock.Where(x=>x.ProductCount>0).ToList();
          
            return View(inComeAddDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddInComeMoney(InComeAddDTO inComeAddDTO)
        {
            inComeAddDTO.Stocks = _stockService.GetList();
            var validator = new IncomeValidator();
            var validationResult = validator.Validate(inComeAddDTO);
            if (validationResult != null)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                    return View(inComeAddDTO);
                }
            }
            Stock stock = _stockService.GetById(inComeAddDTO.StockId);
            if (inComeAddDTO.Count>stock.ProductCount)
            {
                ModelState.AddModelError("", "Tələb olunan məhsulun bazadakı sayı 0-a bərabərdir!");
               
                return View(inComeAddDTO);
            }
          
            AppUser AppUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            Income income = new Income();
            income.FilialId = AppUser.FilialId;
            Kassa budget = _db.Budget.FirstOrDefault();
            budget.Budget = budget.Budget + inComeAddDTO.Price;
            income.Count = inComeAddDTO.Count;
            income.ProductName = stock.ProductName;
            income.Price = inComeAddDTO.Price;
            income.Description = inComeAddDTO.Description;
            income.IncomeDate = DateTime.Now;
            income.AppUserId = AppUser.Id;
            income.StockId = stock.Id;  

            
            stock.ProductCount = stock.ProductCount - inComeAddDTO.Count;

            _InComeService.Create(income);
            _stockService.Update(stock);
            _db.Update(budget);
            _db.SaveChanges();
            return RedirectToAction("IncomeMoneyList", "Income");
        }
        [HttpGet]
        public IActionResult UpdateInCome(int Id)
        {
         
            Income income=_db.Incomes.Include(x=>x.AppUser).FirstOrDefault(x=>x.Id==Id);
          
            UpdateInComeDTO updateInComeDTO= new UpdateInComeDTO();
            updateInComeDTO.Name= income.ProductName;
            updateInComeDTO.Description= income.Description;
            updateInComeDTO.Price = income.Price;
            updateInComeDTO.Count = income.Count;   
            


            return View(updateInComeDTO);
        }
        [HttpPost]
        public async  Task<IActionResult> UpdateIncome(int Id,UpdateInComeDTO updateInComeDTO)
        {
            var validator = new InComeUpdateValidator();
            
            var validationResult = validator.Validate(updateInComeDTO);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                  
                }
                return View(updateInComeDTO);
            }
            Income income = _InComeService.GetById(Id);
            AppUser AppUser =await _UserManager.FindByNameAsync(User.Identity.Name);
            Kassa budget=_db.Budget.FirstOrDefault();
            budget.Budget = (budget.Budget - income.Price)+updateInComeDTO.Price;
            income.Count = updateInComeDTO.Count;
            income.ProductName = updateInComeDTO.Name;
            income.Price = updateInComeDTO.Price;
            income.Description = updateInComeDTO.Description;
            income.AppUserId = AppUser.Id;
            _db.Update(budget);
            _db.SaveChanges();
            _InComeService.Update(income);
           
            return RedirectToAction("IncomeMoneyList");
        }
        [HttpGet]
        public IActionResult ProductList()
        {

            List<Stock> stocks = _db.Stock.Include(x=>x.AppUser).Include(x=>x.Incomes).ToList();
            return View(stocks);
        }

        [HttpGet]
        public IActionResult IncreaseToCountProduct(int ProductId)
        {
            AddProductToStockDTO NewProduct = new AddProductToStockDTO();
            Stock stock=_stockService.GetById(ProductId);
            NewProduct.ProductName= stock.ProductName;  
            NewProduct.SellingPrice= stock.SellingPrice;
            NewProduct.BuyingPrice=stock.BuyingPrice;
            
            return View(NewProduct);
        }
        [HttpPost]
        public async Task<IActionResult> IncreaseToCountProduct(int ProductId,AddProductToStockDTO NewProduct)
        {
            var validate = new AddProductToStockValidator();
            var validationResult = validate.Validate(NewProduct);
         
            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(NewProduct);
            }
            var AppUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            Stock stock = _stockService.GetById(ProductId);
            
            stock.ProductCount = NewProduct.ProductCount;
            stock.SellingPrice = NewProduct.SellingPrice;
            stock.BuyingPrice = NewProduct.BuyingPrice;
            stock.AddingDate= DateTime.Now; 
            stock.AppUserId=AppUser.Id;
          
            _stockService.Update(stock);

            return RedirectToAction("ProductList","Income");
        }
        [HttpGet]
        public IActionResult AddProduct()
        {
            AddProductToStockDTO NewProduct = new AddProductToStockDTO();
           
            return View(NewProduct);
        }
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductToStockDTO NewProduct)
        {
            var validate = new AddProductToStockValidator();
            var validationResult=validate.Validate(NewProduct);

            if (!validationResult.IsValid)
            {
                foreach (var item in validationResult.Errors)
                {
                    ModelState.AddModelError("", item.ErrorMessage);
                }
                return View(NewProduct);
            }
            if (NewProduct.BuyingPrice>=NewProduct.SellingPrice)
            {
                ModelState.AddModelError("", "Maya dəyəri satış qiymətindən aşağı ola bilməz");
               
                return View(NewProduct);
            }
            Stock stock=new Stock();
            AppUser AppUser =await _UserManager.FindByNameAsync(User.Identity.Name);
            stock.AppUserId = AppUser.Id;
            stock.AddingDate = DateTime.Now;
            stock.ProductName=NewProduct.ProductName;
            stock.SellingPrice=NewProduct.SellingPrice;
            stock.BuyingPrice=NewProduct.BuyingPrice;
            stock.ProductCount=NewProduct.ProductCount;
         
            _stockService.Create(stock);
            return RedirectToAction("ProductList", "Income");
        }
        [HttpGet]
        public IActionResult ArchiveProducts()
        {
           
            List<Stock> stock = _db.Stock.Include(x=>x.AppUser).Include(x=>x.Incomes).ToList();
           
            return View(stock);
        }
       
 
    }
}
