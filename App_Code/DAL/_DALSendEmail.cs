using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Mail;

/// <summary>
/// Summary description for _DALPeriod
/// </summary>
public class _DALSendEmail
    {
    DALBase dalobj = new DALBase();


    public _DALSendEmail()
        {
        //
        // TODO: Add constructor logic here
        //
        }

    public void SendEmail(string mailTo, string subject, string msgbody)
        {

        MailMessage message = new MailMessage();
        string msg = string.Empty;

        MailAddress fromAddress = new MailAddress("AMS@csn.edu.pk", "Attendance [Leave(s) Approval]");//new MailAddress("groupcsn@gmail.com");
        message.From = fromAddress;
        message.To.Add(mailTo);

        message.Subject = subject;
        message.IsBodyHtml = true;
        message.Body = msgbody;
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.SubjectEncoding = System.Text.Encoding.UTF8;

        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.Host = "mail.csn.edu.pk";//"smtp.gmail.com";
        System.Net.NetworkCredential nc = new System.Net.NetworkCredential("AMS@csn.edu.pk", "123456");
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
