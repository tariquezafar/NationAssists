using DataEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;

namespace DataServices
{
    public class ServiceAreaMappingServices
    {
        public MethodOutput<string> SaveServiceAreaMapping(ServiceProviderServiceArea objServiceArea)
        {
            OpServiceArea objSA = new OpServiceArea();
            return objSA.SaveServiceAreaMapping(objServiceArea);
        }

        public MethodOutput<ServiceProviderServiceArea> GetAllServiceArea(int ServiceProviderId, int ServiceId, int SubCategoryId)
        {
            OpServiceArea objSA = new OpServiceArea();
            return objSA.GetAllServiceArea(ServiceProviderId,ServiceId,SubCategoryId);
        }
    }
}
