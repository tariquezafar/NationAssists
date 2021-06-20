using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Customer
    {
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
    }
}
