using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Models
{
    public class mServiceEnrollment
    {
        public Service Package { get; set; }

        public int CountryId { get; set; }

        public int CustomerId { get; set; }

        public int ServiceEnrollmentStatusId { get; set; }

        public int SearchServiceEnrollmentStatusId { get; set; }

        public int ServiceId { get; set; }

        public List<ServiceEnrollmentRequest> ServiceEnrollmentRequestList { get; set; }

        public SelectList BindCountry()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindCountry().DataList.OrderBy(s => s.Name).ToList().Select(m => new SelectListItem() { Text = m.Name, Value = m.CountryId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", CountryId);
        }


        public SelectList BindEnrollementStatus(bool Isupdate)
        {
            CommonServices objCS = new CommonServices();
            if (Isupdate)
            {
                IEnumerable<SelectListItem> lstDist = objCS.BindServiceEnrollmentStatus().DataList.Where(x => (x.ServiceEnrollmentStatusId != (int)ServiceEnrollmentStatusEnum.Open && x.ServiceEnrollmentStatusId != (int)ServiceEnrollmentStatusEnum.Closed)).OrderBy(s => s.Status_Name).ToList().Select(m => new SelectListItem() { Text = m.Status_Name, Value = m.ServiceEnrollmentStatusId.ToString() }).OrderBy(s => s.Text).ToList();
                return new SelectList(lstDist, "Value", "Text", ServiceEnrollmentStatusId);
            }
            else
            {
                IEnumerable<SelectListItem> lstDist = objCS.BindServiceEnrollmentStatus().DataList.OrderBy(s => s.Status_Name).ToList().Select(m => new SelectListItem() { Text = m.Status_Name, Value = m.ServiceEnrollmentStatusId.ToString() }).OrderBy(s => s.Text).ToList();
                return new SelectList(lstDist, "Value", "Text", ServiceEnrollmentStatusId);
            }
        }

        public SelectList GetAllServices()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServices().DataList.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceId);

        }
    }
}