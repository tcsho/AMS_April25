using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;

/// <summary>
/// Summary description for BLLSendEmail
/// </summary>
public class BLLSendEmail
{


        public void SendEmail(string mailTo, string subject, string msgbody)
    {
        string hostURL = HttpContext.Current.Request.Url.Host;
        if (hostURL.Substring(0, 6).ToLower() == "attlog" || hostURL.Substring(0, 5).ToLower() == "bcard" || hostURL.Substring(0, 3).ToLower() == "odp")
        {
            if (mailTo != string.Empty && mailTo != "" && mailTo != null)
            {

                MailMessage message = new MailMessage();
                string msg = string.Empty;

                MailAddress fromAddress = new MailAddress("ams@csn.edu.pk", "Attendance Alert");//new MailAddress("groupcsn@gmail.com");
                message.From = fromAddress;
                message.To.Add(mailTo);
                //message.CC.Add("csnho124@csn.edu.pk");
                message.Subject = subject;
                message.IsBodyHtml = true;
                message.Body = msgbody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.SubjectEncoding = System.Text.Encoding.UTF8;

                SmtpClient client = new SmtpClient();
                client.Port = 587;
                //client.Host = "mail.csn.edu.pk";//"smtp.gmail.com";
                client.Host = "smtp.office365.com";
                //client.Host = "cloud.csn.edu.pk";
                // client.Host = "10.1.1.5";
                System.Net.NetworkCredential nc = new System.Net.NetworkCredential("ams@csn.edu.pk", "C1ty.0147#");

                client.UseDefaultCredentials = false;
                client.Credentials = nc;
                try
                {
                    client.Send(message);

                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {

                    }
                }

            }

        }
    }
  
    public void SendEmailNew(string mailTo, string subject, string msgbody, string ccmailTo)
    {
        string hostURL = HttpContext.Current.Request.Url.Host;
        if (hostURL.Substring(0, 6).ToLower() == "attlog" || hostURL.Substring(0, 5).ToLower() == "bcard" || hostURL.Substring(0, 3).ToLower() == "odp" || hostURL.Substring(0, 9).ToLower() == "localhost")
        {
            if (mailTo != string.Empty && mailTo != "" && mailTo != null)
            {
                // Set the SMTP server details
                SmtpClient smtpClient = new SmtpClient("10.1.1.120", 25);
                smtpClient.EnableSsl = false;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ams@csn.edu.pk", "C1ty.0147#");
                //smtpClient.Credentials = new NetworkCredential("noreply@csn.edu.pk", "Master@123");

                // Create a new email message
                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.From = new MailAddress("ams@csn.edu.pk");
                mailMessage.To.Add(mailTo);
                mailMessage.Subject = subject;
                mailMessage.Body = msgbody;
                //mailMessage.Bcc.Add("rumman@csn.edu.pk");
                if (ccmailTo != "")
                    mailMessage.CC.Add(ccmailTo);

                try
                {
                    // Send the email
                    smtpClient.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                    {

                    }
                }
            }
        }
    }
}