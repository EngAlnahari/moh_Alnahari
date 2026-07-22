using Microsoft.AspNetCore.Mvc;
using Microsoft.Controllers;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class SkillController : CommonController<Skill>
    {
        protected override string Endpoint => "Skills"; // اسم الـ API endpoint

        public SkillController(HttpClient httpClient) : base(httpClient) { }

        // استدعاء GetByUser من CommonController
        public async Task<IActionResult> GetUserSkills()
        {
            return await GetByUser();
        }

        public  IActionResult Index()
        {
            return  View();
        }

        // إضافة مهارة
        [HttpPost]
        public async Task<IActionResult> CreateSkill([FromForm] Skill model)
        {
            return await Create(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetSkillById(int id)
        {
            return await GetById(id);
        }
        // تعديل مهارة
        [HttpPost]
        public async Task<IActionResult> UpdateSkill(int id, [FromForm] Skill model)
        {
            return await Update( id, model);
        }

        // حذف مهارة
        [HttpPost]
        public async Task<IActionResult> DeleteSkill(int id)
        {
            return await Delete(id);
        }
    }
}
