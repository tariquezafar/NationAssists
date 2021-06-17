using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NationAssists.Areas.Admin.Models
{
    public class mServiceProvider
    {
        public int ServiceId { get; set; }
        public int ServiceProviderId { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }

        public string CRNumber { get; set; }

        public string OfficeLocationAddress { get; set; }

        public string PostalLocationAddress { get; set; }

        public string ContactPersonName { get; set; }

        public string ContactPersonNo { get; set; }

        public string EscalationPersonName { get; set; }

        public string EscalationPersonContactNo { get; set; }

        public DateTime ServiceProviderAgreementFromDate { get; set; }

        public DateTime ServiceProviderAgreementToDate { get; set; }

        public List<Service> serviceList { get; set; }
        public List<ServiceProviderServiceOpted> ServiceProviderServicesOpted { get; set; }

        public List<ServiceProvider> ServiceProviderList { get; set; }

        public List<ServiceProviderServiceOpted> GetAllServices()
        {
            CommonServices objCS = new CommonServices();
            List<ServiceProviderServiceOpted> objLstServices = new List<ServiceProviderServiceOpted>();
            MethodOutput<ServiceSubCategory> output = new MethodOutput<ServiceSubCategory>();
            output = objCS.BindServiceSubCategory();
            objLstServices = output.DataList.Select(x => new ServiceProviderServiceOpted {

                Name = x.ServiceName,
                IsActive = false,
                ServiceId=x.ServiceId,
                SubCategoryId=x.ServiceSubCategoryId,
                SubCategoryName=x.Name,
                ServiceCode=x.ServiceCode
                
            }).ToList();

            return objLstServices;
        }

    }
}