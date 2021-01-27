using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagmentWeb.Models;

namespace FactoryManagmentWeb.Controllers
{
    public class ShiftController : Controller
    {
        static EmployeeShiftBL employeeShiftBL = new EmployeeShiftBL();
        static EmployeeBL employeeBL = new EmployeeBL();

        static ShiftBL shiftBL = new ShiftBL();

        static UserAccessBL userAccessBL = new UserAccessBL();

        static UserBL userBL = new UserBL();
        // GET: Shift

        public ActionResult Index()
        {

            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];
                
                var shiftsList = shiftBL.GetShifts();
                ViewBag.shifts = shiftsList;

                var employeeShiftsList = employeeShiftBL.GetEmployeeShifts();
                ViewBag.employeesShift = employeeShiftsList;


                var employeeList = employeeBL.GetEmployees();
                ViewBag.employeeList = employeeList;

                return View("Shifts");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult AddShift()
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];
                return View("AddShift");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }


        }

        [HttpPost]
        public ActionResult AddNewShift(Shift s)
        {
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];

                shiftBL.AddNewShift(s);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

    }
}