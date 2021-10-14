using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEngine;
namespace NationAssists.Areas.Admin.Models
{
    public class AllocateServiceRequest
    {
        public int ServiceRequestStatusId { get; set; }

        public int AssignedToUserId { get; set; }


        public string StartDate { get; set; }

        public string EndDate { get; set; }
        

        public int CountryId { get; set; }

        public int UserId { get; set; }


        public string Service { get; set; }

        public int ServiceAllocationStatusId { get; set; }

        public int ServiceProviderId { get; set; }

        public List<ServiceRequest> ServiceRequestList { get; set; }

        public List<ServiceRequest> PendingServiceRequestList { get; set; }

        public List<ServiceRequest> AllocateServiceRequestList { get; set; }

        public List<Users> UserList { get; set; }

        public List<ServiceRequest> AssignedServiceRequestList { get; set; }
        public SelectList GetAllServiceRequestStatus()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServiceRequestStatus().DataList.OrderBy(s => s.ServiceRequestStatusId).ToList().Select(m => new SelectListItem() { Text = m.Status_Name, Value = m.ServiceRequestStatusId.ToString() }).OrderBy(s => s.Value).ToList();

            return new SelectList(lstDist, "Value", "Text", ServiceRequestStatusId);

        }

        public SelectList GetUserByReference(int RefernceId)
        {
            UserServices objCS = new UserServices();
            IEnumerable<SelectListItem> lstDist = objCS.GetUserByReference(RefernceId).DataList.OrderBy(s => s.UserId).ToList().Select(m => new SelectListItem() { Text = m.Name+"("+m.UserCode+")", Value = m.UserId.ToString() }).OrderBy(s => s.Value).ToList();
            return new SelectList(lstDist, "Value", "Text", AssignedToUserId);
        }

        public SelectList BindCountry()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindCountry().DataList.OrderBy(s => s.Name).ToList().Select(m => new SelectListItem() { Text = m.Name, Value = m.CountryId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", CountryId);
        }

        public SelectList BindService()
        {
            List<Service> objLstService = new List<Service>();
            objLstService.Add(new Service { ServiceCode="RA", ServiceName= "RoadSide Assistance Service" });
            objLstService.Add(new Service { ServiceCode = "CR", ServiceName = "Roadside Assistance with Car Replacement Service" });
            objLstService.Add(new Service { ServiceCode = "HA", ServiceName = "Home Assistance Service" });
            IEnumerable<SelectListItem> lstDist = objLstService.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceCode }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", Service);
        }

        public SelectList BindServiceAllocationStatus()
        {
            List<ServiceAllocationStatus> objLstAllocationStatus = new List<ServiceAllocationStatus>();
            objLstAllocationStatus.Add(new ServiceAllocationStatus { ServiceAllocationStatusId = 1, Status_Name = "Open" });
            objLstAllocationStatus.Add(new ServiceAllocationStatus { ServiceAllocationStatusId = 2, Status_Name = "Accepted" });
            objLstAllocationStatus.Add(new ServiceAllocationStatus { ServiceAllocationStatusId = 3, Status_Name = "Rejected" });
            objLstAllocationStatus.Add(new ServiceAllocationStatus { ServiceAllocationStatusId = 4, Status_Name = "Closed" });
            
            IEnumerable<SelectListItem> lstDist = objLstAllocationStatus.ToList().Select(m => new SelectListItem() { Text = m.Status_Name, Value = m.ServiceAllocationStatusId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceAllocationStatusId);
        }
    }

    public class SearchSR
    {
        public int ServiceRequestStatusId { get; set; }
        public string TicketNo { get; set; }

        public string AccountType { get; set; }
       public int BrokerId { get; set; }
       public string AccountSubType { get; set; }
        
       public DateTime ? StartDate { get; set; }
     public DateTime ? EndDate { get; set; }
        public int UserId { get; set; }

        public string Service { get; set; }

        public int ServiceAllocationStatusId { get; set; }


    }
}