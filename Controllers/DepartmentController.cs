using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FactoryManagmentWeb.Models;

namespace FactoryManagmentWeb.Controllers
{
    public class DepartmentController : Controller
    {
        static DepartmentBL departmentBL = new DepartmentBL();

        static EmployeeBL employeeBL = new EmployeeBL();

        static UserAccessBL userAccessBL = new UserAccessBL();

        static UserBL userBL = new UserBL();

        // GET: Department
        public ActionResult Index()
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ///update number of actions
                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions -1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);


                ViewBag.fullname = Session["fullname"];

                var departmentsList = departmentBL.GetDepartments();
                ViewBag.departments = departmentsList;

                var employeesList = employeeBL.GetEmployees();
                ViewBag.employees = employeesList;

                return View("Departments");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult EditDepartment(int id)
        {
            if ((bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ViewBag.fullname = Session["fullname"];

                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);


                Department dep = departmentBL.GetDepartments(id);
                return View("EditDepartment",dep);
               
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetUpdateDepartmentFromUser(Department dep)
        {
            if ( (bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ViewBag.fullname = Session["fullname"];
                
                departmentBL.UpdateDepartment(dep);
                return RedirectToAction("Index");
 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult DeleteDepartment(int id)
        {
            if ( (bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ViewBag.fullname = Session["fullname"];
                
                departmentBL.DeleteDepartment(id);
                return RedirectToAction("Index");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult GetNewDepartmentFromUser(Department dep)
        {
            if ( (bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ViewBag.fullname = Session["fullname"];
        
                departmentBL.AddDepartment(dep);
                return RedirectToAction("Index");
 
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult AddDepartment()
        {
            if ( (bool)Session["authenticated"] == true && (int)Session["numOfAction"] > 0)
            {
                ViewBag.fullname = Session["fullname"];

                var userID = (int)Session["userID"];

                var userAcc = userAccessBL.GetUserAccess(userID);
                Session["numOfAction"] = userAcc.NumOfActions - 1;
                userAccessBL.UpdateUserAccessNum(userAcc);
                userBL.UpdateUserNumOfAction(userID);



                return View("NewDepartment");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}