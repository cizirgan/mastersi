using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastersi.Visualization.Models;
using Facebook;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Mastersi.Visualization.Controllers
{

    public class GraphApiTestController : Controller
    {
        public string pageAddress = "/universidaddesalamanca";

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetData()
        {

            var stringData = MakeRequest(pageAddress + "?fields=id,name,picture");
            return Json(stringData);
        }
        [HttpGet]
        public IActionResult GetDataWithParameter(string parameter)
        {

            var stringData = MakeRequest(parameter);
            return Json(stringData);
        }
        public string MakeRequest(string query)
        {
            WebClient client = new WebClient();
            string AppId = "395268280894053";
            string AppSecret = "b2972f3b132cf38e80fb16c4bfa3ac18";
            // get access token
            string oauthUrl = $"https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={AppId}&client_secret={AppSecret}";
            //string accessToken = client.DownloadString(oauthUrl).Split('=')[1];
            string accessToken = client.DownloadString(oauthUrl);
            JObject o = JObject.Parse(accessToken);

            string aToken = (string)o["access_token"];
            // get data and desedrialize it
            var fbClient = new FacebookClient(aToken);
            var fbData = fbClient.Get(query).ToString();
            return fbData;

        }
    }
}
