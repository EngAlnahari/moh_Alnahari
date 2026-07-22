using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class LandBoundaryModelController : BaseApiController<LandBoundaryModel>
    {
        private readonly string _baseApiUrl = "https://localhost:7027/api/";

        public LandBoundaryModelController(HttpClient httpClient, IConfiguration configuration)
: base(httpClient, configuration, "LandBoundaryModels")
        {
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
