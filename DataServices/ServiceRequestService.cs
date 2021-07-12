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
        public MethodOutput<ServiceRequest> BindServiceRequestForAllocation(int ServiceRequestStatusId, string TicketNo)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceRequestForAllocation(ServiceRequestStatusId,TicketNo);
        }

        public MethodOutput<AllocatedServiceProvider> BindServiceProviderForAllocation(int BlockId, int PlaceId, int GovernotesId, int CountryId, int SubCategoryId, int BrokerId, int ServiceId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceProviderForAllocation(BlockId,PlaceId,GovernotesId,CountryId,SubCategoryId,BrokerId,ServiceId);
        }

        public MethodOutput<string> SaveServiceRequestAllocation(ServiceAllocation objSA)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.SaveServiceRequestAllocation(objSA);
        }

        public MethodOutput<ServiceRequest> BindAllocation(int ServiceProviderId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceAllocation(ServiceProviderId);
        }

        
        public MethodOutput<ServiceRequest> BindAssignedServiceRequest(int UserId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindAssignedServiceRequest(UserId);
        }

        public MethodOutput<ServiceRequest> BindAllServiceRequest(int ServiceRequestStatusId,
            string TicketNo, string AccountType, int BrokerId, string AccountSubType, DateTime StartDate, DateTime EndDate)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindAllServiceRequest(ServiceRequestStatusId,TicketNo,AccountType,BrokerId,AccountSubType,StartDate,EndDate);
        }

        public MethodOutput<string> BindVehicleRegistrationNoListByCPRNumber(string CPRNumber)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindVehicleRegistrationNoListByCPRNumber(CPRNumber);
        }


        public MethodOutput<string> BindChessisListByCPRNumber(string CPRNumber)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindChessisListByCPRNumber(CPRNumber);
        }

        public MethodOutput<ServiceRequest> GetServiceRequestListByCustomer(int CustomerId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceRequestByCustomer(CustomerId);
        }
    }
}
