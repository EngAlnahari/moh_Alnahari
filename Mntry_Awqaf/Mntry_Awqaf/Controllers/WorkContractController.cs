using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class WorkContractController : Controller
    {
        private readonly HttpClient _httpClient;
   
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
      
        public WorkContractController(HttpClient httpClient, IConfiguration configuration)
        {

            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<WorkContract>>(
                $"{_baseApiUrl}WorkContracts"
            );

            try
            {
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                if (_dynamicFormsContext != null)
                {
                    var assignments = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(
                        System.Linq.Queryable.Where(
                            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(_dynamicFormsContext.FieldAssignments, a => a.DynamicField),
                            a => a.ScreenType == "WorkContract"
                        )
                    );
                    
                    var dynamicFields = assignments.Select(a => new {
                        Id = a.Id,
                        Name = a.DynamicField.Name,
                        Type = a.DynamicField.Type
                    }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                    
                    ViewBag.DynamicFields = dynamicFields;
                }

                var valuesResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkContract");
                if (valuesResponse.IsSuccessStatusCode)
                {
                    var valuesContent = await valuesResponse.Content.ReadAsStringAsync();
                    var allRecordValues = System.Text.Json.JsonSerializer.Deserialize<List<DynamicFieldValue>>(valuesContent, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
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
                Console.WriteLine("Error fetching dynamic fields for WorkContract index: " + ex.Message);
            }

            var sortedResponse = response?.OrderByDescending(x => x.Id).ToList() ?? new List<WorkContract>();
            return View(sortedResponse);
        }

        private string ReplacePlaceholdersForWorkContract(string template, WorkContract contract)
        {
            if (string.IsNullOrEmpty(template)) return "";

            // حساب التكلفة الإجمالية
            var totalCost = (contract.WorkCost ?? 0) + (contract.Transportation ?? 0) + (contract.AdditionalCostsTotal ?? 0);

            return template
                .Replace("{TypePiece}", $"({contract.Type ?? "0"})")
                .Replace("{Space}", $"({contract.Space?.ToString() ?? "0"})")
                .Replace("{WorkType}", $"({contract.WorkType ?? ""})")
                .Replace("{Governorate}", $"({contract.Governorate ?? ""})")
                .Replace("{TotalCost}", $"({totalCost.ToString("N0")})")
                .Replace("{OfferName1}", contract.LatePenaltyPercentage?.ToString("N2") ?? "0")
                .Replace("{OfferName2}", contract.AdvancePaymentPercentage?.ToString("N2") ?? "0")
                .Replace("{OfferName3}", contract.ResignationAfterAdvancePercentage?.ToString("N2") ?? "0")
                .Replace("{OfferName5}", contract.CamelResignationPenaltyPercentage?.ToString("N2") ?? "0")
                .Replace("{StartDate}", contract.WorkContractDate ?? "")
                .Replace("{EndDate}", contract.WorkContractDate ?? "");
        }


        public async Task<IActionResult> GetWorkContract(int engineerId)
        {
            try
            {
                var contracts = await _httpClient.GetFromJsonAsync<List<WorkContract>>(
                    $"{_baseApiUrl}WorkContracts/{engineerId}"
                );

                if (contracts != null)
                {
                    foreach (var contract in contracts)
                    {
                        if (contract.ContractItems != null)
                        {
                            foreach (var item in contract.ContractItems)
                            {
                                item.DescriptionTemplate = ReplacePlaceholdersForWorkContract(item.DescriptionTemplate ?? "", contract);
                            }
                        }
                    }
                }

                // هنا إذا أردت عرض عقد واحد يمكن اختيار أول عنصر
                var model = contracts?.FirstOrDefault();
                return PartialView("_WorkContract", model);
            }
            catch (Exception ex)
            {
                return Content("حدث خطأ أثناء تحميل البيانات: " + ex.Message);
            }
        }



        //public async Task<IActionResult> GetWorkContract(int engineerId)
        //{
        //    try
        //    {
        //        // استدعاء الـ API
        //        var borders = await _httpClient.GetFromJsonAsync<List<WorkContract>>(
        //            $"{_baseApiUrl}WorkContracts/{engineerId}"
        //        );


        //        // إعادة PartialView مع البيانات
        //        return PartialView("_WorkContract", borders);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("حدث خطأ أثناء تحميل البيانات: " + ex.Message);
        //    }
        //}










            [HttpPost]
            public async Task<IActionResult> AddAmendments(int contractId, [FromForm] string AmendmentsJson)
            {
                try
                {
                    // التأكد أن العقد موجود
                    var existingContract = await _httpClient.GetFromJsonAsync<WorkContract>(
                        $"{_baseApiUrl}WorkContracts/{contractId}"
                    );
                    if (existingContract == null)
                    {
                        return Json(new { success = false, message = "العقد غير موجود" });
                    }

                    // تحويل JSON إلى اعتراضات
                    var amendments = !string.IsNullOrEmpty(AmendmentsJson)
                        ? JsonSerializer.Deserialize<List<ContractAmendment>>(AmendmentsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                        : new List<ContractAmendment>();

                    foreach (var amend in amendments)
                    {
                        amend.WorkContractId = contractId;
                        amend.CreatedAt = DateTime.Now;

                        await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractAmendments", amend);
                    }

                    // إرسال إشعار للعميل أن المهندس أضاف اعتراض/تعديل
                    var notification = new Notification
                    {
                        UserId = existingContract.ClientId ?? 0, // يروح للعميل
                        ContractId = contractId,
                        Url = $"/User/GetWorkContract?contractId={contractId}",
                        Message = $"قام المهندس بإضافة اعتراض/تعديل على العقد رقم {contractId}.",
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };
                    await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

                    return Json(new { success = true, message = "تم إضافة التعديلات/الاعتراضات بنجاح." });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }




        [HttpPost]
        public async Task<IActionResult> CreateContract([FromForm] WorkContract contract,
                                                [FromForm] string AmendmentsJson,
                                                [FromForm] string NewItemsJson)
        {
            try
            {
                // 🔹 حفظ العقد أولاً
                var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkContracts", contract);
                response.EnsureSuccessStatusCode();
                var createdContract = await response.Content.ReadFromJsonAsync<WorkContract>();

                // 🔹 حفظ الحقول الديناميكية
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workcontract");
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
                                    RecordId = createdContract.Id,
                                    ScreenType = "WorkContract",
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
                            var uniqueName = $"{createdContract.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/workcontract/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = createdContract.Id,
                                ScreenType = "WorkContract",
                                DynamicFieldId = fieldId,
                                Value = relativeUrl
                            });
                        }
                    }
                }

                if (dynamicValues.Any())
                {
                    await _httpClient.PutAsJsonAsync($"{_baseApiUrl}DynamicFieldValues/MultipleUpdate", dynamicValues);
                }

                // 🔹 تحويل JSON إلى قائمة اعتراضات
                var amendments = !string.IsNullOrEmpty(AmendmentsJson)
                    ? System.Text.Json.JsonSerializer.Deserialize<List<ContractAmendment>>(AmendmentsJson, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<ContractAmendment>();

                // 🔹 تحويل JSON إلى قائمة بنود جديدة
                var newItems = !string.IsNullOrEmpty(NewItemsJson)
                    ? System.Text.Json.JsonSerializer.Deserialize<List<ContractItem>>(NewItemsJson, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<ContractItem>();

                // 🔹 ربط الاعتراضات بالعقد وحفظها
                foreach (var amend in amendments)
                {
                    amend.WorkContractId = createdContract.Id;
                    amend.CreatedAt = DateTime.Now;
                    await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractAmendments", amend);
                }

                // 🔹 ربط البنود الجديدة بالعقد وحفظها
                foreach (var item in newItems)
                {
                    item.WorkContractId = createdContract.Id;
                    //item.CreatedAt = DateTime.Now;
                    await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractItems", item);
                }

                // 🔹 إرسال إشعار
                var notification = new Notification
                {
                    UserId = contract.EngineerId ?? 0,
                    ContractId = createdContract.Id,
                    Url = $"/User/GetWorkContract?contractId={createdContract.Id}",
                    Message = $"تم إنشاء عقد جديد للطلب رقم {contract.OrderId} مع البنود والإضافات.",
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };
                await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        //[HttpPost]
        //public async Task<IActionResult> CreateContract([FromForm] WorkContract contract, [FromForm] string AmendmentsJson)
        //{
        //    try
        //    {
        //        // حفظ العقد أولاً
        //        var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkContracts", contract);
        //        response.EnsureSuccessStatusCode();
        //        var createdContract = await response.Content.ReadFromJsonAsync<WorkContract>();

        //        // تحويل JSON إلى قائمة اعتراضات
        //        var amendments = !string.IsNullOrEmpty(AmendmentsJson)
        //            ? JsonSerializer.Deserialize<List<ContractAmendment>>(AmendmentsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
        //            : new List<ContractAmendment>();

        //        // ربط الاعتراضات بالعقد وحفظها
        //        foreach (var amend in amendments)
        //        {
        //            amend.WorkContractId = createdContract.Id;
        //            amend.CreatedAt = DateTime.Now;
        //            await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractAmendments", amend);
        //        }

        //        // إرسال إشعار
        //        var notification = new Notification
        //        {
        //            UserId = contract.EngineerId ?? 0,
        //            ContractId = createdContract.Id,
        //            Message = $"تم إنشاء عقد جديد للطلب رقم {contract.OrderId} مع جميع الإضافات.",
        //            CreatedAt = DateTime.Now,
        //            IsRead = false
        //        };
        //        await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

        //        return Json(new { success = true });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, message = ex.Message });
        //    }
        //}


        //[HttpPost]
        //public async Task<IActionResult> CreateContract(WorkContract contract)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["SuccessMessage"] = "❌ البيانات غير صحيحة";
        //        return RedirectToAction("Index", "TanjezOrder");
        //    }

        //    try
        //    {
        //        // إرسال العقد إلى API
        //        var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkContracts", contract);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            // قراءة العقد المنشأ من الاستجابة للحصول على الـ Id الصحيح
        //            var createdContract = await response.Content.ReadFromJsonAsync<WorkContract>();

        //            if (createdContract != null)
        //            {
        //                // إنشاء الإشعار
        //                var notification = new Notification
        //                {
        //                    UserId = contract.EngineerId ?? 0,
        //                    ContractId = createdContract.Id, // الآن الـ Id صحيح
        //                    Message = $"تم إنشاء عقد جديد للطلب رقم {contract.OrderId}. يرجى المراجعة والموافقة.",
        //                    CreatedAt = DateTime.Now,
        //                    IsRead = false
        //                };

        //                // إرسال الإشعار عبر API
        //                var notifyResponse = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

        //                if (!notifyResponse.IsSuccessStatusCode)
        //                {
        //                    TempData["SuccessMessage"] = "✅ تم إنشاء العقد، لكن فشل إرسال الإشعار للمهندس.";
        //                }
        //                else
        //                {
        //                    TempData["SuccessMessage"] = "✅ تم إنشاء العقد وإرسال الإشعار للمهندس بنجاح.";
        //                }
        //            }
        //            else
        //            {
        //                TempData["SuccessMessage"] = "❌ تم إنشاء العقد لكن لم يتم استرجاع بياناته.";
        //            }
        //        }
        //        else
        //        {
        //            TempData["SuccessMessage"] = "❌ فشل في حفظ العقد. " + response.StatusCode;
        //        }

        //        return RedirectToAction("Index", "TanjezOrder");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["SuccessMessage"] = "⚠ خطأ: " + ex.Message;
        //        return RedirectToAction("Index", "TanjezOrder");
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddAmendment(ContractAmendment model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        TempData["msg"] = "حدث خطأ في البيانات المدخلة!";
        //        return RedirectToAction("ContractDetails", new { id = model.ContractId });
        //    }

        //    try
        //    {
        //        // مثال: إرسال البيانات للـ API
        //        using var client = new HttpClient();
        //        var apiUrl = $"{_baseApiUrl}ContractAmendments"; // رابط الـ API
        //        var response = await client.PostAsJsonAsync(apiUrl, model);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            TempData["msg"] = "تم إرسال البيانات بنجاح!";
        //        }
        //        else
        //        {
        //            TempData["msg"] = "فشل في إرسال البيانات، حاول لاحقاً.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["msg"] = $"حدث خطأ: {ex.Message}";
        //    }

        //    // إعادة توجيه لصفحة العقد نفسها
        //    return RedirectToAction("ContractDetails", new { id = model.ContractId });
        //}



        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Delete Dynamic Fields
                var dfResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkContract");
                if (dfResponse.IsSuccessStatusCode)
                {
                    var dfContent = await dfResponse.Content.ReadAsStringAsync();
                    var allDf = System.Text.Json.JsonSerializer.Deserialize<List<DynamicFieldValue>>(dfContent, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<DynamicFieldValue>();
                    var relatedDf = allDf.Where(df => df.RecordId == id).ToList();
                    foreach (var df in relatedDf)
                    {
                        await _httpClient.DeleteAsync($"{_baseApiUrl}DynamicFieldValues/{df.Id}");
                    }
                }

                // Delete Contract Amendments and Items if they exist in the API
                // Assuming API deletes them automatically due to cascade delete on WorkContract.
                // Otherwise we might need to delete them manually.

                // Delete WorkContract
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}WorkContracts/{id}");
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
