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

        // GET: Department
        public ActionResult Index()
        {
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
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
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];
              
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
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
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
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
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
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
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
            if (Session["authenticated"] != null && (bool)Session["authenticated"] == true)
            {
                ViewBag.fullname = Session["fullname"];
    
                return View("NewDepartment");

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }
    }
}