using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
  public  class DashboardModel
    {
      
        public List<ServiceAllocation> ServiceAllocationList { get; set; }

        public List<Membership> MembershipList { get; set; }

        public List<Users> UserList { get; set; }
        public List<ServiceProvider> ServiceProviderList { get; set; }
        public List<Broker> SourceList { get; set; }

        public List<ServiceRequest> ServiceRequestList { get; set; }

        public List<ServiceEnrollmentRequest> ServiceEnrollmentRequestList { get; set; }

        public List<Customer> CustomerList { get; set; }

        








    }
}
