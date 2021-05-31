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
    public class OpUsers
    {
        public MethodOutput<string> SaveUsers(Users objUsers)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[15];

                //objListSqlParam[0] = new SqlParameter();
                //objListSqlParam[0].ParameterName = "@UserId";
                //objListSqlParam[0].Value = objUsers.UserId;

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@FirstName";
                objListSqlParam[0].Value = objUsers.Name;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@PhoneNo";
                objListSqlParam[1].Value = objUsers.PhoneNo;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@EmailId";
                objListSqlParam[2].Value = objUsers.EmailId;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@Password";
                objListSqlParam[3].Value = objUsers.Password;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@RoleId";
                objListSqlParam[4].Value = objUsers.RoleId;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@BranchId";
                objListSqlParam[5].Value = objUsers.BranchId;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@UserExpiryDate";
                objListSqlParam[6].Value = objUsers.IsActive;

                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@IsActive";
                objListSqlParam[7].Value = objUsers.IsActive;

                objListSqlParam[8] = new SqlParameter();
                objListSqlParam[8].ParameterName = "@UserId";
                objListSqlParam[8].Value = objUsers.UserId;

                objListSqlParam[9] = new SqlParameter();
                objListSqlParam[9].ParameterName = "@UserTypeId";
                objListSqlParam[9].Value = objUsers.UserTypeId;

                objListSqlParam[10] = new SqlParameter();
                objListSqlParam[10].ParameterName = "@MobileNo";
                objListSqlParam[10].Value = objUsers.MobileNo;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveUsers", objListSqlParam).Tables[0];
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

        public MethodOutput<Users> GetUsers(int UserId)
        {
            MethodOutput<Users> output = new MethodOutput<Users>();
            List<Users> objLst = new List<Users>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserId";
                objListSqlParam[0].Value = UserId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetUsers", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLst = dt.AsEnumerable().Select(x => new Users
                    {
                        UserId = x.Field<int>("UserId"),
                        Name = x.Field<string>("Name"),
                        PhoneNo = x.Field<string>("PhoneNo"),
                        EmailId = x.Field<string>("EmailId"),
                        RoleId = x.Field<int>("RoleId"),
                        BranchId = x.Field<int>("BranchId"),
                        UserExpiryDate = x.Field<DateTime>("UserExpiryDate"),
                        IsActive = x.Field<bool>("IsActive"),
                        UserTypeName = x.Field<string>("UserType"),
                        Role = x.Field<string>("Role"),
                        BranchName= x.Field<string>("BranchName"),
                        MobileNo= x.Field<string>("MobileNo"),
                        UserTypeId= x.Field<int>("UserTypeId"),
                        Password=x.Field<string>("Password")
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

        public MethodOutput<string> DeleteUsers(int UserId)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserId";
                objListSqlParam[0].Value = UserId;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_DeleteUsers", objListSqlParam).Tables[0];
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
