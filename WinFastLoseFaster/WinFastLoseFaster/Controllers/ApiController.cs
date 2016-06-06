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
        public ActionResult AmountPaidOut(string callback)
        {
            
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();
            var TotalWon = from w in context.Winners
                           select w.TotalAmount;

            double wins = 0.00;

            Math.Round(wins, 2);

            foreach (var win in TotalWon)
            {
                //Tar ut credits i valuta
                wins += win;
            }

            if (TotalWon.Count() > 0)
            {
                return Json(wins, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return  Json("No wins found ",  JsonRequestBehavior.AllowGet);
            }
        }
    }
}