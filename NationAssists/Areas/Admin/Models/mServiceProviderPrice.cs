using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEngine;

namespace NationAssists.Areas.Admin.Models
{
    public class mServiceProviderPrice
    {
        public int ServiceSubCategoryPriceId { get; set; }
        public int ServiceSubCategoryId { get; set; }
        public int? ServiceProviderId { get; set; }
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

     public int ServiceId { get; set; }

        public string PriceOption { get; set; }

        public int BatchNo { get; set; }
        public int PriceCount { get; set; }
        public List<ServiceSubCategoryPrice> ServiceSubCategoryPriceList { get; set; }




        public SelectList GetAllServiceProvider()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServiceProvider().Data.OrderBy(s => s.FirstName).ToList().Select(m => new SelectListItem() { Text = m.FirstName, Value = m.ServiceProviderId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceProviderId);
        }

        public SelectList GetAllServices(int? ServiceProviderId)
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServicesByServiceProviderId(ServiceProviderId).Data.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceId);

        }
    }
}