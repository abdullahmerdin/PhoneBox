using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhoneBox.Constants.Enums;
using PhoneBox.Entities.Identity;
using PhoneBox.Models;
using System.Security.Claims;

namespace PhoneBox.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        readonly SignInManager<AppUser> _signInManager;
        readonly UserManager<AppUser> _userManager;
        readonly RoleManager<AppRole> _roleManager;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> GiveDefaultPermissions()
        {
            //int rootId = _roleManager.GetRoleIdAsync(new AppRole() { Name = "root" }).Id;
            //AppRole root = _roleManager.Roles.Single(x => x.Id == rootId);
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "GetAllCustomers"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "AddCustomer"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "DeleteCustomer"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "UpdateCustomer"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "AddUser"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "UpdateUser"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "DeleteUser"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "GetAllUserRoles"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "AddUserRole"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "UpdateUserRole"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "DeleteUserRole"));
            //await _roleManager.AddClaimAsync(root, new Claim(CustomClaimTypes.Permission, "AssignUserRole"));


            //int adminId = 7;
            //AppRole admin = _roleManager.Roles.Single(x => x.Id == adminId);
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "AddUser"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "UpdateUser"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "DeleteUser"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "GetAllUserRoles"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "AddUserRole"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "UpdateUserRole"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "DeleteUserRole"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "AssignUserRole"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "GetAllCustomers"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "AddCustomer"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "DeleteCustomer"));
            //await _roleManager.AddClaimAsync(admin, new Claim(CustomClaimTypes.Permission, "UpdateCustomer"));

            //int employeeId = 8;
            //AppRole employee = _roleManager.Roles.Single(x => x.Id == employeeId);
            //await _roleManager.AddClaimAsync(employee, new Claim(CustomClaimTypes.Permission, "GetAllCustomers"));
            //await _roleManager.AddClaimAsync(employee, new Claim(CustomClaimTypes.Permission, "AddCustomer"));
            //await _roleManager.AddClaimAsync(employee, new Claim(CustomClaimTypes.Permission, "DeleteCustomer"));
            //await _roleManager.AddClaimAsync(employee, new Claim(CustomClaimTypes.Permission, "UpdateCustomer"));

            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var roots = await _userManager.GetUsersInRoleAsync("root");
            if (roots.Count == 0)
            {
                AppUser rootUser = new() { FirstName = "root", LastName = "root", UserName = "root" };
                IdentityResult result = await _userManager.CreateAsync(rootUser, "pswrd");
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(rootUser, "root");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserForLoginVM viewModel)
        {
            AppUser appUser = await _userManager.FindByNameAsync(viewModel.Username);
            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(appUser, viewModel.Password, false, false);
            if (result.Succeeded)
                return RedirectToAction("GetAll", "Customers");
            else
                return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
        
    }
}
