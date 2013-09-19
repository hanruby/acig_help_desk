using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_call : MasterAppPage
{
    Ticket _ticket;
    Event _event;
    Comment _comment;
    User_Tickets _user_Tickets;
    long _sub_sub_Category_Id;
    string assigned_To_Emails;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CurrentUser.Is_Engineer() && !CurrentUser.Is_IT_Consultant())
        {
            ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access!");
            return;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        _sub_sub_Category_Id = long.Parse(ddlSubSubCategory.SelectedValue);
        _entity = GetEntity();
        _ticket = new Ticket
        {
            Created_At = DateTime.Now,
            Priority = ddlPriority.SelectedValue,
            State = "Pending",
            Subject = txtSubject.Text,
            Type = ddlType.SelectedValue,
            Updated_At = DateTime.Now,
            Assigned_To_Emails = "Remove"
        };
        _ticket.Created_By = currentUserId;
        _ticket.On_Behalf = long.Parse(ddlCreatedBy.SelectedValue);
        _ticket.Sub_Sub_Category_Id = _sub_sub_Category_Id;
        _entity.AddToTickets(_ticket);
        _entity.SaveChanges();

        _event = new Event
        {
            Created_At = DateTime.Now,
            State = "Open",
            Created_By = currentUserId
        };
        _event.Ticket_Id = _ticket.Id;
        _entity.AddToEvents(_event);
        _entity.SaveChanges();

        var data = from u in _entity.tbl_Users
                   join us in _entity.User_Sub_Sub_Categories
                   on u.Id equals us.User_Id
                   where us.Sub_Sub_Category_Id == _sub_sub_Category_Id && u.Active == true && u.Id != currentUserId
                   select new { Email = u.Email, Id = u.Id };
        var dataList = data.ToList();

        foreach (var x in dataList)
        {
            _user_Tickets = new User_Tickets
            {
                Ticket_Id = _ticket.Id,
                User_Id = x.Id
            };
            _entity.AddToUser_Tickets(_user_Tickets);
            _entity.SaveChanges();
            assigned_To_Emails += x.Email + ",";
        }
        _ticket.Assigned_To_Emails = assigned_To_Emails;
        _entity.SaveChanges();

        _comment = new Comment
        {
            Created_At = DateTime.Now,
            Created_By = currentUserId,
            Notes = txtNotes.Text
        };
        _comment.Ticket_Id = _ticket.Id;

        HttpPostedFile postedFile = Request.Files["uploadFile"];
        if (postedFile != null && postedFile.ContentLength > 0)
        {
            var outputFile = FileHelper.SaveFile(postedFile, _ticket.Id);
            _comment.File_Name = (string)outputFile["FileName"];
            _comment.File_Path = (string)outputFile["FilePath"];
        }
        _entity.AddToComments(_comment);
        _entity.SaveChanges();

        HtmlEmailer emailer = new HtmlEmailer(_entity, _ticket);
        emailer.New_Ticket_EMail();

        Session["NoticeMessage"] = "Successfully created a new ticket!";
        Response.Redirect(Route.GetRootPath("tickets/show.aspx?id=" + _ticket.Id.ToString()));
    }
}