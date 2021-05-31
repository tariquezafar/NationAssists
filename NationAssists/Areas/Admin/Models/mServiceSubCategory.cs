using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataEngine;
using DataServices;

namespace NationAssists.Areas.Admin.Models
{
    public class mServiceSubCategory
    {
        public int ServiceSubCategoryId { get; set; }
        public int ServiceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public List<ServiceSubCategory> ServiceSubCategroyList { get; set; }

        public SelectList GetAllServices()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServices().Data.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceId);
           
        }


    }
}