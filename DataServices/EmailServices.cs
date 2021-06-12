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
        public bool MailSent(List<string> ToEmails, string TemplateCode ,List<string> CC =null, List<string> BCC =null)
        {
            OpEmail objEmail = new OpEmail();
            EmailConfiguration objEC = objEmail.GetEmailConfiguration();
            if (objEC != null)
            {
                  
                MailMessage message = new MailMessage();
                message.From = new MailAddress(objEC.EmailId,objEC.DisplayName);
                string mailbody = "In this article you will learn how to send a email using Asp.Net & C#";
                message.Subject = "Sending Email Using Asp.Net & C#";
                message.Body = mailbody;
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
    }
}



























