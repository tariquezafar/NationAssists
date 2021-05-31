using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Models
{
    public class mServicePrice
    {
        public Int64 ServicePriceId { get; set; }
        public int ServiceId { get; set; }
        public decimal Prices { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }

        public List<ServicePrice> ServicePriceList { get; set; }

        public SelectList GetAllServices()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServices().Data.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceId);

        }
    }
}