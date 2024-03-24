using Business.ValidationRules.AppUserValidation;
using Data.Concrete;
using DTO.DTOS.AppUserDto;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace LazerBeautyFullProject.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly AppDbContext _db;


        public AccountController(UserManager<AppUser> userManager,AppDbContext db ,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _db =db;
        }
        #region Login/Daxil ol
        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto loginVM)
        {
            
            if (string.IsNullOrEmpty(loginVM.UserName) || string.IsNullOrEmpty(loginVM.Password))
            {
                ModelState.AddModelError("", "İstifadəçi adı və şifrə mütləq doldurulmalıdır.");
                return View(loginVM);
            }
            var result = await _signInManager.PasswordSignInAsync(loginVM.UserName,loginVM.Password, false, true);
            
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginVM.UserName);
                if (user.IsBlock==true)
                {
                  
                    ModelState.AddModelError("", "İstifadəçi adı və ya Şifrə yalnışdır!");
                    return View(loginVM);
                
                }
                if(user.FilialId == 1)
                {
                    return RedirectToAction("BudgetPage", "Kassa", new { area = "ArzumMini" });
                }
                else if (user.FilialId==2) 
                {
                    return RedirectToAction("BudgetPage", "Kassa", new { area = "ArzumBeauty" });
                }
                else if(user.FilialId==3)
                {
                    return RedirectToAction("BudgetPage", "Kassa", new { area = "ArzumEstetic" });
                }
                else if (user.FilialId==4)
                {
                    return RedirectToAction("BudgetPage", "TotalKassa", new { area = "Admin" });
                }

                else if (user.FilialId==5)
                {
                    return RedirectToAction("AllUsers", "Users", new { area = "Support" });
                }

            
            }
            ModelState.AddModelError("", "İstifadəçi adı və ya Şifrə yalnışdır!");

          
            return View(loginVM);
        }
        
        #endregion
        public IActionResult Logout()
        {
        _signInManager.SignOutAsync();
            return RedirectToAction("Login","Account");
        }
    }
}
