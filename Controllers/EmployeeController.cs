using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagmentWeb.Models;

namespace FactoryManagmentWeb.Controllers
{
    public class EmployeeController : Controller
    {
        static EmployeeBL employeeBL = new EmployeeBL();
        
        static EmployeeShiftBL employeeShiftBL = new EmployeeShiftBL();
        static DepartmentBL departmentBL = new DepartmentBL();
        static ShiftBL shiftBL = new ShiftBL();

        static UserAccessBL userAccessBL = new UserAccessBL();
        static UserBL userBL = new UserBL();


        // GET: Employee
        public ActionResult Index()
        {
            if( (bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];

                var employeesList = employeeBL.GetEmployees();
                ViewBag.employees = employeesList;

                var employeeShiftsList = employeeShiftBL.GetEmployeeShifts();
                ViewBag.employeesShift = employeeShiftsList;

                var shiftsList = shiftBL.GetShifts();
                ViewBag.shift = shiftsList;

                return View("Employees");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult EditEmployee(int id)
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];

                var departmentsList = departmentBL.GetDepartments();
                ViewBag.Departments = departmentsList;

                Employee emp = employeeBL.GetEmployee(id);
                return View("EditEmployee", emp);

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetUpdateEmployeeFromUser(Employee emp)
        {
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];
                employeeBL.UpdateEmployee(emp);
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        public ActionResult AddShiftToEmployee( int id)
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];
                ViewBag.id = id;

                return View("AddShiftToEmployee");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetNewEmployeeShiftFromUser(EmployeeShift e)
        {
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];
                
                employeeShiftBL.AddShift(e);
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
;
        }

        public ActionResult SearchEmployee()
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];

                return View("SearchResults");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult SearchResult(string phrase)
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);

                ViewBag.fullname = Session["fullname"];
               
                var result = employeeBL.Search(phrase);
                ViewBag.employee = result;
                return View("SearchResults");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult DeleteEmployee(int id)
        {
            employeeBL.DeleteEmployee(id);
            return RedirectToAction("index");
        }
    }
}