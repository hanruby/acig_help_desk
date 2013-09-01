using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acig_Help_DeskModel;
using System.Configuration;
using System.IO;

public class HtmlEmailer
{
    string rootPath, category, Id, subject, url, createdBy, createdByEmail;
    tbl_Users user;
    Ticket ticket;
    Acig_Help_DeskEntities _entity;
    public HtmlEmailer(Acig_Help_DeskEntities e, Ticket _t)
    {
        _entity = e;
        ticket = _t;
        rootPath = ConfigurationManager.AppSettings["RootPath"];
        category = ticket.Sub_Sub_Categories.Sub_Categories.Category.Name + " " + ticket.Sub_Sub_Categories.Sub_Categories.Name + " " +
            " >>> " + ticket.Sub_Sub_Categories.Sub_Categories.Name + " ";
        Id = ticket.Id.ToString();
        subject = ticket.Subject;
        user = _entity.tbl_Users.Where(x => x.Id == ticket.Created_By).First();
        createdBy = user.User_Name;
        createdByEmail = user.Email;
    }

    public void New_Ticket_EMail()
    {
        string body = string.Empty;
        url = ConfigurationManager.AppSettings["RootPath"] + "/tickets/assigned.aspx";
        foreach (var x in ticket.User_Tickets)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Assigned.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{UserName}", x.tbl_Users.User_Name);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{AssignedUser}", createdBy);
            Notifier.SendEmail("crmmailadmin@acig.com.sa", x.tbl_Users.Email, "New Ticket Assigned", body);
        }
        return;
    }

    public void Resoved_Ticket_EMail()
    {
        string body = string.Empty;
        url = ConfigurationManager.AppSettings["RootPath"] + "/tickets/show.aspx?id=" + Id;
        using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Resolved.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Id}", Id);
        body = body.Replace("{UserName}", createdBy);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Category}", category);
        body = body.Replace("{ResolvedUser}", createdBy);
        Notifier.SendEmail("crmmailadmin@acig.com.sa", createdByEmail, "Ticket Resolved", body);
        return;
    }

    public void Closed_Ticket_EMail()
    {
        string body = string.Empty;
        url = ConfigurationManager.AppSettings["RootPath"] + "/tickets/show.aspx?id=" + Id;
        foreach (var x in ticket.User_Tickets)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Closed.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{UserName}", x.tbl_Users.User_Name);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{ClosedUser}", createdBy);
            Notifier.SendEmail("crmmailadmin@acig.com.sa", x.tbl_Users.Email, "Ticket Closed", body);
        }
        return;
    }

    public void New_Comment_TicketEMail()
    {
        string body = string.Empty;
        url = ConfigurationManager.AppSettings["RootPath"] + "/tickets/show.aspx?id=" + Id;
        if (Id == ticket.Created_By.ToString())
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/New_Comment_On_Ticket.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{UserName}", "");
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{CommentUser}", createdBy);
            Notifier.SendEmail("crmmailadmin@acig.com.sa", createdByEmail, "New Comment / Notes on Ticket", body);
        }
        else
        {
            foreach (var x in ticket.User_Tickets)
            {
                using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/New_Comment_On_Ticket.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Id}", Id);
                body = body.Replace("{UserName}", x.tbl_Users.User_Name);
                body = body.Replace("{Url}", url);
                body = body.Replace("{Category}", category);
                body = body.Replace("{CommentUser}", createdBy);
                Notifier.SendEmail("crmmailadmin@acig.com.sa", x.tbl_Users.Email, "New Comment / Notes On Ticket", body);
            }
        }
        return;
    }

    protected string GetPath(string input)
    {
        return HttpContext.Current.Server.MapPath(input);
    }
}