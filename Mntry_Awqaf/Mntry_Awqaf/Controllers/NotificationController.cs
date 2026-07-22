using Microsoft.AspNetCore.Mvc;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class NotificationController : BaseApiController<Notification>
    {
        private readonly string _baseApiUrl = "https://localhost:7027/api/";
        public NotificationController(HttpClient httpClient, IConfiguration configuration)
        : base(httpClient, configuration, "Notifications/GetUserNotificationsOnly") 
        {
        }
        // GET: Index
        public async Task<IActionResult> Index()
        {
            var members = await GetAllAsync();
            TempData["SuccessMessage"] = "تم ارجاع البيانات";
            return View(members);
        }




        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await _httpClient.GetFromJsonAsync<Notification>($"{_baseApiUrl}Notifications/{id}");
            return PartialView("_Delete", notification);
        }

        
        public async Task<IActionResult> Delete1(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}Notifications/{id}");
            if (response.IsSuccessStatusCode)
            {
                return Ok(new { success = true });
            }
            return BadRequest(new { success = false, message = " فشل الحذف" });
        }



    }
}





//// GET: Create
//public IActionResult Create() => View();

//[HttpPost]
//public async Task<IActionResult> Create(MembershipIndivid member)
//{
//    if (await CreateAsync(member))
//    {
//        TempData["SuccessMessage"] = "تم إضافة الفرد بنجاح";

//        return RedirectToAction(nameof(Index));
//    }

//    return View(member);
//}

//// GET: Edit
//public async Task<IActionResult> Edit(int id)
//{
//    var member = await GetByIdAsync(id);
//    return View(member);
//}

//[HttpPost]
//public async Task<IActionResult> Edit(int id, MembershipIndivid member)
//{
//    if (await EditAsync(id, member))
//        return RedirectToAction(nameof(Index));

//    return View(member);
//}
//public async Task<IActionResult> Delete(int id)
//{
//    var member = await GetByIdAsync(id);
//    return View(member);
//}

//[HttpPost, ActionName("Delete")]
//public async Task<IActionResult> DeleteConfirmed(int id)
//{
//    if (await DeleteAsync(id))
//        return RedirectToAction(nameof(Index));

//    return RedirectToAction(nameof(Delete), new { id });
//}