using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

public class Notifier
{
    public static void SendEmail(string From, string To, string Subject, string Body)
    {
        MailMessage mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(From);
        mailMessage.Subject = Subject;
        mailMessage.Body = Body;
        mailMessage.To.Add(To);
        mailMessage.IsBodyHtml = true;
        mailMessage.Priority = MailPriority.Normal;
        mailMessage.Bcc.Add(new MailAddress("crmmailadmin@acig.com.sa"));
        SmtpClient smtpClient = new SmtpClient
        {
            Host = "mail.acig.com.sa",
            Port = 25,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential("crmmailadmin", "passwoRd5656")
        };
        smtpClient.Send(mailMessage);
    }
}