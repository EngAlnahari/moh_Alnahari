using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class Work_FormController : BaseApiController<WorkForm>
    {
        private readonly string _baseApiUrl;

        public Work_FormController(HttpClient httpClient, IConfiguration configuration)
: base(httpClient, configuration, "WorkForms")
        {
            _baseApiUrl = configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5046/api/";
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<WorkForm>>(
                _urlApi
            );

            try
            {
                var _dynamicFormsContext = HttpContext.RequestServices.GetService<Mntry_Awqaf.Data.DynamicFormsDbContext>();
                if (_dynamicFormsContext != null)
                {
                    var assignments = await Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.ToListAsync(
                        System.Linq.Queryable.Where(
                            Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(_dynamicFormsContext.FieldAssignments, a => a.DynamicField),
                            a => a.ScreenType == "WorkForm"
                        )
                    );
                    
                    var dynamicFields = assignments.Select(a => new {
                        Id = a.Id,
                        Name = a.DynamicField.Name,
                        Type = a.DynamicField.Type
                    }).GroupBy(f => f.Id).Select(g => g.First()).ToList<dynamic>();
                    
                    ViewBag.DynamicFields = dynamicFields;
                }

                var valuesResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkForm");
                if (valuesResponse.IsSuccessStatusCode)
                {
                    var valuesContent = await valuesResponse.Content.ReadAsStringAsync();
                    var allRecordValues = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(valuesContent);
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

            var sortedResponse = response?.OrderByDescending(x => x.Id).ToList() ?? new List<WorkForm>();
            return View(sortedResponse);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateWithDynamicFields()
        {
            try
            {
                var model = new WorkForm();
                // 1. Create WorkForm to get ID
                var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}WorkForms", model);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = "فشل إنشاء الاستمارة ❌";
                    return RedirectToAction(nameof(Create));
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var createdWorkForm = JsonConvert.DeserializeObject<WorkForm>(responseContent);
                int recordId = createdWorkForm.Id;

                // 2. Collect Dynamic Fields
                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workform");
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
                                    RecordId = recordId,
                                    ScreenType = "WorkForm",
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
                            var uniqueName = $"{recordId}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/workform/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = recordId,
                                ScreenType = "WorkForm",
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
                        TempData["ErrorMessage"] = "تم الإنشاء ولكن فشل حفظ بعض الحقول الديناميكية.";
                    }
                }

                TempData["SuccessMessage"] = "تم إنشاء الاستمارة بنجاح ✅";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"حدث خطأ غير متوقع: {ex.Message}";
                return RedirectToAction(nameof(Create));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dynResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkForm");
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

            var response = await _httpClient.GetAsync($"{_baseApiUrl}WorkForms/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var workForm = JsonConvert.DeserializeObject<WorkForm>(responseContent);
            return View(workForm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(WorkForm model)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}WorkForms/{model.Id}", model);
                if (!response.IsSuccessStatusCode)
                {
                    TempData["ErrorMessage"] = "فشل تعديل الاستمارة ❌";
                    return View(model);
                }

                var form = Request.Form;
                var dynamicValues = new List<DynamicFieldValue>();
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "workform");
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
                                    RecordId = model.Id,
                                    ScreenType = "WorkForm",
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
                            var uniqueName = $"{model.Id}_{fieldId}_{DateTime.Now:yyyyMMddHHmmss}{ext}";
                            var filePath = Path.Combine(uploadsFolder, uniqueName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var relativeUrl = $"/uploads/workform/{uniqueName}";
                            dynamicValues.Add(new DynamicFieldValue
                            {
                                RecordId = model.Id,
                                ScreenType = "WorkForm",
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
                        ModelState.AddModelError("", "تم تعديل الاستمارة ولكن فشل تحديث الحقول الديناميكية.");
                    }
                }

                TempData["SuccessMessage"] = "تم التعديل بنجاح ✅";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"حدث خطأ غير متوقع: {ex.Message}");
                return View(model);
            }
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // First, find and delete any related LandBoundaryModels to prevent FK errors
                var landBoundariesResponse = await _httpClient.GetAsync($"{_baseApiUrl}LandBoundaryModels");
                if (landBoundariesResponse.IsSuccessStatusCode)
                {
                    var lbContent = await landBoundariesResponse.Content.ReadAsStringAsync();
                    var allLb = JsonConvert.DeserializeObject<List<LandBoundaryModel>>(lbContent) ?? new List<LandBoundaryModel>();
                    var relatedLb = allLb.Where(lb => lb.WorkFormId == id).ToList();
                    foreach (var lb in relatedLb)
                    {
                        await _httpClient.DeleteAsync($"{_baseApiUrl}LandBoundaryModels/{lb.Id}");
                    }
                }

                // Delete Dynamic Fields
                var dfResponse = await _httpClient.GetAsync($"{_baseApiUrl}DynamicFieldValues/WorkForm");
                if (dfResponse.IsSuccessStatusCode)
                {
                    var dfContent = await dfResponse.Content.ReadAsStringAsync();
                    var allDf = JsonConvert.DeserializeObject<List<DynamicFieldValue>>(dfContent) ?? new List<DynamicFieldValue>();
                    var relatedDf = allDf.Where(df => df.RecordId == id).ToList();
                    foreach (var df in relatedDf)
                    {
                        await _httpClient.DeleteAsync($"{_baseApiUrl}DynamicFieldValues/{df.Id}");
                    }
                }

                // Delete WorkForm
                var response = await _httpClient.DeleteAsync($"{_baseApiUrl}WorkForms/{id}");
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
