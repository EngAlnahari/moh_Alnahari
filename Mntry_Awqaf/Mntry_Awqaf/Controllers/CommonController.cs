using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;
using System.Net.Http.Json;

namespace Microsoft.Controllers
{
    // مخصص ليكون أساس لكل المتحكمات المشتركة
    public abstract class CommonController<T> : Controller where T : class
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseApiUrl;
        protected abstract string Endpoint { get; }
        

        public CommonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseApiUrl = "https://localhost:7027/api/"; // عدل الرابط حسب مشروعك
        }

        // في CommonController<T>
        public virtual async Task<IActionResult> GetAll()
        {
            var list = await _httpClient.GetFromJsonAsync<List<T>>($"{_baseApiUrl}{Endpoint}");
            return PartialView($"_{Endpoint}", list);
        }

        // 🔹 استرجاع بيانات المستخدم
        public virtual async Task<IActionResult> GetByUser()
        {
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0;
            var list = await _httpClient.GetFromJsonAsync<List<T>>($"{_baseApiUrl}{Endpoint}/User/{userId}");
            return PartialView($"_{Endpoint}", list);
        }


        public virtual async Task<IActionResult> GetByUserDetail()
        {
            string Endpoint2 = "Users";
            int userId = HttpContext.Session.GetInt32("UserID") ?? 0;

            // جلب مستخدم واحد فقط
            var user = await _httpClient.GetFromJsonAsync<User>($"{_baseApiUrl}Users/{userId}");

            // إعادة PartialView مع نموذج واحد
            return PartialView($"_{Endpoint2}", user);
        }


        // 🔹 استرجاع عنصر واحد


        // 🔹 إنشاء عنصر جديد


        public virtual async Task<IActionResult> GetById(int id)
        {
            var item = await _httpClient.GetFromJsonAsync<T>($"{_baseApiUrl}{Endpoint}/{id}");
            return Json(item);
        }
        // 🔹 تعديل عنصر
        [HttpPost]
        public virtual async Task<IActionResult> Create([FromForm] T model)
        {
            // إذا كان النموذج يحتوي على UserID، عينه من الجلسة
            if (model is IUserModel userModel)
                userModel.UserID = HttpContext.Session.GetInt32("UserID") ?? 0;

            var response = await _httpClient.PostAsJsonAsync($"{_baseApiUrl}{Endpoint}", model);
            return Json(new { success = response.IsSuccessStatusCode });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(int id, [FromForm] T model)
        {
            if (model is IUserModel userModel)
                userModel.UserID = HttpContext.Session.GetInt32("UserID") ?? 0;

            var response = await _httpClient.PutAsJsonAsync($"{_baseApiUrl}{Endpoint}/{id}", model);
            return Json(new { success = response.IsSuccessStatusCode });
        }


        // 🔹 حذف عنصر
        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}{Endpoint}/{id}");
            return Json(new { success = response.IsSuccessStatusCode });
        }
    }
}




