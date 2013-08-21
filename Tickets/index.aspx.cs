using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Tickets_index : MasterAppPage
{
    string _scope, _customScope;
    DataTable dt;
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        dr = null;
        _scope = Request.QueryString["scope"];
        if (_scope == "closed")
        {
            _customScope = "Closed";
            hdnFldScope.Value = _scope;
        }
        else if (_scope == "resolved")
        {
            hdnFldScope.Value = _scope;
            _customScope = "Resolved";
        }
        else
        {
            _scope = "pending";
            hdnFldScope.Value = _scope;
            _customScope = "Open";
        }
        currentUserId = CurrentUser.Id();
        GetHeader();
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   //join u in _entity.tbl_Users
                   //on t.Assigned_To equals u.Id
                   join sc in _entity.Sub_Categories
                   on t.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   where (t.Created_By == currentUserId) && t.State == _scope
                   orderby t.Created_By
                   select new
                   {
                       //AssignTo = u.Email,
                       OpenAt = t.Created_At,
                       ResolvedAt = t.Resolved_Date,
                       ClosedAt = t.Closed_Date,
                       CategoryName = c.Name,
                       SubCategoryName = sc.Name,
                       Id = t.Id
                   };
        foreach (var x in data)
        {
            dr = dt.NewRow();
            //dr["Assigned To"] = x.AssignTo;
            dr["Open At"] = x.OpenAt;
            if (_scope == "resolved" || _scope == "closed")
            {
                dr["Resolved At"] = x.ResolvedAt;
            }
            if (_scope == "closed")
            {
                dr["Closed At"] = x.ClosedAt;
            }
            dr["Details"] = x.Id;
            dt.Rows.Add(dr);
        }
        gvTickets.DataSource = dt;
        gvTickets.DataBind();
        if (_customScope == "Open")
        {
            _customScope = "Pending";
        }
        lblHeading.Text = _customScope + " Tickets";
    }

    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.DataRow) return;
        int index = 2;
        if (hdnFldScope.Value == "resolved")
        {
            index = 3;
        }
        else if (hdnFldScope.Value == "closed")
        {
            index = 4;
        }
        var text = e.Row.Cells[index].Text;
        LinkButton lb;
        lb = new LinkButton();
        lb.CommandArgument = text;
        lb.CommandName = "NumClick";
        lb.Text = "Details";
        lb.PostBackUrl = "show.aspx?id=" + text;
        lb.CssClass = "blue-link";
        e.Row.Cells[index].Controls.Add((Control)lb);
    }

    protected void GetHeader()
    {
        dt = new DataTable();
        //dt.Columns.Add(new DataColumn("Assigned To", typeof(string)));
        dt.Columns.Add(new DataColumn("Open At", typeof(string)));
        if (_scope == "resolved")
        {
            dt.Columns.Add(new DataColumn("Resolved At", typeof(string)));
        }
        else if (_scope == "closed")
        {
            dt.Columns.Add(new DataColumn("Resolved At", typeof(string)));
            dt.Columns.Add(new DataColumn("Closed At", typeof(string)));
        }
        dt.Columns.Add(new DataColumn("Details", typeof(string)));
    }
}