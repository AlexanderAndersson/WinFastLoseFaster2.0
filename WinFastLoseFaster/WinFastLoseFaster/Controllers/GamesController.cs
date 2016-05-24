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


            Game thisGame = new Game() { Timestamp = DateTime.Now };

            context.Games.Add(thisGame);
            context.SaveChanges();

            var myUserList = from u in context.Users
                             select u;

            User user1 = myUserList.ToList().First();
            User user2 = myUserList.ToList().Last();

            List<User> hejsan = new List<User>() { user1, user2 };
            List<Bet> bets = new List<Bet>() { new Bet { user = user1, game = thisGame, Wager = 50, }, new Bet { user = user2, game = thisGame, Wager = 50 } };
            List<Winner> winner1 = new List<Winner>() { new Winner { TotalAmount = (int)((bets[0].Wager + bets[1].Wager) * 0.97), WinnerId = user1 } };
            List<Winner> winner2 = new List<Winner>() { new Winner { TotalAmount = (int)((bets[0].Wager + bets[1].Wager) * 0.97), WinnerId = user2 } };

            context.Bets.Add(bets.First());
            context.Bets.Add(bets.Last());
            

            if (randomGen < 50)
            {
                ViewBag.krona = "Det blev krona";
                thisGame = new Game() { GameActive = false, Gametype = 0, Timestamp = DateTime.Now, user = hejsan, Userbet = bets, WinnerId = winner1 };
                user1.Deposit += thisGame.WinnerId.First().TotalAmount;
                //context.
                context.Winners.Add(winner1.First());

                

            }
            else
            {
                ViewBag.klave = "Det blev klave";
                thisGame = new Game() { GameActive = false, Gametype = 0, Timestamp = DateTime.Now, user = hejsan, Userbet = bets, WinnerId = winner2 };
                user2.Deposit += thisGame.WinnerId.First().TotalAmount;
                //context.Games.Find(winner2).WinnerId.Add(new Winner { TotalAmount = thisGame.WinnerId.First().TotalAmount, WinnerId = user2 });
                context.Winners.Add(winner2.First());

            }

            ViewBag.Winner = thisGame.WinnerId.First().WinnerId.Username;
            ViewBag.wins = user1.Games.ToList().Count;


            //context.Games.Add(thisGame);


            context.SaveChanges();


            return View();
        }
    }
}