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
        public UserAccessBL userAccessBL = new UserAccessBL();
        // GET: Login
        public ActionResult Index()
        {
            Session["authenticated"] = false;
            return View("Login");
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Login");
        }

        public ActionResult Home()
        {
            if ( (bool)Session["authenticated"] == true)
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
 
                var date = DateTime.Now;
                var user = userBL.GetUser(userName, password);
                
                Session["userID"] = user.ID;
                Session["numOfAction"] = user.NumOfAction;
                Session["fullname"] = user.FullName;


                var userAcc = userAccessBL.GetUserAccess(user.ID);
                
                if (userAcc != null && userAcc.Date.ToString("MM/dd/yyyy") != date.ToString("MM/dd/yyyy"))
                {
                    userAccessBL.DeleteUserAction(user.ID);
                }

                if (userAcc!= null && (int)Session["numOfAction"] <= 0)
                {
                    Console.WriteLine("too many actions try tomorrow");

                    Session["authenticated"] = false;
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    if (userAcc == null )
                    {
                        Session["numOfAction"] = 5;
                        Session["authenticated"] = true;
                        UserAccess access = new UserAccess() { UserID = user.ID, Date = date, NumOfActions = (int)Session["numOfAction"] };
                        userAccessBL.AddUserAccess(access);
                        userBL.ResetUserNumOfAction((int)Session["userID"]);
                    }
                    else
                    {
                        if(userAcc != null && (int)Session["numOfAction"] > 0)
                        {
                            Session["authenticated"] = true;
                        }
                    }

                }
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