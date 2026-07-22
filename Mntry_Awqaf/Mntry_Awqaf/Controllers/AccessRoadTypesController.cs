using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class AccessRoadTypesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public AccessRoadTypesController(HttpClient httpClient, IConfiguration configuration)
        {

            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }




        [HttpPost]
        public async Task<IActionResult> Create(PurposeSurvey depart)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}PurposeSurveys", depart);
            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<PurposeSurvey>(dat);
                if (datuser != null)
                {
                    return RedirectToAction("Login");

                }

            }
            return View(res);
        }
    }
}
