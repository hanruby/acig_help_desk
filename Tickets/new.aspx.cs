using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_new : MasterAppPage
{
    Ticket _ticket;
    Event _event;
    Comment _comment;
    User_Tickets _user_Tickets;
    long _sub_sub_Category_Id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdnFldId.Value = CurrentUser.Id().ToString();
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
            State = "Open",
            Subject = txtSubject.Text,
            Type = ddlType.SelectedValue,
            Updated_At = DateTime.Now
        };
        _ticket.Created_By = currentUserId;
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

        foreach (var y in _entity.User_Sub_Sub_Categories.Where(x => x.Sub_Sub_Category_Id == _sub_sub_Category_Id).Select(x => x.User_Id).ToList().Distinct())
        {
            if (currentUserId != y)
            {
                _user_Tickets = new User_Tickets
                {
                    Ticket_Id = _ticket.Id,
                    User_Id = y
                };
                _entity.AddToUser_Tickets(_user_Tickets);
                _entity.SaveChanges();
            }
        }

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

        Session["NoticeMessage"] = "Successfully created a new ticket!";
        Response.Redirect(Route.GetRootPath("tickets/show.aspx?id=" + _ticket.Id.ToString()));
    }
}