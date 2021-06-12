using DataEngine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace DbOperation
{
   public class OpEmail
    {

        public EmailConfiguration GetEmailConfiguration()
        {
            EmailConfiguration objEC = new EmailConfiguration();
            try
            {
                DataTable dt = new DataTable();
                dt = SqlHelper.ExecuteDataset(SqlHelper.ConnectionString, CommandType.StoredProcedure, "usp_GetEmailConfiguration").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    objEC.EmailConfigurationId = Convert.ToInt32(dt.Rows[0]["EmailConfigurationId"]);
                    objEC.Port = Convert.ToInt32(dt.Rows[0]["Port"]);
                    objEC.EmailId= Convert.ToString(dt.Rows[0]["EmailId"]);
                    objEC.Host = Convert.ToString(dt.Rows[0]["Host"]);
                    objEC.EnableSSl=Convert.ToBoolean(dt.Rows[0]["EnableSSl"]);
                    objEC.Password = Convert.ToString(dt.Rows[0]["Password"]);
                    objEC.DisplayName = Convert.ToString(dt.Rows[0]["DisplayName"]);
                }
            }
            catch (Exception ex)
            {
                
            }
            return objEC;
        }
    }
}
