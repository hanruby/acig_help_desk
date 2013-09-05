using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Tickets_assigned : MasterAppPage
{
    DataTable dt;
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CurrentUser.Is_Engineer())
        {
            ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
            return;
        }
        if (!IsPostBack)
        {
            BindGvOpenTickets();
            BindGvResolvedTickets();
            BindGvClosedTickets();
            lblMainHeader.Text = "Tickets Assigned To Me !";
            lblOpen.Text = "Pending Tickets!";
            lblResolved.Text = "Resolved Tickets!";
            lblClosed.Text = "Closed Tickets!";
        }
    }

    protected void gvTicketsOpen_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsOpen.PageIndex = e.NewPageIndex;
        BindGvOpenTickets();
    }

    protected void gvTicketsResolved_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsResolved.PageIndex = e.NewPageIndex;
        BindGvResolvedTickets();
    }

    protected void gvTicketsClosed_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsClosed.PageIndex = e.NewPageIndex;
        BindGvClosedTickets();
    }

    protected void gvTicketsOpen_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var text = e.Row.Cells[3].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[3].Controls.Add((Control)lb);

        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Resolve";
        lb.PostBackUrl = "resolve.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[4].Controls.Add((Control)lb);
    }

    protected void gvTicketsResolved_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var text = e.Row.Cells[4].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[4].Controls.Add((Control)lb);
    }

    protected void gvTicketsClosed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var text = e.Row.Cells[5].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[5].Controls.Add((Control)lb);
    }

    protected void GetHeader(string scope)
    {
        dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Open At", typeof(string)));
        if (scope == "resolved")
        {
            dt.Columns.Add(new DataColumn("Resolved At", typeof(string)));
        }
        else if (scope == "closed")
        {
            dt.Columns.Add(new DataColumn("Resolved At", typeof(string)));
            dt.Columns.Add(new DataColumn("Closed At", typeof(string)));
        }
        dt.Columns.Add(new DataColumn("Category", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));
        if (scope == "open")
        {
            dt.Columns.Add(new DataColumn("Resolve?", typeof(string)));
        }
    }

    protected void BindGvOpenTickets()
    {
        dr = null;
        currentUserId = CurrentUser.Id();
        GetHeader("open");
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ssc in _entity.Sub_Sub_Categories
                   on t.Sub_Sub_Category_Id equals ssc.Id
                   join sc in _entity.Sub_Categories
                   on ssc.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   where u.Id == currentUserId && t.State == "Pending"
                   orderby t.Id descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Id = t.Id
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Open At"] = x.OpenAt;
            dr["Category"] = x.CategoryName + " >> " + x.SubCategoryName + " >> " + x.SubSubCategoryName; ;
            dr["Details"] = x.Id;
            dr["Resolve?"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTicketsOpen.DataSource = dt;
        gvTicketsOpen.DataBind();
    }

    protected void BindGvResolvedTickets()
    {
        dr = null;
        currentUserId = CurrentUser.Id();
        GetHeader("resolved");
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ssc in _entity.Sub_Sub_Categories
                   on t.Sub_Sub_Category_Id equals ssc.Id
                   join sc in _entity.Sub_Categories
                   on ssc.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   where u.Id == currentUserId && t.State == "Resolved"
                   orderby t.Created_By descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Id = t.Id
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Open At"] = x.OpenAt;
            dr["Category"] = x.CategoryName + " >> " + x.SubCategoryName + " >> " + x.SubSubCategoryName; ;
            dr["Resolved At"] = x.ResolvedAt;
            dr["Details"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTicketsResolved.DataSource = dt;
        gvTicketsResolved.DataBind();
    }

    protected void BindGvClosedTickets()
    {
        dr = null;
        currentUserId = CurrentUser.Id();
        GetHeader("closed");
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ssc in _entity.Sub_Sub_Categories
                   on t.Sub_Sub_Category_Id equals ssc.Id
                   join sc in _entity.Sub_Categories
                   on ssc.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   where u.Id == currentUserId && t.State == "Closed"
                   orderby t.Created_By descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Id = t.Id
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Open At"] = x.OpenAt;
            dr["Category"] = x.CategoryName + " >> " + x.SubCategoryName + " >> " + x.SubSubCategoryName; ;
            dr["Resolved At"] = x.ResolvedAt;
            dr["Closed At"] = x.ClosedAt;
            dr["Details"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTicketsClosed.DataSource = dt;
        gvTicketsClosed.DataBind();
    }

}