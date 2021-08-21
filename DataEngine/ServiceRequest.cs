using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceRequest
    {
        public Int64 ServiceRequestId { get; set; }
        public int CustomerId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceSubCategoryId { get; set; }
        public string VehicleRegistrationNumber { get; set; }
        public string ChessisNo { get; set; }
        public string ServiceLocation { get; set; }
        public int CountryID { get; set; }
        public int GovernotesId { get; set; }
        public int PlaceId { get; set; }

        public int BlockId { get; set; }
        public string LocationPinCode { get; set; }
        public DateTime DateOfAccident { get; set; }
        public string NameOfWorkShop { get; set; }
        public string CarHandledWorkShopDate { get; set; }
        public DateTime? EstimatedRepairCompletedDate { get; set; }
        public string BuildingNo { get; set; }
        public int ServiceRequestStatus { get; set; }
        public DateTime ServiceRequestedDate { get; set; }
        public DateTime? ServiceCompletedDate { get; set; }
        public string ServiceFeedBack { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int SequenceNo { get; set; }
        public string TicketNo { get; set; }
        public int CreatedBy { get; set; }
        public bool StepiniCondtion { get; set; }
        public string CollectRepairVehicleAddress { get; set; }
        public string ContactMobileNo { get; set; }
        public string ReleventDetails { get; set; }
        public string Remarks { get; set; }

        public string Customer_Name { get; set; }
        public string ServiceName { get; set; }
        public string SubCategoryName { get; set; }
        public string CountryName { get; set; }
        public string GovernoratesName { get; set; }
        public string Place_Name { get; set; }
        public string Block_Code { get; set; }
        public string Status_Name { get; set; }

        


        public int BrokerId { get; set; }

        public Int64? ServiceAllocationId { get; set; }
        public int? ServiceAllocationStatus { get; set; }

        public int ServiceProviderId { get; set; }

        public string Allocation_Status { get; set; }
        public string AllocationRemarks { get; set; }
        public int? AcceptedBy { get; set; }
        public int? AssignedTo { get; set; }

        public string EmailId { get; set; }
        public string AssignedToUser { get; set; }
        public string ServiceAllocationRemarks { get; set; }
    }

    public class ServiceRequestStatus
    {
     public int   ServiceRequestStatusId {get;set;}
     public string   Status_Name            {get;set;}
     public bool  IsActive { get; set; }
    }

    public class AllocatedServiceProvider
    {
        public int ServiceProviderId { get; set; }

        public string ServiceProviderCode { get; set; }

        public string ServiceProviderName { get; set; }

        public string ContactPersonName { get; set; }
        public string MobileNo { get; set; }

        public string EmailId { get; set; }

        public decimal ServicePrice { get; set; }

        public Int64 ServicePriceId { get; set; }
    }

    public class ServiceAllocation
    {
     public Int64   ServiceAllocationId           {get;set;}
     public Int64   ServiceRequestId              {get;set;}
     public int   ServiceProviderId             {get;set;}
     public int   ServiceId                     {get;set;}
     public int   ServiceSubCategoryId          {get;set;}
     public DateTime   ServiceAllocationDate         {get;set;}
     public int   ServiceAllocationStatus       {get;set;}
     public bool   IsActive                      {get;set;}
     public DateTime   CreatedDate                   {get;set;}
     public DateTime ModifyDate { get; set; }
        public int? AssignedToUser { get; set; }

        public string Remarks { get; set; }

        public string ReasonForRejection { get; set; }
        public decimal ServicePrice { get; set; }
        public Int64 ServicePriceId { get; set; }

        public string AllocationRemarks { get; set; }

        public string Action { get; set; }
        public string AssignmentRemarks { get; set; }

        public int AcceptedBy { get; set; }
    }

    public class ServiceAllocationStatus
    {
        public int ServiceAllocationStatusId { get; set; }
        public string Status_Name { get; set; }
        public bool IsActive { get; set; }
      
    }

    public class CustomerStatus
    {
        public bool IsHavingMembership { get; set; }
        
        public bool IsMemberShipExpired { get; set; }

        public string CustomerType { get; set; }

        public bool IsSignUpCompleted { get; set; }

        public int CustomerId { get; set; }

        public int SourceId { get; set; }

        public string SourceType { get; set; }

        public string SourceName { get; set; }

        public string SourceTypeCode { get; set; }

        public string EffectiveDate { get; set; }

        public string ExpiryDate { get; set; }
    }

    public class ServiceRemarks
    {
        public Int64 RemarksId { get; set;}
        public Int64 ServiceRequestId { get; set; }
        public string Remarks { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public string CreatedByUser { get; set; }
    }
}
