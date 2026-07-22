using Microsoft.AspNetCore.Mvc;
using Microsoft.Controllers;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class QualificationController : CommonController<Qualification>
    {
        protected override string Endpoint => "Qualifications";

        public QualificationController(HttpClient httpClient) : base(httpClient) { }

        public async Task<IActionResult> GetUserQualifications()
        {
            return await GetByUser();
        }
        [HttpGet]
        public async Task<IActionResult> GetQualificationById(int id)
        {
            return await GetById(id);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateQualification([FromForm] Qualification model)
        {
            return await Create(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQualification(int id, [FromForm] Qualification model)
        {
            return await Update(id, model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteQualification(int id)
        {
            return await Delete(id);
        }
    }
}
