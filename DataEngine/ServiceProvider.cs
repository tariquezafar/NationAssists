using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceProvider
    {
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

        public string Escalation_Person_EmailId { get; set; }

        public DateTime ServiceProviderAgreementFromDate { get; set; }

        public DateTime ServiceProviderAgreementToDate { get; set; }

        public List<ServiceProviderServiceOpted> ServiceProviderServicesOpted { get; set; }

        public string Password { get; set; }

        public string SelectedServiceOpted { get; set; }

        public List<ServiceProviderDocument> ServiceProviderDocuments { get; set; }

        public string PriceOption { get; set; }

        public string ServiceProviderCode { get; set; }

        public string EscalationLandlineNo { get; set; }

        public List<ServiceProviderServiceArea> ServiceProviderAreas { get; set; }
        public List<ServiceSubCategoryPrice> ServiceProviderPriceList { get; set; }

        public DateTime CreatedDate { get; set; }
        public string Agg_Start_Date { get; set; }
        public string Agg_End_Date { get; set; }
    }
}
