using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBox.Entities.Identity;
using PhoneBox.Models;

namespace PhoneBox.Controllers
{
    public class UsersController : Controller
    {
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public UsersController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        [HttpGet]
        [Authorize(Policy ="GetAllUsers")]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        [HttpGet]
        [Authorize(Policy ="AddUser")]
        public IActionResult AddUser()
        {
            ViewBag.Roles = _roleManager.Roles.Select(x => new RoleWithSelectVM
            {
                Id = x.Id,
                Name = x.Name,
                IsSelected = false,
                Claims = _roleManager.GetClaimsAsync(x).Result
            });


            
            return View();
        }

        [HttpPost]
        [Authorize(Policy ="AddUser")]
        public async Task<IActionResult> AddUser(AddUserVM model)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = model.UserName,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    foreach (var role in model.Roles.Where(x => x.IsSelected = true))
                    {
                        await _userManager.AddToRoleAsync(appUser, role.Name);
                    }
                    return RedirectToAction("GetAllUsers", "Users");
                }   
            }
            return View(model);
        }

        [Authorize(Policy ="DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            await _userManager.DeleteAsync(user);
            return RedirectToAction("GetAllUsers");
        }
    }
}
