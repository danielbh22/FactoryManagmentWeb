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

        public User GetUser(string userName, string pwd)
        {
            User result = db.Users.Where(x => x.UserName == userName && x.Password == pwd).First();

            return result;
        }

        public void UpdateUserNumOfAction(int userID)
        {
            User u = db.Users.Where(x => x.ID == userID).First();
            int num = u.NumOfAction - 1;
            u.NumOfAction = num;
            db.SaveChanges();
        }

        public void ResetUserNumOfAction(int userID)
        {
            User u = db.Users.Where(x => x.ID == userID).First();
            u.NumOfAction = 5;
            db.SaveChanges();
        }
    }
}