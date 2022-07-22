using Microsoft.AspNetCore.Mvc;

namespace PhoneBox.Controllers
{
    public class ContactsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
