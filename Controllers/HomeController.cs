using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FactoryManagmentWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["authenticated"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Employees()
        {
            ViewBag.Message = "Employees.";

            return View();
        }

        public ActionResult Departments()
        {
            ViewBag.Message = "Departments page.";

            return View();
        }
        public ActionResult Shifts()
        {
            ViewBag.Message = "Shifts page.";

            return View();
        }
    }
}