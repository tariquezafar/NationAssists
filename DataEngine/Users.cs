using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public int? BranchId { get; set; }
        public DateTime UserExpiryDate { get; set; }
        public bool IsActive { get; set; }

        public string UserTypeName { get; set; }
        public int UserTypeId { get; set; }

        public string MobileNo { get; set; }

        public string Role { get; set; }

        public string BranchName { get; set; }

        public List<UserType> UserTypeList { get; set; }

        public int UserReferenceId { get; set; }


    }

    public class UserType
    {
      public int  UserTypeId    {get;set;}
      public string  UserTypeName      {get;set;}
      public bool IsActive { get; set; }
    }
}
