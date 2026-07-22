using Microsoft.AspNetCore.Mvc;
using Microsoft.Controllers;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class ExperienceController : CommonController<Experience>
    {
        protected override string Endpoint => "Experiences"; // اسم API endpoint
         // اسم API endpoint

        public ExperienceController(HttpClient httpClient) : base(httpClient) { }

        public async Task<IActionResult> GetUserExperiences()
        {
            return await GetByUser();
        }
        public async Task<IActionResult> GetUser()
        {
            return await GetByUserDetail();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateExperience([FromForm] Experience model)
        {
            return await Create(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetExperienceById(int id)
        {
            return await GetById(id);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateExperience(int id, [FromForm] Experience model)
        {
            return await Update(id, model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteExperience(int id)
        {
            return await Delete(id);
        }
    }
}
