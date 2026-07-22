using Microsoft.AspNetCore.Mvc;
using Microsoft.Controllers;
using Mntry_Awqaf.Models;

namespace Mntry_Awqaf.Controllers
{
    public class CertificateController : CommonController<Certificate>
    {
        protected override string Endpoint => "Certificates";

        public CertificateController(HttpClient httpClient) : base(httpClient) { }

        public async Task<IActionResult> GetUserCertificates()
        {
            return await GetByUser();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCertificateById(int id)
        {
            return await GetById(id);
        }

                [HttpPost]
                public async Task<IActionResult> CreateCertificate([FromForm] Certificate model)
                {
                    return await Create(model);
                }

        [HttpPost]
        public async Task<IActionResult> UpdateCertificate(int id, [FromForm] Certificate model)
        {
            return await Update(id, model);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCertificate(int id)
        {
            return await Delete(id);
        }
    }
}
