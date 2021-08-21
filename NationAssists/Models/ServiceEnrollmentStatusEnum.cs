using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Models
{
    public enum ServiceEnrollmentStatusEnum
    {
        Open = 1,
        Accepted = 2,
        Rejected = 3,
        SendForModification = 4,
        Closed=5
    }

    public static class UserTypeCode
    {
        public static string Customer = "CS";
        public static string Employee = "EMP";
       

    }

}