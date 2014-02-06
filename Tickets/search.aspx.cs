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
            if (CurrentUser.Is_Vendor())
            {
                ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
                return;
            }
            BindBreadCrumbRepeater("search_ticket");
            UpdateFields(true, false, false);
        }
    }

    protected void ddlSearchField_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchField.SelectedValue == "Open Date")
        {
            UpdateFields(false, true, false);
        }
        else if (ddlSearchField.SelectedValue == "Id")
        {
            UpdateFields(true, false, true);
        }
        else
        {
            UpdateFields(true, false, false);
        }
    }

    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void UpdateFields(bool field1, bool field2, bool field3)
    {
        txtString.Visible = field1;
        rfvString.Enabled = field3;
        revId.Enabled = field3;
        txtDate.Visible = field2;
        rfvDate.Enabled = field2;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _entity = GetEntity();
        if (ddlTicketType.SelectedValue == "All" && ddlSearchField.SelectedValue == "All")
        {
            SearchAll();
        }
        else if (ddlTicketType.SelectedValue != "All" && ddlSearchField.SelectedValue == "All")
        {
            SearchByTicketState();
        }
        else if (ddlSearchField.SelectedValue == "Id")
        {
            SearchById();
        }
        else if (ddlSearchField.SelectedValue == "Created By")
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
                   where ((u.User_Name.Contains(txtString.Text) || u.Email.Contains(txtString.Text)))
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, 
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> "
                       + t.Sub_Sub_Categories.Name), Subject = t.Subject, State = t.State };
        if (ddlTicketType.SelectedValue != "All")
        {
            data = data.Where(x => x.State == ddlTicketType.SelectedValue);
        }
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchByAssignedTo()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Assigned_To_Emails.Contains(txtString.Text)
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails,
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " 
                       + t.Sub_Sub_Categories.Name), Subject = t.Subject, State = t.State  };
        if (ddlTicketType.SelectedValue != "All")
        {
            data = data.Where(x => x.State == ddlTicketType.SelectedValue);
        }
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchBySubject()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Subject.Contains(txtString.Text)
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails,
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " 
                       + t.Sub_Sub_Categories.Name), Subject = t.Subject, State = t.State  };
        if (ddlTicketType.SelectedValue != "All")
        {
            data = data.Where(x => x.State == ddlTicketType.SelectedValue);
        }
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
                   where t.Created_At >= startTime && t.Created_At <= endTime
                   orderby t.Id descending
                   select new { Id = t.Id, Opened_At = t.Created_At, Opened_By = u.Email, Assigned_To = t.Assigned_To_Emails, Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> " + t.Sub_Sub_Categories.Name), Subject = t.Subject };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchById()
    {
        var id = int.Parse(txtString.Text);
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.Id == id
                   orderby t.Id descending
                   select new
                   {
                       Id = t.Id,
                       Opened_At = t.Created_At,
                       Opened_By = u.Email,
                       Assigned_To = t.Assigned_To_Emails,
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> "
                       + t.Sub_Sub_Categories.Name),
                       Subject = t.Subject,
                       State = t.State
                   };
        if (ddlTicketType.SelectedValue != "All")
        {
            data = data.Where(x => x.State == ddlTicketType.SelectedValue);
        }
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchAll()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   orderby t.Id descending
                   select new
                   {
                       Id = t.Id,
                       Opened_At = t.Created_At,
                       Opened_By = u.Email,
                       Assigned_To = t.Assigned_To_Emails,
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> "
                       + t.Sub_Sub_Categories.Name),
                       Subject = t.Subject,
                       State = t.State
                   };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }

    protected void SearchByTicketState()
    {
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where t.State == ddlTicketType.SelectedValue
                   orderby t.Id descending
                   select new
                   {
                       Id = t.Id,
                       Opened_At = t.Created_At,
                       Opened_By = u.Email,
                       Assigned_To = t.Assigned_To_Emails,
                       Full_Category = (t.Sub_Sub_Categories.Sub_Categories.Category.Name + " >> " + t.Sub_Sub_Categories.Sub_Categories.Name + " >> "
                       + t.Sub_Sub_Categories.Name),
                       Subject = t.Subject,
                       State = t.State
                   };
        gvTickets.DataSource = data;
        gvTickets.DataBind();
    }
}