using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
    public class ServiceProviderPriceService
    {
        public MethodOutput<ServiceSubCategoryPrice> GetServiceSubCategoryPriceList(int ServiceProviderId, int ServiceId)
        {
            OpServiceProviderPrice obj = new OpServiceProviderPrice();
            return obj.GetServiceSubCategoryPriceList(ServiceProviderId,ServiceId);
        }

        public MethodOutput<string> SaveSubCategoryPrice(ServiceSubCategoryPrice objSubCategoryPrice)
        {
            OpServiceProviderPrice obj = new OpServiceProviderPrice();
            return obj.SaveSubCategoryPrice(objSubCategoryPrice);
        }
    }
}
