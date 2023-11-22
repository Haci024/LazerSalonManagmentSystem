using Data.Concrete;
using DTO.DTOS.AppUserDto;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LazerBeautyFullProject.Areas.Support.Controllers
{
    [Area("Support")]   
    public class UsersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _db;
        private readonly RoleManager<IdentityRole> _role;
        public UsersController(UserManager<AppUser> userManager,AppDbContext db,RoleManager<IdentityRole> role) { 
        
        _db = db;
        _userManager = userManager;
            _role = role;
        }
        [HttpGet]
        public IActionResult AllUsers()
        {
            List<AppUser> users = new List<AppUser>();
          
          return View(users);
        }
        [HttpGet]
        public IActionResult AddNewUser()
        { 
            NewUserDTO newUserDTO = new NewUserDTO();

            newUserDTO.IdentityRoles = _role.Roles.ToList();
            newUserDTO.Filials=_db.Filials.ToList();
            return View(newUserDTO);
        }
        [HttpPost]
        public async Task<IActionResult>  AddNewUser(NewUserDTO newUserDTO)
        {
            newUserDTO.Filials = _db.Filials.ToList();
            newUserDTO.IdentityRoles = _role.Roles.ToList();

            if (newUserDTO.Role==null)
            {
                ModelState.AddModelError("", "Xeta baş verdi!");
                return View(newUserDTO);
            }
            if (ModelState.IsValid)
            {
                ModelState.AddModelError("", "Rol təyin etmədiniz!");

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
                        
                        return RedirectToAction("Login", "Account");
                    }
                }
            }
            ModelState.AddModelError("", "Rol təyin edilməyib!");


            return RedirectToAction("Login","Account");
        }

        [HttpGet]
        public IActionResult MyProfile()
        {

            return View();
        }

    }
}
