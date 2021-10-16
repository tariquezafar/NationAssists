using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Models
{
    public class mMembership
    {
        public int BrokerId { get; set; }

        public int ServiceId { get; set; }

        public string Service { get; set; }

        public List<Membership> MembershipList { get; set; }

        public SelectList BindService()
        {
            List<Service> objLstService = new List<Service>();
            objLstService.Add(new Service { ServiceCode = "RA", ServiceName = "RoadSide Assistance Service" });
            objLstService.Add(new Service { ServiceCode = "CR", ServiceName = "Roadside Assistance with Car Replacement Service" });
            objLstService.Add(new Service { ServiceCode = "HA", ServiceName = "Home Assistance Service" });
            IEnumerable<SelectListItem> lstDist = objLstService.OrderBy(s => s.ServiceName).ToList().Select(m => new SelectListItem() { Text = m.ServiceName, Value = m.ServiceCode }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", Service);
        }
    }

}