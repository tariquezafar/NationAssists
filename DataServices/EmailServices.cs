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
        public bool MailSent(List<string> ToEmails,Dictionary<string,string> TemplateValues, string TemplateCode  , bool IsHavingAttachment =false, List<string> CC =null, List<string> BCC =null )
        {
            bool IsMailSent = true;
            OpEmail objEmail = new OpEmail();
            EmailConfiguration objEC = objEmail.GetEmailConfiguration();
            Template objTemp = GetEmailTemplate(TemplateCode);
            if (objEC != null && objTemp.EmailTemplateMasterId!=0)
            {
                  
                MailMessage message = new MailMessage();
                
                message.From = new MailAddress(objEC.EmailId,objEC.DisplayName);
                if (TemplateCode == EmailTemplate.EMAILVERIFICATION)
                {
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

                }
                else if (TemplateCode == EmailTemplate.CUSTOMER_WELCOME)
                {
                    string UserName = string.Empty;
                    string Password = string.Empty;
                    string CustomerName = string.Empty;
                    string LoginURL = string.Empty;
                    if (TemplateValues.TryGetValue("CustomerName", out CustomerName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@CustomerName", CustomerName);
                    }
                    if (TemplateValues.TryGetValue("UserName", out UserName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@UserName", UserName);
                    }

                    if (TemplateValues.TryGetValue("Password", out Password))
                    {
                        objTemp.Body = objTemp.Body.Replace("@Password", Password);
                    }
                    if (TemplateValues.TryGetValue("LoginURL", out LoginURL))
                    {
                        objTemp.Body = objTemp.Body.Replace("@LoginURL", LoginURL);
                    }
                }

                else if (TemplateCode == EmailTemplate.RAISE_SERVICE_REQUEST)
                {
                    string CustomerName = string.Empty;
                    string ServiceType = string.Empty;
                    string SourceName = string.Empty;
                    string TicketNo = string.Empty;
                    if (TemplateValues.TryGetValue("CustomerName", out CustomerName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@CustomerName", CustomerName);
                    }

                    if (TemplateValues.TryGetValue("ServiceType", out ServiceType))
                    {
                        objTemp.Body = objTemp.Body.Replace("@ServiceType", ServiceType);
                    }

                    if (TemplateValues.TryGetValue("SourceName", out SourceName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@SourceName", SourceName);
                    }

                    if (TemplateValues.TryGetValue("TicketNo", out TicketNo))
                    {
                        objTemp.Body = objTemp.Body.Replace("@TicketNo", TicketNo);
                    }
                }

                else if (TemplateCode == EmailTemplate.ALLOCATE_SERVICE_REQUEST)
                {
                    string TicketNo = string.Empty;
                    string CustomerName = string.Empty;
                    if (TemplateValues.TryGetValue("CustomerName", out CustomerName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@CustomerName", CustomerName);
                    }
                    if (TemplateValues.TryGetValue("TicketNo", out TicketNo))
                    {
                        objTemp.Body = objTemp.Body.Replace("@TicketNo", TicketNo);
                    }
                }

                else if (TemplateCode == EmailTemplate.CLOSED_SERVICE_REQUEST)
                {
                    string CustomerName = string.Empty;
                    string ServiceType = string.Empty;
                    string SourceName = string.Empty;
                    string TicketNo = string.Empty;
                    if (TemplateValues.TryGetValue("CustomerName", out CustomerName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@CustomerName", CustomerName);
                    }

                    if (TemplateValues.TryGetValue("ServiceType", out ServiceType))
                    {
                        objTemp.Body = objTemp.Body.Replace("@ServiceType", ServiceType);
                    }

                    if (TemplateValues.TryGetValue("SourceName", out SourceName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@SourceName", SourceName);
                    }

                    if (TemplateValues.TryGetValue("TicketNo", out TicketNo))
                    {
                        objTemp.Body = objTemp.Body.Replace("@TicketNo", TicketNo);
                    }
                }

                else if (TemplateCode == EmailTemplate.SERVICE_ENROLLMENT)
                {
                    string CustomerName = string.Empty;
                    string DownloadURL = string.Empty;
                    string CertifcatePath = string.Empty;
               
                    if (TemplateValues.TryGetValue("CustomerName", out CustomerName))
                    {
                        objTemp.Body = objTemp.Body.Replace("@CustomerName", CustomerName);
                    }

                    if (TemplateValues.TryGetValue("DownloadURL", out DownloadURL))
                    {
                        objTemp.Body = objTemp.Body.Replace("@DownloadURL", DownloadURL);
                    }
                    TemplateValues.TryGetValue("CertificatePath", out CertifcatePath);
                    
                    if (IsHavingAttachment)
                    {
                       // System.Net.Mime.ContentType ct = new System.Net.Mime.ContentType(System.Net.Mime.MediaTypeNames.Application.Octet);
                        System.Net.Mail.Attachment attach = new System.Net.Mail.Attachment(CertifcatePath);
                        message.Attachments.Add(attach);

                    }

                }
                else if (TemplateCode == EmailTemplate.CUSTOMER_ENQUIRY)
                {
                    string Name = string.Empty;
                    string MobileNumber = string.Empty;
                    string EmailAddress = string.Empty;
                    string Reason = string.Empty;
                    string Message = string.Empty;
                    if (TemplateValues.TryGetValue("Name", out Name))
                    {
                        objTemp.Body = objTemp.Body.Replace("@Name", Name);
                    }

                    if (TemplateValues.TryGetValue("MobileNumber", out MobileNumber))
                    {
                        objTemp.Body = objTemp.Body.Replace("@MobileNumber", MobileNumber);
                    }

                    if (TemplateValues.TryGetValue("EmailAddress", out EmailAddress))
                    {
                        objTemp.Body = objTemp.Body.Replace("@EmailAddress", EmailAddress);
                    }

                    if (TemplateValues.TryGetValue("Reason", out Reason))
                    {
                        objTemp.Body = objTemp.Body.Replace("@Reason", Reason);
                    }
                    if (TemplateValues.TryGetValue("Message", out Message))
                    {
                        objTemp.Body = objTemp.Body.Replace("@Message", Message);
                    }
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
                    IsMailSent = true;

                }

                catch (Exception ex)
                {
                    IsMailSent = false;
                }
            }

            return IsMailSent;

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



























