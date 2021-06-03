using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;
using DataEngine;

namespace DbOperation
{
  public  class OpBroker
    {

        public MethodOutput<string> SaveBroker(Broker objBroker)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                string strServiceOptedXML = string.Empty;
                string strBrokerDocsXML = string.Empty;
                #region Generate XML For ServcieOpted
                if (!String.IsNullOrEmpty(objBroker.SelectedServiceOpted))
                {
                    strServiceOptedXML += "<Broker>";
                    foreach (string item in objBroker.SelectedServiceOpted.Split(','))
                    {
                        strServiceOptedXML += "<ServiceOpted><ServiceSubCategoryId>" + Convert.ToString(item) + "</ServiceSubCategoryId></ServiceOpted>";

                    }
                    strServiceOptedXML += "</Broker>";

                }

                if (objBroker.BrokerDocuments != null && objBroker.BrokerDocuments.Any())
                {
                    strBrokerDocsXML += "<Broker>";
                    foreach (BrokerDocument item in objBroker.BrokerDocuments)
                    {
                        strBrokerDocsXML += "<Document><DocumentPath>" + item.DocumentPath + "</DocumentPath></Document>";

                    }
                    strBrokerDocsXML += "</Broker>";

                }
                #endregion

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[22];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BrokerId";
                objListSqlParam[0].Value = objBroker.BrokerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@Broker_Type";
                objListSqlParam[1].Value = objBroker.Broker_Type;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@Broker_Name";
                objListSqlParam[2].Value = objBroker.Broker_Name;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@Office_Location_Address";
                objListSqlParam[3].Value = objBroker.Office_Location_Address;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@EmailId";
                objListSqlParam[4].Value = objBroker.EmailId;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@MobileNo";
                objListSqlParam[5].Value = objBroker.MobileNo;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@Office_Postal_Address";
                objListSqlParam[6].Value = objBroker.Office_Postal_Address;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@Contact_Person_Name";
                objListSqlParam[7].Value = objBroker.Contact_Person_Name;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@Contact_Person_Contact_No";
                objListSqlParam[8].Value = objBroker.Contact_Person_Contact_No;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@Escalation_Person_Name";
                objListSqlParam[9].Value = objBroker.Escalation_Person_Name;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@Escalation_Person_Contact_No";
                objListSqlParam[10].Value = objBroker.Escalation_Person_Contact_No;

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@Agreement_Start_Date";
                objListSqlParam[11].Value = objBroker.Agreement_Start_Date;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@Agreement_End_Date";
                objListSqlParam[12].Value = objBroker.Agreement_End_Date;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@Commission_Paybable";
                objListSqlParam[13].Value = objBroker.Commission_Paybable;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@Estimated_Business_In_A_Year";
                objListSqlParam[14].Value = objBroker.Estimated_Business_In_A_Year;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@Payment_Terms_Credit_Terms";
                objListSqlParam[15].Value = objBroker.Payment_Terms_Credit_Terms;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@BrokerServiceOptedXML";
                objListSqlParam[16].Value = strServiceOptedXML;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@Remarks";
                objListSqlParam[17].Value = objBroker.Remarks;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@BrokerDocumentsXML";
                objListSqlParam[18].Value = strBrokerDocsXML;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@Price_Option";
                objListSqlParam[19].Value = objBroker.Price_Option;

                objListSqlParam[20] = new SqlParameter();
                objListSqlParam[20].ParameterName = "@IsActive";
                objListSqlParam[20].Value = objBroker.IsActive;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveBroker", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["Error"]);

                }
            }
            catch (Exception ex)
            {

                output.ErrorMessage = ex.Message;
            }

            return output;

        }

        public MethodOutput<Broker> GetAllBroker(int BrokerId,string BrokerType)
        {
            MethodOutput<Broker> output = new MethodOutput<Broker>();
            List<Broker> objLst = new List<Broker>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[2];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BrokerId";
                objListSqlParam[0].Value = BrokerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@BrokerType";
                objListSqlParam[1].Value = BrokerType;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_showAllBroker", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Broker
                    {
                Contact_Person_Name         = x.Field<string>("Contact_Person_Name"),
                        Contact_Person_Contact_No = x.Field<string>("Contact_Person_Contact_No"),
                        Remarks = x.Field<string>("Remarks"),
                        EmailId = x.Field<string>("EmailId"),
                        Escalation_Person_Contact_No = x.Field<string>("Escalation_Person_Contact_No"),
                        Escalation_Person_Name = x.Field<string>("Escalation_Person_Name"),
                        Broker_Name = x.Field<string>("Broker_Name"),
                        Broker_Type = x.Field<string>("Broker_Type"),
                        Estimated_Business_In_A_Year = x.Field<string>("Estimated_Business_In_A_Year"),
                        MobileNo = x.Field<string>("MobileNo"),
                        Office_Location_Address = x.Field<string>("Office_Location_Address"),
                        Office_Postal_Address = x.Field<string>("Office_Postal_Address"),
                        SelectedServiceOpted = x.Field<string>("SelectedServiceOpted"),
                        Agreement_Start_Date = x.Field<DateTime>("Agreement_Start_Date"),
                        Agreement_End_Date = x.Field<DateTime>("Agreement_End_Date"),
                        BrokerId = x.Field<int>("BrokerId"),
                        BrokerDocuments = !String.IsNullOrEmpty(x.Field<string>("BrokerDocuments")) && x.Field<string>("BrokerDocuments").IndexOf(',') != -1 ? x.Field<string>("BrokerDocuments").Split(',').Select(y => new BrokerDocument
                        {
                            DocumentPath = y,
                            BrokerId = x.Field<int>("BrokerId"),
                        }).ToList() : null,
                        Price_Option = x.Field<string>("Price_Option"),
                        Commission_Paybable= x.Field<decimal>("Commission_Paybable"),
                        CreatedDate= x.Field<DateTime>("CreatedDate"),
                        Payment_Terms_Credit_Terms= x.Field<string>("Payment_Terms_Credit_Terms"),
                        IsActive= x.Field<bool>("IsActive")
                    }).ToList();

                    output.Data = objLst;
                    output.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                output.Data = objLst;
                output.ErrorMessage = ex.Message;
            }
            return output;
        }

        public MethodOutput<string> DeleteBroker(int BrokerId)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@BrokerId";
                objListSqlParam[0].Value = BrokerId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_DeleteBroker", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["Error"]);

                }
            }
            catch (Exception ex)
            {

                output.ErrorMessage = ex.Message;
            }




            return output;
        }
    }
}
