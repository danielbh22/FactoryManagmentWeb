using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagmentWeb.Models;

namespace FactoryManagmentWeb.Controllers
{
    public class LoginController : Controller
    {
        public UserBL userBL = new UserBL();
        // GET: Login
        public ActionResult Index()
        {
            //Session["authenticated"] = false;
            return View("Login");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Login");
        }

        public ActionResult Home()
        {
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];

                return View("HomePage");
            }
            else
            {
                return View("Login");
            }
        }


        [HttpPost]
        public ActionResult GetLoginData(string userName, string password)
        {
            var isAuth = userBL.IsAuthenticated(userName, password);
            if (isAuth == true)
            {
                Session["authenticated"] = true;
                Session["counter"] = 5;
                Session["date"] = "";

                var userFullName = userBL.GetFullName(userName, password);
                Session["fullname"] = userFullName;

                return RedirectToAction ("Home", "Login");
            }
            else
            {
                Session["authenticated"] = false;
                return RedirectToAction("Index","Login");
            }

            
        }
    }
}