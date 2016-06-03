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
                                where a.WinningUser.Username == user.Username
                                select a.TotalAmount;

                var betAmount = from b in context.Bets
                                where b.user.Username == user.Username
                                select b.Wager;

                var winBetAmount = from b in context.Bets
                                   where b.user.Username == user.Username
                                   where b.game.Winners == user
                                   select b.Wager;

                int matchesLost = user.Games.Count() - numberOfWins.Count();

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

                List <Game> myGames = new List<Game>();

                foreach (Game game in user.Games.AsEnumerable())
                {
                    myGames.Add(game);
                }

                if (matchesLost == 0)
                {
                    matchesLost = 1;
                }

                ViewBag.WLR = Math.Round((double)numberOfWins.Count() / matchesLost, 2);

                if (double.IsNaN(ViewBag.WLR))
                {
                    ViewBag.WLR = 1;
                }

                ViewBag.Picture = user.Picture;
                ViewBag.Username = user.Username;
                ViewBag.Wins = numberOfWins.Count();
                ViewBag.Loss = matchesLost;
                ViewBag.Profit = won - bets;
                ViewBag.Credits = user.Credits;

                ViewBag.currentUser = user;
                ViewBag.BetsAmount = bets;
                ViewBag.Deposit = user.Deposit;
                ViewBag.Withdrawal = user.Withdrawal;
                ViewBag.myGames = user.Games.OrderByDescending(g => g.Timestamp);
                ViewBag.MatchesPlayed = user.Games.Count();
            }
            else
            {
                return RedirectToAction("/Index", "User");
            }
            return View();
        }

        public ActionResult ChangeProfilePicture()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            string username = Session["username"].ToString();

            string newProfilePictureUrl = Request["newProfilePicture"];

            var getUser = from u in context.Users
                          where u.Username == username
                          select u;

            User user = getUser.FirstOrDefault();

            user.Picture = newProfilePictureUrl;

            context.SaveChanges();
            

            return RedirectToAction("Profile", "Profile");
        }
    }
}