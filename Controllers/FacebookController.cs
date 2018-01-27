using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastersi.Visualization.Models;
using Facebook;
using System.Net;

namespace Mastersi.Visualization.Controllers
{
    [Route("api/Facebook")]
    public class FacebookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            string AppId = "395268280894053";
            string AppSecret = "b2972f3b132cf38e80fb16c4bfa3ac18";
            string usalFB = "universidaddesalamanca";

            var client = new WebClient();

            var fbClient = new FacebookClient("EAACEdEose0cBADaIuMGGB74qPvums4KOEWDUBPnGaV9Hkw3KRbZCWQX0juqZC5kcNKs4ZB9UZAeRZAwi9hv72veXqB5y2x9DXTexZCeEpz4Br6WNJKl0l5KTuZCmvlqDmzjzFUOs1N7Jjqhc5t6c17kTa3BdPU6QpLr1rlP8y4hGlD01fdlV92MZB3cyf0SBokgZD");

            fbClient.AppId = AppId;
            fbClient.AppSecret = AppSecret;

            //var deneme = fbClient.Get("v2.11/universidaddesalamanca");
          // var deneme = fbClient.Get()


            return Json("siktir git");


        }


    }
}
