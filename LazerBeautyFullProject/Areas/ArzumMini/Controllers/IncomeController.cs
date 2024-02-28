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
       
        private readonly TimeHelper _timeHelper;
        public IncomeController(InComeService inComeService,IStockService stockService,AppDbContext db,UserManager<AppUser> userManager)
        {
            _InComeService = inComeService;
            _db = db;
            _timeHelper=new TimeHelper();
            _stockService= stockService;
            _UserManager = userManager;
        }
        [HttpGet]
        public IActionResult IncomeMoneyList()
        {

            List<Income> incomeList = _db.Incomes.Include(x => x.AppUser).Include(x=>x.Stock).Where(x=>x.FilialId==1).ToList();
          
            return View(incomeList);
        }
        [HttpGet]
        public IActionResult AddInComeMoney( )
        {
    InComeAddDTO inComeAddDTO = new InComeAddDTO();
            inComeAddDTO.Stocks = _db.Stock.Where(x=>x.RemainCount>0).Where(x => x.FilialId == 1).ToList();
          
            return View(inComeAddDTO);
        }
        [HttpPost]
        public async Task<IActionResult> AddInComeMoney(InComeAddDTO inComeAddDTO)
        {
            inComeAddDTO.Stocks = _db.Stock.Where(x => x.RemainCount > 0 && x.FilialId == 1).ToList();
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
            if (inComeAddDTO.Count>stock.RemainCount)
            {
                ModelState.AddModelError("", "Daxil edilmiş sayda məhsul bazada mövcud deyil!");
               
                return View(inComeAddDTO);
            }
          
            AppUser AppUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            Income income = new Income();
           
            income.Count = inComeAddDTO.Count;
            income.ProductName = stock.ProductName;
            income.FilialId = 1;
            income.Price = inComeAddDTO.Price;
            income.BuyingPrice = stock.BuyingPrice*inComeAddDTO.Count;
            income.Description = inComeAddDTO.Description;
            income.IncomeDate = _timeHelper.GetAzerbaijanTime();;
            income.AppUserId = AppUser.Id;
            income.StockId = stock.Id;
            stock.RemainCount = stock.RemainCount - inComeAddDTO.Count;
            stock.SellingCount = stock.SellingCount + inComeAddDTO.Count;
            _InComeService.Create(income);
            _stockService.Update(stock);
            
            return RedirectToAction("IncomeMoneyList", "Income");
        }
        [HttpGet]
        public IActionResult UpdateInCome(int Id)
        {

            Income income = _InComeService.GetById(Id);
          
            UpdateInComeDTO updateInComeDTO= new UpdateInComeDTO();
            updateInComeDTO.Name= income.ProductName;
            updateInComeDTO.Price = income.Price;
            updateInComeDTO.Count = income.Count;
            return View(updateInComeDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateIncome(int Id, UpdateInComeDTO updateInComeDTO)
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
            var AppUser = await _UserManager.FindByNameAsync(User.Identity.Name);
            Stock stock = _stockService.GetById(income.StockId);
           
          
            if ((updateInComeDTO.Count - income.Count) > stock.RemainCount)
            {
                ModelState.AddModelError("", "Bazada daxil etdiyiniz miqdarda məhsul yoxdur!");
                return View(updateInComeDTO);
            }
            if (income.Count > updateInComeDTO.Count)
            {
              
                income.ProductName = updateInComeDTO.Name;
                income.Price = income.Price - (income.Count - updateInComeDTO.Count) * stock.SellingPrice;
                income.Description = updateInComeDTO.Description;
                stock.RemainCount = stock.RemainCount + (income.Count - updateInComeDTO.Count);
                stock.SellingCount = stock.SellingCount - (income.Count - updateInComeDTO.Count);
                income.BuyingPrice = income.BuyingPrice - ((income.Count - updateInComeDTO.Count) * stock.BuyingPrice);
                income.Count = income.Count - (income.Count - updateInComeDTO.Count);
                income.AppUserId = AppUser.Id;
                income.FilialId = 1;
             
                _stockService.Update(stock);
                _InComeService.Update(income);
                return RedirectToAction("IncomeMoneyList");
            }
            else
            {
             
                stock.RemainCount = stock.RemainCount - (updateInComeDTO.Count - income.Count);
                stock.SellingCount = stock.SellingCount + (updateInComeDTO.Count - income.Count);
                income.ProductName = updateInComeDTO.Name;
                income.Price = income.Price + (updateInComeDTO.Count - income.Count) * stock.SellingPrice;
                income.BuyingPrice = income.BuyingPrice + (updateInComeDTO.Count - income.Count) * stock.BuyingPrice;
                income.Count = income.Count + (updateInComeDTO.Count - income.Count);
                income.Description = updateInComeDTO.Description;
                income.AppUserId = AppUser.Id;
                income.FilialId = 1;
               
                _stockService.Update(stock);
                _InComeService.Update(income);
                return RedirectToAction("IncomeMoneyList");
            }
        }
        [HttpGet]
        public IActionResult ProductList()
        {
            FilialDepoDTO filialDepoDTO = new FilialDepoDTO();
            filialDepoDTO.ArzumMiniStock = _db.Stock.Include(x => x.AppUser).Include(x => x.Incomes).Where(x => x.FilialId == 1 && x.RemainCount > 0).ToList();
            filialDepoDTO.ArzumBeautyStock = _db.Stock.Include(x => x.AppUser).Include(x => x.Incomes).Where(x => x.FilialId == 2 && x.RemainCount>0).ToList();
            filialDepoDTO.ArzumEsteticStock = _db.Stock.Include(x => x.AppUser).Include(x => x.Incomes).Where(x => x.FilialId == 3 && x.RemainCount > 0).ToList();

            return View(filialDepoDTO);
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
            
            stock.TotalCount = stock.TotalCount+NewProduct.ProductCount;
            stock.RemainCount = stock.RemainCount + NewProduct.ProductCount;
            stock.SellingPrice = NewProduct.SellingPrice;
            stock.BuyingPrice = NewProduct.BuyingPrice;            
            stock.AddingDate= _timeHelper.GetAzerbaijanTime();; 
            stock.AppUserId=AppUser.Id;
            stock.FilialId = 1;
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
            var AppUser =await _UserManager.FindByNameAsync(User.Identity.Name);
            stock.AppUserId = AppUser.Id;
            stock.AddingDate = _timeHelper.GetAzerbaijanTime();;
            stock.ProductName=NewProduct.ProductName;
            stock.SellingPrice=NewProduct.SellingPrice;
            stock.BuyingPrice=NewProduct.BuyingPrice;
            stock.TotalCount=NewProduct.ProductCount;
            stock.RemainCount = NewProduct.ProductCount;
            stock.SellingCount = 0;
            stock.FilialId = 1;
         
            _stockService.Create(stock);
            return RedirectToAction("ProductList", "Income");
        }
        [HttpGet]
        public IActionResult ArchiveProducts()
        {
           
            List<Stock> stock = _db.Stock.Include(x=>x.AppUser).Include(x=>x.Incomes).Where(x=>x.FilialId==1 && x.RemainCount==0).ToList();
           
            return View(stock);
        }
        [HttpGet]
        public IActionResult UpdateProductCount(int ProductId)
        {
            AddProductToStockDTO NewProduct = new AddProductToStockDTO();
            Stock stock = _stockService.GetById(ProductId);
            NewProduct.ProductName = stock.ProductName;
            NewProduct.SellingPrice = stock.SellingPrice;
            NewProduct.BuyingPrice = stock.BuyingPrice;
            return View(NewProduct);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProductCount(int ProductId, AddProductToStockDTO NewProduct)
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
           
            stock.SellingPrice = NewProduct.SellingPrice;
            stock.BuyingPrice = NewProduct.BuyingPrice;
            stock.RemainCount = stock.RemainCount + NewProduct.ProductCount;
            stock.TotalCount = stock.TotalCount + NewProduct.ProductCount;
            stock.AddingDate = _timeHelper.GetAzerbaijanTime();
            stock.AppUserId = AppUser.Id;
            stock.FilialId = 1;
            _stockService.Update(stock);

            return RedirectToAction("ProductList", "Income");
        }


    }
}
