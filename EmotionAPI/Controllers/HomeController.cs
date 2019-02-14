using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastersi.Cognitive.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Mastersi.Cognitive.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult GetAnalysis(string text)
        {
            return Json(text);
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(IFormFile file)
        {

            if (file == null || file.Length == 0)
                return Content("file not selected");

            var guid = Guid.NewGuid();

            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Uploads",
                        guid.ToString() + ".jpg");

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }



            return Ok(new { guid });
        }

    }
}
