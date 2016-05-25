using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using WinFastLoseFaster.Models;

namespace WinFastLoseFaster.Controllers
{
    public class GamesController : Controller
    {
        // GET: Games
        public ActionResult Coinflip()
        {
            Random random = new Random();
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();           
            int randomGen = random.Next(101);
            ViewBag.Random = randomGen;

            var myUserList = from u in context.Users
                             select u;

            User user1 = myUserList.ToList().First();
            User user2 = myUserList.ToList().Last();

            List<User> users = new List<User>() { user1, user2 };

            Game thisGame = new Game() { Timestamp = DateTime.Now, users = users};

            context.Games.Add(thisGame);
            context.SaveChanges();


            List<Bet> bets = new List<Bet>() { new Bet { user = user1, game = thisGame, Wager = 50}, new Bet { user = user2, game = thisGame, Wager = 50 } };
            List<Winner> winner1 = new List<Winner>() { new Winner { TotalAmount = (int)((bets[0].Wager + bets[1].Wager) * 0.97), WinningUser = user1, game = thisGame } };
            List<Winner> winner2 = new List<Winner>() { new Winner { TotalAmount = (int)((bets[0].Wager + bets[1].Wager) * 0.97), WinningUser = user2, game = thisGame } };

            context.Bets.Add(bets.First());
            context.Bets.Add(bets.Last());

            user1.Credits -= bets.First().Wager;
            user2.Credits -= bets.Last().Wager;
            context.SaveChanges();

            if (randomGen < 50)
            {
                thisGame = new Game() { GameActive = false, Gametype = 0, Timestamp = DateTime.Now, Userbets = bets, Winners = winner1 };
                user1.Credits += thisGame.Winners.First().TotalAmount;
                context.Winners.Add(winner1.First());
            }
            else
            {
                thisGame = new Game() { GameActive = false, Gametype = 0, Timestamp = DateTime.Now, Userbets = bets, Winners = winner2 };
                user2.Credits += thisGame.Winners.First().TotalAmount;
                context.Winners.Add(winner2.First());
            }

            ViewBag.Winner = thisGame.Winners.First().WinningUser.Username;
            ViewBag.winns = user1.Games.Count();

            context.SaveChanges();

            return View();
        }
    }
}