using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class OwnershipsController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public OwnershipsController(HttpClient httpClient, IConfiguration configuration)
        {

            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<ownership>>($"{_baseApiUrl}ownerships");
                return View(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching ownerships: " + ex.Message);
                return View(new List<ownership>());
            }
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ownership model)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ownerships", model);

            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<ownership>(dat);
                if (datuser != null)
                {
                    return Json(new { success = true, item = new { id = datuser.Id, name = datuser.Name } });
                }
            }

            return Json(new { success = false, message = "فشل الحفظ، يرجى المحاولة مرة أخرى." });
        }

    }
}
