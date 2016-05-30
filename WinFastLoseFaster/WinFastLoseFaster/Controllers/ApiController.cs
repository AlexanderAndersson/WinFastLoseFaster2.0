using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinFastLoseFaster.Models;

namespace WinFastLoseFaster.Controllers
{
    public class ApiController : Controller
    {
        // GET: Api
        public ActionResult Index()
        {
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();
            var TotalWon = from w in context.Winners
                           select w.TotalAmount;

            List<double> wins = new List<double>();

            foreach (var win in TotalWon)
            {
                //Tar ut credits i valuta
                wins.Add(win / 10.00);
            }

            if (TotalWon.Count() > 0)
            {
                return Json(wins, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("No wins found " +  JsonRequestBehavior.AllowGet);
            }
        }
    }
}