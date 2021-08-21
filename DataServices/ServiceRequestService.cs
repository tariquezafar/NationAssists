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
        public MethodOutput<ServiceRequest> BindServiceRequestForAllocation(int ServiceRequestStatusId, string TicketNo, string CustomerName, string ContactNo, string EmailId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceRequestForAllocation(ServiceRequestStatusId,TicketNo,  CustomerName,  ContactNo,   EmailId);
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
            string TicketNo, string AccountType, int BrokerId, string AccountSubType, DateTime StartDate, DateTime EndDate , int UserId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindAllServiceRequest(ServiceRequestStatusId,TicketNo,AccountType,BrokerId,AccountSubType,StartDate,EndDate,UserId);
        }

        public MethodOutput<string> BindVehicleRegistrationNoListByCPRNumber(string CPRNumber,int ServiceId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindVehicleRegistrationNoListByCPRNumber(CPRNumber,ServiceId);
        }


        public MethodOutput<string> BindChessisListByCPRNumber(string CPRNumber, int ServiceId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindChessisListByCPRNumber(CPRNumber,ServiceId);
        }

        public MethodOutput<ServiceRequest> GetServiceRequestListByCustomer(int CustomerId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindServiceRequestByCustomer(CustomerId);
        }

        public MethodOutput<CustomerStatus> GetCustomerStatus(string CPRNumber)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindCustomerStatus(CPRNumber);
        }

        public MethodOutput<VehicleDetail> BindVehicleDetailByCPRNumber(string CPRNumber, int ServiceId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindVehicleDetailByCPRNumber(CPRNumber,ServiceId);
        }

        public MethodOutput<Customer> GetCustomerDetail(string CPRNumber)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.BindCustomeDetail(CPRNumber);
        }
        public MethodOutput<ServiceProvider> GetAllServiceProviderByServiceRequest(Int64 ServiceRequestId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.GetAllServiceProviderByServiceRequest(ServiceRequestId);
        }
        public MethodOutput<Users> GetAllAssignToUser(int UserId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.GetAllAssignToUser(UserId);
        }

        public MethodOutput<string> UpdateServiceRequest(ServiceAllocation objSA)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.UpdateServiceRequest(objSA);
        }

        public MethodOutput<ServiceRemarks> GetAllServiceRemarks(Int64 ServiceRequestId)
        {
            OpServiceRequest objCommon = new OpServiceRequest();
            return objCommon.GetAllServiceRemarks(ServiceRequestId);
        }

    }
}
