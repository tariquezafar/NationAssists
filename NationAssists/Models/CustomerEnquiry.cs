using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Models
{
    public class CustomerEnquiry
    {
        public string Name { get; set; }

        public string Reason { get; set; }

        public string EmailAddress { get; set; }

        public string MobileNumber { get; set; }

        public string Message { get; set; }

    }
}