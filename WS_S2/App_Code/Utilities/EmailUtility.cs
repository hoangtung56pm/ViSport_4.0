using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mail;

namespace SMSManager_API.Library.Utilities
{
    public class EmailUtility
    {
        public static bool DoSendMail(string server, string user, string password,string from, string to, string cc, string subject, string content)
        {
            MailMessage message = new MailMessage();

            message.To = to;
            message.From = from;
            message.Cc = cc;
            message.Subject = subject;
            message.BodyEncoding = Encoding.UTF8;
            message.BodyFormat = MailFormat.Html;
            message.Body = content;
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserver", server);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", 25);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusing", 2);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", 1);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", user);
            message.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", password);

            try
            {
                SmtpMail.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
