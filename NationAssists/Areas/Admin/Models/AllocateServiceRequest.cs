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




        public List<ServiceRequest> ServiceRequestList { get; set; }

        public List<ServiceRequest> PendingServiceRequestList { get; set; }

        public List<ServiceRequest> AllocateServiceRequestList { get; set; }

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
    }

    public class SearchSR
    {
        public int ServiceRequestStatusId { get; set; }
        public string TicketNo { get; set; }

        public string AccountType { get; set; }
       public int BrokerId { get; set; }
       public string AccountSubType { get; set; }
        
       public DateTime StartDate { get; set; }
     public DateTime EndDate { get; set; }
        
    }
}