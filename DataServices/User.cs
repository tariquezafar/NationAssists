using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;
using DataEngine;


namespace DataServices
{
    public class User
    {
        public MethodOutput<string> SaveUsers(Users users)
        {
            OpUsers obj = new OpUsers();
            return obj.SaveUsers(users);
        }

        public MethodOutput<Users> GetUsers(int UserId)
        {
            OpUsers obj = new OpUsers();
            return obj.GetUsers(UserId);
        }

        public MethodOutput<string> DeleteUsers(int UserId)
        {
            OpUsers obj = new OpUsers();
            return obj.DeleteUsers(UserId);
        }
    }
}
