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


            return RedirectToAction("PlayCoinflip", "Games", new { gameId = newGame.Id });
            //return RedirectToAction("Coinflip", "Games");
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
                
                return RedirectToAction("/PlayCoinflip", "Games", new { gameId = gameToJoin.Id });

            }//Användaren försöker spela mot sig själv, som man inte får.

            List<User> usersToPlay = new List<User>() { creater, joiner };

            List<Bet> bets = gameToJoin.Userbets.ToList();

            int wager = bets.FirstOrDefault().Wager;

            if (joiner.Credits < wager)
            {
                return RedirectToAction("/Index", "Home");

            }//joining player doesn't have enough credits to join the coinflip and gets redirected back to home/Index

            if (gameToJoin.GameActive == false)
            {
                return RedirectToAction("/Coinflip", "Ganes");

            }//Check if gameActive is false, if so, they can't join it.


            Bet newBet = new Bet() { game = gameToJoin, user = joiner, Wager = wager };

            bets.Add(newBet);

            joiner.Credits -= wager;

            int totalAmount = (int)((bets.First().Wager + bets.Last().Wager));

            List<Winner> winner = new List<Winner>();
            winner.Add(new Winner { game = gameToJoin, TotalAmount = (int)((totalAmount) /** 0.97*/) });

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


            return RedirectToAction("PlayCoinflip", "Games", new { gameId = gameToJoin.Id });
            //return RedirectToAction("/Coinflip", "Games");
            //return View();
        }


        public ActionResult ListCoinflipGames()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();
            //context.Configuration.ProxyCreationEnabled = false;

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
            
            //hej

            if (myList.Count() > 0)
            {

                foreach (var game in myList)
                {

                    GhettoListCoinflipGames shit = new GhettoListCoinflipGames()
                    {
                        Creater = game.users.FirstOrDefault().Username,
                        Wager = game.Userbets.FirstOrDefault().Wager,
                        GameId = game.Id,
                        ShortDate = game.Timestamp.ToShortDateString(),
                        ShortTime = game.Timestamp.ToShortTimeString(),
                        PictureURL = game.users.FirstOrDefault().Picture
                        
                    };

                    


                    takeThisJson.Add(shit);
                    //takeThisJson2.Add(game.users.FirstOrDefault().Username);
                    //takeThisJson2.Add(game.Userbets.FirstOrDefault().Wager.ToString());
                    //takeThisJson2.Add(game.Id.ToString());

                }

            }

            


            return Json(new { activeCoinflipGame = takeThisJson },
                JsonRequestBehavior.AllowGet);


            //return View(myList.ToList());

        }

        public ActionResult PlayCoinflip(int gameId)
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            var currentGame = from g in context.Games
                              where g.Id == gameId
                              select g;

            Game gameToPlay = currentGame.FirstOrDefault();

            ViewBag.creater = null;
            ViewBag.joiner = null;
            ViewBag.winner = null;
            ViewBag.gameActive = null;

            if (gameToPlay.GameActive == true)
            {
                ViewBag.noOpponent = "Waiting for someone to join the game.";

                User creater = gameToPlay.users.FirstOrDefault();

                ViewBag.creater = creater;

                ViewBag.gameActive = gameToPlay.GameActive;

            }
            else
            {

                User creater = gameToPlay.users.First();
                User joiner = gameToPlay.users.Last();

                ViewBag.creater = creater;
                ViewBag.joiner = joiner;
                ViewBag.winner = gameToPlay.Winners.FirstOrDefault().WinningUser;

            }

            return View();
        }


        public ActionResult PlayCoinflipJson(int gameId)
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            var currentGame = from g in context.Games
                              where g.Id == gameId
                              select g;

            Game gameToPlay = currentGame.FirstOrDefault();
            

            GhettoCoinflipGameStatus takeThisJson = new GhettoCoinflipGameStatus();

            if (gameToPlay.GameActive == true)
            {
                User creater = gameToPlay.users.FirstOrDefault();

                takeThisJson = new GhettoCoinflipGameStatus()
                {
                    gameId = gameToPlay.Id,
                    gameActive = gameToPlay.GameActive,
                    CreaterUsername = creater.Username,
                    CreaterPicture = creater.Picture,
                    JoinerUsername = "",
                    JoinerPicture = "",
                    WinnerUsername = "",
                    WinnerPicture = "",
                    Wager = gameToPlay.Userbets.FirstOrDefault().Wager
                };

            }
            else
            {
                User creater = gameToPlay.users.FirstOrDefault();
                User joiner = gameToPlay.users.LastOrDefault();
                User winner = gameToPlay.Winners.FirstOrDefault().WinningUser;

                takeThisJson = new GhettoCoinflipGameStatus()
                {
                    gameId = gameToPlay.Id,
                    gameActive = gameToPlay.GameActive,
                    CreaterUsername = creater.Username,
                    CreaterPicture = creater.Picture,
                    JoinerUsername = joiner.Username,
                    JoinerPicture = joiner.Picture,
                    WinnerUsername = winner.Username,
                    WinnerPicture = winner.Picture,
                    Wager = gameToPlay.Userbets.FirstOrDefault().Wager

                };

            }

            

            return Json(new { activeCoinflipGame = takeThisJson },
                JsonRequestBehavior.AllowGet);
        }

    }

}