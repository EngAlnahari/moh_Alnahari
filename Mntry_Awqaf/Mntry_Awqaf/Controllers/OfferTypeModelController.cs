using System.Text;
using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class OfferTypeModelController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        private readonly string Endpoint = "TransportModels";
        public OfferTypeModelController( HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(TransportModel tanjezOrder)
        {
           
                if (ModelState.IsValid)
                {
                    // إعداد البيانات للإرسال
                    var json = System.Text.Json.JsonSerializer.Serialize(tanjezOrder);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // إرسال البيانات إلى API
                    var response = await _httpClient.PostAsync($"{_baseApiUrl}TransportModels", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // نجح الإرسال
                        TempData["SuccessMessage"] = "تم إضافة الطلب بنجاح";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        // فشل الإرسال
                        var errorContent = await response.Content.ReadAsStringAsync();
                        ModelState.AddModelError("", $"خطأ في إضافة الطلب: {errorContent}");
                    }
                }
            
          
            return View(tanjezOrder);
        }

        public IActionResult Index1()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index1(TransportModel tanjezOrder)
        {

            if (ModelState.IsValid)
            {
                // إعداد البيانات للإرسال
                var json = System.Text.Json.JsonSerializer.Serialize(tanjezOrder);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // إرسال البيانات إلى API
                var response = await _httpClient.PostAsync($"{_baseApiUrl}TransportModels", content);

                if (response.IsSuccessStatusCode)
                {
                    // نجح الإرسال
                    TempData["SuccessMessage"] = "تم إضافة الطلب بنجاح";
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    // فشل الإرسال
                    var errorContent = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"خطأ في إضافة الطلب: {errorContent}");
                }
            }


            return View(tanjezOrder);
        }

        public IActionResult Index3()
        {
            return View();
        }

        public IActionResult Create()
        {
            return PartialView("_Create"); // اسم الفيو الجزئي

        }

    }
}
