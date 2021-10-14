using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceSubCategoryPrice
    {
        public int ServiceSubCategoryPriceId { get; set; }
        public int ServiceSubCategoryId { get; set; }
        public int ServiceProviderId { get; set; }
        public decimal ? Price { get; set; }
        public DateTime ? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime? Created_Date { get; set; }

        public string PriceOption { get; set; }

        public int PriceCount { get; set; }

        public int BatchNo { get; set; }
        public List<ServiceSubCategoryPrice> SubCategoryPriceList { get; set; }

        public string AgreementStartDate { get; set; }

        public string AgreementEndDate { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string CreatedDate { get; set; }
    }
}
