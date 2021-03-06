using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NationAssists.Areas.Admin.Models
{
    public class mBrokerPrice
    {
        public int BrokerServicePriceId { get; set; }
        public int SubCategoryId { get; set; }
        public int BrokerId { get; set; }
        public decimal? Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }

        public int ServiceId { get; set; }
        public string ServiceName { get; set; }

        public string SubCategoryName { get; set; }

        public DateTime? Created_Date { get; set; }

        public string PriceOption { get; set; }

        public int PriceCount { get; set; }

        public int BatchNo { get; set; }
        public List<BrokerPrice> BrokerPriceList { get; set; }
    }
}