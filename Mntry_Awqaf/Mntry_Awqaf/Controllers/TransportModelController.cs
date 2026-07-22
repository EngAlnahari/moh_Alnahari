using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class TransportModelController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        private readonly string Endpoint = "TransportModels";
        public TransportModelController(HttpClient httpClient, IConfiguration configuration)
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
            var model = new TransportModelWithDetails();
            var wagesTemplate = await GetWorkWagesTemplate();
            var ListwagesTemplate = wagesTemplate.Select(d => new SelectListItem
            {
                Value = d.Id.ToString(),
                Text = d.NameTemplete.ToString(),
            }).ToList();
            ViewBag.WagesTemplate = new SelectList(ListwagesTemplate, "Value", "Text");

            //// تهيئة 4 صفوف فارغة للجدول
            //for (int i = 0; i < 5; i++)
            //{
            //    model.transportModels.Add(new TransportModel());
            //}

            return View(model);

        }
      
        [HttpPost]
        public async Task<IActionResult> Create(TransportModelWithDetails model)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "البيانات غير صحيحة. تأكد من إدخال جميع الحقول." });
            }

            try
            {
                int templateId = 0;

                // 🔹 تحقق هل القالب موجود مسبقًا
                var existingResponse = await _httpClient.GetAsync($"{_baseApiUrl}TransportationScheduleTemplates/byname/{model.scheduleTemplate.NameTemplete}");
                if (existingResponse.IsSuccessStatusCode)
                {
                    var existingContent = await existingResponse.Content.ReadAsStringAsync();
                    var existingTemplate = JsonConvert.DeserializeObject<TransportationScheduleTemplate>(existingContent);

                    if (existingTemplate != null)
                    {
                        templateId = existingTemplate.Id;
                    }
                }

                // 🔹 إذا لم يكن موجود → أنشئ قالب جديد
                if (templateId == 0)
                {
                    var templateResponse = await _httpClient.PostAsJsonAsync(
                        $"{_baseApiUrl}TransportationScheduleTemplates",
                        model.scheduleTemplate
                    );

                    if (!templateResponse.IsSuccessStatusCode)
                    {
                        var errorContent = await templateResponse.Content.ReadAsStringAsync();
                        return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
                    }

                    var templateContent = await templateResponse.Content.ReadAsStringAsync();
                    var createdTemplate = JsonConvert.DeserializeObject<TransportationScheduleTemplate>(templateContent);

                    templateId = createdTemplate.Id;
                }

                // 🔹 ربط جميع صفوف transportModels بالقالب (قديم أو جديد)
                foreach (var transport in model.transportModels)
                {
                    transport.TransportationScheduleTemplateID = templateId;
                }

                // 🔹 إضافة/تحديث صفوف transportModels
                var transportResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}TransportModels/Multiple",
                    model.transportModels
                );

                if (!transportResponse.IsSuccessStatusCode)
                {
                    var errorContent = await transportResponse.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = $"خطأ عند حفظ بيانات الجدول: {errorContent}" });
                }

                return Json(new { success = true, message = "✅ تم حفظ القالب وبيانات الجدول بنجاح" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"حدث خطأ غير متوقع: {ex.Message}" });
            }
        }

        //public async Task<IActionResult> Create(TransportModelWithDetails model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Json(new { success = false, message = "البيانات غير صحيحة. تأكد من إدخال جميع الحقول." });
        //    }

        //    try
        //    {
        //        var offerTypeResponse = await _httpClient.PostAsJsonAsync(
        //            $"{_baseApiUrl}TransportationScheduleTemplates",
        //            model.scheduleTemplate
        //        );

        //        if (!offerTypeResponse.IsSuccessStatusCode)
        //        {
        //            var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //            return Json(new { success = false, message = $"خطأ عند حفظ القالب الرئيسي: {errorContent}" });
        //        }

        //        var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //        var createdOfferType = JsonConvert.DeserializeObject<TransportationScheduleTemplate>(offerTypeContent);

        //        foreach (var wage in model.transportModels)
        //        {
        //            wage.TransportationScheduleTemplateID = createdOfferType.Id;
        //        }

        //        var wagesResponse = await _httpClient.PostAsJsonAsync(
        //            $"{_baseApiUrl}TransportModels/Multiple",
        //            model.transportModels
        //        );

        //        if (!wagesResponse.IsSuccessStatusCode)
        //        {
        //            var errorContent = await wagesResponse.Content.ReadAsStringAsync();
        //            return Json(new { success = false, message = $"خطأ عند حفظ بيانات الجدول: {errorContent}" });
        //        }

        //        return Json(new { success = true, message = "✅ تم حفظ القالب وبيانات الجدول بنجاح" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = $"حدث خطأ غير متوقع: {ex.Message}" });
        //    }
        //}

        //        [HttpPost]
        //        public async Task<IActionResult> Create(TransportModelWithDetails model)
        //        {
        //            if (!ModelState.IsValid)
        //            {
        //                return View(model);
        //            }

        //            try
        //            {
        //                var offerTypeResponse = await _httpClient.PostAsJsonAsync(
        //                    $"{_baseApiUrl}TransportationScheduleTemplates",
        //                    model.scheduleTemplate
        //                );

        //                if (!offerTypeResponse.IsSuccessStatusCode)
        //                {
        //                    var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //                    ModelState.AddModelError("", $"حدث خطأ عند حفظ القالب الرئيسي: {errorContent}");
        //                    return View(model);
        //                }

        //                var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
        //                var createdOfferType = JsonConvert.DeserializeObject<TransportationScheduleTemplate>(offerTypeContent);

        //                foreach (var wage in model.transportModels)
        //                {
        //                    wage.TransportationScheduleTemplateID = createdOfferType.Id;
        //                }

        //                var wagesResponse = await _httpClient.PostAsJsonAsync(
        //    $"{_baseApiUrl}TransportModels/Multiple",
        //    model.transportModels
        //);


        //                if (!wagesResponse.IsSuccessStatusCode)
        //                {
        //                    var errorContent = await wagesResponse.Content.ReadAsStringAsync();
        //                    ModelState.AddModelError("", $"حدث خطأ عند حفظ بيانات الجدول: {errorContent}");
        //                    return View(model);
        //                }

        //                TempData["SuccessMessage"] = "تم حفظ القالب وبيانات الجدول بنجاح";
        //                return RedirectToAction("Create");
        //            }
        //            catch (Exception ex)
        //            {
        //                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
        //                return View(model);
        //            }
        //        }

        private async Task<List<TransportationScheduleTemplate>> GetWorkWagesTemplate()
        {
            var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}TransportationScheduleTemplates");
            return JsonConvert.DeserializeObject<List<TransportationScheduleTemplate>>(resp);
        }

        public IActionResult Create1()
        {
            return PartialView(); // اسم الفيو الجزئي

        }
      
      

        public IActionResult Create2()
        {
            return PartialView(); // اسم الفيو الجزئي

        }

        public IActionResult Create3()
        {
            return PartialView(); // اسم الفيو الجزئي

        }

        public IActionResult Create4()
        {
            return PartialView(); // اسم الفيو الجزئي

        }



        public IActionResult Create5()
        {
            return PartialView(); // اسم الفيو الجزئي

        }

        public IActionResult Create6()
        {
            return PartialView(); // اسم الفيو الجزئي

        }






        //[HttpPost]
        //public async Task<IActionResult> Create(TransportModel depart)
        //{
        //    var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}TransportModels", depart);
        //    if (res.IsSuccessStatusCode)
        //    {
        //        var dat = await res.Content.ReadAsStringAsync();
        //        var datuser = JsonConvert.DeserializeObject<TransportModel>(dat);
        //        if (datuser != null)
        //        {
        //            TempData["SuccessMessage"] = "تم إضافة الطلب بنجاح";
        //            return RedirectToAction("Create", "TransportModel");

        //        }

        //    }
        //    return View(res);
        //}
        //public async Task<IActionResult> Create(TransportModel depart)
        //{
        //    var res = await _httpClient.PostAsJsonAsync(URLAPI, depart);

        //    if (res.IsSuccessStatusCode)
        //    {
        //        var dat = await res.Content.ReadAsStringAsync();
        //        var datuser = JsonConvert.DeserializeObject<TransportModel>(dat);

        //        if (datuser != null)
        //        {
        //            return Json(new { success = true, data = datuser });
        //        }
        //    }

        //    return Json(new { success = false, message = "حدث خطأ أثناء الحفظ" });
        //}

    }
    

}
