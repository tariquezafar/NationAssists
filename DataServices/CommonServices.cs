using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbOperation;
using DataEngine;

namespace DataServices
{
  public  class CommonServices
    {
        public MethodOutput<Service> BindServices()
        {
            OpCommon objCommon = new OpCommon();
           return objCommon.BindService();


        }

        public MethodOutput<Users> BindUsers()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindUser();
        }

        public MethodOutput<Role> BindRoles()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindRole();
        }

        public MethodOutput<Branch> BindBranches()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindBranch();
        }

        public MethodOutput<ServiceSubCategory> BindServiceSubCategory()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceSubCategory();
        }

        public MethodOutput<ServiceProvider> BindServiceProvider(string UserTye="")
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceProvider(UserTye);
        }


        public MethodOutput<Service> BindServicesByServiceProviderId(int? ServiceProviderId)
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceOptedByServiceProviderId(ServiceProviderId);


        }

        public MethodOutput<UserType> BindUserType()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindUserType();
        }

    }
}
