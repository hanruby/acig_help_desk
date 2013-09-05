using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_full_report : MasterAppPage
{
    string role, role2;
    Report_User rptUser;
    Report_State rprtState;
    List<Report_User> lstReportUsers, lstReportUsers2;
    protected void Page_Load(object sender, EventArgs e)
    {
        role = CurrentUser.Role();
        role2 = CurrentUser.Role2();
        if (role != "manager" && role2 != "admin")
        {
            ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
            return;
        }
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   join d in _entity.Departments
                   on u.Department_Id equals d.Id
                   group t by new { d.Name, t.State } into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers = new List<Report_User>();
        foreach (var x in data)
        {
            if (lstReportUsers.Where(y => y.email == x.State.Name).Count() > 0)
            {
                rptUser = lstReportUsers.Where(y => y.email == x.State.Name).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Name };
                lstReportUsers.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrFull.DataSource = lstReportUsers;
        rptrFull.DataBind();

        var data2 = from t in _entity.Tickets
                    join ut in _entity.User_Tickets
                    on t.Id equals ut.Ticket_Id
                    join u in _entity.tbl_Users
                    on ut.User_Id equals u.Id
                    join d in _entity.Departments
                    on u.Department_Id equals d.Id
                    group t by new { d.Name, t.State } into Grp
                    select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers2 = new List<Report_User>();
        foreach (var x in data2)
        {
            if (lstReportUsers2.Where(y => y.email == x.State.Name).Count() > 0)
            {
                rptUser = lstReportUsers2.Where(y => y.email == x.State.Name).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Name };
                lstReportUsers2.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrFullReportAssigned.DataSource = lstReportUsers2;
        rptrFullReportAssigned.DataBind();
    }
}