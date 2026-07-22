using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class WorkWagesTemplateController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/ // رابط الـ API الخاص بك

        public WorkWagesTemplateController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }

        // GET: عرض الفورم
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


            var wagesTemplate = await GetWorkWagesTemplate();
            var ListwagesTemplate = wagesTemplate.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.WorkWagesName.ToString(),
            }).ToList();
            ViewBag.WagesTemplate = new SelectList(ListwagesTemplate, "Value", "Text");
            // تهيئة 4 صفوف فارغة للجدول
            for (int i = 0; i < 4; i++)
            {
                model.WorkWages.Add(new WorkWagesTaple());
            }

            return View(model);
        }

        private async Task<List<SpecificSpaceTemplate>> Getdepart()
        {
            var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}SpecificSpaceTemplates");
            return JsonConvert.DeserializeObject<List<SpecificSpaceTemplate>>(resp);
        }
        private async Task<List<WorkWagesTemplate>> GetWorkWagesTemplate()
        {
            var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}WorkWagesTemplates");
            return JsonConvert.DeserializeObject<List<WorkWagesTemplate>>(resp);
        }
        // POST: حفظ البيانات
        [HttpPost]
        public async Task<IActionResult> Create(WorkOfferWithDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "البيانات غير صالحة" });
            }

            try
            {
                int templateId = 0;

                // 🔹 تحقق هل القالب موجود مسبقًا
                var existingResponse = await _httpClient.GetAsync($"{_baseApiUrl}WorkWagesTemplates/byname/{model.OfferType.WorkWagesName}");
                if (existingResponse.IsSuccessStatusCode)
                {
                    // القالب موجود → نأخذ Id
                    var existingContent = await existingResponse.Content.ReadAsStringAsync();
                    var existingTemplate = JsonConvert.DeserializeObject<WorkWagesTemplate>(existingContent);

                    if (existingTemplate != null)
                    {
                        templateId = existingTemplate.Id;
                    }
                }

                // 🔹 إذا لم يكن موجود → أنشئ قالب جديد
                if (templateId == 0)
                {
                    var offerTypeResponse = await _httpClient.PostAsJsonAsync(
                        $"{_baseApiUrl}WorkWagesTemplates",
                        model.OfferType
                    );

                    if (!offerTypeResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
                        return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
                    }

                    var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
                    var createdOfferType = JsonConvert.DeserializeObject<WorkWagesTemplate>(offerTypeContent);

                    templateId = createdOfferType.Id;
                }

                // 🔹 ربط صفوف الأجور بنفس القالب (قديم أو جديد)
                foreach (var wage in model.WorkWages)
                {
                    wage.WorkWagesTemplatesID = templateId;
                }

                // 🔹 إضافة/تحديث صفوف الأجور
                var wagesResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}WorkWagesTaples/Multiple",
                    model.WorkWages
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


        //[HttpPost]
        //public async Task<IActionResult> Create(WorkOfferWithDetailsViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, message = "البيانات غير صالحة" });
        //    }

        //    try
        //    {
        //        var offerTypeResponse = await _httpClient.PostAsJsonAsync(
        //            $"{_baseApiUrl}WorkWagesTemplates",
        //            model.OfferType
        //        );

        //        if (!offerTypeResponse.IsSuccessStatusCode)
        //        {
        //            var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //            return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
        //        }

        //        var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //        var createdOfferType = JsonConvert.DeserializeObject<WorkWagesTemplate>(offerTypeContent);

        //        foreach (var wage in model.WorkWages)
        //        {
        //            wage.WorkWagesTemplatesID = createdOfferType.Id;
        //        }

        //        var wagesResponse = await _httpClient.PostAsJsonAsync(
        //            $"{_baseApiUrl}WorkWagesTaples/Multiple",
        //            model.WorkWages
        //        );

        //        if (!wagesResponse.IsSuccessStatusCode)
        //        {
        //            var errorContent = await wagesResponse.Content.ReadAsStringAsync();
        //            return Json(new { success = false, message = $"خطأ عند حفظ بيانات الجدول: {errorContent}" });
        //        }

        //        return Json(new { success = true, message = "تم الحفظ بنجاح ✅" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"خطأ غير متوقع: {ex.Message}" });
        //    }
        //}


    }
}
