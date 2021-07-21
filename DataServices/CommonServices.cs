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

        public MethodOutput<Service> BindServiceOptedByBrokerId(int? BrokerId)
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceOptedByBrokerId(BrokerId);
        }

        public MethodOutput<UserType> BindUserType()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindUserType();
        }

        public MethodOutput<Country> BindCountry()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindCountry();
        }

        public MethodOutput<Governotes> BindGovernotes(int CountryId)
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindGovernotes(CountryId);
        }

        public MethodOutput<Place> BindPlaces(int GovernotesId)
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindPlaces(GovernotesId);
        }

        public MethodOutput<Block> BindBlock(int PlaceId)
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindBlockCode(PlaceId);
        }

        public MethodOutput<ServiceRequestStatus> BindServiceRequestStatus()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceRequestStatus();
        }

        public MethodOutput<ServiceEnrollmentStatus> BindServiceEnrollmentStatus()
        {
            OpCommon objCommon = new OpCommon();
            return objCommon.BindServiceEnrollmentStatus();
        }

    }
}
