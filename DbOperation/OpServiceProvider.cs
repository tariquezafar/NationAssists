using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DbOperation
{
   public class OpServiceProvider
    {
        public MethodOutput<string> SaveServiceProvider(ServiceProvider objServiceProvider)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                string strServiceOptedXML = string.Empty;
                string strServiceProviderDocsXML = string.Empty;
                #region Generate XML For ServcieOpted
                if (!String.IsNullOrEmpty(objServiceProvider.SelectedServiceOpted))
                {
                    strServiceOptedXML += "<ServiceProvider>";
                    foreach (string item in objServiceProvider.SelectedServiceOpted.Split(','))
                    {
                        strServiceOptedXML += "<ServiceOpted><ServiceSubCategoryId>" + Convert.ToString( item) + "</ServiceSubCategoryId></ServiceOpted>";

                    }
                    strServiceOptedXML += "</ServiceProvider>";

                }

                if (objServiceProvider.ServiceProviderDocuments !=null  && objServiceProvider.ServiceProviderDocuments.Any())
                {
                    strServiceProviderDocsXML += "<ServiceProvider>";
                    foreach (ServiceProviderDocument item in objServiceProvider.ServiceProviderDocuments)
                    {
                        strServiceProviderDocsXML += "<Document><DocumentPath>" + item.DocumentPath + "</DocumentPath></Document>";

                    }
                    strServiceProviderDocsXML += "</ServiceProvider>";

                }
                #endregion

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[20];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = objServiceProvider.ServiceProviderId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@FirstName";
                objListSqlParam[1].Value = objServiceProvider.FirstName;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@MiddleName";
                objListSqlParam[2].Value = objServiceProvider.MiddleName;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@LastName";
                objListSqlParam[3].Value = objServiceProvider.LastName;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@EmailId";
                objListSqlParam[4].Value = objServiceProvider.EmailId;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@MobileNo";
                objListSqlParam[5].Value = objServiceProvider.MobileNo;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@PhoneNo";
                objListSqlParam[6].Value = objServiceProvider.PhoneNo;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@CRNumber";
                objListSqlParam[7].Value = objServiceProvider.CRNumber;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@OfficeLocationAddress";
                objListSqlParam[8].Value = objServiceProvider.OfficeLocationAddress;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@OfficePostalAddress";
                objListSqlParam[9].Value = objServiceProvider.PostalLocationAddress;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@ContactPersonName";
                objListSqlParam[10].Value = objServiceProvider.ContactPersonName;

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@ContactPersonContactDetail";
                objListSqlParam[11].Value = objServiceProvider.ContactPersonNo;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@EscalationContactDetail";
                objListSqlParam[12].Value = objServiceProvider.EscalationPersonName;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@ServiceProviderAgreementFromDate";
                objListSqlParam[13].Value = objServiceProvider.ServiceProviderAgreementFromDate;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@ServiceProviderAgreementEndDate";
                objListSqlParam[14].Value = objServiceProvider.ServiceProviderAgreementToDate;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@EscalationPersonName";
                objListSqlParam[15].Value = objServiceProvider.EscalationPersonName;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@ServiceProviderServiceOptedXml";
                objListSqlParam[16].Value = strServiceOptedXML;

                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@Password";
                objListSqlParam[17].Value = objServiceProvider.Password;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@ServiceProviderDocumentsXML";
                objListSqlParam[18].Value = strServiceProviderDocsXML;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@PricingOption";
                objListSqlParam[19].Value = objServiceProvider.PriceOption;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceProvider", objListSqlParam).Tables[0];
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


        public MethodOutput<ServiceProvider> GetAllServiceProvider(int ServiceProviderId)
        {
            MethodOutput<ServiceProvider> output = new MethodOutput<ServiceProvider>();
            List<ServiceProvider> objLst = new List<ServiceProvider>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = ServiceProviderId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_showAllServiceProvider", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new ServiceProvider
                    {
                     ContactPersonName = x.Field<string>("ContactPersonName"),
                        CRNumber = x.Field<string>("CRNumber"),
                        ContactPersonNo = x.Field<string>("ContactPersonContactDetail"),
                        EmailId = x.Field<string>("EmailId"),
                        EscalationPersonContactNo = x.Field<string>("EscalationContactDetail"),
                        EscalationPersonName = x.Field<string>("EscalationPersonName"),
                        FirstName = x.Field<string>("FirstName"),
                        LastName = x.Field<string>("LastName"),
                        MiddleName = x.Field<string>("MiddleName"),
                        MobileNo = x.Field<string>("MobileNo"),
                        OfficeLocationAddress = x.Field<string>("OfficeLocationAddress"),
                        PhoneNo = x.Field<string>("PhoneNo"),
                        SelectedServiceOpted = x.Field<string>("SelectedServiceOpted"),
                        ServiceProviderAgreementFromDate =x.Field<DateTime>("ServiceProviderAgreementFromDate"),
                        ServiceProviderAgreementToDate = x.Field<DateTime>("ServiceProviderAgreementEndDate"),
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        ServiceProviderDocuments =!String.IsNullOrEmpty(x.Field<string>("ServiceProviderDocuments")) && x.Field<string>("ServiceProviderDocuments").IndexOf(',')!=-1? x.Field<string>("ServiceProviderDocuments").Split(',').Select(y=>new ServiceProviderDocument { 
                        DocumentPath=y,
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        }).ToList():null,
                        PriceOption= x.Field<string>("Pricing_option")
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

        public MethodOutput<string> DeleteServiceProvider(int ServiceProviderId)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = ServiceProviderId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_DeleteServiceProvider", objListSqlParam).Tables[0];
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
