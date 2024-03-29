﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Acig_Help_DeskModel;

public partial class Tickets_responded : MasterAppPage
{
    DataTable dt;
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrentUser.Is_Vendor())
            {
                ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
                return;
            }
            BindBreadCrumbRepeater("ticket_responded");
            BindGvOpenTickets();
            BindGvClarifiedTickets();
            BindGvClosedTickets();
            lblOpen.Text = "Pending Tickets!";
            lblClarified.Text = "Clarified Tickets!";
            lblClosed.Text = "Closed Tickets!";
        }
    }

    protected void gvTicketsOpen_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsOpen.PageIndex = e.NewPageIndex;
        BindGvOpenTickets();
    }

    protected void gvTicketsClarified_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsClarified.PageIndex = e.NewPageIndex;
        BindGvClarifiedTickets();
    }

    protected void gvTicketsClosed_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTicketsClosed.PageIndex = e.NewPageIndex;
        BindGvClosedTickets();
    }

    protected void gvTicketsOpen_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void gvTicketsClarified_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var text = e.Row.Cells[7].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[7].Controls.Add((Control)lb);
    }

    protected void gvTicketsClosed_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        var text = e.Row.Cells[7].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[7].Controls.Add((Control)lb);
    }

    protected void GetHeader(string scope)
    {
        dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Subject", typeof(string)));
        dt.Columns.Add(new DataColumn("Open At", typeof(string)));
        if (scope == "clarified")
        {
            dt.Columns.Add(new DataColumn("Clarification Requested At", typeof(string)));
            dt.Columns.Add(new DataColumn("Clarified At", typeof(string)));
        }
        else if (scope == "closed")
        {
            dt.Columns.Add(new DataColumn("Resolved At", typeof(string)));
            dt.Columns.Add(new DataColumn("Closed At", typeof(string)));
        }
        dt.Columns.Add(new DataColumn("Assigned To", typeof(string)));
        dt.Columns.Add(new DataColumn("Category", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));
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
                   where (t.Created_By == currentUserId || t.On_Behalf == currentUserId) && t.State == "Pending"
                   orderby t.Id descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Assigned_To = t.Assigned_To_Emails,
                       Id = t.Id,
                       Subject = t.Subject
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Subject"] = x.Subject;
            dr["Open At"] = x.OpenAt;
            dr["Category"] = x.CategoryName + " >> " + x.SubCategoryName + " >> " + x.SubSubCategoryName;
            dr["Assigned To"] = x.Assigned_To;
            dr["Details"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTicketsOpen.DataSource = dt;
        gvTicketsOpen.DataBind();
    }

    protected void BindGvClarifiedTickets()
    {
        dr = null;
        currentUserId = CurrentUser.Id();
        GetHeader("clarified");
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ssc in _entity.Sub_Sub_Categories
                   on t.Sub_Sub_Category_Id equals ssc.Id
                   join sc in _entity.Sub_Categories
                   on ssc.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   where (t.Created_By == currentUserId || t.On_Behalf == currentUserId) && t.State == "Clarified"
                   orderby t.Created_By descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ClarificationAt = t.Clarification_Date,
                       ClarifiedAt = t.Clarified_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Assigned_To = t.Assigned_To_Emails,
                       Id = t.Id,
                       Subject = t.Subject
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Subject"] = x.Subject;
            dr["Open At"] = x.OpenAt;
            dr["Assigned To"] = x.Assigned_To;
            dr["Category"] = x.CategoryName + " >> " + x.SubCategoryName + " >> " + x.SubSubCategoryName;
            dr["Clarification Requested At"] = x.ClarificationAt;
            dr["Clarified At"] = x.ClarifiedAt;
            dr["Details"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTicketsClarified.DataSource = dt;
        gvTicketsClarified.DataBind();
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
                   where (t.Created_By == currentUserId || t.On_Behalf == currentUserId) && t.State == "Closed"
                   orderby t.Created_By descending
                   select new
                   {
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       SubSubCategoryName = ssc.Name,
                       Assigned_To = t.Assigned_To_Emails,
                       Id = t.Id,
                       Subject = t.Subject
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            dr["ID"] = x.Id;
            dr["Subject"] = x.Subject;
            dr["Open At"] = x.OpenAt;
            dr["Assigned To"] = x.Assigned_To;
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