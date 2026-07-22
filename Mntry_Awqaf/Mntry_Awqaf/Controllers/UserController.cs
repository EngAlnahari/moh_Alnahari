
using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using Newtonsoft.Json;

namespace Mntry_Awqaf.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseApiUrl; // http://awqaf12.somee.com/api/
        public UserController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _baseApiUrl = configuration["ApiSettings:BaseUrl"]!;
        }
        public async Task<IActionResult> GetUserDetails(int userId)
        {
            var user = await _httpClient.GetFromJsonAsync<User>($"{_baseApiUrl}Users/{userId}");
            return PartialView("_UserDetails", user); // أو View(user)
        }

        public async Task<IActionResult> GetUserDetails()
        {
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0;
            var user = await _httpClient.GetFromJsonAsync<User>($"{_baseApiUrl}Users/{userId}");
            return PartialView("_UserDetails", user); // أو View(user)
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetFromJsonAsync<List<User>>($"{_baseApiUrl}Users");
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        } 
        public IActionResult Eng()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLogin userlogin)
        {
            // تسجيل ما يُرسل إلى API
            System.Diagnostics.Debug.WriteLine($"Sending to API: Name={userlogin.Name}, Pass={userlogin.Pass}");
            System.Diagnostics.Debug.WriteLine($"API URL: {_baseApiUrl}Users/Userlogin");

            var res = await _httpClient.PostAsJsonAsync<UserLogin>($"{_baseApiUrl}Users/Userlogin", userlogin);

            // تسجيل استجابة API
            System.Diagnostics.Debug.WriteLine($"API Response Status: {res.StatusCode}");
            var responseContent = await res.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine($"API Response Content: {responseContent}");

            if (res.IsSuccessStatusCode)
            {
                var read = await res.Content.ReadFromJsonAsync<Models.Token>();

                var tken = read?.token;


                if (!string.IsNullOrEmpty(tken))
                {
                    HttpContext.Session.SetString("GetToken", tken);
                    HttpContext.Session.SetString("UserType", read.UserType.ToString());
                    HttpContext.Session.SetInt32("UserID", read.UserId);

                    return RedirectToAction("Index", "TanjezOrder");

                }

            }
            TempData["ErrorMessage"] = $"اسم المستخدم او كلمة المرور غير صحيحة (Status: {res.StatusCode})";
            return View(userlogin);

        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadCv(IFormFile CvFile, IFormFile CertFile)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserID");
                if (userId == null)
                {
                    TempData["SuccessMessage"] = "لم يتم العثور على المستخدم، الرجاء تسجيل الدخول.";
                    return RedirectToAction("Login", "User");
                }

                // التجاوز المباشر وتحديث الحالة
                var model = new ApproveEngineerDto {
                    engineerId = userId.Value,
                    Is_Authentic = "1" // حالة الموافقة
                };

                var response = await ApproveEngineer(model);
                
                TempData["SuccessMessage"] = "تم تجاوز التحقق بنجاح! يمكنك الآن إنشاء العروض.";
                return RedirectToAction("Index", "WorkOffer");
            }
            catch (Exception ex)
            {
                TempData["SuccessMessage"] = $"حدث خطأ داخلي: {ex.Message}";
                return RedirectToAction("Index", "WorkOffer");
            }
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


        public async Task<IActionResult> GetWorkContract(int contractId)
        {
            try
            {
                // استدعاء API لجلب العقد
                var contract = await _httpClient.GetFromJsonAsync<WorkContract>(
                    $"{_baseApiUrl}WorkContracts/{contractId}"
                );

                if (contract == null)
                    return Content("لا توجد بيانات للعقد.");

                // 🔹 استدعاء API لجلب البنود
                var itemsResponse = await _httpClient.GetAsync($"{_baseApiUrl}ContractItems/GetContractItems/{contractId}");

                //var itemsResponse = await _httpClient.GetAsync($"{_baseApiUrl}ContractItems/GetContractItems");
                if (itemsResponse.IsSuccessStatusCode)
                {
                    var allItems = await itemsResponse.Content.ReadFromJsonAsync<List<ContractItem>>();
                    // ترتيب البنود حسب Count وإرسالها للعقد
                    contract.ContractItems = allItems?.OrderBy(x => x.Count).ToList();
                }

                // معالجة القيم داخل الوصف
                if (contract.ContractItems != null)
                {
                    foreach (var item in contract.ContractItems)
                    {
                        item.DescriptionTemplate = ReplacePlaceholdersForWorkContract(item.DescriptionTemplate ?? "", contract);
                    }
                }

                // ترتيب الاعتراضات
                if (contract.contractAmendments != null)
                {
                    contract.contractAmendments = contract.contractAmendments.OrderBy(ca => ca.CreatedAt).ToList();
                }

                // 🔹 إرسال PartialView مع العقد والبنود
                return PartialView("_WorkContract", contract);
            }
            catch (Exception ex)
            {
                return Content("حدث خطأ أثناء تحميل البيانات: " + ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateContractItems([FromBody] ContractItem depart)
        {
            try
            {
                // أخذ رقم المستخدم من الـ Claims
               
                

                // إرسال البند للـ API
                var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}ContractItems", depart);

                if (!res.IsSuccessStatusCode)
                {
                    var errorDetails = await res.Content.ReadAsStringAsync(); // جلب نص الخطأ من الـ API
                    return Json(new { success = false, message = "فشل حفظ البند في الـ API.", details = errorDetails });
                }

                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<ContractItem>(dat);
                if (datuser == null)
                {
                    return Json(new { success = false, message = "لم يتم إرجاع بيانات البند بعد الحفظ." });
                }

                // جلب بيانات العقد لإرسال الإشعار
                var existingContract = await _httpClient.GetFromJsonAsync<WorkContract>(
                    $"{_baseApiUrl}WorkContracts/{depart.WorkContractId}"
                );

                if (existingContract != null && existingContract.ClientId.HasValue)
                {
                    var notification = new Notification
                    {
                        UserId = existingContract.ClientId,
                        ContractId = depart.WorkContractId ?? 0,
                        Url = $"/User/GetWorkContract?contractId={depart.WorkContractId}",
                        Message = $"تم إضافة بند جديد على العقد رقم {depart.WorkContractId}: {depart.Title}",
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };

                    var notifyRes = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

                    if (!notifyRes.IsSuccessStatusCode)
                    {
                        var notifError = await notifyRes.Content.ReadAsStringAsync();
                        return Json(new { success = false, message = "تم حفظ البند لكن فشل إرسال الإشعار.", details = notifError });
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "تم إضافة البند وإرسال الإشعار بنجاح.",
                    data = new { id = datuser.Id, title = datuser.Title }
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ غير متوقع.", details = ex.Message });
            }
        }

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
                    ? System.Text.Json.JsonSerializer.Deserialize<List<ContractAmendment>>(AmendmentsJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
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


        [HttpPut]
        public async Task<IActionResult> UpdateContractStatusWithNotification(int contractId, [FromBody] UpdateStatusDto dto)
        {
            try
            {
                // التحقق من صحة الحالة
                if (!Enum.IsDefined(typeof(ContractStatus), dto.Status))
                    return Json(new { success = false, message = "الحالة غير صحيحة." });

                var newStatus = (ContractStatus)dto.Status;

                // تحديث الحالة في الـ API
                var updateRes = await _httpClient.PutAsJsonAsync(
                    $"{_baseApiUrl}WorkContracts/UpdateStatus/{contractId}",
                    dto // إرسال JSON صالح { "status": 1 }
                );

                if (!updateRes.IsSuccessStatusCode)
                {
                    var err = await updateRes.Content.ReadAsStringAsync();
                    return Json(new { success = false, message = "فشل تحديث الحالة.", details = err });
                }

                // جلب بيانات العقد لإرسال الإشعار
                var existingContract = await _httpClient.GetFromJsonAsync<WorkContract>(
                    $"{_baseApiUrl}WorkContracts/{contractId}"
                );

                if (existingContract != null && existingContract.ClientId.HasValue)
                {
                    var notification = new Notification
                    {
                        UserId = existingContract.ClientId,
                        ContractId = contractId,
                        Url = $"/User/GetWorkContract?contractId={contractId}",
                        Message = newStatus == ContractStatus.EngineerApproved
                            ? $"تمت الموافقة على العقد رقم {contractId} من قبل المهندس."
                            : $"تم رفض العقد رقم {contractId} من قبل المهندس.",
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };

                    await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);
                }

                return Json(new { success = true, message = "تم تحديث الحالة وإرسال الإشعار بنجاح." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "حدث خطأ غير متوقع.", details = ex.Message });
            }
        }

        //public async Task<IActionResult> GetWorkContract(int contractId)
        //{
        //    try
        //    {
        //        var contract = await _httpClient.GetFromJsonAsync<WorkContract>(
        //            $"{_baseApiUrl}WorkContracts/{contractId}"  // هنا يجب أن يكون API يقبل contractId
        //        );

        //        return PartialView("_WorkContract", contract);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Content("حدث خطأ أثناء تحميل البيانات: " + ex.Message);
        //    }
        //}

        [HttpPost]
        public async Task<IActionResult> Create(User depart)
        {
            var res = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Users", depart);
            if (res.IsSuccessStatusCode)
            {
                var dat = await res.Content.ReadAsStringAsync();
                var datuser = JsonConvert.DeserializeObject<User>(dat);
                if (datuser != null)
                {
                    return RedirectToAction("Login");

                }

            }
            TempData["ErrorMessage"] = "حدث خطأ أثناء إنشاء المستخدم";
            return View(depart);
        }


        [HttpPost]
        public async Task<IActionResult> ApproveEngineer([FromBody] ApproveEngineerDto model)
        {
            try
            {
                // 1️⃣ تحديث حالة المهندس
                var response = await _httpClient.PutAsJsonAsync(
                    $"{_baseApiUrl}Users/{model.engineerId}/AuthenticStatus", model.Is_Authentic
                );

                if (!response.IsSuccessStatusCode)
                    return Json(new { success = false, message = "حدث خطأ أثناء تحديث حالة المهندس ❌" });

                // 2️⃣ إرسال الإشعار بعد نجاح التحديث
                if (model.engineerId != 0)
                {
                    string statusMessage = model.Is_Authentic switch
                    {
                        "1" => "تمت الموافقة على طلبك ✅",
                        "2" => "تم توثيق حسابك بنجاح 🧾",
                        "3" => "تم رفض طلبك ❌",
                        _ => "تم تحديث حالتك من قبل الإدارة"
                    };

                    var notification = new Notification
                    {
                        UserId = model.engineerId,
                        Url = "/WorkOffer/Index",
                        Message = statusMessage,
                        CreatedAt = DateTime.Now,
                        IsRead = false
                    };

                    var notifyRes = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}Notifications", notification);

                    if (!notifyRes.IsSuccessStatusCode)
                    {
                        var notifError = await notifyRes.Content.ReadAsStringAsync();
                        return Json(new
                        {
                            success = true,
                            message = "تم تحديث الحالة بنجاح ✅، لكن فشل إرسال الإشعار ⚠️",
                            details = notifError
                        });
                    }
                }

                // 3️⃣ النتيجة النهائية في حالة النجاح الكامل
                return Json(new { success = true, message = "تم تحديث حالة المهندس وإرسال الإشعار بنجاح ✅" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "فشل الاتصال بالخادم ❌: " + ex.Message });
            }
        }

        public class ApproveEngineerDto
        {
            public int engineerId { get; set; }
            public string Is_Authentic { get; set; }
        }



        public async Task<PartialViewResult> GetNotificationsPartial()
        {
            int engineerId = HttpContext.Session.GetInt32("UserID") ?? 0;

            var response = await _httpClient.GetAsync($"{_baseApiUrl}Notifications/GetEngineerNotifications?engineerId={engineerId}");


            if (!response.IsSuccessStatusCode)
            {
                return PartialView("_NotificationsPartial", new List<Notification>());
            }

            var engineerNotifications = await response.Content.ReadFromJsonAsync<List<Notification>>();

            return PartialView("_NotificationsPartial", engineerNotifications);
        }

        [HttpGet]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var response = await _httpClient.PostAsync($"{_baseApiUrl}Notifications/MarkAsRead/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "User"); // أو أي صفحة ثانية بعد القراءة
            }

            return BadRequest("فشل تحديث الإشعار");
        }


        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<User>($"{_baseApiUrl}Users/{id}");

            return View(response);
        }
        public async Task<IActionResult> Delete1(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}Users/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(response);
        }



    }
}
