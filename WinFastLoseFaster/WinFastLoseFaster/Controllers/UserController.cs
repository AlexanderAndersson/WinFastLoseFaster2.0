using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WinFastLoseFaster.Models;

namespace WinFastLoseFaster.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Logout()
        {

            Session["isLoggedIn"] = false;
            Session["Username"] = "";
            Session["credits"] = 0;

            return RedirectToAction("/Index", "User");
        }

        public ActionResult Login()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();


            string username = Request["inputUsername"];
            string password = Request["inputPassword"];

            var userList = from u in context.Users
                           where u.Username == username.Trim()
                           select u;


            if (userList.Count() == 1)
            {
                if (userList.First().Password == password)
                {
                    Session["isLoggedIn"] = true;
                    Session["username"] = username;
                    Session["credits"] = userList.First().Credits;

                    return RedirectToAction("/Coinflip", "Games");
                }
                else
                {
                    ViewBag.Wrong = "Wrong username or password";
                }//Right username, wrong password

            }//No user with inputted username
            else
            {
                //ViewBag.Wrong = "Wrong username or password";
            }
            return View("Index");
        }

        public ActionResult Register()
        {
            int faults = 0;
            using (WinFastLoseFasterContext context = new WinFastLoseFasterContext())
            {


                Random rnd = new Random();

                List<string> profilePictures = new List<string>()
            {
                "http://orig04.deviantart.net/77b2/f/2010/215/7/b/43___fsjal_wason__by_ztoonlinkz.png",
                "http://i0.kym-cdn.com/photos/images/facebook/000/841/850/42c.jpg",
                "http://i3.kym-cdn.com/photos/images/original/000/013/251/lenny_fsjal.jpg",
                "http://orig07.deviantart.net/94d2/f/2014/212/8/0/fsjal_squirtle_by_toonstar96-d7t4sie.png",
                "http://orig11.deviantart.net/4b44/f/2015/304/0/3/the_flash_fsjal___flasjal_by_limedraagon-d9eziqk.jpg",
                "https://c1.staticflickr.com/5/4098/4806016720_46eed66416.jpg",
                /*"http://img04.deviantart.net/840f/i/2015/359/f/2/hitler_fsjal_by_gnay12-d9lfrzc.jpg",*/
                "http://vignette3.wikia.nocookie.net/peido/images/a/a1/Fsjal_spongebob.png/revision/latest?cb=20110801205739",
                "http://vignette1.wikia.nocookie.net/creepypasta/images/0/09/FSJAL_Link_by_Lucas47_46.jpg/revision/latest?cb=20131113024225",
                "http://orig11.deviantart.net/4320/f/2015/289/a/6/fsjal_terror_inferno_with_ak_by_gibiart-d9d9sf6.png",
                "http://orig00.deviantart.net/be69/f/2009/298/5/6/batman_fsjal_by_spidernaruto.jpg",
                "http://i3.kym-cdn.com/photos/images/newsfeed/000/006/647/iron_man20110724-22047-vhlnn6.png",
                "http://orig09.deviantart.net/35fd/f/2010/193/5/e/13___fsjal_homer_jay_simpson__by_ztoonlinkz.png",
                "https://c2.staticflickr.com/4/3312/5833167537_2ab677d082.jpg",
                "http://orig05.deviantart.net/1b89/f/2011/322/1/0/captain_america_fsjal_by_xian_the_miguel-d4gl5dw.png",
                "http://orig06.deviantart.net/6a87/f/2013/046/f/2/snorlax_fsjal_by_zunyokingdom-d5v3nfp.jpg",
                "http://orig01.deviantart.net/d6ca/f/2010/077/0/b/plastic_soldier_fsjal_by_platinumglitchmint.jpg",
                "http://orig08.deviantart.net/8cbc/f/2010/199/0/6/23___fsjal_yoshi__by_ztoonlinkz.png",
                "https://pbs.twimg.com/media/BJXYR9sCUAArAO-.jpg"

            };


                string username = Request["inputUsername"];
                string password = Request["inputPassword"];
                string password2 = Request["inputRetypePassword"];
                string email = Request["inputEmail"];
                string checkbox = Request["inputCheckbox"];



                if (username.Trim().Length < 3)
                {
                    faults++;

                }

                if (password.Trim().Length < 6)
                {
                    faults++;

                }

                if (password != password2)
                {
                    faults++;

                }

                if (email.Length == 0)
                {
                    faults++;

                }

                if (checkbox == "false")
                {
                    faults++;

                }

                var userList = from u in context.Users
                               where u.Username == username
                               select u;

                var emailList = from e in context.Users
                                where e.Mail == email
                                select e;

                if (emailList.Count() > 0)
                {
                    //Email is taken.
                    faults++;

                }

                if (userList.Count() > 0)
                {
                    //There's already a user with that username
                    faults++;

                }


                if (faults == 0)
                {

                    User userToAdd = new User() { Username = username.Trim(), Password = password, Mail = email, Credits = 1000, Deposit = 0, Withdrawal = 0, Games = { }, bets = { } };
                    userToAdd.Picture = profilePictures.ElementAt(rnd.Next(profilePictures.Count));

                    context.Users.Add(userToAdd);
                    context.SaveChanges();

                    Session["isLoggedIn"] = true;
                    Session["username"] = username.Trim();
                    Session["credits"] = userToAdd.Credits;


                }

                
            }
            if (faults == 0)
                return RedirectToAction("/Index", "Home");
            else
                return RedirectToAction("/Index", "User");


        }


    }
}