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
            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            var myList = from cg in context.Games
                         where cg.Gametype == Game.GameEnum.Coinflip && cg.GameActive == true
                         orderby cg.Userbets.FirstOrDefault().Wager descending
                         select cg;

            string username = Session["username"].ToString();

            var getUser = from u in context.Users
                          where u.Username == username
                          select u;

            User user = getUser.FirstOrDefault();

            Session["credits"] = user.Credits;

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

            var myList = from cg in context.Games
                         where cg.Gametype == Game.GameEnum.Coinflip && cg.GameActive == true
                         orderby cg.Userbets.FirstOrDefault().Wager descending
                         select cg;


            //ZooContext context = new ZooContext();
            //int count = context.Djur.Count();
            //return Json(new { Count = count },
            //    JsonRequestBehavior.AllowGet);

            return Json(new { activeCoinflipGames = myList.ToList() },
                JsonRequestBehavior.AllowGet);


            return View(myList.ToList());

        }



    }
}