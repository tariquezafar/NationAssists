using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
   public class ServicePriceService
    {
        public MethodOutput<string> SavePrices(ServicePrice price)
        {
            OpServicePrice obj = new OpServicePrice();
            return obj.SaveServicePrices(price);
        }

        public MethodOutput<ServicePrice> GetServicePrices(int ServicePriceId)
        {
            OpServicePrice obj = new OpServicePrice();
            return obj.GetServicePricesByServiceId(ServicePriceId);
        }
    }
}
