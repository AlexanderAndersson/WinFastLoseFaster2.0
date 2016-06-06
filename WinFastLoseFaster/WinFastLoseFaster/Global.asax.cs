using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using WinFastLoseFaster.App_Start;

namespace WinFastLoseFaster
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            Session["isLoggedIn"] = false;
            Session["username"] = "";
            Session["credits"] = 0;


        }

    }
}
