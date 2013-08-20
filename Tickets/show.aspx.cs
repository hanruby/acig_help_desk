using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_show : MasterAppPage
{
    Ticket _ticket;
    long _id;
    string routePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        routePath = Route.GetRootPath("download.aspx?id=");
        _id = long.Parse(Request.QueryString["id"]);
        _entity = new Acig_Help_DeskEntities();
        _ticket = _entity.Tickets.Where(x => x.Id == _id).First();
        var ticketData = from t in _entity.Tickets
                         join sc in _entity.Sub_Categories
                         on t.Sub_Category_Id equals sc.Id
                         join c in _entity.Categories
                         on sc.Category_Id equals c.Id
                         join u in _entity.tbl_Users
                         on t.Created_By equals u.Id
                         join u1 in _entity.tbl_Users
                         on t.Assigned_To equals u1.Id
                         where t.Id == _id
                         select new { 
                             AssignFrom = u.Email,
                             AssignTo = u1.Email,
                             CategoryName = c.Name,
                             SubCategoryName = sc.Name,
                             Priority = t.Priority,
                             State = t.State,
                             Subject = t.Subject,
                             Type = t.Type
                         };
        rptrTickets.DataSource = ticketData;
        rptrTickets.DataBind();

        var data = from ev in _entity.Events
                   where ev.Ticket_Id == _id
                   select new
                   {
                       CreatedAt = ev.Created_At,
                       State = ev.State
                   };
        gvEvents.DataSource = data;
        gvEvents.DataBind();

        var commentData = from c in _entity.Comments
                          where c.Ticket_Id == _id
                          select new
                          {
                              CreatedAt = c.Created_At,
                              Notes = c.Notes,
                              Visible = c.File_Path,
                              Url = c.Id
                          };
        gvComments.DataSource = commentData;
        gvComments.DataBind();
    }

    protected bool FileLinkVisibile(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return !string.IsNullOrEmpty(obj.ToString());
    }

    protected string FileDownloadUrl(object obj)
    {
        return Route.GetRootPath("download.aspx?id=" + obj.ToString());
    }
}