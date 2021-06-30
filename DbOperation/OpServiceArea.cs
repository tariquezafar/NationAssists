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
   public  class OpServiceArea
    {
        public MethodOutput<string> SaveServiceAreaMapping(ServiceProviderServiceArea objServiceArea)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                string strServiceAreaXML = string.Empty;
                if (objServiceArea.ServiceAreas != null && objServiceArea.ServiceAreas.Any())
                {
                    strServiceAreaXML += "<ServiceProvider>";
                    foreach (ServiceProviderServiceArea item in objServiceArea.ServiceAreas)
                    {
                        strServiceAreaXML += "<ServiceArea>";
                        strServiceAreaXML += "<ServiceProviderId>" + item.ServiceProviderId + "</ServiceProviderId>";
                        strServiceAreaXML += "<ServiceId>" + item.ServiceId + "</ServiceId>";
                        strServiceAreaXML += "<ServiceSubCategoryId>" + item.ServiceSubCategoryId + "</ServiceSubCategoryId>";
                        strServiceAreaXML += "<CountryId>" + item.CountryId + "</CountryId>";
                        strServiceAreaXML += "<GovernotesId>" + item.GovernotesId + "</GovernotesId>";
                        strServiceAreaXML += "<PlaceId>" + item.PlaceId + "</PlaceId>";
                        strServiceAreaXML += "<PinCode>" + item.PinCode + "</PinCode>";
                        strServiceAreaXML += "<BlockCode>" + item.BlockId + "</BlockCode>";
                        strServiceAreaXML += "</ServiceArea>";

                    }
                    strServiceAreaXML += "</ServiceProvider>";
                }

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[5];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = objServiceArea.ServiceProviderId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = objServiceArea.ServiceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@ServiceSubCategoryId";
                objListSqlParam[2].Value = objServiceArea.ServiceSubCategoryId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@ServiceAreaXml";
                objListSqlParam[3].Value = strServiceAreaXML;

                

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceProviderServiceArea", objListSqlParam).Tables[0];
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

        public MethodOutput<ServiceProviderServiceArea> GetAllServiceArea(int ServiceProviderId,int ServiceId,int SubCategoryId)
        {
            MethodOutput<ServiceProviderServiceArea> output = new MethodOutput<ServiceProviderServiceArea>();
            List<ServiceProviderServiceArea> objLst = new List<ServiceProviderServiceArea>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[4];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceProviderId";
                objListSqlParam[0].Value = ServiceProviderId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = ServiceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@ServiceSubCategoryId";
                objListSqlParam[2].Value = SubCategoryId;

                DataSet ds = new DataSet();
                
                ds = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceArea", objListSqlParam);
                dt = ds != null && ds.Tables.Count > 0 ? ds.Tables[0] : new DataTable();
                

                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new ServiceProviderServiceArea
                    {
                        ServiceProviderServiceAreaId= x.Field<Int64>("ServiceProviderServiceAddressId"),
                        CountryId = x.Field<int>("CountryId"),
                        BlockId = x.Field<int?>("BlockCode"),
                        CountryName = x.Field<string>("CountryName"),
                        GovernotesId = x.Field<int?>("GovernotesId"),
                        GovernotesName = x.Field<string>("GovernoratesName"),
                        PlaceName = x.Field<string>("Place_Name"),
                        PinCode = x.Field<string>("PinCode"),
                        PlaceId = x.Field<int?>("PlaceId"),
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceName = x.Field<string>("ServiceName"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        SubCategoryName = x.Field<string>("SubCategegoryName")
                       
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
    }
}
