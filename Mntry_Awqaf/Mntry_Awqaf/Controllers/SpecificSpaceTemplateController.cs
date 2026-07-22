using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class SpecificSpaceTemplateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public SpecificSpaceTemplateController( HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
      

        public IActionResult Create()
        {
            var model = new SpecificwithAlmjal();

            // تهيئة 4 صفوف فارغة للجدول
            for (int i = 0; i < 3; i++)
            {
                model.specificSpaceTemplates.Add(new SpecificSpaceTemplate());
            }

            return View(model);

        }



        [HttpPost]
        public async Task<IActionResult> Create(SpecificwithAlmjal model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "البيانات غير صحيحة. تأكد من إدخال جميع الحقول." });
            }

            try
            {
                var offerTypeResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}Almjals",
                    model.almjal
                );

                if (!offerTypeResponse.IsSuccessStatusCode)
                {
                    var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
                }

                var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
                var createdOfferType = JsonConvert.DeserializeObject<Almjal>(offerTypeContent);

                foreach (var wage in model.specificSpaceTemplates)
                {
                    wage.AlmjalID = createdOfferType.ID;
                }

                var wagesResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}SpecificSpaceTemplates/Multiple",
                    model.specificSpaceTemplates
                );

                if (!wagesResponse.IsSuccessStatusCode)
                {
                    var errorContent = await wagesResponse.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"خطأ عند حفظ بيانات الجدول: {errorContent}" });
                }

                return Json(new { success = true, message = "✅ تم حفظ القالب وبيانات الجدول بنجاح", newId = createdOfferType.ID });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"حدث خطأ غير متوقع: {ex.Message}" });
            }
        }

        //public async Task<IActionResult> Create(List<SpecificSpaceTemplate> templates)
        //{
        //    try
        //    {
        //        var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}SpecificSpaceTemplates", templates);

        //        if (res.IsSuccessStatusCode)
        //        {
        //            return Json(new { success = true, message = "تم الحفظ بنجاح ✅" });
        //        }
        //        else
        //        {
        //            var errorContent = await res.Content.ReadAsStringAsync();
        //            return Json(new { success = false, message = $"فشل الحفظ ❌: {errorContent}" });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"حدث خطأ غير متوقع: {ex.Message}" });
        //    }
        //}





    }
}
