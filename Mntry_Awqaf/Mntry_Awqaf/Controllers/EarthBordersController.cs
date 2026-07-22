using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class EarthBordersController : Controller
    {

        
        private readonly HttpClient _httpClient;
        //private readonly string _apiBaseUrl = "https://localhost:7095/api";
        //private readonly string URLAPI = "https://localhost:7095/api/TanjezOrders";
        private readonly string _baseApiUrl;
        public EarthBordersController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }


        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<EarthBorders>>(_baseApiUrl);

            return View(response);
        }






        public IActionResult Create()
        {
            var model = new TanjezOrderWithEarthBordersViewModel();
            return View(model);

        }



        [HttpPost]
        public async Task<IActionResult> Create(EarthBorders depart)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}EarthBorders", depart);
            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<EarthBorders>(dat);
                if (datuser != null)
                {
                    TempData["SuccessMessage"] = "تم إضافة الطلب بنجاح";
                    return RedirectToAction("Create", "TransportModel");

                }

            }
            return View(res);
        }

    }
}
