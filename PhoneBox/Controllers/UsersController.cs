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
        public IActionResult GetAll()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> Delete(int userId)
        {
            AppUser user = await _userManager.FindByIdAsync(userId.ToString());
            _userManager.DeleteAsync(user);
            return RedirectToAction("GetAll");
        }


    }
}
