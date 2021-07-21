using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Models
{
    public class mServiceRequest
    {
        public int ServiceId { get; set; }
        public string CPRNumber { get; set; }
        public int CustomerId { get; set; }

        public int CountryId { get; set; }

        public string ChessisNo { get; set; }

        public string VehicleRegistrationNo { get; set; }

        public List<ServiceRequest> ServiceRequestList { get; set; }

      

        public SelectList BindServicesByCPRNumber(string CPRNumber)
        {
            ServiceRequestService objCS = new ServiceRequestService();
            IEnumerable<SelectListItem> lstDist = objCS.BindServicesByCPRNumber(CPRNumber).DataList.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName+" (" +m.ServiceCode+")", Value = m.ServiceId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceId);
        }


        public SelectList BindCountry()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindCountry().DataList.OrderBy(s => s.Name).ToList().Select(m => new SelectListItem() { Text = m.Name, Value = m.CountryId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", CountryId);
        }

        
    }
}