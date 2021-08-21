using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataEngine;
using System.Data;
using System.Data.SqlClient;
using Utility;

namespace DbOperation
{
   public class OpLogin
    {
        public LoginOutput ValidateLogin(LoginInput objLogin)
        {
            LoginOutput objLoginOutput = new LoginOutput();
            try
            {
                SqlParameter[] objListSqlParam = new SqlParameter[5];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@LoginType";
                objListSqlParam[0].Value = objLogin.LoginType;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@EmailId";
                objListSqlParam[1].Value = objLogin.EmailId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@Password";
                objListSqlParam[2].Value = objLogin.Password;

                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_ValidateUserLogin",objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objLoginOutput.IsValid = true;
                    objLoginOutput.IsPasswordExpired = false;
                    if (objLogin.LoginType == "Customer")
                    {
                        objLoginOutput.CustomerDetail = new Customer
                        {
                            CustomerId = Convert.ToInt32(dt.Rows[0]["CustomerId"]),
                            Name = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]),
                            EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                            AccountType = Convert.ToString(dt.Rows[0]["AccountType"]),
                            IsHavingMembership = Convert.ToBoolean(dt.Rows[0]["IsHavingMembership"]),
                            NationalId = Convert.ToString(dt.Rows[0]["NationalId"]),
                            Password = Convert.ToString(dt.Rows[0]["Password"])
                        };
                    }
                    else
                    {
                        objLoginOutput.UserDetail = new Users
                        {
                            UserId = Convert.ToInt32(dt.Rows[0]["UserId"]),
                            Name = Convert.ToString(dt.Rows[0]["FirstName"]) + " " + Convert.ToString(dt.Rows[0]["LastName"]),
                            EmailId = Convert.ToString(dt.Rows[0]["EmailId"]),
                            UserTypeName = Convert.ToString(dt.Rows[0]["UserTypeName"]),
                            RoleName = Convert.ToString(dt.Rows[0]["Role"]),
                            UserTypeCode = Convert.ToString(dt.Rows[0]["UserTypeCode"]),
                            UserReferenceId = Convert.ToInt32(dt.Rows[0]["UserReferenceId"]),
                           Password = Convert.ToString(dt.Rows[0]["Password"])
                        };
                    }

                   

                }
                    
            }
            catch (Exception ex)
            {
                objLoginOutput.ErrorMessage = ex.Message;
            }
            return objLoginOutput;
        }
    }
}
