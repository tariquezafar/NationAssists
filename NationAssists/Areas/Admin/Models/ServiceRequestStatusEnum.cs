using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Areas.Admin.Models
{
    public enum ServiceRequestStatusEnum
    {
        Open = 1,
        Assigned = 2,
        Re_Assigned = 3,
        Closed = 4,
        Pending=5

    }

    public enum ServiceRequestAllocationStatusEnum
    {
        Open = 1,
        Accepted = 2,
        Rejected = 3,
        Closed = 4

    }

   


}