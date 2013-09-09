using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Tickets_search : MasterAppPage
{
    DataTable dt;
    DataRow dr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateFields(true, false);
        }
    }

    protected void ddlSearchField_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchField.SelectedValue == "Open Date")
        {
            UpdateFields(false, true);
        }
        else
        {
            UpdateFields(true, false);
        }
    }

    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void UpdateFields(bool field1, bool field2)
    {
        txtString.Visible = field1;
        rfvString.Enabled = field1;
        txtDate.Visible = field2;
        rfvDate.Enabled = field2;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _entity = GetEntity();
        if (ddlSearchField.SelectedValue == "Created By")
        {
            SearchByCreatedBy();
        }
        else if (ddlSearchField.SelectedValue == "Assigned To")
        {
            SearchByAssignedTo();
        }
        else if (ddlSearchField.SelectedValue == "Subject")
        {
            SearchBySubject();
        }
        else
        {
            SearchByDate();
        }
    }

    protected void GetHeader()
    {
        dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Open At", typeof(string)));
        dt.Columns.Add(new DataColumn("Category", typeof(string)));
        dt.Columns.Add(new DataColumn("Subject", typeof(string)));
        dt.Columns.Add(new DataColumn("Open By", typeof(string)));
        dt.Columns.Add(new DataColumn("Assigned To", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));
    }

    protected void SearchByCreatedBy()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where ((u.User_Name.Contains(txtString.Text) || u.Email.Contains(txtString.Text)) && t.State == ddlTicketType.SelectedValue)
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " + t.Sub_Sub_Categories.Name), Subject = t.Subject };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchByAssignedTo()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Assigned_To_Emails.Contains(txtString.Text) && t.State == ddlTicketType.SelectedValue
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " + t.Sub_Sub_Categories.Name), Subject = t.Subject };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchBySubject()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Subject.Contains(txtString.Text) && t.State == ddlTicketType.SelectedValue
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " + t.Sub_Sub_Categories.Name), Subject = t.Subject };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchByDate()
    {
        DateTime startTime, endTime;
        DateTimeHelper.GetStartAndEndTime(txtDate.Text, out startTime, out endTime);
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Created_At >= startTime && t.Created_At <= endTime && t.State == ddlTicketType.SelectedValue
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " + t.Sub_Sub_Categories.Name), Subject = t.Subject };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }
}