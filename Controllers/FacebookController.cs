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

    public class FacebookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var stringObject = MakeRequest("/universidaddesalamanca?fields=id,about,name,fan_count,username");
            var fbPageObject = JsonConvert.DeserializeObject<FacebookPage>(stringObject);

            return Json(JsonConvert.SerializeObject(fbPageObject));
        }

        [HttpGet]
        public IActionResult GetPosts()
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
            var fbData = fbClient.Get("/universidaddesalamanca?fields=posts").ToString();

            var usalPosts = fbClient.Get("/universidaddesalamanca/posts").ToString();
            var posts = JsonConvert.DeserializeObject<FacebookPostData>(usalPosts);
            return Json(fbData);
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

        public IActionResult GetPostLikesAndComments()
        {

            var stringObject = MakeRequest("/universidaddesalamanca/posts?fields=created_time,message,comments.summary(true),likes.summary(true)&limit=50");

            return Json(stringObject);
        }


    }
}
