using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
   public class ServiceRequestService
    {
        public MethodOutput<Service> BindServicesByCPRNumber(string CPRNumber)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServicesByCPRNumber(CPRNumber);
        }

        public MethodOutput<string> SaveServiceRequest(ServiceRequest SR)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.SaveServiceRequest(SR);
        }
    }
}
