using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Areas.Admin.Models
{
    public class mMembership
    {
        public int BrokerId { get; set; }

        public int ServiceId { get; set; }


        public List<Membership> MembershipList { get; set; }
    }

}