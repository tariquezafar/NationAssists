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
    public class OpServicePrice
    {
        public MethodOutput<string> SaveServicePrices(ServicePrice objPrice)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[5];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceId";
                objListSqlParam[0].Value = objPrice.ServiceId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@Price";
                objListSqlParam[1].Value = objPrice.Prices;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@StartDate";
                objListSqlParam[2].Value = objPrice.StartDate;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@EndDate";
                objListSqlParam[3].Value = objPrice.EndDate;

          

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SavePrice", objListSqlParam).Tables[0];
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

        public MethodOutput<ServicePrice> GetServicePricesByServiceId(int ServiceId)
        {
            MethodOutput<ServicePrice> output = new MethodOutput<ServicePrice>();
            List<ServicePrice> objLst = new List<ServicePrice>();

            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceId";
                objListSqlParam[0].Value = ServiceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetServciePrices", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new ServicePrice
                    {
                        ServicePriceId = x.Field<int>("ServicePriceId"),
                        ServiceName= x.Field<string>("ServiceName"),
                        Prices = x.Field<decimal>("Price"),
                        StartDate = x.Field<DateTime>("StartDate"),
                        EndDate = x.Field<DateTime>("EndDate"),
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


        public MethodOutput<ServicePrice> GetAllPackages()
        {
            MethodOutput<ServicePrice> output = new MethodOutput<ServicePrice>();
            List<ServicePrice> objLst = new List<ServicePrice>();

            try
            {
                DataTable dt = new DataTable();
               
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ShowAllPackage").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new ServicePrice
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceCode = x.Field<string>("ServiceCode"),
                        ServiceName = x.Field<string>("ServiceName"),
                        Prices = x.Field<decimal>("Price"),
                        SubCategoryName = x.Field<string>("subCategoryName"),

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

    }
}
