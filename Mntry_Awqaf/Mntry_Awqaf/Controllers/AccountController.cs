using Microsoft.AspNetCore.Mvc;

namespace Mntry_Awqaf.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Social()
        {
            return View();
        }

        public IActionResult Home()
        {
            return View();
        }
    }
}
