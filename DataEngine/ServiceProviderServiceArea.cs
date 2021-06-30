using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class ServiceProviderServiceArea
    {
        public Int64 ServiceProviderServiceAreaId { get; set; }


        public int ServiceProviderId { get; set; }
        public int ServiceId { get; set; }
        public int ServiceSubCategoryId { get; set; }
        public int CountryId { get; set; }
        public int? GovernotesId { get; set; }
        public int? PlaceId { get; set; }
        public int? BlockId { get; set; }
        public string PinCode { get; set; }

        public string ServiceName { get; set; }

        public string SubCategoryName { get; set; }

        public string CountryName { get; set; }

        public string GovernotesName { get; set; }

        public string PlaceName { get; set; }

        public List<ServiceProviderServiceArea> ServiceAreas { get; set; }
    }

    public class Governotes
    {
        public int CountryId { get; set; }
        public int GovernorateId { get; set; }

        public string GovernoratesName { get; set; }
    }

    public class Place
    {
     public int   PlaceId           {get;set;}
     public int   GovernorateId     {get;set;}
     public string Place_Name { get; set; }
    }

    public class Block
    {
     public int   PlaceId           {get;set;}
     public string   Block_Code     {get;set;}
     public int BlockId { get; set; }
    }
}
