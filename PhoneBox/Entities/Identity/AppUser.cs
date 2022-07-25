using Microsoft.AspNetCore.Identity;

namespace PhoneBox.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
