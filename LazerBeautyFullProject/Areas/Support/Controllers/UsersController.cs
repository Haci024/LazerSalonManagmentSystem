using Business.IServices;
using Business.ValidationRules.LazerValidator;
using Data.Concrete;
using DTO.DTOS.AppUserDto;
using DTO.DTOS.LazerAppointmentDTO;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol;
using System.Runtime.CompilerServices;
using System.Text;
using Validation.ValidationRules.AppUserValidation;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]
    [Authorize(Roles ="SuperSupporter,Supporter")]
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _role;
       private readonly SignInManager<AppUser> _signIn;
        public UsersController(UserManager<AppUser> userManager,AppDbContext db,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> role) { 
        
        _db = db;
        _userManager = userManager;
        _role = role;
        _signIn = signInManager;
        }
        [HttpGet]
        public IActionResult AllUsers()
        {
            AllUserListDTO allUserListDTO = new AllUserListDTO();
            allUserListDTO.Users = _userManager.Users.Include(x=>x.Filial).Where(x=>x.UserName!="Odissey").ToList();
            if (User.IsInRole("SuperSupporter"))
            {
                allUserListDTO.Users = _userManager.Users.Include(x => x.Filial).ToList();
            }
            else
            {
                allUserListDTO.Users = _userManager.Users.Include(x => x.Filial).Where(x => x.UserName != "Odissey").ToList();
            }
           
            
         


            return View(allUserListDTO);
        }
        [HttpGet]
        public IActionResult AddNewUser()
        { 
            NewUserDTO newUserDTO = new NewUserDTO();

            newUserDTO.IdentityRoles = _role.Roles.Where(x => x.Name != "SuperSupporter").ToList();
            newUserDTO.Filials=_db.Filials.ToList();
            return View(newUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult>  AddNewUser(NewUserDTO newUserDTO)
        {
            newUserDTO.Filials = _db.Filials.ToList();
            newUserDTO.IdentityRoles = _role.Roles.Where(x=>x.Name!="SuperSupporter").ToList();

            if (newUserDTO.Role==null)
            {
                ModelState.AddModelError("", "Rol təyin etmədiniz!");
               
                return View(newUserDTO);
            }
            bool IsExist = _db.Users.Any(x => x.UserName == newUserDTO.UserName);
            if (IsExist)
            {
                ModelState.AddModelError("", "Bu  istifadəçi adı artıq sistemdə mövcuddur!");

                return View(newUserDTO);
            }

            if (newUserDTO.Password.IsNullOrEmpty() && newUserDTO.ConfirmPassword.IsNullOrEmpty())
            {
                ModelState.AddModelError("", "Şifrə və ya şifrə təkrarını daxil etmədiniz");

                return View(newUserDTO);
            }
            static string GenerateRandomString(int length)
            {
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                StringBuilder stringBuilder = new StringBuilder(length);

                for (int i = 0; i < length; i++)
                {
                    int index = random.Next(chars.Length);
                    stringBuilder.Append(chars[index]);
                }

                return stringBuilder.ToString();
            }
           
            AppUser appUser = new AppUser();
            appUser.Id = GenerateRandomString(25);
            appUser.UserName = newUserDTO.UserName;
            appUser.FullName = newUserDTO.FullName;
            appUser.ForgetPasswordCode = "000000";
            appUser.FilialId = newUserDTO.FilialId;
            
            var createUserResult = await _userManager.CreateAsync(appUser,newUserDTO.Password);
           
            if (createUserResult.Succeeded)
            {
               
                var roleExists = await _role.RoleExistsAsync(newUserDTO.Role);

                if (roleExists)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(appUser, newUserDTO.Role);
                    if (addToRoleResult.Succeeded)
                    {
                        return RedirectToAction("Login", "Account",new {area=" "});
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Rol əlavə edilmədi!");

                    return View(newUserDTO);
                }
            }  
                foreach (var item in createUserResult.Errors)
                {
                    ModelState.AddModelError("",item.Description);          
                }
                return View(newUserDTO);
        
          
        }
        [HttpGet]
        public async Task<IActionResult> UserBlock(string AppUserId)
        {
         

            AppUser user =await _userManager.Users.FirstOrDefaultAsync(u => u.Id == AppUserId);

            if (user.IsBlock==true)
            {
                
                user.IsBlock = false;
                var updateResult = _userManager.UpdateAsync(user).Result;

                if (!updateResult.Succeeded)
                {

                    return RedirectToAction("AllUsers", "Users");
                }

            }
            else
            {
               
                user.IsBlock= true;
                var updateResult = _userManager.UpdateAsync(user).Result;

                if (!updateResult.Succeeded)
                {
                   await _signIn.SignOutAsync();
                    return RedirectToAction("AllUsers", "Users");
                }
            }

        
         
            return RedirectToAction("AllUsers", "Users");
        }
   
        [HttpGet]
        public async Task<IActionResult> UpdatePassword(string AppUserId)
        {
           
                if (AppUserId == null)
                {

                    return NotFound();
                }
                AppUser appUser =await  _userManager.FindByIdAsync(AppUserId);
			PasswordUpdateDTO passwordUpdateDTO = new PasswordUpdateDTO();
            passwordUpdateDTO.UserName = appUser.UserName;


				if (appUser == null)
                {
                    return NotFound();
                }
                
                return View(passwordUpdateDTO);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePassword(string AppUserId, PasswordUpdateDTO passwordUpdateDTO)
        {
            AppUser user = await _userManager.FindByIdAsync(AppUserId);

            if (user != null)
            {
                var validator = new UpdatePasswordValidator();
                var validationResult = validator.Validate(passwordUpdateDTO);
                if (!validationResult.IsValid)
                {
                    foreach (var validationRules in validationResult.Errors)
                    {
                        ModelState.AddModelError("", validationRules.ErrorMessage);
                    }
                    return View(passwordUpdateDTO);
                }
                if (!string.IsNullOrEmpty(passwordUpdateDTO.Password))
                {
                    user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, passwordUpdateDTO.Password);

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {

                        return RedirectToAction("AllUsers", "Users");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Xəta baş verdi!");

                    return View(passwordUpdateDTO);

                }



            }
           return View(passwordUpdateDTO);
        }
    




        [HttpGet]
        public IActionResult AllInfo(string AppUserId)
        {
            var AppUser = _userManager.Users.FirstOrDefault(x => x.Id == AppUserId);
            NewUserDTO newUserDTO = new NewUserDTO();
            newUserDTO.FullName = AppUser.FullName;
            newUserDTO.UserName = AppUser.UserName;
            newUserDTO.Filials = _db.Filials.Where(x => x.Id == AppUser.FilialId).ToList();

            return View(newUserDTO);
        }
    }
}
