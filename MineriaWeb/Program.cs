using System;
using System.IO;
using System.Net;
using Facebook;
using Newtonsoft.Json.Linq;

namespace Mastersi.MineriaWeb
{
    class Program
    {
        public static string pageAddress = "/universidaddesalamanca/";
        static void Main(string[] args)
        {
            string facebookQuery = pageAddress + "posts?fields=created_time,message,comments.summary(true),likes.summary(true)&limit=100";
            var stringObject = MakeRequest(facebookQuery);
            string workinDirectory = Directory.GetCurrentDirectory();
            string excelFile = string.Format("FacebookData-{0}.xlsx", DateTime.Now.Millisecond);
            string file = Path.Combine(workinDirectory, excelFile);

            var jsonObject = JObject.Parse(stringObject);

            for (int i = 1; i < 100; i++)
            {
                // System.Console.WriteLine(i + " Time: " + jsonObject["data"][i]["created_time"]);
            }


            var dizi = jsonObject["data"];


            //ExcelFile.CreateFile(file, dizi);



            // Get comments of a post

            string postId = "8585811569_10155476492266570";
            string fields = "?fields=comments";
            var commentQuery = postId + fields;

            var comments = JObject.Parse(MakeRequest(commentQuery));
            var commentToken = comments["comments"];



            ExcelFile.CreateFileForComments(file, commentToken["data"]);

        }

        public static string MakeRequest(string query)
        {
            WebClient client = new WebClient();
            string AppId = "395268280894053";
            string AppSecret = "b2972f3b132cf38e80fb16c4bfa3ac18";
            // get access token
            string oauthUrl = $"https://graph.facebook.com/oauth/access_token?type=client_cred&client_id={AppId}&client_secret={AppSecret}";

            string accessToken = client.DownloadString(oauthUrl);
            JObject o = JObject.Parse(accessToken);

            string aToken = (string)o["access_token"];

            var fbClient = new FacebookClient(aToken);
            var fbData = fbClient.Get(query).ToString();
            return fbData;

        }
    }
}
