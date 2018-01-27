using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mastersi.Visualization.Models;

namespace Mastersi.Visualization.Controllers
{
    [Route("api/Facebook")]
    public class FacebookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json("Selam");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
