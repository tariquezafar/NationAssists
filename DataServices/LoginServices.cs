using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using DbOperation;

namespace DataServices
{
   public class LoginServices
    {
        public LoginOutput ValidateLogin(LoginInput objLogin)
        {
            OpLogin objLoginOPs = new OpLogin();
            return objLoginOPs.ValidateLogin(objLogin);
        }
    }
}
