using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class ServiceEnrollmentService
    {
        public MethodOutput<string> SaveServiceEnrollmentRequest(ServiceEnrollmentRequest objSER)
        {
            OpServiceEnrollment obj = new OpServiceEnrollment();
            return obj.SaveServiceEnrollmentRequest(objSER);
        }

        public MethodOutput<ServiceEnrollmentRequest> GetAllServiceEnrollementRequst(int CustomerId, int ServiceId, int EnrollmentStatusId, string CustomerName)
        {
            OpServiceEnrollment obj = new OpServiceEnrollment();
            return obj.GetAllServiceEnrollementRequst(CustomerId, ServiceId,EnrollmentStatusId,CustomerName);
        }

        public MethodOutput<string> UpdateServiceEnrollmentRequest(ServiceEnrollmentRequest objSER)
        {
            OpServiceEnrollment obj = new OpServiceEnrollment();
            return obj.UpdateServiceEnrollmentRequest(objSER);
        }
    }
}
