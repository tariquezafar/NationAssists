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

        public MethodOutput<Membership> GetAllMemberShip(int SourceId, int PackageId, Int64 MembershipId)
        {
            OpMembership obj = new OpMembership();
            return obj.GetAllMemberShip(SourceId,PackageId,MembershipId);

        }

        public SearchCPROutput SearchMemberShipByCPRNumber(string CPRNumber, int SourceId)
        {
            OpMembership obj = new OpMembership();
            return obj.SearchMemberShipByCPRNumber(CPRNumber,SourceId);
        }
    }
}
