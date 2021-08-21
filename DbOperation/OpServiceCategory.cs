using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using Utility;
using System.Data.SqlClient;
using System.Data;

namespace DbOperation
{
    public class OpServiceCategory
    {
        public MethodOutput<string> SaveServiceCategory(ServiceSubCategory objServiceCategory)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {


                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[5];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceSubCategoryId";
                objListSqlParam[0].Value = objServiceCategory.ServiceSubCategoryId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceId";
                objListSqlParam[1].Value = objServiceCategory.ServiceId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@Name";
                objListSqlParam[2].Value = objServiceCategory.Name;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@IsActive";
                objListSqlParam[3].Value = objServiceCategory.IsActive;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveServiceSubCategroy", objListSqlParam).Tables[0];
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

        public MethodOutput<ServiceSubCategory> GetServiceSubCategoryByServiceId(int ServiceId)
        {
            MethodOutput<ServiceSubCategory> output = new MethodOutput<ServiceSubCategory>();
            List<ServiceSubCategory> objLst = new List<ServiceSubCategory>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceId";
                objListSqlParam[0].Value = ServiceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceCategoryByServiceId", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new ServiceSubCategory
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                        Name = x.Field<string>("Name"),
                        IsActive = x.Field<bool>("IsActive"),
                        ServiceName = x.Field<string>("Service_Name")
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

        public MethodOutput<string> DeleteServiceSubCategory(int ServiceId)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceCategoryId";
                objListSqlParam[0].Value = ServiceId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_DeleteServiceSubCategory", objListSqlParam).Tables[0];
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


        public MethodOutput<string> SaveService(Service objService)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {


                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[5];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ServiceId";
                objListSqlParam[0].Value = objService.ServiceId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@ServiceCode";
                objListSqlParam[1].Value = objService.ServiceCode;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@ServiceName";
                objListSqlParam[2].Value = objService.ServiceName;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@IsActive";
                objListSqlParam[3].Value = objService.IsActive;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveService", objListSqlParam).Tables[0];
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

        public MethodOutput<Service> GetAllServices()
        {
            MethodOutput<Service> output = new MethodOutput<Service>();
            List<Service> objLst = new List<Service>();
            try
            {

                DataTable dt = new DataTable();
               
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServices").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Service
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceCode = x.Field<string>("ServiceCode"),
                        IsActive = x.Field<bool>("IsActive"),
                        ServiceName = x.Field<string>("ServiceName"),
                        PackageCode = x.Field<string>("PackageCode"),
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
