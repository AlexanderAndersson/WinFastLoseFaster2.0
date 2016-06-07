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

                Session["credits"] = user.Credits;

                var numberOfWins = from n in context.Winners
                                   where n.WinningUser.Id == user.Id
                                   select n;

                var amountWon = from a in context.Winners
                                where a.WinningUser.Username == user.Username
                                select a.TotalAmount;

                var betAmount = from b in context.Bets
                                where b.user.Username == user.Username
                                select b.Wager;

                var inactiveGames = from a in user.Games
                                 where a.GameActive == false
                                 select a;

                var amountLost = from a in inactiveGames
                                 where a.Winners.FirstOrDefault().WinningUser != user
                                 select a.Userbets.FirstOrDefault().Wager;

                //var winBetAmount = from w in context.Bets
                //                   where w.user.Username == user.Username
                //                   where w.game.Winners == user
                //                   select w.Wager;

                var notActiveGames = from l in user.Games
                                     where l.GameActive != true
                                     select l;

                int matchesLost = notActiveGames.Count() - numberOfWins.Count();

                int bets = 0;
                int won = 0;
                double loss = 0.00;
                double wonAmount = 0.00;

                foreach (var bet in betAmount)
                {
                    bets += bet;                 
                }

                foreach (var wins in amountWon)
                {
                    won += wins;
                    wonAmount += (wins / 2 )/** 0.97*/;             
                }

                foreach (var l in amountLost)
                {
                    loss -= l;
                }

                //foreach (var losses in amountLost)
                //{
                //    loss -= losses;
                //}

                List <Game> myGames = new List<Game>();

                foreach (Game game in user.Games.AsEnumerable())
                {
                    myGames.Add(game);
                }

                if (matchesLost == 0)
                {
                    if (numberOfWins.Count() == 0)
                    {
                        ViewBag.WLR = 1;
                    }
                    else
                    {
                        ViewBag.WLR = Math.Round((double)numberOfWins.Count() / 1, 2);
                    }                 
                }
                else
                {
                    ViewBag.WLR = Math.Round((double)numberOfWins.Count() / matchesLost, 2);
                }

                if (double.IsNaN(ViewBag.WLR))
                {
                    ViewBag.WLR = 1;
                }

                ViewBag.Picture = user.Picture;
                ViewBag.Username = user.Username;
                ViewBag.Wins = numberOfWins.Count();
                ViewBag.Loss = matchesLost;
                ViewBag.Profit = Math.Round(loss + wonAmount, 0);
                ViewBag.Credits = user.Credits;

                ViewBag.amountWon = Math.Round(wonAmount, 0);
                ViewBag.currentUser = user;
                ViewBag.BetsAmount = bets;
                ViewBag.Deposit = user.Deposit;
                ViewBag.Withdrawal = user.Withdrawal;
                ViewBag.myGames = user.Games.OrderByDescending(g => g.Timestamp);
                ViewBag.MatchesPlayed = user.Games.Count();
                ViewBag.amountLost = loss;
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

        public ActionResult Bank()
        {
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            string loggedInUser = (string)Session["username"];

            var myUserList = from u in context.Users
                             where u.Username == loggedInUser
                             select u;

            User user = myUserList.First();

            string deposit = Request["deposit"];
            string withdrawal = Request["withdrawal"];

            int d = 0;
            int w = 0;

            int.TryParse(deposit, out d);
            int.TryParse(withdrawal, out w);

            if (!int.TryParse(deposit, out d))
            {
                ViewBag.letter = "Not allowed letters";
            }
            else if (d > 1000)
            {
                ViewBag.tooMuch = "Max amount is 1000";
            }
            else if(d < 1)
            {
                ViewBag.less = "Less then 0 not acceptable";
            }
            else
            {
                user.Deposit += d;
                user.Credits += d;
            }

            if (w > user.Credits)
            {
                ViewBag.tooCredits = "Not enought credits";
            }
            else if (w < 1)
            {
                ViewBag.less = "Less then 0 not acceptable";
            }
            else
            {
                user.Withdrawal += w;
                user.Credits -= w;
            }

            context.SaveChanges();

            return RedirectToAction("/Profile","Profile");

            //return View();
        }
    }
}