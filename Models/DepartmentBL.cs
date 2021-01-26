using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{
    public class DepartmentBL
    {
        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public List<Department> GetDepartments()
        {
            return db.Departments.ToList();
        }

        public Department GetDepartments(int id)
        {
            Department dep = db.Departments.Where(x => x.ID == id).First();
            return dep;
        }

        public void UpdateDepartment(Department dep)
        {
            Department d = db.Departments.Where(x => x.ID == dep.ID).First();
            d.Name = dep.Name;
            d.Manager = dep.Manager;
            db.SaveChanges();
        }

        public void DeleteDepartment(int id)
        {
            Department dep = db.Departments.Where(x => x.ID == id).First();
            db.Departments.Remove(dep);
            db.SaveChanges();

        }

        public void AddDepartment(Department dep)
        {
            db.Departments.Add(dep);
            db.SaveChanges();
        }
    }
}