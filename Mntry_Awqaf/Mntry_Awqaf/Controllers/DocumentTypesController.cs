using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class DocumentTypesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public DocumentTypesController(HttpClient httpClient, IConfiguration configuration)
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
        public async Task<IActionResult> Create([FromBody] DocumentType model)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}DocumentTypes", model);

            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<DocumentType>(dat);
                if (datuser != null)
                {
                    return Json(new { success = true, item = new { id = datuser.Id, name = datuser.DocumentTypeName } });
                }
            }
            return Json(new { success = false, message = "فشل الحفظ، يرجى المحاولة مرة أخرى." });
        }



    }
}
