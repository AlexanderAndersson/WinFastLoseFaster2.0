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
            /*
            Random random = new Random();
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();           
            int randomGen = random.Next(101);
            ViewBag.Random = randomGen;

            int wager = 50;

            var myUserList = from u in context.Users
                             select u;

            User user1 = myUserList.ToList().First();
            User user2 = myUserList.ToList().Last();

            List<User> users = new List<User>() { user1, user2 };

            Game thisGame = new Game() { Timestamp = DateTime.Now, users = users};

            context.Games.Add(thisGame);
            context.SaveChanges();


            List<Bet> bets = new List<Bet>() { new Bet { user = user1, game = thisGame, Wager = wager}, new Bet { user = user2, game = thisGame, Wager = wager } };
            List<Winner> winner1 = new List<Winner>() { new Winner { TotalAmount = (int)((wager + wager) * 0.97), WinningUser = user1, game = thisGame } };
            List<Winner> winner2 = new List<Winner>() { new Winner { TotalAmount = (int)((wager + wager) * 0.97), WinningUser = user2, game = thisGame } };

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
            */

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            var myList = from cg in context.Games
                         where cg.Gametype == Game.GameEnum.Coinflip && cg.GameActive == true
                         orderby cg.Userbets.FirstOrDefault().Wager descending
                         select cg;


            return View(myList.ToList());
        }

        public ActionResult CreateCoinflip()
        {
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            string strWager = Request["TextSum"];
            int wager = 0;
            string createrName = (string)Session["username"];


            if (!int.TryParse(strWager, out wager))
            {
                return RedirectToAction("/Coinflip", "Games");
            }

            var myUserList = from u in context.Users
                             where u.Username == createrName
                             select u;

            //var myUserList = from u in context.Users
            //                 select u;

            User creater = myUserList.First();

            if (creater.Credits < wager)
            {
                return RedirectToAction("/Coinflip", "Games");

            }//User doesn't have enough Credits to create the game

            List<User> user = new List<User>() { creater };

            Game newGame = new Game() { Timestamp = DateTime.Now, Gametype = Game.GameEnum.Coinflip, GameActive = true, users = user };

            context.Games.Add(newGame);
            context.SaveChanges();
         
            List<Bet> bets = new List<Bet>() { new Bet { user = creater, game = newGame, Wager = wager } };

            context.Bets.Add(bets.First());

            creater.Credits -= bets.First().Wager;
            Session["credits"] = creater.Credits;

            newGame.Userbets = bets;
            
            context.SaveChanges();

            return RedirectToAction("Coinflip", "Games");
        }

        public ActionResult JoinCoinflip()
        {

            string strCoinflipGameId = Request["coinflipGameId"];
            string username = (string)Session["username"];

            Random rnd = new Random();

            int coinflipGameId = 0;

            if (!int.TryParse(strCoinflipGameId, out coinflipGameId))
            {
                return RedirectToAction("Coinflip", "Games");

            }//Check if coinflipGameId is int


            WinFastLoseFasterContext context = new WinFastLoseFasterContext();


            var gameJoinList = from g in context.Games
                               where g.Id == coinflipGameId
                               select g;


            Game gameToJoin = gameJoinList.FirstOrDefault();


            var userJoinList = from u in context.Users
                               where u.Username == username
                               select u;



            User creater = gameToJoin.users.FirstOrDefault();
            User joiner = userJoinList.FirstOrDefault();

            if (creater.Username == joiner.Username)
            {
                return RedirectToAction("Coinflip", "Games");

            }//Användaren försöker spela mot sig själv, som man inte får.

            List<User> usersToPlay = new List<User>() { creater, joiner };

            List<Bet> bets = gameToJoin.Userbets.ToList();

            int wager = bets.FirstOrDefault().Wager;

            if (joiner.Credits < wager)
            {
                return RedirectToAction("/Index", "Home");

            }//joining player doesn't have enough credits to join the coinflip and gets redirected back to home/Index


            Bet newBet = new Bet() { game = gameToJoin, user = joiner, Wager = wager };

            bets.Add(newBet);

            joiner.Credits -= wager;

            int totalAmount = (int)((bets.First().Wager + bets.Last().Wager) * 0.97);

            List<Winner> winner = new List<Winner>();
            winner.Add(new Winner { game = gameToJoin, TotalAmount = totalAmount });

            if (rnd.Next(101) < 50)
            {
                winner.FirstOrDefault().WinningUser = creater;
                creater.Credits += totalAmount;

            }
            else
            {
                winner.FirstOrDefault().WinningUser = joiner;
                joiner.Credits += totalAmount;

            }

            gameToJoin.Timestamp = DateTime.Now;
            gameToJoin.GameActive = false;
            gameToJoin.Winners = winner;
            gameToJoin.users = usersToPlay;
            gameToJoin.Userbets = bets;

            Session["credits"] = joiner.Credits;

            context.Winners.Add(winner.FirstOrDefault());

            context.Bets.Add(newBet);

            context.SaveChanges();


            //ZooContext context = new ZooContext();
            //int count = context.Djur.Count();
            //return Json(new { Count = count },
            //    JsonRequestBehavior.AllowGet);


            //string createrUsername = creater.Username;
            //string createrProfilePicture = creater.Picture;


            //string joinerUsername = joiner.Username;
            //string joinerProfilePicture = joiner.Picture;


            //return Json(new { createrUsername = createrUsername, createrProfilePicture = createrProfilePicture, joinerUsername = joinerUsername, joinerProfilePicture = joinerProfilePicture },
            //    JsonRequestBehavior.AllowGet);


            return RedirectToAction("/Coinflip", "Games");
            //return View();
        }


        public ActionResult ListCoinflipGames()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();
            context.Configuration.ProxyCreationEnabled = false;

            var myList = from cg in context.Games
                         where cg.Gametype == Game.GameEnum.Coinflip && cg.GameActive == true
                         orderby cg.Userbets.FirstOrDefault().Wager descending
                         select cg;


            //ZooContext context = new ZooContext();
            //int count = context.Djur.Count();
            //return Json(new { Count = count },
            //    JsonRequestBehavior.AllowGet);


            //var myList2 = context.Games.Where(g => g.GameActive == true && g.Gametype == Game.GameEnum.Coinflip);


            //foreach (var game in myList2)
            //{
            //    context.Entry(game).Collection(g => g.users).Load();
            //    context.Entry(game).Collection(g => g.Userbets).Load();

            //}

            //List<string> myList3 = new List<string>();

            //foreach (var game in myList2)
            //{


            //}


            //return Json(new { activeCoinflipGame = myList2.ToList() },
            //    JsonRequestBehavior.AllowGet);

            List<GhettoListCoinflipGames> takeThisJson = new List<GhettoListCoinflipGames>();
            List<string> takeThisJson2 = new List<string>();

            foreach (var game in myList)
            {

                GhettoListCoinflipGames shit = new GhettoListCoinflipGames()
                {
                    
                    Creater = game.users.FirstOrDefault().Username,
                    Wager = game.Userbets.FirstOrDefault().Wager,
                    GameId = game.Id

                };

                takeThisJson.Add(shit);
                takeThisJson2.Add(game.users.FirstOrDefault().Username);
                takeThisJson2.Add(game.Userbets.FirstOrDefault().Wager.ToString());
                takeThisJson2.Add(game.Id.ToString());

            }


            return Json(new { activeCoinflipGame = takeThisJson },
                JsonRequestBehavior.AllowGet);


            //return View(myList.ToList());

        }



    }
}