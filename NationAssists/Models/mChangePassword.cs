using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Models
{
    public class mChangePassword
    {
        public string UserType { get; set; }

        public int UserId { get; set; }

        public string Password { get; set; }

        public string OldPassword { get; set; }
    }
}