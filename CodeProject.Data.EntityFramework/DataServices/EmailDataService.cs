using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeProject.Interfaces;
using CodeProject.Business.Entities;
using CodeProject.Business.Common;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;

namespace CodeProject.Data.EntityFramework
{
    /// <summary>
    /// Email Data Service
    /// </summary>
    public class EmailDataService : EntityFrameworkService, IEmailDataService
    {

        /// <summary>
        /// Send Email
        /// </summary>
        /// <param name="customer"></param>
        public void SendEmail(Email email)
        {
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            string host = smtpSection.Network.Host;
            int port = smtpSection.Network.Port;
            string username = smtpSection.Network.UserName;
            string password = smtpSection.Network.Password;
            bool enableSsl = smtpSection.Network.EnableSsl;
            bool useDefaultCredentials = smtpSection.Network.DefaultCredentials;

            String from = email.From;
            String to = email.To;
            String mailBody = email.MailBody;
            String subject = email.Subject;


            MailMessage mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subject;
            mailMessage.Body = mailBody;
            mailMessage.BodyEncoding = Encoding.UTF8;
            mailMessage.IsBodyHtml = false;

            SmtpClient client = new SmtpClient();
            NetworkCredential basicCredential1 = new NetworkCredential(username, password);
            client.EnableSsl = enableSsl;
            client.UseDefaultCredentials = useDefaultCredentials;
            client.Credentials = basicCredential1;
            try
            {
                client.Send(mailMessage);
            }

            catch (Exception ex)
            {
                throw ex;
            }  
        }

    }

}

