using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FactoryManagmentWeb.Models
{
    public class UserAccessBL
    {
        FactoryManagementDBEntities db = new FactoryManagementDBEntities();

        public void AddUserAccess(UserAccess acc)
        {
            db.UserAccesses.Add(acc);
            db.SaveChanges();
        }

        public UserAccess GetUserAccess(int userId)
        {

            UserAccess acc = db.UserAccesses.Where(x => x.UserID == userId).FirstOrDefault();
            
            if (acc == null)
            {
                return null;
            
            }
            else
            {
                return acc;
            }
            

        }

        public void UpdateUserAccessNum(UserAccess acc)
        {
            UserAccess d = db.UserAccesses.Where(x => x.ID == acc.ID).First();
            int num = acc.NumOfActions - 1;
            d.NumOfActions = num;
            db.SaveChanges();
        }

        public void DeleteUserAction(int userId)
        {
            UserAccess userAcc = db.UserAccesses.Where(x => x.UserID == userId).First();
            db.UserAccesses.Remove(userAcc);
            db.SaveChanges();

        }

    }
}