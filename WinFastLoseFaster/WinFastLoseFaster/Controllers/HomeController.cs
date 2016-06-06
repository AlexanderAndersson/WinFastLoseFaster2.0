using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinFastLoseFaster.Models;

namespace WinFastLoseFaster.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            //int count = context.Users.Count();

            return RedirectToAction("/Index", "User");
        }
    }
}