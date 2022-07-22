using PhoneBox.Entities.Base;
using PhoneBox.Entities.Identity;

namespace PhoneBox.Entities
{
    public class PhoneNumber : BaseEntity
    {
        public string Number { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }

    }
}
