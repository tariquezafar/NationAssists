using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices
{
   public class MembershipServices
    {
        public MethodOutput<string> SaveMembership(Membership objMembership)
        {
            OpMembership obj = new OpMembership();
            return obj.SaveMembership(objMembership);

        }

        public MethodOutput<Membership> GetAllMemberShip(int SourceId, int PackageId, Int64 MembershipId, string CPRNumber, string PolicyType, string PolicyNo, string InsuredName
            , string MobileNo, string EmailId, string VehicleRegistrationNo, string ChassisNo, string SourceType,string service, DateTime? StartDate, DateTime? EndDate)
        {
            OpMembership obj = new OpMembership();
            return obj.GetAllMemberShip(SourceId,PackageId,MembershipId,  CPRNumber,  PolicyType,  PolicyNo,  InsuredName
            ,  MobileNo,  EmailId,  VehicleRegistrationNo,  ChassisNo,  SourceType,service,StartDate,EndDate);

        }

        public SearchCPROutput SearchMemberShipByCPRNumber(string CPRNumber, int SourceId)
        {
            OpMembership obj = new OpMembership();
            return obj.SearchMemberShipByCPRNumber(CPRNumber,SourceId);
        }
    }
}
