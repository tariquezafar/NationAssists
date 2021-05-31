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

        public int                ServiceProviderId         {get;set;}
        public int                ServiceId                 {get;set;}
        public int                ServiceSubCategoryId      {get;set;}
        public string               Address                   {get;set;}
        public int                CountryId                 {get;set;}
        public Int64                StateId                   {get;set;}
        public Int64 CityId { get; set; }
    }
}
