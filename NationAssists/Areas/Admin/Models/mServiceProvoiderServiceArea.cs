using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ICardPrinter.Areas.Admin.Models
{
    public class mServiceProvoiderServiceArea
    {
        public Int64 ServiceProviderServiceAreaId { get; set; }
        public string AreaType { get; set; }

        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceSubCategoryId { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
        public Int64 StateId { get; set; }
        public Int64 CityId { get; set; }

        public SelectList GetAllServiceProvider()
        {
            CommonServices objCS = new CommonServices();
            IEnumerable<SelectListItem> lstDist = objCS.BindServiceProvider().DataList.OrderBy(s => s.FirstName).ToList().Select(m => new SelectListItem() { Text = m.FirstName, Value = m.ServiceProviderId.ToString() }).OrderBy(s => s.Text).ToList();
            return new SelectList(lstDist, "Value", "Text", ServiceProviderId);
        }
    }
}