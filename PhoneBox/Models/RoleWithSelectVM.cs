using System.Security.Claims;

namespace PhoneBox.Models
{
    public class RoleWithSelectVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }     
        public IList<Claim> Claims { get; set; }
    }
}
