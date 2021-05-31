using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEngine
{
   public class Broker
    {
     public string   BrokerId                       {get;set;}
     public string   Broker_Type                    {get;set;}
     public string   Broker_Name                    {get;set;}
     public string   Office_Location_Address        {get;set;}
     public string   Office_Postal_Address          {get;set;}
     public string   Contact_Person_Name            {get;set;}
     public string   Contact_Person_Contact_No      {get;set;}
     public string   Escalation_Person_Name         {get;set;}
     public string   Escalation_Person_Contact_No   {get;set;}
     public string   Agreement_Start_Date           {get;set;}
     public string   Agreement_End_Date             {get;set;}
     public string   Commission_Paybable            {get;set;}
     public string   Estimated_Business_In_A_Year   {get;set;}
     public string   Payment_Terms_Credit_Terms     {get;set;}
     public string   Remarks                        {get;set;}
     public string   Price_Option                   {get;set;}
     public string   IsActive                       {get;set;}
     public string   CreatedDate                    {get;set;}
     public string   MobileNo                       {get;set;}
     public string EmailId { get; set; }


    }


    public class BrokerDocument
    {
      public int  BrokerDocumentId

      public int  BrokerId
      public int  DocumentPath
      public int  CreatedDate
      public int  IsActive
    }
}
