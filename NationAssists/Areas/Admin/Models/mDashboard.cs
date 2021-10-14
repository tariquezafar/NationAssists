using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Areas.Admin.Models
{
    public class mDashboard
    {
        public int TotalNoOfMembeshipIssuedTillDate { get; set; }
        public int NoOfRSAMembeshipIssuedTillDate { get; set; }

        public int NoOfHAMembeshipIssuedTillDate { get; set; }

        public int NoOfRSAplusCRMembeshipIssuedTillDate { get; set; }


        public int TotalNoOfMembeshipIssuedForTheDay { get; set; }
        public int NoOfRSAMembeshipIssuedForTheDay { get; set; }

        public int NoOfHAMembeshipIssuedForTheDay { get; set; }

        public int NoOfRSAplusCRMembeshipIssuedForTheDay { get; set; }

        public int NoOfMembershipSoldForTheMonth { get; set; }

        public int TotalNoOfActiveSourceUser { get; set; }

        public int NoOfServiceRequestAllocated { get; set; }

        public int NoOfServiceRequestAllocatedToday { get; set; }

        public int NoOfServiceRequestClosedToday { get; set; }

        public int NoOfServiceRequestPending { get; set; }

        public int NoOfServiceRequestClosed { get; set; }

        public int NoOfServiceRequestAccepted { get; set; }

        public int NoOfRSAServiceRequestAccepted { get; set; }

        public int NoOfCRServiceRequestAccepted { get; set; }

        public int NoOfHAServiceRequestAccepted { get; set; }

        public int TotalNoOfUsers { get; set; }

        public int TotalNoOfAgentsBroker { get; set; }

        public int TotalNoOfCustomer { get; set; }

        public int TotalNoOfServiceRequests { get; set; }

        public int TotalNoOfServiceProvider { get; set; }

        public int TotalNoOfOpenServiceRequest { get; set; }

        public int TotalNoOfClosedServiceRequest { get; set; }

        public int TotalNoOfOpenRAServiceRequest { get; set; }

        public int TotalNoOfPendingRAServiceRequest { get; set; }

        public int TotalNoOfClosedRAServiceRequest { get; set; }


        public int TotalNoOfOpenCRServiceRequest { get; set; }

        public int TotalNoOfPendingCRServiceRequest { get; set; }

        public int TotalNoOfClosedCRServiceRequest { get; set; }

        public int TotalNoOfOpenHAServiceRequest { get; set; }

        public int TotalNoOfPendingHAServiceRequest { get; set; }

        public int TotalNoOfClosedHAServiceRequest { get; set; }


        public int TotalNoOfOpenServiceRequestAllocation { get; set; }

        public int TotalNoOfClosedServiceRequestAllocation { get; set; }

    }
}