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
                SqlParameter[] objListSqlParam = new SqlParameter[25];

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

                objListSqlParam[11] = new SqlParameter();
                objListSqlParam[11].ParameterName = "@UserReferenceId";
                objListSqlParam[11].Value = objUsers.UserReferenceId;

                objListSqlParam[12] = new SqlParameter();
                objListSqlParam[12].ParameterName = "@CPRNumber";
                objListSqlParam[12].Value = objUsers.CPRNumber;

                objListSqlParam[13] = new SqlParameter();
                objListSqlParam[13].ParameterName = "@CPRExpiryDate";
                objListSqlParam[13].Value = objUsers.CPRExpiryDate;

                objListSqlParam[14] = new SqlParameter();
                objListSqlParam[14].ParameterName = "@PassportNumber";
                objListSqlParam[14].Value = objUsers.PassportNumber;

                objListSqlParam[15] = new SqlParameter();
                objListSqlParam[15].ParameterName = "@PassportExpiryDate";
                objListSqlParam[15].Value = objUsers.PassportExpiryDate;

                objListSqlParam[16] = new SqlParameter();
                objListSqlParam[16].ParameterName = "@VisaNumber";
                objListSqlParam[16].Value = objUsers.VisaNumber;


                objListSqlParam[17] = new SqlParameter();
                objListSqlParam[17].ParameterName = "@ContactAddressHomeCountry";
                objListSqlParam[17].Value = objUsers.ContactAddressHomeCountry;

                objListSqlParam[18] = new SqlParameter();
                objListSqlParam[18].ParameterName = "@ContactAddressLocal";
                objListSqlParam[18].Value = objUsers.ContactAddressLocal;

                objListSqlParam[19] = new SqlParameter();
                objListSqlParam[19].ParameterName = "@MobileNumberLocal";
                objListSqlParam[19].Value = objUsers.MobileNumberLocal;


                objListSqlParam[20] = new SqlParameter();
                objListSqlParam[20].ParameterName = "@EmergencyContactPersonName";
                objListSqlParam[20].Value = objUsers.EmergencyContactPersonName;

                objListSqlParam[21] = new SqlParameter();
                objListSqlParam[21].ParameterName = "@DateOfJoining";
                objListSqlParam[21].Value = objUsers.DateOfJoining;

                objListSqlParam[22] = new SqlParameter();
                objListSqlParam[22].ParameterName = "@Remarks";
                objListSqlParam[22].Value = objUsers.Remarks;

                objListSqlParam[23] = new SqlParameter();
                objListSqlParam[23].ParameterName = "@Reference_Code";
                objListSqlParam[23].Value = objUsers.Reference_Code;



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

        public MethodOutput<Users> GetUsers(int UserId, int UserTypeId,int UserReferenceId, string UserCode, string UserName, int CreateBy )
        {
            MethodOutput<Users> output = new MethodOutput<Users>();
            List<Users> objLst = new List<Users>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[7];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserId";
                objListSqlParam[0].Value = UserId;
            
                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@UserTypeId";
                objListSqlParam[1].Value = UserTypeId;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@UserReferenceId";
                objListSqlParam[2].Value = UserReferenceId;

              
                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@UserCode";
                objListSqlParam[3].Value = UserCode;

               
                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@UserName";
                objListSqlParam[4].Value = UserName;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@CreatedBy";
                objListSqlParam[5].Value = CreateBy;
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
                        RoleName = x.Field<string>("Role"),
                        BranchName= x.Field<string>("BranchName"),
                        MobileNo= x.Field<string>("MobileNo"),
                        UserTypeId= x.Field<int>("UserTypeId"),
                        Password=x.Field<string>("Password"),
                        UserReferenceId= x.Field<int>("UserReferenceId"),
                        CPRExpiryDate = x.Field<DateTime?>("CPRExpiryDate"),
                        PassportExpiryDate = x.Field<DateTime?>("PassportExpiryDate"),
                        DateOfJoining = x.Field<DateTime?>("DateOfJoining"),
                        CPRNumber = x.Field<string>("CPRNumber"),
                        PassportNumber = x.Field<string>("PassportNumber"),
                        VisaNumber = x.Field<string>("VisaNumber"),
                        ContactAddressHomeCountry = x.Field<string>("ContactAddressHomeCountry"),
                        ContactAddressLocal = x.Field<string>("ContactAddressLocal"),
                        MobileNumberLocal = x.Field<string>("MobileNumberLocal"),
                        EmergencyContactPersonName = x.Field<string>("EmergencyContactPersonName"),
                        Remarks = x.Field<string>("Remarks"),
                        UserCode = x.Field<string>("UserCode"),
                        SourceName=x.Field<string>("SourceName"),
                        CPR_Expiry_Date = x.Field<string>("CPR_Expiry_Date"),
                        PASSPORT_Expiry_Date = x.Field<string>("PASSPORT_Expiry_Date"),
                        Date_OF_Joining = x.Field<string>("Date_OF_Joining")

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


        public MethodOutput<Users> GetUsersByReferenceId(int UserReferenceId)
        {
            MethodOutput<Users> output = new MethodOutput<Users>();
            List<Users> objLst = new List<Users>();
            try
            {

                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@ReferenceId";
                objListSqlParam[0].Value = UserReferenceId;
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetUsersByRefereneId", objListSqlParam).Tables[0];
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
                        RoleName = x.Field<string>("Role"),
                        BranchName = x.Field<string>("BranchName"),
                        MobileNo = x.Field<string>("MobileNo"),
                        UserTypeId = x.Field<int>("UserTypeId"),
                        Password = x.Field<string>("Password"),
                        UserReferenceId = x.Field<int>("UserReferenceId"),
                        CPRExpiryDate = x.Field<DateTime?>("CPRExpiryDate"),
                        PassportExpiryDate = x.Field<DateTime?>("PassportExpiryDate"),
                        DateOfJoining = x.Field<DateTime?>("DateOfJoining"),
                        CPRNumber = x.Field<string>("CPRNumber"),
                        PassportNumber = x.Field<string>("PassportNumber"),
                        VisaNumber = x.Field<string>("VisaNumber"),
                        ContactAddressHomeCountry = x.Field<string>("ContactAddressHomeCountry"),
                        ContactAddressLocal = x.Field<string>("ContactAddressLocal"),
                        MobileNumberLocal = x.Field<string>("MobileNumberLocal"),
                        EmergencyContactPersonName = x.Field<string>("EmergencyContactPersonName"),
                        Remarks = x.Field<string>("Remarks"),
                        UserCode = x.Field<string>("UserCode"),
                        SourceName = x.Field<string>("SourceName")

                    }).ToList();

                    output.DataList = objLst;
                    output.ErrorMessage = string.Empty;
                }
                else
                {
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

        public MethodOutput<string> UpdateUserPassword(int UserId, string UserType, string Password)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {
                DataTable dt = new DataTable();

                SqlParameter[] objListSqlParam = new SqlParameter[3];

                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@UserId";
                objListSqlParam[0].Value = UserId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@UserType";
                objListSqlParam[1].Value = UserType;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@Password";
                objListSqlParam[2].Value = Password;

                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_UpdateUserPassword", objListSqlParam).Tables[0];
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
