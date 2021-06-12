using DataEngine;
using DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ICardPrinter.Areas.Admin.Models
{
    public class mBroker
    {
        public int BrokerId { get; set; }
        public string Broker_Type { get; set; }
        public string Broker_Name { get; set; }
        public string Office_Location_Address { get; set; }
        public string Office_Postal_Address { get; set; }
        public string Contact_Person_Name { get; set; }
        public string Contact_Person_Contact_No { get; set; }
        public string Escalation_Person_Name { get; set; }
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



        public string SelectedServiceOpted { get; set; }
        public List<BrokerDocument> BrokerDocuments { get; set; }


        public List<BrokerOptedService> BrokerOptedServices { get; set; }

        public List<Broker> BrokerList { get; set; }

        public List<BrokerServiceCommissionPayable> BrokerServiceCommissionPayableList { get; set; }
        public string BranchLocation { get; set; }
        public List<BrokerOptedService> GetAllServices()
        {
            CommonServices objCS = new CommonServices();
            List<BrokerOptedService> objLstServices = new List<BrokerOptedService>();
            MethodOutput<ServiceSubCategory> output = new MethodOutput<ServiceSubCategory>();
            output = objCS.BindServiceSubCategory();
            objLstServices = output.Data.Select(x => new BrokerOptedService
            {

                Name = x.ServiceName,
                IsActive = false,
                ServiceId = x.ServiceId,
                SubCategoryId = x.ServiceSubCategoryId,
                SubCategoryName = x.Name,
                ServiceCode = x.ServiceCode

            }).ToList();

            return objLstServices;
        }
    }
}