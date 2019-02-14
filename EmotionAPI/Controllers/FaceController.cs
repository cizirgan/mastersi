using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastersi.Cognitive.Models;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Mastersi.Cognitive.Controllers
{
    public class FaceController : Controller
    {
        private IConfiguration _configuration;
         public FaceController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DetectFace(string imageDataBase64)
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
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _configuration["FaceApiKey"]);
            string requestParameters = "returnFaceId=true&returnFaceLandmarks=false&returnFaceAttributes=age,gender,headPose,smile,facialHair,glasses,emotion,hair,makeup,occlusion,accessories,blur,exposure,noise";

            string uriBase = _configuration["FaceApiUrl"];
            string uri = uriBase + "?" + requestParameters;

            

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
