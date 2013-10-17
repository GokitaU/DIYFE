using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using RazorEngine;
using System.IO;
using System.Text;

namespace DIYFEWeb
{
    //public enum EmailTemplate
    //{
    //    DefaultTemplate = "Views\\Email\\TemplateOne.htm"
    //}

    /// <summary>
    /// Summary description for TemplateResolver
    /// </summary>
    public class EmailTemplateResolver
    {
        #region Methods

        public static string GetEmailBody(string templatePath, dynamic model)
        {
            var template = File.ReadAllText(templatePath);
            var body = Razor.Parse(template, model);
            
            return body;
        }

        #endregion Methods
    }

    /// <summary>
    /// This class contains methods to create a mail message to be sent to a user.
    /// </summary>
    public static class EmailMessageFactory
    {
        #region Methods

        public static MailMessage GetWelcomeEmail(string toAddress, string userName, string userFullName, string password, string loginUrl)
        {

            var templatePath = HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/Welcome.cshtml");

            var body = EmailTemplateResolver.GetEmailBody(
                templatePath,
                new
                {
                    UserName = userName,
                    FullName = userFullName,
                    Password = password,
                    LoginUrl = loginUrl
                });

            return new MailMessage(
                    StaticConfig.MailSenderAddress,
                    toAddress,
                    "Welcome to our site!",
                    body);
        }

        public static MailMessage GetErrorEmail(Exception err)
        {

            var templatePath = HttpContext.Current.Server.MapPath("~/Views/EmailTemplates/Error.cshtml");

            var body = EmailTemplateResolver.GetEmailBody(
                templatePath,
                err);

            return new MailMessage(
                    StaticConfig.MailSenderAddress,
                    StaticConfig.MailErrorAddress,
                    "Error In " + StaticConfig.SiteName,
                    body);
        }

        #endregion Methods
    }

    public static class EmailClient
    {
        #region Methods

        public static SendEmailResult SendEmail(System.Net.Mail.MailMessage message)
        {
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient mailClient = new SmtpClient();
            var log = new SendEmailResult() { Message = message };
            try
            {
                mailClient.Send(message);
            }
            catch (Exception ex)
            {
                log.Exception = ex;
            }

            return log;
        }

        #endregion Methods
    }

    public class SendEmailResult
    {
        #region Properties

        public Exception Exception
        {
            get;
            set;
        }

        public bool Failed
        {
            get { return this.Exception != null; }
        }

        public System.Net.Mail.MailMessage Message
        {
            get;
            internal set;
        }

        #endregion Properties
    }

}