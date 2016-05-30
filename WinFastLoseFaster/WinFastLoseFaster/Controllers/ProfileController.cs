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

                var numberOfWins = from g in context.Winners
                             where g.WinningUser.Id == user.Id
                             select g;

                var amountWon = from a in context.Winners
                                where a.WinningUser.Username == loggedInUser
                                select a.TotalAmount;


                var betAmount = from b in context.Bets
                                where b.user.Username == loggedInUser
                                select b.Wager;

                int bets = 0;
                int won = 0;

                foreach (var bet in betAmount)
                {
                    bets += bet;
                }

                foreach (var wins in amountWon)
                {
                    won += wins;
                }


                List < Game > myGames = new List<Game>();

                foreach (Game game in user.Games.AsEnumerable())
                {
                    myGames.Add(game);
                }

                ViewBag.Username = user.Username;
                ViewBag.Bets = user.bets.Count();
                ViewBag.Deposit = user.Deposit;
                ViewBag.Wins = numberOfWins.Count(); ;
                ViewBag.WLR = (double)numberOfWins.Count() / user.Games.Count;
                ViewBag.Picture = user.Picture;
                ViewBag.Profit = won - bets;
                ViewBag.Credits = user.Credits;
            }
            else
            {
                return RedirectToAction("/Index", "User");
            }
            return View();

        }
    }
}