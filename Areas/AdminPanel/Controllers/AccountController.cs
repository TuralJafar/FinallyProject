using FinallyProject.Models;
using FinallyProject.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinallyProject.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser>userManager,SignInManager<AppUser>signInManager)
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
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (registerVM == null) return View();
            AppUser appUser = new AppUser()
            {
                Name = registerVM.Name,
                Email = registerVM.Email,
                UserName = registerVM.Username,
                Surname = registerVM.Surname,

            };
            IdentityResult result = await _userManager.CreateAsync(appUser, registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, "Not found");
                }
                return View();
            }
            await _signInManager.SignInAsync(appUser, isPersistent: false);

            return RedirectToAction("Index", "Home", new {area= ""});

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (loginVM == null) return View();
            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);
            if (appUser == null)
            {
                appUser = await _userManager.FindByNameAsync(loginVM.Email);
                if (appUser == null)
                {
                    ModelState.AddModelError(string.Empty, "Not found");
                }
                return View();
            }
            await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, false, true);
            return RedirectToAction("Index", "Home", new {area= ""});
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home",new {area= ""});
        }
    }
}
    

