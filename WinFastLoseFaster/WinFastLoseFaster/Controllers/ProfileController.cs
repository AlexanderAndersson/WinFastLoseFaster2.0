using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WinFastLoseFaster.Models;

namespace WinFastLoseFaster.Controllers
{
    public class ProfileController : Controller
    {
        // GET: Profil
        public ActionResult Profile()
        {
            bool isLoggedIn = (bool)Session["isLoggedIn"];

            if (isLoggedIn)
            {
                WinFastLoseFasterContext context = new WinFastLoseFasterContext();

                string loggedInUser = (string)Session["username"];

                var myUserList = from u in context.Users
                                 where u.Username == loggedInUser
                                 select u;

                User user = myUserList.First();

                var myWinnerList = from u in context.Games
                                   where u.Winners == user
                                   select u;


                List<Game> myGames = new List<Game>();

                foreach (Game game in user.Games.AsEnumerable())
                {
                    myGames.Add(game);

                }


                var gamesWon = from g in context.Winners
                               where g.WinningUser.Id == user.Id
                               select g;


                ViewBag.Username = user.Username;
                ViewBag.Bets = user.bets;
                ViewBag.Deposit = user.Deposit;
                ViewBag.Wins = gamesWon.Count();
                //ViewBag.KD = myWinnerList.Count() / user.Games.Count();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
            return View();

        }
    }
}