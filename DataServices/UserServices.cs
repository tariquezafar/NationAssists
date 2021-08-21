using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;
using DataEngine;


namespace DataServices
{
    public class UserServices
    {
        public MethodOutput<string> SaveUsers(Users users)
        {
            OpUsers obj = new OpUsers();
            return obj.SaveUsers(users);
        }

        public MethodOutput<Users> GetUsers(int UserId, int UserTypeId, int UserReferenceId, string UserCode, string UserName)
        {
            OpUsers obj = new OpUsers();
            return obj.GetUsers( UserId, UserTypeId, UserReferenceId,  UserCode, UserName);
        }

        public MethodOutput<string> DeleteUsers(int UserId)
        {
            OpUsers obj = new OpUsers();
            return obj.DeleteUsers(UserId);
        }

        public MethodOutput<Users> GetUserByReference(int ReferenceId)
        {
            OpUsers obj = new OpUsers();
            return obj.GetUsersByReferenceId(ReferenceId);
        }

        public MethodOutput<string> UpdateUserPassword(int UserId, string UserType, string Password)
        {
            OpUsers obj = new OpUsers();
            return obj.UpdateUserPassword(UserId,UserType,Password);
        }


    }
}
