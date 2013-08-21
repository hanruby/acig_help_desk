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
        _ticket.Assigned_To = long.Parse(ddlAssignTo.SelectedValue);
        _ticket.Created_By = currentUserId;
        _ticket.Sub_Category_Id = long.Parse(ddlSubCategory.SelectedValue);
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

        _comment = new Comment
        {
            Created_At = DateTime.Now,
            Created_By = currentUserId,
            Notes = txtNotes.Text
        };
        _comment.Ticket_Id = _ticket.Id;
        HttpPostedFile postedFile = Request.Files["uploadFile"];
        if (postedFile != null)
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