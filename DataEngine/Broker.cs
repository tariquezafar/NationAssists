using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
    public class Broker
    {
        public int BrokerId { get; set; }
        public string Broker_Type { get; set; }
        public string Broker_Name { get; set; }
        public string Office_Location_Address { get; set; }
        public string Office_Postal_Address { get; set; }
        public string Contact_Person_Name { get; set; }
        public string Contact_Person_Contact_No { get; set; }
        public string Escalation_Person_Name { get; set; }

        public string Escalation_Person_EmailId { get; set; }
        public string Escalation_Person_Contact_No { get; set; }
        public DateTime Agreement_Start_Date { get; set; }
        public DateTime Agreement_End_Date { get; set; }
        public decimal Commission_Paybable { get; set; }
        public string Estimated_Business_In_A_Year { get; set; }
        public string Payment_Terms_Credit_Terms { get; set; }
        public string Remarks { get; set; }
        public string Price_Option { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }


     public string    CRNumber                  {get;set;}
     public DateTime ?   CRExpiryDate              {get;set;}
     public string    VATRegistrationNumber     {get;set;}
     public string    Landline                  {get;set;}
     public string    EscalationLandlineNo      {get;set;}
     public int? DeclarationPeriod { get; set; }
        public string SelectedServiceOpted { get; set; }
        public List<BrokerDocument> BrokerDocuments { get; set; }


        public List<BrokerOptedService> BrokerOptedServices { get; set; }
        
        public string BrokerServiceCommissionPayable { get; set; }

        public List<BrokerServiceCommissionPayable> lstBrokerServiceCommissionPayable { get; set; }

        public string BranchLocation { get; set; }

        public string BrokerCode { get; set; }

        public string Password { get; set; }

        public string Agg_Start_Date { get; set; }
        public string Agg_End_Date { get; set; }
        public string CPR_Expiry_Date { get; set; }
    }


    public class BrokerDocument
    {
      public int  BrokerDocumentId      {get;set;}
                                        
      public int  BrokerId              {get;set;}
      public string  DocumentPath          {get;set;}
      public DateTime  CreatedDate           {get;set;}
      public bool IsActive { get; set; }
    }

    public class BrokerOptedService
    {
        public int BrokerServiceOptedId { get; set; }
        public int BrokerId { get; set; }
        public int ServiceId { get; set; }
        public int SubCategoryId { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        public string SubCategoryName { get; set; }

        public string ServiceCode { get; set; }

     
    }

    public class BrokerServiceCommissionPayable

    {
        public int BrokerServiceCommissionPayableId { get; set; }
        public int BrokerId { get; set; }
        public int ServiceId { get; set; }
        public decimal Commission_Paybable { get; set; }
        public DateTime Commission_StartDate { get; set; }
        public DateTime Commission_EndDate { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Commission_Start_Date { get; set; }
        public string Commission_End_Date { get; set; }
    }


}
