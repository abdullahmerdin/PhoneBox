using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBox.Entities.Identity;
using PhoneBox.Models;
using PhoneBox.Repositories.Abstracts;

namespace PhoneBox.Controllers
{
    public class AuthController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //LOGIN PAGE ON GET 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        //LOGIN PAGE ON POST
        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginVM viewModel)
        {
            AppUser appUser = await _userManager.FindByNameAsync(viewModel.Username);
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, viewModel.Password, false, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            else
                return View();
        }

        //REGISTER PAGE ON GET
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        //REGISTER PAGE ON POST
        [HttpPost]
        public async Task<IActionResult> Register(UserForRegisterVM viewModel)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    Email = viewModel.Email,
                    UserName = viewModel.Username,
                    FirstName = viewModel.FirstName,
                    LastName = viewModel.LastName
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, viewModel.Password);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
    }
}
