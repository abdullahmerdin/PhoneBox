using Microsoft.AspNetCore.Identity;
using PhoneBox.Constants.Enums;

namespace PhoneBox.Constants.Identity
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(RoleTypes.root.ToString()));
        }
    }
}
