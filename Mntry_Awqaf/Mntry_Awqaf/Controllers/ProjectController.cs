using Microsoft.AspNetCore.Mvc;
using Microsoft.Controllers;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class ProjectController : CommonController<Project>
    {
        protected override string Endpoint => "Projects";

        public ProjectController(HttpClient httpClient) : base(httpClient) { }

        public async Task<IActionResult> GetUserProjects()
        {
            return await GetByUser();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromForm] Project model)
        {
            return await Create(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetProjectById(int id)
        {
            return await GetById(id);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] Project model)
        {
            return await Update(id, model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProject(int id)
        {
            return await Delete(id);
        }
    }
}
