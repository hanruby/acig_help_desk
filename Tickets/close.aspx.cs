using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_close : MasterAppPage
{
    Comment _comment;
    Ticket _ticket;
    Event _event;
    long _id;
    string routePath;
    bool found;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            routePath = Route.GetRootPath("");
            currentUserId = CurrentUser.Id();
            _id = long.Parse(Request.QueryString["id"]);
            hdnFldTicketId.Value = _id.ToString();
            _entity = new Acig_Help_DeskEntities();
            var ticketData = from t in _entity.Tickets
                             join sc in _entity.Sub_Categories
                             on t.Sub_Category_Id equals sc.Id
                             join c in _entity.Categories
                             on sc.Category_Id equals c.Id
                             join u in _entity.tbl_Users
                             on t.Created_By equals u.Id
                             where t.Id == _id && t.State == "Resolved" && t.Created_By == currentUserId
                             select new
                             {
                                 Id = t.Id,
                                 AssignFromId = u.Id,
                                 AssignFrom = u.Email,
                                 CategoryName = c.Name,
                                 SubCategoryName = sc.Name,
                                 Priority = t.Priority,
                                 State = t.State,
                                 Subject = t.Subject,
                                 Type = t.Type
                             };
            found = false;
            foreach (var x in ticketData)
            {
                found = true;
            }
            if (!found)
            {
                ErrorRedirect(routePath + "not_authorized.aspx", "Not authorized to access that ticket!");
                return;
            }
            rptrTickets.DataSource = ticketData;
            rptrTickets.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        routePath = Route.GetRootPath("tickets/index.aspx");
        _id = long.Parse(hdnFldTicketId.Value);
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();

        _ticket = _entity.Tickets.Where(x => x.Id == _id).First();
        _ticket.Rating = ddlRating.SelectedValue;
        _ticket.Closed_Date = DateTime.Now;
        _ticket.State = "Closed";

        if (txtDescription.Text.Trim().Length > 0)
        {
            _comment = new Comment
            {
                Created_At = DateTime.Now,
                Created_By = currentUserId,
                Notes = txtDescription.Text,
                Ticket_Id = _id
            };
            _entity.AddToComments(_comment);
            _entity.SaveChanges();
        }

        _event = new Event
        {
            Created_At = DateTime.Now,
            State = "Closed",
            Created_By = currentUserId,
            Ticket_Id = _id
        };
        _entity.AddToEvents(_event);
        _entity.SaveChanges();

        SuccessRedirect(routePath, "Successfully updated !");
    }
}