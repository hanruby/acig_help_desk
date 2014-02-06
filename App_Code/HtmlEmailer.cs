using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acig_Help_DeskModel;
using System.Configuration;
using System.IO;

public class HtmlEmailer
{
    string rootPath, category, Id, subject, url, createdBy, createdByEmail, createdByDeptEmail, assignedToDeptEmail;
    long currentUserId;
    tbl_Users user, custUser;
    Department custDept;
    Ticket ticket;
    Acig_Help_DeskEntities _entity;
    List<string> ccEmails;
    List<long> managerIds;
    public HtmlEmailer(Acig_Help_DeskEntities e, Ticket _t)
    {
        _entity = e;
        ticket = _t;
        rootPath = ConfigurationManager.AppSettings["RootPath"];
        category = ticket.Sub_Sub_Categories.Sub_Categories.Category.Name + " >>> " + ticket.Sub_Sub_Categories.Sub_Categories.Name +
            " >>> " + ticket.Sub_Sub_Categories.Name + " ";
        Id = ticket.Id.ToString();
        subject = ticket.Subject;
        user = _entity.tbl_Users.Where(x => x.Id == ticket.Created_By).First();
        createdBy = user.User_Name;
        createdByEmail = user.Email;
        url = ConfigurationManager.AppSettings["RootPath"] + "/tickets/show.aspx?id=" + Id;
        Create_CC_EMails();
        AddManagerCCEmails();
    }

    public void Create_CC_EMails()
    {
        ccEmails = new List<string>();
        managerIds = new List<long>();
        currentUserId = CurrentUser.Id();
        AddManagerIDs(user.Department.Manager_Id);
        if (currentUserId == ticket.Created_By)
        {
            AddCCEmail(createdByEmail);
        }
        else
        {
            foreach (var ut in ticket.User_Tickets)
            {
                custUser = ut.tbl_Users;
                custDept = custUser.Department;
                if (custUser.Role == "vendor" && !string.IsNullOrEmpty(custUser.Vendor_Emails))
                {
                    var vendorEmails = custUser.Vendor_Emails.Split(',');
                    foreach (var e in vendorEmails)
                    {
                        AddCCEmail(e);
                    }
                }
                AddCCEmail(custUser.Email);
                AddManagerIDs(custDept.Manager_Id);
                AddManagerIDs(custDept.Manager_Id_2);
                AddManagerIDs(custDept.Manager_Id_3);
            }
        }
    }

    public void AddManagerIDs(long? id)
    {
        if (id != null && id.Value != 0 && !managerIds.Contains(id.Value))
        {
            managerIds.Add(id.Value);
        }
    }

    public void AddCCEmail(string email)
    {
        if (!ccEmails.Contains(email))
        {
            ccEmails.Add(email);
        }
    }

    public void AddManagerCCEmails()
    {
        var lst = _entity.tbl_Users.Where(x => managerIds.Contains(x.Id)).Select(x => x.Email).ToList();
        foreach (var x in lst)
        {
            AddCCEmail(x);
        }
    }

    public void New_Ticket_EMail()
    {
        string body = string.Empty;
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
            Notifier.SendEmail(x.tbl_Users.Email, "IT Help Desk - New Ticket Assigned", body, ccEmails);
        }
        return;
    }

    public void Resoved_Ticket_EMail()
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Resolved.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Id}", Id);
        body = body.Replace("{Subject}", subject);
        body = body.Replace("{UserName}", createdBy);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Category}", category);
        body = body.Replace("{ResolvedUser}", CurrentUser.User().User_Name);
        Notifier.SendEmail(createdByEmail, "IT Help Desk - Ticket Resolved", body, ccEmails);
        if (ticket.On_Behalf != null)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Resolved.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            var customId = long.Parse(ticket.On_Behalf.ToString());
            var customUser = _entity.tbl_Users.Where(x => x.Id == customId).First();
            body = body.Replace("{UserName}", customUser.Email);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{ResolvedUser}", CurrentUser.User().User_Name);
            Notifier.SendEmail(createdByEmail, "IT Help Desk - Ticket Resolved", body, ccEmails);
        }
        return;
    }

    public void Ticket_Clarification_EMail()
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Clarification.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Id}", Id);
        body = body.Replace("{Subject}", subject);
        body = body.Replace("{UserName}", createdBy);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Category}", category);
        body = body.Replace("{User}", CurrentUser.User().User_Name);
        Notifier.SendEmail(createdByEmail, "IT Help Desk - Ticket Clarification", body, ccEmails);
        if (ticket.On_Behalf != null)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Clarification.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            var customId = long.Parse(ticket.On_Behalf.ToString());
            var customUser = _entity.tbl_Users.Where(x => x.Id == customId).First();
            body = body.Replace("{UserName}", customUser.Email);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{User}", CurrentUser.User().User_Name);
            Notifier.SendEmail(createdByEmail, "IT Help Desk - Ticket Clarification", body, ccEmails);
        }
        return;
    }

    public void Closed_Ticket_EMail()
    {
        string body = string.Empty;
        foreach (var x in ticket.User_Tickets)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Closed.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{UserName}", x.tbl_Users.User_Name);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{ClosedUser}", createdBy);
            Notifier.SendEmail(x.tbl_Users.Email, "IT Help Desk - Ticket Closed", body, ccEmails);
        }
        return;
    }

    public void New_Comment_TicketEMail()
    {
        string body = string.Empty;
        if (CurrentUser.Id().ToString() == ticket.Created_By.ToString())
        {
            foreach (var x in ticket.User_Tickets)
            {
                using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/New_Comment_On_Ticket.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Id}", Id);
                body = body.Replace("{Subject}", subject);
                body = body.Replace("{UserName}", x.tbl_Users.User_Name);
                body = body.Replace("{Url}", url);
                body = body.Replace("{Category}", category);
                body = body.Replace("{CommentUser}", createdBy);
                Notifier.SendEmail(x.tbl_Users.Email, "IT Help Desk - New Comment / Notes On Ticket", body, ccEmails);
            }
        }
        else
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/New_Comment_On_Ticket.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{UserName}", createdBy);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{CommentUser}", CurrentUser.User().User_Name);
            Notifier.SendEmail(createdByEmail, "IT Help Desk - New Comment / Notes on Ticket", body, ccEmails);

            if (ticket.On_Behalf != null)
            {
                using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Resolved.htm")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{Id}", Id);
                body = body.Replace("{Subject}", subject);
                var customId = long.Parse(ticket.On_Behalf.ToString());
                var customUser = _entity.tbl_Users.Where(x => x.Id == customId).First();
                body = body.Replace("{UserName}", customUser.Email);
                body = body.Replace("{Url}", url);
                body = body.Replace("{Category}", category);
                body = body.Replace("{CommentUser}", CurrentUser.User().User_Name);
                Notifier.SendEmail(createdByEmail, "IT Help Desk - New Comment / Notes on Ticket", body, ccEmails);
            }
        }
        return;
    }

    public void Re_Open_Ticket_EMail()
    {
        string body = string.Empty;
        foreach (var x in ticket.User_Tickets)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Re_Open.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{UserName}", x.tbl_Users.User_Name);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{ClosedUser}", createdBy);
            Notifier.SendEmail(x.tbl_Users.Email, "IT Help Desk - Ticket Re Open", body, ccEmails);
        }
        return;
    }

    public void Clarifed_Ticket_EMail()
    {
        string body = string.Empty;
        foreach (var x in ticket.User_Tickets)
        {
            using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_Clarified.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Id}", Id);
            body = body.Replace("{Subject}", subject);
            body = body.Replace("{UserName}", x.tbl_Users.User_Name);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Category}", category);
            body = body.Replace("{User}", createdBy);
            Notifier.SendEmail(x.tbl_Users.Email, "IT Help Desk - Ticket Clarified", body, ccEmails);
        }
        return;
    }

    public static void New_User_Sign_Up(Acig_Help_DeskEntities entity, tbl_Users user)
    {
        string body, parsedBody = string.Empty;
        using (StreamReader reader = new StreamReader(HttpContext.Current.Server.MapPath("~/Email_Templates/New_User_Signup.htm")))
        {
            body = reader.ReadToEnd();
        }
        string url = ConfigurationManager.AppSettings["RootPath"] + "/admin/users/index.aspx";
        parsedBody = body.Replace("{UserName}", "mustafa");
        parsedBody = parsedBody.Replace("{user_name}", user.User_Name);
        parsedBody = parsedBody.Replace("{email}", user.Email);
        Notifier.SendEmail("mustafa@acig.com.sa", "IT Help Desk - New User Sign up", parsedBody, new List<string>());
        parsedBody = body.Replace("{UserName}", "ubaid");
        parsedBody = parsedBody.Replace("{user_name}", user.User_Name);
        parsedBody = parsedBody.Replace("{email}", user.Email);
        parsedBody = parsedBody.Replace("{Url}", url);
        Notifier.SendEmail("ubaid@acig.com.sa", "IT Help Desk - New User Sign up", parsedBody, new List<string>());
        Notifier.SendEmail("ubaidkhan88@gmail.com", "IT Help Desk - New User Sign up", parsedBody, new List<string>(), false);
        return;
    }

    public void Reassign_Ticket_EMail(tbl_Users user)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Ticket_ReAssigned.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Id}", Id);
        body = body.Replace("{UserName}", user.User_Name);
        body = body.Replace("{Subject}", subject);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Category}", category);
        body = body.Replace("{AssignedUser}", createdBy);
        Notifier.SendEmail(user.Email, "IT Help Desk - Ticket ReAssigned", body, ccEmails);
        return;
    }

    public void Assign_Vendor_Ticket_EMail(tbl_Users user)
    {
        string body = string.Empty;
        using (StreamReader reader = new StreamReader(GetPath("~/Email_Templates/Assign_Vendor.htm")))
        {
            body = reader.ReadToEnd();
        }
        body = body.Replace("{Id}", Id);
        body = body.Replace("{UserName}", user.User_Name);
        body = body.Replace("{Subject}", subject);
        body = body.Replace("{Url}", url);
        body = body.Replace("{Category}", category);
        body = body.Replace("{AssignedUser}", createdBy);
        Notifier.SendEmail(user.Email, "IT Help Desk - New Ticket Assigned", body, ccEmails);
        return;
    }

    protected string GetPath(string input)
    {
        return HttpContext.Current.Server.MapPath(input);
    }
}