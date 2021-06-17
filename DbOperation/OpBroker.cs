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
                string strBrokerCommissionXML = string.Empty;
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

                if (objBroker.lstBrokerServiceCommissionPayable != null && objBroker.lstBrokerServiceCommissionPayable.Any())
                {
                    strBrokerCommissionXML += "<Broker>";
                    foreach (BrokerServiceCommissionPayable item in objBroker.lstBrokerServiceCommissionPayable)
                    {
                        strBrokerCommissionXML += "<CommissionPayable>";
                        strBrokerCommissionXML += "<ServiceId>" + item.ServiceId + "</ServiceId>";
                        strBrokerCommissionXML += "<Commission_Paybable>" + item.Commission_Paybable.ToString() + "</Commission_Paybable>";
                        strBrokerCommissionXML += "<Commission_StartDate>" + item.Commission_StartDate.ToString() + "</Commission_StartDate>";
                        strBrokerCommissionXML += "<Commission_EndDate>" + item.Commission_EndDate.ToString() + "</Commission_EndDate>";
                        strBrokerCommissionXML += "</CommissionPayable>";
                    }
                    strBrokerCommissionXML += "</Broker>";
                }

                #endregion

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[30];
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
                objListSqlParam[13].ParameterName = "@Estimated_Business_In_A_Year";
                objListSqlParam[13].Value = objBroker.Estimated_Business_In_A_Year;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@Payment_Terms_Credit_Terms";
                objListSqlParam[14].Value = objBroker.Payment_Terms_Credit_Terms;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@BrokerServiceOptedXML";
                objListSqlParam[15].Value = strServiceOptedXML;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@Remarks";
                objListSqlParam[16].Value = objBroker.Remarks;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@BrokerDocumentsXML";
                objListSqlParam[17].Value = strBrokerDocsXML;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@Price_Option";
                objListSqlParam[18].Value = objBroker.Price_Option;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@IsActive";
                objListSqlParam[19].Value = objBroker.IsActive;

                objListSqlParam[20] = new SqlParameter();
                objListSqlParam[20].ParameterName = "@CRNumber";
                objListSqlParam[20].Value = objBroker.CRNumber;

                objListSqlParam[21] = new SqlParameter();
                objListSqlParam[21].ParameterName = "@CRExpiryDate";
                objListSqlParam[21].Value = objBroker.CRExpiryDate;

                objListSqlParam[22] = new SqlParameter();
                objListSqlParam[22].ParameterName = "@VATRegistrationNumber";
                objListSqlParam[22].Value = objBroker.VATRegistrationNumber;

                objListSqlParam[23] = new SqlParameter();
                objListSqlParam[23].ParameterName = "@Landline";
                objListSqlParam[23].Value = objBroker.Landline;

                objListSqlParam[24] = new SqlParameter();
                objListSqlParam[24].ParameterName = "@EscalationLandlineNo";
                objListSqlParam[24].Value = objBroker.EscalationLandlineNo;

                objListSqlParam[25] = new SqlParameter();
                objListSqlParam[25].ParameterName = "@DeclarationPeriod";
                objListSqlParam[25].Value = objBroker.DeclarationPeriod;

                objListSqlParam[26] = new SqlParameter();
                objListSqlParam[26].ParameterName = "@BrokerServiceCommissionXml";
                objListSqlParam[26].Value = strBrokerCommissionXML;

                objListSqlParam[27] = new SqlParameter();
                objListSqlParam[27].ParameterName = "@BranchLocation";
                objListSqlParam[27].Value = objBroker.BranchLocation;

                objListSqlParam[28] = new SqlParameter();
                objListSqlParam[28].ParameterName = "@Escalation_Person_EmailId";
                objListSqlParam[28].Value = objBroker.Escalation_Person_EmailId;

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
                DataSet ds = new DataSet();
                DataTable dtCommission = new DataTable();
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_showAllBroker", objListSqlParam);
                dt =ds!=null && ds.Tables.Count>0 ?ds.Tables[0] :new DataTable();
                dtCommission = ds != null && ds.Tables.Count > 1 ? ds.Tables[1] : new DataTable();

                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Broker
                    {
                        BrokerCode= x.Field<string>("BrokerCode"),
                        Contact_Person_Name = x.Field<string>("Contact_Person_Name"),
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
                        CreatedDate = x.Field<DateTime>("CreatedDate"),
                        Payment_Terms_Credit_Terms = x.Field<string>("Payment_Terms_Credit_Terms"),
                        IsActive = x.Field<bool>("IsActive"),
                        CRNumber = x.Field<string>("CRNumber"),
                        CRExpiryDate = x.Field<DateTime?>("CRExpiryDate"),
                        VATRegistrationNumber = x.Field<string>("VATRegistrationNumber"),
                        Landline = x.Field<string>("Landline"),
                        EscalationLandlineNo = x.Field<string>("EscalationLandlineNo"),
                        DeclarationPeriod = x.Field<int?>("DeclarationPeriod"),
                        BranchLocation = x.Field<string>("BranchLocation"),
                        lstBrokerServiceCommissionPayable = dtCommission.Rows.Count > 0 ? dtCommission.AsEnumerable().Select(y => new BrokerServiceCommissionPayable
                        {
                            BrokerId = y.Field<int>("BrokerId"),
                            Commission_EndDate = y.Field<DateTime>("Commission_EndDate"),
                            Commission_StartDate = y.Field<DateTime>("Commission_StartDate"),
                            Commission_Paybable = y.Field<decimal>("Commission_Paybable"),
                            ServiceId= y.Field<int>("ServiceId"),
                        }).ToList() : new List<BrokerServiceCommissionPayable>()
                    }).ToList();

                    output.DataList = objLst;
                    output.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                output.DataList = objLst;
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
