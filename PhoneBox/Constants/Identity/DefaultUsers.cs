using Microsoft.AspNetCore.Identity;
using PhoneBox.Constants.Enums;
using PhoneBox.Entities.Identity;
using System.Security.Claims;

namespace PhoneBox.Constants.Identity
{
    public static class DefaultUsers
    {
        public static async Task SeedRootUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var rootUser = new AppUser
            {
                UserName = "root",
            };
            if (userManager.Users.All(u => u.Id != rootUser.Id))
            {
                var user = await userManager.FindByEmailAsync(rootUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(rootUser, "pswrd");
                    await userManager.AddToRoleAsync(rootUser, RoleTypes.employee.ToString());
                    await userManager.AddToRoleAsync(rootUser, RoleTypes.admin.ToString());
                    await userManager.AddToRoleAsync(rootUser, RoleTypes.root.ToString());
                }
                await roleManager.SeedClaimsForRootUser();
            }
        }
        private async static Task SeedClaimsForRootUser(this RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("root");
            await roleManager.AddPermissionClaim(adminRole, "Products");
        }
        public static async Task AddPermissionClaim(this RoleManager<AppRole> roleManager, AppRole role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
