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
        if (ddlSearchField.SelectedValue == "date")
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
        if (ddlSearchField.SelectedValue != "Assigned To" && ddlSearchField.SelectedValue != "Created By")
        {
            var data = _entity.Tickets.Where(x => x.State == ddlTicketType.SelectedValue);
            switch (ddlSearchField.SelectedValue)
            {
                case "Subject":
                    {
                        data = data.Where(x => x.Subject.Contains(txtString.Text));
                        break;
                    }
                case "date":
                    {
                        DateTime startTime, endTime;
                        DateTimeHelper.GetStartAndEndTime(txtDate.Text, out startTime, out endTime);
                        data = data.Where(x => x.Created_At >= startTime && x.Created_At <= endTime);
                        break;
                    }
            }
            dr = null;
            GetHeader();
        }
        else
        {
            var dd = from t in _entity.Tickets
                     join u in _entity.tbl_Users
                     on t.Created_By equals u.Id
                     where u.User_Name.Contains(txtString.Text) || u.Email.Contains(txtString.Text)
                     orderby t.Id descending
                     select new { CTicket = t, Cuser = u };

        }
    }

    protected void GetHeader()
    {
        dt = new DataTable();
        dt.Columns.Add(new DataColumn("ID", typeof(string)));
        dt.Columns.Add(new DataColumn("Open At", typeof(string)));
        dt.Columns.Add(new DataColumn("Category", typeof(string)));
        dt.Columns.Add(new DataColumn("Open By", typeof(string)));
        dt.Columns.Add(new DataColumn("Assigned To", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));
    }

    protected void SearchBySubject()
    {

    }
}