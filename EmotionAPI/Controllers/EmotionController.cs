using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Mastersi.Cognitive.Controllers
{
    public class EmotionController : Controller
    {
        private IConfiguration _configuration;


        public EmotionController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public IActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult UploadImage()
        {

            var guid = Guid.NewGuid();

            var name = HttpContext.Request.Form["img"];
            var a = name.ToString();

            string convert = a.Replace("data:image/png;base64,", String.Empty);

            byte[] image64 = Convert.FromBase64String(convert);


            var filePath = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/Uploads",
                        guid.ToString() + ".jpg");
            System.IO.File.WriteAllBytes(filePath, image64);


            return Ok(new { name });
        }

        [HttpPost]
        public IActionResult GetImageEmotions(string imageDataBase64)
        {

            var guid = Guid.NewGuid();

            string convert = imageDataBase64.Replace("data:image/png;base64,", String.Empty);

            byte[] image64 = Convert.FromBase64String(convert);

            /* This saves images to uploads folder
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads", guid.ToString() + ".jpg");
            System.IO.File.WriteAllBytes(filePath, image64); */

            var veriler = MakeRequest(image64);

            return Ok(new { veriler });
        }

        private async Task<string> MakeRequest(byte[] image64)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration["EmotionApiKey"]);

            string uri = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize?";
            HttpResponseMessage response;
            string responseContent;

            using (var content = new ByteArrayContent(image64))
            {
                // This example uses content type "application/octet-stream".
                // The other content types you can use are "application/json" and "multipart/form-data".
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;
            }

            return (responseContent);


        }

    }
}
