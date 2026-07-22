using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class WorkWagesTapleController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public WorkWagesTapleController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var model = new WorkOfferWithDetailsViewModel();
            var dept = await Getdepart();
            var Listdepart = dept.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.Price.ToString(),
            }).ToList();
            ViewBag.Departs = new SelectList(Listdepart, "Value", "Text");
            return View(model);

        }

     
        private async Task<List<SpecificSpaceTemplate>> Getdepart()
        {
            var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}SpecificSpaceTemplates");
            return JsonConvert.DeserializeObject<List<SpecificSpaceTemplate>>(resp);
        }

        [HttpPost]
        public async Task<IActionResult> Create(WorkWagesTaple depart)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkWagesTaples", depart);
            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<WorkWagesTaple>(dat);
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
