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
   public class OpCustomer
    {
        public MethodOutput<string> SaveCustomer(Customer objCustomer)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {


                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[15];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CustomerId";
                objListSqlParam[0].Value = objCustomer.CustomerId;

                objListSqlParam[1] = new SqlParameter();
                objListSqlParam[1].ParameterName = "@FirstName";
                objListSqlParam[1].Value = objCustomer.FirstName;

                objListSqlParam[2] = new SqlParameter();
                objListSqlParam[2].ParameterName = "@LastName";
                objListSqlParam[2].Value = objCustomer.LastName;

                objListSqlParam[3] = new SqlParameter();
                objListSqlParam[3].ParameterName = "@EmailId";
                objListSqlParam[3].Value = objCustomer.EmailId;

                objListSqlParam[4] = new SqlParameter();
                objListSqlParam[4].ParameterName = "@MobileNo";
                objListSqlParam[4].Value = objCustomer.MobileNo;

                objListSqlParam[5] = new SqlParameter();
                objListSqlParam[5].ParameterName = "@NationalId";
                objListSqlParam[5].Value = objCustomer.NationalId;

                objListSqlParam[6] = new SqlParameter();
                objListSqlParam[6].ParameterName = "@Gender";
                objListSqlParam[6].Value = objCustomer.Gender;


            
                objListSqlParam[7] = new SqlParameter();
                objListSqlParam[7].ParameterName = "@Password";
                objListSqlParam[7].Value = objCustomer.Password;

               


                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_SaveCustomer", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    output.Data= Convert.ToString(dt.Rows[0]["CustomerCode"]);
                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["Error"]);

                }
            }
            catch (Exception ex)
            {

                output.ErrorMessage = ex.Message;
            }

            return output;
        }


        public MethodOutput<string> UpdateCustomerEmailVerificationStatus(string CustomerCode)
        {
            MethodOutput<string> output = new MethodOutput<string>();
            try
            {


                DataTable dt = new DataTable();
                SqlParameter[] objListSqlParam = new SqlParameter[1];
                objListSqlParam[0] = new SqlParameter();
                objListSqlParam[0].ParameterName = "@CustomerCode";
                objListSqlParam[0].Value = CustomerCode;

              dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_UpdateCustomerEmailVerification", objListSqlParam).Tables[0];
                if (dt.Rows.Count > 0)
                {
                   
                    output.ErrorMessage = Convert.ToString(dt.Rows[0]["ERROR"]);

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
