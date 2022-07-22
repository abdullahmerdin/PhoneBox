using Microsoft.AspNetCore.Identity;
using PhoneBox.Entities.Base;

namespace PhoneBox.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}
