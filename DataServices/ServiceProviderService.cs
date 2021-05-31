using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;
using DataEngine;

namespace DataServices
{
  public  class ServiceProviderService
    {
        public MethodOutput<string> SaveServiceProvider(ServiceProvider serviceProvider)
        {
            OpServiceProvider obj = new OpServiceProvider();
            return obj.SaveServiceProvider(serviceProvider);
        }

        public MethodOutput<ServiceProvider> GetAllServiceProvider(int ServiceProviderId)
        {
            OpServiceProvider obj = new OpServiceProvider();
            return obj.GetAllServiceProvider(ServiceProviderId);
        }


        public MethodOutput<string> DeleteServiceServiceProvider(int ServiceProviderId)
        {
            OpServiceProvider obj = new OpServiceProvider();
            return obj.DeleteServiceProvider(ServiceProviderId);
        }

    }
}
