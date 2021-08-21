using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;
using DataEngine;


namespace DataServices
{
   public class ServiceSubCategoryService
    {

        public MethodOutput<string> SaveServiceSubCategory(ServiceSubCategory serviceSubCategory)
        {
            OpServiceCategory obj = new OpServiceCategory();
            return obj.SaveServiceCategory(serviceSubCategory);
        }

        public MethodOutput<ServiceSubCategory> GetServiceSubCategoryByServiceId(int ServiceId)
        {
            OpServiceCategory obj = new OpServiceCategory();
            return obj.GetServiceSubCategoryByServiceId(ServiceId);
        }

        public MethodOutput<string> DeleteServiceSubCategory(int ServiceId)
        {
            OpServiceCategory obj = new OpServiceCategory();
            return obj.DeleteServiceSubCategory(ServiceId);
        }



        public MethodOutput<string> SaveService(Service service)
        {
            OpServiceCategory obj = new OpServiceCategory();
            return obj.SaveService(service);
        }

        public MethodOutput<Service> GetAllServices()
        {
            OpServiceCategory obj = new OpServiceCategory();
            return obj.GetAllServices();
        }



    }
}


