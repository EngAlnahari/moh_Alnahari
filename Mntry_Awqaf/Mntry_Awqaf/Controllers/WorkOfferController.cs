using System.Diagnostics;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Mntry_Awqaf.Controllers
{
    public class WorkOfferController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public WorkOfferController(HttpClient httpClient, IConfiguration configuration)
        {
           
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;

        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<WorkOffer>>($"{_baseApiUrl}WorkOffers");

            try
            {
                var contracts = await _httpClient.GetFromJsonAsync<List<WorkContract>>($"{_baseApiUrl}WorkContracts");
                ViewBag.Contracts = contracts?
                    .Where(c => c.OfferId.HasValue)
                    .GroupBy(c => c.OfferId.Value)
                    .ToDictionary(g => g.Key, g => g.First().Id) ?? new Dictionary<int, int>();
            }
            catch (Exception ex)
            {
                ViewBag.Contracts = new Dictionary<int, int>();
                Console.WriteLine("Error fetching contracts: " + ex.Message);
            }

            try
            {
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                if (_dynamicFormsContext != null)
                {
                    var assignments = await _dynamicFormsContext.FieldAssignments
                        .Include(a => a.DynamicField)
                        .Where(a => a.ScreenType == "WorkOffer")
                        .ToListAsync();
                    
                    var dynamicFields = assignments.Select(a => new {
                        Id = a.Id,
                        Name = a.DynamicField.Name,
                        Type = a.DynamicField.Type
                    }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                    
                    ViewBag.DynamicFields = dynamicFields;
                }

                var valuesResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}DynamicFieldValues/WorkOffer");
                if (!string.IsNullOrEmpty(valuesResponse))
                {
                    var allRecordValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(valuesResponse);
                    ViewBag.DynamicValues = allRecordValues;
                }
                else
                {
                    ViewBag.DynamicValues = new List<DynamicFieldValue>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.DynamicFields = new List<dynamic>();
                ViewBag.DynamicValues = new List<DynamicFieldValue>();
                Console.WriteLine("Error fetching dynamic fields for index: " + ex.Message);
            }

            var sortedResponse = response?.OrderByDescending(x => x.Id).ToList() ?? new List<WorkOffer>();
            return View(sortedResponse);
        }


        [HttpGet]
        public async Task<IActionResult> CheckCv()
        {
            var userId = HttpContext.Session.GetInt32("UserID");

            if (userId == null)
                return Content("login_required");

            var response = await _httpClient.GetAsync($"{_baseApiUrl}Users/{userId}");

            if (!response.IsSuccessStatusCode)
                return Content("error");

            var user = await response.Content.ReadFromJsonAsync<User>();

            if ((user?.Is_Authentic != "1" || user?.Is_Authentic != "2") && string.IsNullOrEmpty(user?.Cv))
            {
                // المستخدم لا يملك CV وحالته 1 أو 2 ➜ نعرض الـPartial
                return PartialView("_UploadCvPartial", user);
            }


            // المستخدم لديه CV ➜ نرجع نص بسيط ليعرف JavaScript أنه جاهز
            return Content("ok");
        }


      

        // جلب قوالب التنقلات
        private async Task<List<TransportationScheduleTemplate>> Transport()
        {
            try
            {
                var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}TransportationScheduleTemplates");
                return JsonConvert.DeserializeObject<List<TransportationScheduleTemplate>>(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطأ في جلب قوالب التنقلات: " + ex.Message);
                return new List<TransportationScheduleTemplate>();
            }
        }

        private async Task<List<AdditionalCostType>>  Additionalcost()
        {
            try
            {
                var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}AdditionalCostTypes");
                return JsonConvert.DeserializeObject<List<AdditionalCostType>>(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطأ في جلب قوالب التنقلات: " + ex.Message);
                return new List<AdditionalCostType>();
            }
        }

     

     

        // جلب أقسام العمل
        private async Task<List<WorkWagesTemplate>> Getdepart()
        {
            try
            {
                var resp = await _httpClient.GetStringAsync($"{_baseApiUrl}WorkWagesTemplates");
                return JsonConvert.DeserializeObject<List<WorkWagesTemplate>>(resp);
            }
            catch (Exception ex)
            {
                Console.WriteLine("خطأ في جلب الأقسام: " + ex.Message);
                return new List<WorkWagesTemplate>();
            }
        }

        public async Task<IActionResult> GetTemplateDetails(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_baseApiUrl}TransportationScheduleTemplates/GetTransportationTemplate/{id}");

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"فشل جلب البيانات من API. StatusCode: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();
                var template = JsonConvert.DeserializeObject<TransportationScheduleTemplate>(json);

                // إعادة PartialView بدلاً من JSON
                return PartialView("_TransportTemplateDetails", template?.transportModels ?? new List<TransportModel>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ داخلي: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateTransportRow(int id, [FromBody] TransportModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}TransportModels/{id}", model);

                if (response.IsSuccessStatusCode)
                    return Ok("تم التحديث بنجاح");

                var error = await response.Content.ReadAsStringAsync();
                return BadRequest($"فشل التحديث من API. StatusCode: {response.StatusCode}\n{error}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ أثناء التحديث: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public async Task<IActionResult> GetWorkWagesTemplateDetails(int id)
        {
            try
            {
                // جلب البيانات من الـ API
                var response = await _httpClient.GetAsync($"{_baseApiUrl}WorkWagesTemplates/GetWorkWagesTemplateWithDetail/{id}");

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"فشل جلب البيانات من API. StatusCode: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();
                var template = JsonConvert.DeserializeObject<WorkWagesTemplate>(json);

                // إعادة PartialView مع البيانات
                return PartialView("_WorkWagesDetailsTable", template?.workWagesTaples ?? new List<WorkWagesTaple>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ داخلي: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateWorkWage(int id, [FromBody] WorkWagesTaple model)
        {
            try
            {
                // تحقق من صحة البيانات في الـ ModelState
                if (!ModelState.IsValid)
                {
                    var modelErrors = string.Join("\n", ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage));
                    return BadRequest($"فشل التحديث: أخطاء التحقق من البيانات:\n{modelErrors}");
                }

                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}WorkWagesTaples/{id}", model);

                if (response.IsSuccessStatusCode)
                    return Ok("تم التحديث بنجاح");

                // قراءة نص الخطأ من API
                var error = await response.Content.ReadAsStringAsync();

                // إظهار التفاصيل مع StatusCode
                return BadRequest($"فشل التحديث من API. StatusCode: {response.StatusCode}\n{error}");
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode(500, $"حدث خطأ في الاتصال بالـ API: {httpEx.Message}\n{httpEx.StackTrace}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ أثناء تحديث البيانات: {ex.Message}\n{ex.StackTrace}");
            }
        }

        public async Task<IActionResult> GetAdditionalCostDetails(int id)
        {
            try
            {
                // جلب البيانات من الـ API
                var response = await _httpClient.GetAsync($"{_baseApiUrl}AdditionalCostTypes/GetAdditionalCostTemplate/{id}");

                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, $"فشل جلب البيانات من API. StatusCode: {response.StatusCode}");

                var json = await response.Content.ReadAsStringAsync();
                var template = JsonConvert.DeserializeObject<AdditionalCostType>(json);

                // إعادة PartialView مع البيانات
                return PartialView("_AdditionalCostDetailsPartial", template?.additionalCostModels ?? new List<AdditionalCostModel>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ داخلي: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdditionalCost(int id, [FromBody] AdditionalCostModel model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}AdditionalCostModels/{id}", model);

                if (response.IsSuccessStatusCode)
                    return Ok("تم التحديث بنجاح");

                var error = await response.Content.ReadAsStringAsync();
                return BadRequest($"فشل التحديث من API. StatusCode: {response.StatusCode}\n{error}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"حدث خطأ أثناء التحديث: {ex.Message}\n{ex.StackTrace}");
            }
        }


        public async Task<IActionResult> Create()
        {

            // جلب بيانات الأقسام
            var dept = await Getdepart();
            ViewBag.Departs = dept;

            // جلب قوالب التنقلات
            var transport = await Transport();
            ViewBag.Transports = transport;

            var additionalc = await Additionalcost();
            ViewBag.AdditionalCosts = additionalc;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWithDynamicFields(WorkOffer model)
        {
            model.OfferName = model.OfferName ?? "عرض جديد";
            // Ensure ID is default so DB generates it
            model.Id = 0;
            
            try
            {
                // 1. Create WorkOffer
                var offerResponse = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkOffers", model);
                if (!offerResponse.IsSuccessStatusCode)
                {
                    var errorContent = await offerResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"حدث خطأ عند حفظ العرض: {errorContent}");
                    return View("Create", model);
                }

                var createdOffer = JsonConvert.DeserializeObject<WorkOffer>(await offerResponse.Content.ReadAsStringAsync());

                // 2. Collect Dynamic Fields
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workoffer");
                Directory.CreateDirectory(uploadsFolder);

                // Text fields
                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("DynamicField_"))
                    {
                        var fieldIdStr = key.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            var textValue = form[key].ToString();
                            if (!string.IsNullOrWhiteSpace(textValue))
                            {
                                dynamicValues.Add(new DynamicFieldValue
                                {
                                    RecordId = createdOffer.Id,
                                    ScreenType = "WorkOffer",
                                    DynamicFieldId = fieldId,
                                    Value = textValue
                                });
                            }
                        }
                    }
                }

                // File fields
                foreach (var file in Request.Form.Files)
                {
                    if (file.Name.StartsWith("DynamicField_") && file.Length > 0)
                    {
                        var fieldIdStr = file.Name.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            var ext = Path.GetExtension(file.FileName);
                            var uniqueName = $"{createdOffer.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/workoffer/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = createdOffer.Id,
                                ScreenType = "WorkOffer",
                                DynamicFieldId = fieldId,
                                Value = relativeUrl
                            });
                        }
                    }
                }

                // 3. Save Dynamic Fields
                if (dynamicValues.Any())
                {
                    var valuesResponse = await _httpClient.PostAsJsonAsync(
                        $"{_baseApiUrl}DynamicFieldValues/Multiple",
                        dynamicValues
                    );
                    if (!valuesResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "تم حفظ العرض ولكن فشل حفظ الحقول الديناميكية.");
                    }
                }

                TempData["SuccessMessage"] = "تم حفظ العرض مع الحقول بنجاح";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
                return View("Create", model);
            }
        }

        private async Task PopulateDropdowns()
        {
            var dept = await Getdepart();
            ViewBag.Departs = dept;

            // جلب قوالب التنقلات
            var transport = await Transport();
            ViewBag.Transports = transport;

            var additionalc = await Additionalcost();
            ViewBag.AdditionalCosts = additionalc;

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Fetch dynamic values for this record
            var dynResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkOffer");
            if (dynResponse.IsSuccessStatusCode)
            {
                var dynContent = await dynResponse.Content.ReadAsStringAsync();
                var allValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(dynContent);
                ViewBag.DynamicValues = allValues.Where(v => v.RecordId == id).ToList();
            }
            else
            {
                ViewBag.DynamicValues = new List<DynamicFieldValue>();
            }

            // جلب بيانات العنصر المطلوب تعديله من الـ API
            var response = await _httpClient.GetAsync($"{_baseApiUrl}WorkOffers/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<WorkOffer>(json);
                return View(model); // تمرير النموذج إلى صفحة التعديل
            }

            TempData["ErrorMessage"] = "تعذر تحميل البيانات للتعديل.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkOffer model)
        {
            try
            {
                // 1. Update WorkOffer itself
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}WorkOffers/{model.Id}", model);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = "فشل تعديل العرض ❌";
                    return View(model);
                }

                // 2. Collect Dynamic Fields
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workoffer");
                Directory.CreateDirectory(uploadsFolder);

                // Text fields
                foreach (var key in form.Keys)
                {
                    if (key.StartsWith("DynamicField_"))
                    {
                        var fieldIdStr = key.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            var textValue = form[key].ToString();
                            if (!string.IsNullOrWhiteSpace(textValue))
                            {
                                dynamicValues.Add(new DynamicFieldValue
                                {
                                    RecordId = model.Id,
                                    ScreenType = "WorkOffer",
                                    DynamicFieldId = fieldId,
                                    Value = textValue
                                });
                            }
                        }
                    }
                }

                // File fields
                foreach (var file in Request.Form.Files)
                {
                    if (file.Name.StartsWith("DynamicField_") && file.Length > 0)
                    {
                        var fieldIdStr = file.Name.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            var ext = Path.GetExtension(file.FileName);
                            var uniqueName = $"{model.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/workoffer/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = model.Id,
                                ScreenType = "WorkOffer",
                                DynamicFieldId = fieldId,
                                Value = relativeUrl
                            });
                        }
                    }
                }

                // 3. Save Dynamic Fields using MultipleUpdate
                if (dynamicValues.Any())
                {
                    var valuesResponse = await _httpClient.PutAsJsonAsync(
                        $"{_baseApiUrl}DynamicFieldValues/MultipleUpdate",
                        dynamicValues
                    );
                    if (!valuesResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "تم تعديل العرض ولكن فشل تحديث الحقول الديناميكية.");
                    }
                }

                TempData["SuccessMessage"] = "تم تعديل الطلب بنجاح ✅";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
                return View(model);
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Objected()
        {
            return View();
        }
        public IActionResult Unacceptable()
        {
            return View();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}WorkOffers/{id}");
                if (response.IsSuccessStatusCode)
                    return Json(new { success = true, message = "تم الحذف بنجاح" });
                else
                    return Json(new { success = false, message = "فشل الحذف، حاول مرة أخرى" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }
}
