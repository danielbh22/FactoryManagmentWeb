using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{
    public class EmployeeBL
    {

        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public List<Employee> empS;

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }

        public Employee GetEmployee(int id)
        {
            Employee emp = db.Employees.Where(x => x.ID == id).First();
            return emp;
        }

        public void UpdateEmployee(Employee emp)
        {
            Employee e = db.Employees.Where(x => x.ID == emp.ID).First();
            e.FirstName = emp.FirstName;
            e.LastName = emp.LastName;
            e.StratWorkYear = emp.StratWorkYear;
            e.DepartmentID = emp.DepartmentID;
            db.SaveChanges();
        }
        public List<Employee> Search(string phrase)
        {
            List<Department> empDep = db.Departments.Where(x => x.Name.Contains(phrase)).ToList();


            if (empDep == null)
            {

                return db.Employees.Where(x => x.FirstName.Contains(phrase) || x.LastName.Contains(phrase)).ToList();
            }
            else
            {

                empS = db.Employees.Where(x => x.FirstName.Contains(phrase) || x.LastName.Contains(phrase)).ToList();
                foreach (var item in empDep)
                {
                     
                    empS.AddRange(db.Employees.Where(x => x.DepartmentID == item.ID).ToList());
                }

                return empS;
            }
                
        }

        public void DeleteEmployee(int id)
        {
          
            db.EmployeeShifts.RemoveRange(db.EmployeeShifts.Where(x => x.EmployeeID == id));
            db.SaveChanges();

            Employee emp = db.Employees.Where(x => x.ID == id).First();
            db.Employees.Remove(emp);
            db.SaveChanges();
        }

     //   public List<EmployeeShift> GetEmployeeShiftFrom()
      //  {
            
      //      return db.EmployeeShifts.ToList();
     //   }

    }
}