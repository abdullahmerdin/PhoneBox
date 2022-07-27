using System.ComponentModel.DataAnnotations;

namespace PhoneBox.Models
{
    public class AddRoleVM
    {
        [Required(ErrorMessage = "Lütfen rol adını giriniz.")]
        public string Name { get; set; }
        public List<PolicyWithIsSelectVM> Policies { get; set; }
    }
}
