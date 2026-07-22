using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ImageUploadController> _logger;

        public ImageUploadController(IWebHostEnvironment env, ILogger<ImageUploadController> logger)
        {
            _env = env;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("لا توجد صورة للرفع.");

            try
            {
                var uploadsFolder = Path.Combine(_env.ContentRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                var imageUrl = $"{baseUrl}/Uploads/{uniqueFileName}";
                return Ok(new { imageUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

     

        [HttpPost("uploadPdf")]
        public async Task<IActionResult> UploadPdf(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("لا يوجد ملف PDF للرفع.");

            try
            {
                var pdfsFolder = Path.Combine(_env.ContentRootPath, "Pdfs");
                if (!Directory.Exists(pdfsFolder))
                {
                    Directory.CreateDirectory(pdfsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(pdfsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                var pdfUrl = $"{baseUrl}/Pdfs/{uniqueFileName}";
                return Ok(new { pdfUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
      
        
        
        [HttpPost("deleteFile")]
        public IActionResult DeleteFile([FromBody] DeleteFileRequest request)
        {
            if (string.IsNullOrEmpty(request.FileType) || string.IsNullOrEmpty(request.FileName))
            {
                return BadRequest("نوع الملف أو اسم الملف غير محدد.");
            }

            string folderPath;
            switch (request.FileType.ToLower())
            {
                case "image":
                    folderPath = Path.Combine(_env.ContentRootPath, "Uploads");
                    break;
                case "video":
                    folderPath = Path.Combine(_env.ContentRootPath, "videos");
                    break;
                case "pdf":
                    folderPath = Path.Combine(_env.ContentRootPath, "Pdfs");
                    break;
                default:
                    return BadRequest("نوع الملف غير مدعوم.");
            }

            try
            {
                var filePath = Path.Combine(folderPath, request.FileName);
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return Ok("تم حذف الملف بنجاح.");
                }
                else
                {
                    return NotFound("الملف غير موجود.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطأ في الخادم الداخلي: {ex.Message}");
            }
        }
        [HttpPost("uploadVideo")]
        public async Task<IActionResult> UploadVideo(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("لا يوجد فيديو للرفع.");

            try
            {
                var videosFolder = Path.Combine(_env.ContentRootPath, "videos");
                if (!Directory.Exists(videosFolder))
                {
                    Directory.CreateDirectory(videosFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                var filePath = Path.Combine(videosFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";
                var videoUrl = $"{baseUrl}/videos/{uniqueFileName}";
                return Ok(new { videoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("deleteVideo")]
        public IActionResult DeleteVideo(string videoFileName)
        {
            try
            {
                var videosFolder = Path.Combine(_env.ContentRootPath, "videos");
                var videoPath = Path.Combine(videosFolder, videoFileName);

                if (System.IO.File.Exists(videoPath))
                {
                    System.IO.File.Delete(videoPath);
                    return Ok("تم حذف الفيديو بنجاح.");
                }
                else
                {
                    return NotFound("الفيديو غير موجود.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"خطأ في الخادم الداخلي: {ex.Message}");
            }
        }

        public class DeleteFileRequest
        {
            public string FileType { get; set; }
            public string FileName { get; set; }
        }


    }
}
