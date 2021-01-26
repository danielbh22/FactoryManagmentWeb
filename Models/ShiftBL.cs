using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{


    public class ShiftBL
    {
        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public List<Shift> GetShifts()
        {
            return db.Shifts.ToList();
        }

        public Shift GetShifts(int id)
        {
            Shift shi = db.Shifts.Where(x => x.ID == id).First();
            return shi;
        }

        public void AddNewShift(Shift s)
        {
            db.Shifts.Add(s);
            db.SaveChanges();
        }
    }
}