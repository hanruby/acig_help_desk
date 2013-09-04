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
        _entity = GetEntity();
        currentUserId = CurrentUser.Id();
        lblTotalPending.Text = _entity.Tickets.Where(x => x.State == "Pending").Count().ToString();
        lblTotalResolved.Text = _entity.Tickets.Where(x => x.State == "Resolved").Count().ToString();
        lblTotalClosed.Text = _entity.Tickets.Where(x => x.State == "Closed").Count().ToString();
        if (!CurrentUser.Is_Engineer())
        {
            tblAssigned.Visible = false;
            return;
        }
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   where ut.User_Id == currentUserId
                   group t by t.State into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };
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
    }
}