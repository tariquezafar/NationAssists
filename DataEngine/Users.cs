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

        public string RoleName { get; set; }

        public string BranchName { get; set; }

        public List<UserType> UserTypeList { get; set; }

        public int UserReferenceId { get; set; }

        public string CPRNumber { get; set; }
        public DateTime? CPRExpiryDate { get; set; }
        public string PassportNumber { get; set; }
        public DateTime? PassportExpiryDate { get; set; }
        public string VisaNumber { get; set; }
        public string ContactAddressHomeCountry { get; set; }
        public string ContactAddressLocal { get; set; }
        public string MobileNumberLocal { get; set; }
        public string EmergencyContactPersonName { get; set; }
        public DateTime? DateOfJoining { get; set; }
        public string Remarks { get; set; }

        public string Reference_Code { get; set; }

        public string UserCode { get; set; }

        public string SourceName { get; set; }

        public string UserTypeCode { get; set;}

    }

    public class UserType
    {
      public int  UserTypeId    {get;set;}
      public string  UserTypeName      {get;set;}
      public bool IsActive { get; set; }
    }

    public class LoginInput
    {
        public string LoginType { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }

        public bool RememberMe { get; set; } 

    }

    public class LoginOutput
    {
        public bool IsValid { get; set; }
        public bool IsPasswordExpired { get; set; }

        public Users UserDetail { get; set; }

        public Customer CustomerDetail { get; set; }

        public string ErrorMessage { get; set; }

        
    }
}
