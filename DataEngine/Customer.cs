using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Customer
    {
        public DateTime CreatedDate { get; set; }
     public int   CustomerId             {get;set;}
     public string   FirstName              {get;set;}
     public string   MiddleName             {get;set;}
     public string   LastName               {get;set;}
     public string   DisplayName            {get;set;}
     public string   ImagePath              {get;set;}
     public string   EmailId                {get;set;}
     public string   MobileNo               {get;set;}
     public string   PhoneNo                {get;set;}
     public int   BrokerId               {get;set;}
     public bool   IsEmailIdVerified      {get;set;}
     public bool   IsMobileNoVerified     {get;set;}
     public bool IsActive { get; set; }

        public string Gender { get; set; }

    public string    NationalId         {get;set;}
    public string    AccountType        {get;set;}
    public string AccountSubType { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Customer_Reference_Code { get; set; }

        public bool IsHavingMembership { get; set; }

        public List<Membership> MembershipList { get; set; }

        public string Source_Name { get; set; }

        public string Source_Type { get; set; }

        public bool IsCustomerCreatedFromCRM { get; set; }
    }
}
