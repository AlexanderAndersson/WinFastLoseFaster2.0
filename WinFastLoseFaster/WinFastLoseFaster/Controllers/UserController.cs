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

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            string username = Request["inputUsername"];
            string password = Request["inputPassword"];

            var userList = from u in context.Users
                           where u.Username == username
                           select u;


            if (userList.Count() == 1)
            {
                if (userList.First().Password == password)
                {
                    Session["isLoggedIn"] = true;
                    Session["username"] = username;

                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    

                }//Right username, wrong password

            }//No user with inputted username



            return RedirectToAction("Index", "User");
        }

        public ActionResult Register()
        {

            WinFastLoseFasterContext context = new WinFastLoseFasterContext();

            string username = Request["inputUsername"];
            string password = Request["inputPassword"];
            string password2 = Request["inputRetypePassword"];
            string email = Request["inputEmail"];
            string checkbox = Request["inputCheckbox"];

            int faults = 0;


            if(username.Trim().Length < 3)
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

            /*if (checkbox == "false")
            {
                faults++;

            }*/

            var userList = from u in context.Users
                           where u.Username == username
                           select u;
            
            if (userList.Count() > 0)
            {
                //There's already a user with that username
                faults++;

            }


            if (faults == 0)
            {

                username = userList.First().Username;

                User userToAdd = new User() { Username = username, Password = password, Mail = email, Picture = "http://vignette3.wikia.nocookie.net/peido/images/a/a1/Fsjal_spongebob.png/revision/latest?cb=20110801205739", Credits = 1000, Deposit = 0, Withdrawal = 0 };

                context.Users.Add(userToAdd);
                context.SaveChanges();

                Session["isLoggedIn"] = true;
                Session["username"] = username;
  

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "User");

            }


            
        }


    }
}