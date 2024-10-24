using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_ver2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class StudentsV2Controller : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", file.FileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, file.FileName);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }

            return Ok(file);
        }

        [HttpGet("Download/{fileName}")]
        public IActionResult Download(string fileName)
        {
            if (fileName == null) return NotFound();

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", fileName);

            var bytes = System.IO.File.ReadAllBytes(path);

            return File(bytes, "Test/txt", fileName);
        }
    }
}
