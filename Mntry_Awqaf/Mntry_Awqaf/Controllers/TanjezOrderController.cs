using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Reflection;

namespace Mntry_Awqaf.Controllers
{
    public class TanjezOrderController : BaseApiController<TanjezOrder>
    {
        
     
        private readonly string _baseApiUrl; 
        private readonly string Endpoint = "TanjezOrders";
        public TanjezOrderController(HttpClient httpClient, IConfiguration configuration)
       : base(httpClient, configuration, "TanjezOrders")
        {
            _baseApiUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5046/api/";
        }


        // GET: Edit
        private async Task PopulateDropdowns()
        {
            var documentTypes = await GetData<DocumentType>($"{_baseApiUrl}DocumentTypes");
            ViewBag.DocumentTypes = new SelectList(documentTypes ?? new List<DocumentType>(), "Id", "DocumentTypeName");

            var ownerships = await GetData<ownership>($"{_baseApiUrl}ownerships");
            ViewBag.Ownerships = new SelectList(ownerships ?? new List<ownership>(), "Id", "Name");

            var accessRoadType = await GetData<AccessRoadType>($"{_baseApiUrl}AccessRoadTypes");
            ViewBag.AccessRoadTypes = new SelectList(accessRoadType ?? new List<AccessRoadType>(), "Id", "AccessRoad");

            var placeTypes = await GetData<PlaceType>($"{_baseApiUrl}PlaceTypes");
            ViewBag.PlaceTypes = new SelectList(placeTypes ?? new List<PlaceType>(), "PlaceTypeName", "PlaceTypeName");

            var purposes = await GetData<PurposeSurvey>($"{_baseApiUrl}PurposeSurveys");
            ViewBag.Purposes = new SelectList(purposes ?? new List<PurposeSurvey>(), "Id", "Name");
        }

        // في الـ GET
        public async Task<IActionResult> Edit(int id)
        {
            var orderResponse = await _httpClient.GetAsync($"{_baseApiUrl}TanjezOrders/{id}");
            if (!orderResponse.IsSuccessStatusCode) return NotFound();

            var tanjezOrder = JsonConvert.DeserializeObject<TanjezOrder>(await orderResponse.Content.ReadAsStringAsync());

            var earthBorders = new List<EarthBorders>();
            var earthResponse = await _httpClient.GetAsync($"{_baseApiUrl}EarthBorders/ByOrder/{id}");
            if (earthResponse.IsSuccessStatusCode)
            {
                earthBorders = JsonConvert.DeserializeObject<List<EarthBorders>>(await earthResponse.Content.ReadAsStringAsync());
            }

            var model = new TanjezOrderWithEarthBordersViewModel
            {
                tanjezOrder = tanjezOrder,
                earthBorders = earthBorders
            };

            await PopulateDropdowns();

            try
            {
                // Fetch dynamic fields assignments
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                var assignments = await _dynamicFormsContext.FieldAssignments
                    .Include(a => a.DynamicField)
                    .Where(a => a.ScreenType == "TanjezOrder")
                    .ToListAsync();
                
                var dynamicFields = assignments.Select(a => new {
                    Id = a.Id,
                    Name = a.DynamicField.Name,
                    Type = a.DynamicField.Type
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                
                ViewBag.DynamicFields = dynamicFields;

                // Fetch dynamic field values for THIS record
                var valuesResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}DynamicFieldValues/TanjezOrder/{id}");
                var recordDynamicValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(valuesResponse);
                ViewBag.DynamicValues = recordDynamicValues;
            }
            catch (Exception ex)
            {
                ViewBag.DynamicFields = new List<dynamic>();
                ViewBag.DynamicValues = new List<DynamicFieldValue>();
                Console.WriteLine("Error fetching dynamic fields in Edit: " + ex.Message);
            }

            return View(model);
        }

        // في الـ POST
        [HttpPost]
        public async Task<IActionResult> Edit(TanjezOrderWithEarthBordersViewModel model)
        {
            try
            {
                // لا نحتاج للتحقق من ModelState لأننا أزلنا الحقول الثابتة
                // ولن نقوم بتحديث TanjezOrder نفسها عبر الـ API لأن كل البيانات أصبحت في الحقول الديناميكية


                // Collect Dynamic Fields
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "tanjezorder");
                Directory.CreateDirectory(uploadsFolder);

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
                                    RecordId = model.tanjezOrder.Id,
                                    ScreenType = "TanjezOrder",
                                    DynamicFieldId = fieldId,
                                    Value = textValue
                                });
                            }
                        }
                    }
                }

                foreach (var file in Request.Form.Files)
                {
                    if (file.Name.StartsWith("DynamicField_") && file.Length > 0)
                    {
                        var fieldIdStr = file.Name.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            var ext = Path.GetExtension(file.FileName);
                            var uniqueName = $"{model.tanjezOrder.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/tanjezorder/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = model.tanjezOrder.Id,
                                ScreenType = "TanjezOrder",
                                DynamicFieldId = fieldId,
                                Value = relativeUrl
                            });
                        }
                    }
                }

                if (dynamicValues.Any())
                {
                    var valuesResponse = await _httpClient.PutAsJsonAsync(
                        $"{_baseApiUrl}DynamicFieldValues/MultipleUpdate",
                        dynamicValues
                    );
                    if (!valuesResponse.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError("", "تم التحديث ولكن فشل تحديث الحقول الديناميكية.");
                    }
                }

                TempData["SuccessMessage"] = "تم تحديث البيانات بنجاح";
                return RedirectToAction("Edit", new { id = model.tanjezOrder.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"خطأ غير متوقع: {ex.Message}");
                await PopulateDropdowns();
                return View(model);
            }
        }



        
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<TanjezOrder>>(
                _urlApi
            );

            try
            {
                var workContractsUrl = _urlApi.Replace("TanjezOrders", "WorkContracts");
                var contracts = await _httpClient.GetFromJsonAsync<List<WorkContract>>(workContractsUrl);
                ViewBag.Contracts = contracts?
                    .Where(c => c.OrderId.HasValue)
                    .GroupBy(c => c.OrderId.Value)
                    .ToDictionary(g => g.Key, g => g.First().Id) ?? new Dictionary<int, int>();
            }
            catch (Exception ex)
            {
                ViewBag.Contracts = new Dictionary<int, int>();
                Console.WriteLine("Error fetching contracts: " + ex.Message);
            }

            try
            {
                // Fetch dynamic fields assignments from local context
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                var assignments = await _dynamicFormsContext.FieldAssignments
                    .Include(a => a.DynamicField)
                    .Where(a => a.ScreenType == "TanjezOrder")
                    .ToListAsync();
                
                // Extract unique dynamic fields to build table headers based on FieldAssignments
                var dynamicFields = assignments.Select(a => new {
                    Id = a.Id,                  // FieldAssignment.Id — يتطابق مع DynamicFieldValue.DynamicFieldId
                    Name = a.DynamicField.Name,
                    Type = a.DynamicField.Type  // نوع الحقل لعرض الصور أو الملفات
                }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                
                ViewBag.DynamicFields = dynamicFields;

                // Fetch dynamic field values
                var valuesResponse = await _httpClient.GetStringAsync($"{_baseApiUrl}DynamicFieldValues/TanjezOrder");
                var dynamicValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(valuesResponse);
                ViewBag.DynamicValues = dynamicValues;
            }
            catch (Exception ex)
            {
                ViewBag.DynamicFields = new List<dynamic>();
                ViewBag.DynamicValues = new List<DynamicFieldValue>();
                Console.WriteLine("Error fetching dynamic fields: " + ex.Message);
            }

            return View(response);
        }


        // جلب جميع المستخدمين (للـ Select2)
        // جلب جميع المستخدمين للـ Select2
        public async Task<IActionResult> GetUsers()
        {
            var response = await _httpClient.GetFromJsonAsync<List<User>>($"{_baseApiUrl}Users");
            return Json(response.Select(u => new { id = u.ID, text = u.Frist_Name, phone = u.Phone_number1 }));
        }

        // جلب العروض المرتبطة بالمستخدم
        public async Task<IActionResult> GetUserOffers(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<List<WorkOffer>>(
                $"{_baseApiUrl}Users/GetUserOffers/{id}"
            );

            return Json(new { workOffers = response });
        }





        public async Task<IActionResult> GetBorders(int orderId )
        {
            try
            {
                // استدعاء الـ API
                var borders = await _httpClient.GetFromJsonAsync<List<EarthBorders>>(
                    $"{_baseApiUrl}TanjezOrders/GetBordersByOrder/{orderId}"
                );

                // إعادة PartialView مع البيانات
                return PartialView("_BordersPartial", borders);
            }
            catch (Exception ex)
            {
                return Content("حدث خطأ أثناء تحميل البيانات: " + ex.Message);
            }
        }
        private string ReplacePlaceholders(string template, WorkOfferDataDto model)
        {
            if (string.IsNullOrEmpty(template)) return "";
            var totalCost =
    (model.SpecificSpaceTemplates.FirstOrDefault()?.Price ?? 0) +
    model.TotalTransportationCost +
    model.AdditionalCostsTotal;
            return template
                 .Replace("{TypePiece}", $"({model.TanjezOrder?.TypePiece ?? "0"})")
                .Replace("{Space}", $"({model.TanjezOrder?.Space?.ToString() ?? "00"})")
                .Replace("{WorkType}", $"({model.TanjezOrder?.PlaceName ?? ""})")
                .Replace("{Governorate}", $"({model.TanjezOrder?.City ?? ""})")
                .Replace("{TotalCost}", $"({totalCost.ToString()})")

                .Replace("{OfferName1}", model.OfferName1?.ToString() ?? "0")
                .Replace("{OfferName2}", model.OfferName2?.ToString() ?? "0")
                .Replace("{OfferName3}", model.OfferName3?.ToString() ?? "0")
                .Replace("{OfferName5}", model.OfferName5?.ToString() ?? "0")
                .Replace("{StartDate}", model.TanjezOrder?.PeriodType ?? "")
                .Replace("{EndDate}", model.TanjezOrder?.MaximumDuration ?? "00");
        }


        [HttpPost]
        public async Task<IActionResult> ChooseEngineer(
     int offerId,
     string type,
     int space,
     string workType,
     string governorateParam,
     string transportationFeesPayer,
     string overNightFeesPayer,
     string allowanceFeesPayer,
     int? orderId)
        {
            try
            {
                // طلب بيانات العقد
                var apiUrl = $"{_baseApiUrl}WorkOffers/GetUserData/{offerId}?type={type}&space={space}&workType={workType}&governorateParam={governorateParam}&transportationFeesPayer={transportationFeesPayer}&overNightFeesPayer={overNightFeesPayer}&allowanceFeesPayer={allowanceFeesPayer}&orderId={orderId}";
                var response = await _httpClient.GetAsync(apiUrl);

                if (!response.IsSuccessStatusCode)
                    return BadRequest("فشل الاتصال بالـ API");

                var result = await response.Content.ReadFromJsonAsync<WorkOfferDataDto>();

                // 🔹 استدعاء API لجلب البنود
                var itemsResponse = await _httpClient.GetAsync($"{_baseApiUrl}ContractItems/GetContractItems");
                if (itemsResponse.IsSuccessStatusCode)
                {
                    var items = await itemsResponse.Content.ReadFromJsonAsync<List<ContractItemDto>>();

                    // استبدال النصوص قبل إرسالها للـ View
                    foreach (var item in items)
                    {
                        item.DescriptionTemplate = ReplacePlaceholders(item.DescriptionTemplate, result);
                    }

                    result.ContractItems = items;
                }

                return View(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ: {ex.Message}");
            }
        }

        

        [HttpPost]
        public async Task<IActionResult> Choose(int userId, string locat, decimal width, string workType)
        {
            try
            {
                // بناء URL بشكل صحيح
                var apiUrl = $"{_baseApiUrl}WorkOffers/GetUserData/{userId}?type={locat}&space={width}&workType={workType}";

                var response = await _httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<WorkOfferDataDto>();
                    return View(result);
                }

                return BadRequest("فشل الاتصال بالـ API");
            }
            catch (Exception ex)
            {
                return BadRequest($"حدث خطأ: {ex.Message}");
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> Choose(int userId, string locat, decimal width)
        //{
        //    try
        //    {
        //        // بناء URL بشكل صحيح (userId في المسار, والباقي في Query String)
        //        var apiUrl = $"{_baseApiUrl}WorkOffers/GetUserData/{userId}?type={locat}&space={width}";

        //        var response = await _httpClient.GetAsync(apiUrl);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadFromJsonAsync<UserData>();
        //            return View(result);
        //        }

        //        return BadRequest("فشل الاتصال بالـ API");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"حدث خطأ: {ex.Message}");
        //    }
        //}

        // إضافة دالة GET لعرض النموذج الأولي

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
      
        private async Task<List<T>> GetData<T>(string url) where T : class
        {
            var resp = await _httpClient.GetStringAsync(url);
            var list = JsonConvert.DeserializeObject<List<T>>(resp);
            return list ?? new List<T>();
        }

       


        //private async Task<List<SpecificSpaceTemplate>> Getdepart()
        //{
        //    var resp = await _httpClient.GetStringAsync("http://awqaf12.somee.com/api/PurposeSurveys");
        //    return JsonConvert.DeserializeObject<List<SpecificSpaceTemplate>>(resp);
        //}
        // POST: حفظ البيانات
        public async Task<IActionResult> Create()
        {
            var model = new TanjezOrderWithEarthBordersViewModel();

            // جلب أنواع المستندات
            var documentTypes = await GetData<DocumentType>($"{_baseApiUrl}DocumentTypes");
            ViewBag.DocumentTypes = new SelectList(documentTypes ?? new List<DocumentType>(), "Id", "DocumentTypeName");

            // جلب الملكيات
            var ownerships = await GetData<ownership>($"{_baseApiUrl}ownerships");
            ViewBag.Ownerships = new SelectList(ownerships ?? new List<ownership>(), "Id", "Name");

            // جلب طرق الوصول
            var accessRoadType = await GetData<AccessRoadType>($"{_baseApiUrl}AccessRoadTypes");
            ViewBag.AccessRoadTypes = new SelectList(accessRoadType ?? new List<AccessRoadType>(), "Id", "AccessRoad");

            // جلب أنواع الأماكن
            var placeTypes = await GetData<PlaceType>($"{_baseApiUrl}PlaceTypes");
            ViewBag.PlaceTypes = new SelectList(placeTypes ?? new List<PlaceType>(), "PlaceTypeName", "PlaceTypeName");

            // جلب أغراض المسح
            var purposes = await GetData<PurposeSurvey>($"{_baseApiUrl}PurposeSurveys");
            ViewBag.Purposes = new SelectList(purposes ?? new List<PurposeSurvey>(), "Id", "Name");

            // تهيئة 4 صفوف فارغة للجدول
            for (int i = 0; i < 4; i++)
            {
                model.earthBorders.Add(new EarthBorders());
            }

            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> CreateWithDynamicFields(TanjezOrderWithEarthBordersViewModel model)
        {
            if (model.tanjezOrder == null)
            {
                model.tanjezOrder = new TanjezOrder();
            }
            model.tanjezOrder.TypePiece = model.tanjezOrder.TypePiece ?? "-";
            model.tanjezOrder.City = model.tanjezOrder.City ?? "-";
            model.tanjezOrder.Status = "جديد";
            model.tanjezOrder.CreatedAt = DateTime.Now;
            try
            {
                // 1. Create TanjezOrder
                var offerTypeResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}TanjezOrders",
                    model.tanjezOrder
                );

                if (!offerTypeResponse.IsSuccessStatusCode)
                {
                    var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"حدث خطأ عند حفظ الطلب الرئيسي: {errorContent}");
                    return View("Create", model);
                }

                var createdOfferType = JsonConvert.DeserializeObject<TanjezOrder>(await offerTypeResponse.Content.ReadAsStringAsync());

                // 2. Collect Dynamic Fields (نصوص + ملفات)
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "tanjezorder");
                Directory.CreateDirectory(uploadsFolder); // تأكد من وجود المجلد

                // أ) الحقول النصية العادية
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
                                    RecordId = createdOfferType.Id,
                                    ScreenType = "TanjezOrder",
                                    DynamicFieldId = fieldId,
                                    Value = textValue
                                });
                            }
                        }
                    }
                }

                // ب) حقول الملفات (صور + مستندات)
                foreach (var file in Request.Form.Files)
                {
                    if (file.Name.StartsWith("DynamicField_") && file.Length > 0)
                    {
                        var fieldIdStr = file.Name.Replace("DynamicField_", "");
                        if (int.TryParse(fieldIdStr, out int fieldId))
                        {
                            // اسم فريد للملف: recordId_fieldId_timestamp.ext
                            var ext = Path.GetExtension(file.FileName);
                            var uniqueName = $"{createdOfferType.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            // تخزين المسار النسبي للرابط
                            var relativeUrl = $"/uploads/tanjezorder/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = createdOfferType.Id,
                                ScreenType = "TanjezOrder",
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
                        ModelState.AddModelError("", "تم حفظ الطلب ولكن فشل حفظ الحقول الديناميكية.");
                    }
                }

                TempData["SuccessMessage"] = "تم حفظ الطلب مع جميع الحقول بنجاح";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
                return View("Create", model);
            }
        }

[HttpPost]
        public async Task<IActionResult> Create(TanjezOrderWithEarthBordersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var offerTypeResponse = await _httpClient.PostAsJsonAsync(
                    $"{_baseApiUrl}TanjezOrders",
                    model.tanjezOrder
                );

                if (!offerTypeResponse.IsSuccessStatusCode)
                {
                    var errorContent = await offerTypeResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"حدث خطأ عند حفظ القالب الرئيسي: {errorContent}");
                    return View(model);
                }

                var offerTypeContent = await offerTypeResponse.Content.ReadAsStringAsync();
                var createdOfferType = JsonConvert.DeserializeObject<TanjezOrder>(offerTypeContent);

                foreach (var wage in model.earthBorders)
                {
                    wage.TanjezOrderID = createdOfferType.Id;
                }

                var wagesResponse = await _httpClient.PostAsJsonAsync(
    $"{_baseApiUrl}EarthBorders/Multiple",
    model.earthBorders
);


                if (!wagesResponse.IsSuccessStatusCode)
                {
                    var errorContent = await wagesResponse.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", $"حدث خطأ عند حفظ بيانات الجدول: {errorContent}");
                    return View(model);
                }

                TempData["SuccessMessage"] = "تم حفظ القالب وبيانات الجدول بنجاح";
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
                return View(model);
            }
        }


      

        public IActionResult CreateContractItems()
        {
            return PartialView("_Create");
        }




        [HttpPost]
        public async Task<IActionResult> CreateContractItems([FromBody] ContractItem depart)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractItems", depart);
            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<ContractItem>(dat);
                if (datuser != null)
                {
                    // إعادة JSON بدل Redirect
                    return Json(new { id = datuser.Id, title = datuser.Title });
                }
            }
            return BadRequest("حدث خطأ أثناء الحفظ");
        }




        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var member = await GetByIdAsync(id);
            if (member == null)
                return NotFound();

            return PartialView("_DeleteModal", member);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}TanjezOrders/{id}");
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
