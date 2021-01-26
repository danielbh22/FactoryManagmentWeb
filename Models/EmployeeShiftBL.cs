using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{
    public class EmployeeShiftBL
    {

        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public List<EmployeeShift> GetEmployeeShifts()
        {
            return db.EmployeeShifts.ToList();
        }

        public void AddShift(EmployeeShift e)
        {
            db.EmployeeShifts.Add(e);
            db.SaveChanges();
        }
    }
}