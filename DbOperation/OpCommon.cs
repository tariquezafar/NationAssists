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

                    objOutput.DataList = objLstService;
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

                    objOutput.DataList = objLstRole;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;
            }
            return objOutput;
        }

        public MethodOutput<UserType> BindUserType()
        {
            MethodOutput<UserType> objOutput = new MethodOutput<UserType>();
            List<UserType> objLstRole = new List<UserType>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllUserType").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstRole = dt.AsEnumerable().Select(x => new UserType
                    {
                        UserTypeId = x.Field<int>("UserTypeId"),
                        UserTypeName = x.Field<string>("UserType"),
                        IsActive = x.Field<bool>("IsActive"),
                        UserTypeCode= x.Field<string>("UserTypeCode")
                    }).ToList();

                    objOutput.DataList = objLstRole;
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

                    objOutput.DataList = objLstBranch;
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

                    objOutput.DataList = objLstUser;
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

                    objOutput.DataList = objLstServiceSubCategory;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }


        public MethodOutput<ServiceProvider> BindServiceProvider(string UserType="")
        {
            MethodOutput<ServiceProvider> objOutput = new MethodOutput<ServiceProvider>();
            List<ServiceProvider> objLstServiceProvider = new List<ServiceProvider>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserType";
                objListSqlParam[0].Value = UserType;   
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceProvider", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstServiceProvider = dt.AsEnumerable().Select(x => new ServiceProvider
                    {
                        ServiceProviderId = x.Field<int>("ServiceProviderId"),
                        FirstName = x.Field<string>("SERVICE_PROVIDER_NAME")
                    }).ToList();

                    objOutput.DataList = objLstServiceProvider;
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

                        objOutput.DataList = objLstService;
                        objOutput.ErrorMessage = string.Empty;
                    }
                }
                else
                {
                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }


        public MethodOutput<Service> BindServiceOptedByBrokerId(int? BrokerId)
        {
            MethodOutput<Service> objOutput = new MethodOutput<Service>();
            List<Service> objLstService = new List<Service>();
            try
            {
                if (BrokerId != null)
                {
                    DataTable dt = new DataTable();
                    SqlParameter[] objListSqlParam = new SqlParameter[1];
                    objListSqlParam[0] = new SqlParameter();
                    objListSqlParam[0].ParameterName = "@BrokerId";
                    objListSqlParam[0].Value = BrokerId;

                    dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetServiceOptedByBrokerId", objListSqlParam).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        objLstService = dt.AsEnumerable().Select(x => new Service
                        {
                            ServiceId = x.Field<int>("ServiceId"),
                            ServiceName = x.Field<string>("ServiceName"),
                            ServiceCode=x.Field<string>("ServiceCode"),

                        }).ToList();

                        objOutput.DataList = objLstService;
                        objOutput.ErrorMessage = string.Empty;
                    }
                }
                else
                {
                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;


        }

        public MethodOutput<Country> BindCountry()
        {
            MethodOutput<Country> objOutput = new MethodOutput<Country>();
            List<Country> objLstCountry = new List<Country>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllCountry").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstCountry = dt.AsEnumerable().Select(x => new Country
                    {
                        CountryId = x.Field<int>("CountryId"),
                        Name = x.Field<string>("CountryName"),
                        IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.DataList = objLstCountry;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;
            }
            return objOutput;
        }

        public MethodOutput<Governotes> BindGovernotes(int CountryId)
        {
            MethodOutput<Governotes> objOutput = new MethodOutput<Governotes>();
            List<Governotes> objLstService = new List<Governotes>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CountryId";
                objListSqlParam[0].Value = CountryId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindGovernotes", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new Governotes
                    {
                        GovernorateId = x.Field<int>("GovernorateId"),
                        GovernoratesName = x.Field<string>("GovernoratesName"),
                        
              }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }

            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;

        }


        public MethodOutput<Place> BindPlaces(int GovernotesId)
        {
            MethodOutput<Place> objOutput = new MethodOutput<Place>();
            List<Place> objLstService = new List<Place>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@GovernotesId";
                objListSqlParam[0].Value = GovernotesId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindPlaces", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new Place
                    {
                        PlaceId = x.Field<int>("PlaceId"),
                        Place_Name = x.Field<string>("Place_Name"),

                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }

            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;

        }


        public MethodOutput<Block> BindBlockCode(int PlaceId)
        {
            MethodOutput<Block> objOutput = new MethodOutput<Block>();
            List<Block> objLstService = new List<Block>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@PlaceId";
                objListSqlParam[0].Value = PlaceId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_BindBlockCode", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstService = dt.AsEnumerable().Select(x => new Block
                    {
                        BlockId = x.Field<int>("BlockId"),
                        Block_Code = x.Field<string>("Block_Code"),

                    }).ToList();

                    objOutput.DataList = objLstService;
                    objOutput.ErrorMessage = string.Empty;
                }
            }

            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;


            }
            return objOutput;

        }


        public MethodOutput<ServiceRequestStatus> BindServiceRequestStatus()
        {
            MethodOutput<ServiceRequestStatus> objOutput = new MethodOutput<ServiceRequestStatus>();
            List<ServiceRequestStatus> objLstCountry = new List<ServiceRequestStatus>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceRequestStatus").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstCountry = dt.AsEnumerable().Select(x => new ServiceRequestStatus
                    {
                        ServiceRequestStatusId = x.Field<int>("ServiceRequestStatusId"),
                        Status_Name = x.Field<string>("Status_Name"),
                        IsActive = x.Field<bool>("IsActive")
                    }).ToList();

                    objOutput.DataList = objLstCountry;
                    objOutput.ErrorMessage = string.Empty;
                }
            }
            catch (Exception ex)
            {
                objOutput.ErrorMessage = ex.Message;
            }
            return objOutput;
        }


        public MethodOutput<ServiceEnrollmentStatus> BindServiceEnrollmentStatus()
        {
            MethodOutput<ServiceEnrollmentStatus> objOutput = new MethodOutput<ServiceEnrollmentStatus>();
            List<ServiceEnrollmentStatus> objLstEnrollmentStatus = new List<ServiceEnrollmentStatus>();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetAllServiceEnrollmentStatus").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLstEnrollmentStatus = dt.AsEnumerable().Select(x => new ServiceEnrollmentStatus
                    {
                        ServiceEnrollmentStatusId = x.Field<int>("ServiceEnrollmentStatusId"),
                        Status_Name = x.Field<string>("Status_Name")
                    }).ToList();

                    objOutput.DataList = objLstEnrollmentStatus;
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
