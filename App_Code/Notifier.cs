﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

public class Notifier
{
    public static void SendEmail(string To, string Subject, string Body, List<string> ccEmails, bool sendCC = true)
    {
        MailMessage mailMessage = new MailMessage();
        //mailMessage.From = new MailAddress(From);
        mailMessage.From = new MailAddress("IT-Support@acig.com.sa");
        mailMessage.Subject = Subject;
        mailMessage.Body = Body;
        mailMessage.To.Add(To);
        mailMessage.IsBodyHtml = true;
        mailMessage.Priority = MailPriority.Normal;
        if (sendCC)
        {
            if (!ccEmails.Contains("magdi@acig.com.sa"))
            {
                ccEmails.Add("magdi@acig.com.sa");
            }
        }
        foreach (var x in ccEmails)
        {
            try
            {
                mailMessage.CC.Add(new MailAddress(x));
            }
            catch
            {
            }
        }
        //mailMessage.Bcc.Add(new MailAddress("crmmailadmin@acig.com.sa"));
        mailMessage.Bcc.Add(new MailAddress("IT-Support@acig.com.sa"));
        SmtpClient smtpClient = new SmtpClient
        {
            Host = "mail.acig.com.sa",
            Port = 25,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            //Credentials = new NetworkCredential("crmmailadmin", "passwoRd5656")
            Credentials = new NetworkCredential("IT-Support", "passwoRd101")
        };
        smtpClient.Send(mailMessage);
    }
}