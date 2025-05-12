using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProniaOneToManyFileCRUD.DAL;
using ProniaOneToManyFileCRUD.Models;
using ProniaOneToManyFileCRUD.ViewModels.Account;

namespace ProniaOneToManyFileCRUD.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            if (!ModelState.IsValid)
            {
                return View(registerVm);
            }

            AppUser appUser = new AppUser()
            {
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                Email = registerVm.Email,
                UserName = registerVm.Username,
            };
            var result = await _userManager.CreateAsync(appUser,registerVm.Password); //bu eslinde bizim contexte elave etmeyiizi ve savchange elemeyimizi evez edir amma program.cs de verdiyimiz seyleri yoxlamiyib elesem error verende indexe qaytarir amma ki database e useri yazmir. bunun ucun bunu result a beraber edib resultnan olari yoxluyuruq biz
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View();
            }

            
            return  RedirectToAction("Login"); 
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // burda birinci view u veririk soram controlleri veririk
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            if (!ModelState.IsValid) { return View(); }
            AppUser user = await _userManager.FindByEmailAsync(loginVm.EmailOrUsername);
            if(user == null)
            {
                user = await _userManager.FindByNameAsync(loginVm.EmailOrUsername);
            }
            if (user == null)
            {
                ModelState.AddModelError("", "EmailOrUsername ve ya Password sehvdir");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginVm.Password,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Birazdan yeniden daxil olmagi sinayin");
                return View();
            }
            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "EmailOrUsername ve ya Password sehvdir");
                return View();
            }

            await _signInManager.SignInAsync(user, loginVm.Remember);
            return RedirectToAction("Index", "Home");
        }
    }
}
