using DataEngine;
using DbOperation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace DataServices
{
    public class EmailServices
    {
        public bool MailSent(List<string> ToEmails,Dictionary<string,string> TemplateValues, string TemplateCode ,List<string> CC =null, List<string> BCC =null )
        {
            OpEmail objEmail = new OpEmail();
            EmailConfiguration objEC = objEmail.GetEmailConfiguration();
            Template objTemp = GetEmailTemplate(TemplateCode);
            if (objEC != null && objTemp.EmailTemplateMasterId!=0)
            {
                  
                MailMessage message = new MailMessage();
                
                message.From = new MailAddress(objEC.EmailId,objEC.DisplayName);
                string UserName = string.Empty;
                string URL = string.Empty;
                if (TemplateValues.TryGetValue("UserName", out UserName))
                {
                    objTemp.Body = objTemp.Body.Replace("@UserName", UserName);
                }

                if (TemplateValues.TryGetValue("URL", out URL))
                {
                    objTemp.Body = objTemp.Body.Replace("@URL", URL);
                }
                
                // string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
                message.Subject = objTemp.EmailSubject;//"Sending Email Using Asp.Net & C#";
                message.Body = objTemp.Body;
                message.BodyEncoding = Encoding.UTF8;
                if (ToEmails != null && ToEmails.Any())
                {
                    foreach (string ToEmail in ToEmails)
                    {
                        message.To.Add(ToEmail);
                    }
                }

                if (CC != null && CC.Any())
                {
                    foreach (string CCEmail in CC)
                    {
                        message.CC.Add(CCEmail);
                    }
                }

                if (BCC != null && BCC.Any())
                {
                    foreach (string BCCEmail in BCC)
                    {
                        message.Bcc.Add(BCCEmail);
                    }
                }
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient(objEC.Host,objEC.Port); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential(objEC.EmailId, objEC.Password);
                client.EnableSsl = objEC.EnableSSl;
                client.UseDefaultCredentials = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                }

                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;

        }

        public Template GetEmailTemplate( string TemplateCode)
        {
            Template objTemp = new Template();
            OpEmail obj = new OpEmail();
            MethodOutput<Template> objData = new MethodOutput<Template>();
            objData = obj.GetEmailTemplates(TemplateCode);
            if (objData.ErrorMessage == "" && objData.DataList != null)
            {
                objTemp = objData.DataList[0];
            }

            return objTemp;
        }
    }
}



























