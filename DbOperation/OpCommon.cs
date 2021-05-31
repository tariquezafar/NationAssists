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
   public class OpCommon
    {
        public MethodOutput<Service> BindService()
        {
            MethodOutput<Service> objOutput = new MethodOutput<Service>();
            List<Service> objLstService = new List<Service>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString,CommandType.StoredProcedure, "usp_GetAllService").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new Service {
                        ServiceId=x.Field<int>("ServiceId"),
                        ServiceName= x.Field<string>("ServiceName"),
                        ServiceCode = x.Field<string>("ServiceCode"),
                        IsActive= x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.Data = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }

        public MethodOutput<Role> BindRole()
        {
            MethodOutput<Role> objOutput = new MethodOutput<Role>();
            List<Role> objLstRole = new List<Role>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllRole").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstRole = dt.AsEnumerable().Select(x => new Role
                    {
                        RoleId = x.Field<int>("RoleId"),
                        RoleName = x.Field<string>("Role"),
                        IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.Data = objLstRole;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;
            }
            return objOutput;
        }

        public MethodOutput<Branch> BindBranch()
        {
            MethodOutput<Branch> objOutput = new MethodOutput<Branch>();
            List<Branch> objLstBranch = new List<Branch>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllBranch").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstBranch = dt.AsEnumerable().Select(x => new Branch
                    {
                        BranchId = x.Field<int>("BranchId"),
                        CompanyId = x.Field<int>("CompanyId"),
                        BranchName = x.Field<string>("BranchName"),
                        IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.Data = objLstBranch;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;
            }
            return objOutput;
        }

        public MethodOutput<Users> BindUser()
        {
            MethodOutput<Users> objOutput = new MethodOutput<Users>();
            List<Users> objLstUser = new List<Users>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetUsers").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstUser = dt.AsEnumerable().Select(x => new Users
                    {
                        UserId = x.Field<int>("UserId"),
                        Name = x.Field<string>("Name"),
                        PhoneNo = x.Field<string>("PhoneNo"),
                        EmailId = x.Field<string>("EmailId")
                        //IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.Data = objLstUser;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }

        public MethodOutput<ServiceSubCategory> BindServiceSubCategory()
        {
            MethodOutput<ServiceSubCategory> objOutput = new MethodOutput<ServiceSubCategory>();
            List<ServiceSubCategory> objLstServiceSubCategory = new List<ServiceSubCategory>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceCategory").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstServiceSubCategory = dt.AsEnumerable().Select(x => new ServiceSubCategory
                    {
                        ServiceId = x.Field<int>("ServiceId"),
                        ServiceName = x.Field<string>("SERVICE_NAME"),
                       ServiceSubCategoryId = x.Field<int>("ServiceSubCategoryId"),
                       Name = x.Field<string>("SUB_CAT_NAME"),
                        ServiceCode= x.Field<string>("ServiceCode"),

                        IsActive =false
                    }).ToList();

                    objOutput.Data = objLstServiceSubCategory;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }


        public MethodOutput<ServiceProvider> BindServiceProvider()
        {
            MethodOutput<ServiceProvider> objOutput = new MethodOutput<ServiceProvider>();
            List<ServiceProvider> objLstServiceProvider = new List<ServiceProvider>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceProvider").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstServiceProvider = dt.AsEnumerable().Select(x => new ServiceProvider
                    {
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        FirstName = x.Field<string>("SERVICE_PROVIDER_NAME")
                    }).ToList();

                    objOutput.Data = objLstServiceProvider;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;
        }


        public MethodOutput<Service> BindServiceOptedByServiceProviderId(int? ServiceProviderId)
        {
            MethodOutput<Service> objOutput = new MethodOutput<Service>();
            List<Service> objLstService = new List<Service>();
            try
            {
                if (ServiceProviderId != null)
                {
                    DataTable dt = new DataTable();
                    SqlParameter[] objListSqlParam = new SqlParameter[1];
                    objListSqlParam[0] = new SqlParameter();
                    objListSqlParam[0].ParameterName = "@ServiceProviderId";
                    objListSqlParam[0].Value = ServiceProviderId;

                    dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetServiceOptedByServiceProviderId", objListSqlParam).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        objLstService = dt.AsEnumerable().Select(x => new Service
                        {
                            ServiceId = x.Field<int>("ServiceId"),
                            ServiceName = x.Field<string>("ServiceName"),

                        }).ToList();

                        objOutput.Data = objLstService;
                        objOutput.ErrorMessage = string.Empty;
                    }
                }
                else
                {
                    objOutput.Data = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }
    }
}
