using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class AdditionalCostModelController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        private readonly string Endpoint = "AdditionalCostModels";
        public AdditionalCostModelController(HttpClient httpClient, IConfiguration configuration)
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
                var model = new AdditionalCostWithModel
                {
                    AdditionalCostModeles = new List<AdditionalCostModel> { new AdditionalCostModel() }
                };

                var wagesTemplate = await GetWorkWagesTemplate() ?? new List<AdditionalCostType>();

                var ListwagesTemplate = wagesTemplate
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.AdditionalCostTypeName
                    })
                    .ToList();

                ViewBag.WagesTemp = new SelectList(ListwagesTemplate, "Value", "Text");
                return PartialView("_AdditionalCostFormPartial", model);
            }


       

            [HttpPost]
            public async Task<IActionResult> Create(AdditionalCostWithModel model)
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { success = false, message = "البيانات غير صالحة" });
                }

                try
                {
                    int templateId = 0;

                    // 🔹 تحقق هل القالب موجود مسبقًا
                    var existingResponse = await _httpClient.GetAsync($"{_baseApiUrl}AdditionalCostTypes/byname/{model.AdditionalCost.AdditionalCostTypeName}");
                    if (existingResponse.IsSuccessStatusCode)
                    {
                        // القالب موجود → نأخذ Id
                        var existingContent = await existingResponse.Content.ReadAsStringAsync();
                        var existingTemplate = JsonConvert.DeserializeObject<AdditionalCostType>(existingContent);

                        if (existingTemplate != null)
                        {
                            templateId = existingTemplate.Id;
                        }
                    }

                    // 🔹 إذا لم يكن موجود → أنشئ قالب جديد
                    if (templateId == 0)
                    {
                        var offerTypeResponse = await _httpClient.PostAsJsonAsync(
                            $"{_baseApiUrl}AdditionalCostTypes",
                            model.AdditionalCost
                        );

                        if (!offerTypeResponse.IsSuccessStatusCode)
                        {
                            var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
                            return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
                        }

                        var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
                        var createdOfferType = JsonConvert.DeserializeObject<AdditionalCostType>(offerTypeContent);

                        templateId = createdOfferType.Id;
                    }

                    // 🔹 ربط صفوف الأجور بنفس القالب (قديم أو جديد)
                    foreach (var wage in model.AdditionalCostModeles)
                    {
                        wage.AdditionalCostTypeId = templateId;
                    }

                    // 🔹 إضافة/تحديث صفوف الأجور
                    var wagesResponse = await _httpClient.PostAsJsonAsync(
                        $"{_baseApiUrl}AdditionalCostModels/Multiple",
                        model.AdditionalCostModeles
                    );

                    if (!wagesResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await wagesResponse.Content.ReadAsStringAsync();
                        return Json(new { success = false, message = $"خطأ عند حفظ بيانات الجدول: {errorContent}" });
                    }

                    return Json(new { success = true, message = "تم الحفظ بنجاح ✅" });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = $"خطأ غير متوقع: {ex.Message}" });
                }
            }

        private async Task<List<AdditionalCostType>> GetWorkWagesTemplate()
        {
            try
            {
                var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}AdditionalCostTypes");
                return JsonConvert.DeserializeObject<List<AdditionalCostType>>(resp);
            }
            catch
            {
                return new List<AdditionalCostType>(); // رجع فاضي بدل الخطأ
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(AdditionalCostModel depart)
        //{
        //    try
        //    {
        //        var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}AdditionalCostModels", depart);

        //        if (res.IsSuccessStatusCode)
        //        {
        //            var dat = await res.Content.ReadAsStringAsync();
        //            var datuser = JsonConvert.DeserializeObject<AdditionalCostModel>(dat);

        //            if (datuser != null)
        //            {
        //                TempData["SuccessMessage"] = "تم حفظ القالب وبيانات الجدول بنجاح";
        //                return RedirectToAction("Create", "WorkOffer");
        //            }
        //        }

        //        // لو الرد مش ناجح نعرض محتوى الخطأ القادم من الـ API
        //        var errorContent = await res.Content.ReadAsStringAsync();
        //        return Json(new { success = false, message = $"❌ فشل الإضافة: {errorContent}" });
        //    }
        //    catch (Exception ex)
        //    {
        //        // أي استثناءات داخلية نرجعها هنا
        //        return Json(new { success = false, message = $"⚠️ خطأ غير متوقع: {ex.Message}" });
        //    }
        //}

    }
}
