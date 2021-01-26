using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{
    public class UserBL
    {

        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public bool IsAuthenticated(string userName, string pwd)
        {
            var result = db.Users.Where(x => x.UserName == userName && x.Password == pwd);
            if(result.Count() == 0)
            {
                return false;
            }
            else
            {

                return true;
            }
        }

        public string GetFullName (string userName, string pwd)
        {
            var result = db.Users.Where(x => x.UserName == userName && x.Password == pwd);

            var fullname = result.First().FullName;

            return fullname;
        }
    }
}