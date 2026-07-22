using Microsoft.AspNetCore.Mvc;

namespace Mntry_Awqaf.Controllers
{
    public class TypeModeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();// اسم الفيو الجزئي

        }
    }
}
