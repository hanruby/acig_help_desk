using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_report : MasterAppPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HideReportLinks(lnkBtnReportSuperVisor, lnkBtnFullReport, lnkBtnReportByUser);
        _entity = GetEntity();
        currentUserId = CurrentUser.Id();
        lblTotalPending.Text = _entity.Tickets.Where(x => x.State == "Pending" && x.Created_By == currentUserId).Count().ToString();
        lblTotalResolved.Text = _entity.Tickets.Where(x => x.State == "Resolved" && x.Created_By == currentUserId).Count().ToString();
        lblTotalClosed.Text = _entity.Tickets.Where(x => x.State == "Closed" && x.Created_By == currentUserId).Count().ToString();
        lblTotalCount.Text = (long.Parse(lblTotalPending.Text) + long.Parse(lblTotalResolved.Text) + long.Parse(lblTotalClosed.Text)).ToString();
        if (!CurrentUser.Is_Engineer())
        {
            divEngineer1.Visible = false;
            divEngineer2.Visible = false;
            return;
        }
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   where ut.User_Id == currentUserId
                   group t by t.State into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };
        lblAssignedPending.Text = lblAssignedResolved.Text = lblAssignedClosed.Text = "0";
        foreach (var x in data)
        {
            if (x.State == "Pending")
            {
                lblAssignedPending.Text = x.Count.ToString();
            }
            else if (x.State == "Resolved")
            {
                lblAssignedResolved.Text = x.Count.ToString();
            }
            else
            {
                lblAssignedClosed.Text = x.Count.ToString();
            }
        }
        lblAssignedTotal.Text = (long.Parse(lblAssignedPending.Text) + long.Parse(lblAssignedResolved.Text) + long.Parse(lblAssignedClosed.Text)).ToString();
        var data2 = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   join d in _entity.Departments
                   on u.Department_Id equals d.Id
                   where ut.User_Id == currentUserId
                   group t by d.Name into Grp
                   select new { Count = Grp.Count(), Department = Grp.Key };
        rptrDeptTickets.DataSource = data2;
        rptrDeptTickets.DataBind();
    }
}