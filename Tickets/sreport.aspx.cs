using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Collections;

public partial class Tickets_sreport : MasterAppPage
{
    string role, role2;
    long deptId;
    Report_User rptUser;
    Report_State rprtState;
    List<Report_User> lstReportUsers, lstReportUsers2;
    string startDate, endDate;
    DateTime startDateT, endDateT;
    Hashtable hash;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            role = CurrentUser.Role();
            role2 = CurrentUser.Role2();
            deptId = CurrentUser.DepartmentId();
            if (role != "supervisor")
            {
                ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
                return;
            }
            BindCreatedGridViewCustom("this_week");
            if (deptId != 1 && deptId != 2)
            {
                divAssigned.Visible = false;
                return;
            }
            BindAssignGridViewCustom("this_week");
        }
    }

    protected void BindAssignGridViewFull()
    {
        lblTicketsAssigned.Text = "Tickets Assigned To My Department Users !!";
        txtStartDate.Text = txtEndDate.Text = string.Empty;
        deptId = CurrentUser.DepartmentId();
        _entity = GetEntity();
        var data2 = from t in _entity.Tickets
                    join ut in _entity.User_Tickets
                    on t.Id equals ut.Ticket_Id
                    join u in _entity.tbl_Users
                    on ut.User_Id equals u.Id
                    where u.Department_Id == deptId
                    group t by new { u.Email, t.State } into Grp
                    select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers2 = new List<Report_User>();
        foreach (var x in data2)
        {
            if (lstReportUsers2.Where(y => y.email == x.State.Email).Count() > 0)
            {
                rptUser = lstReportUsers2.Where(y => y.email == x.State.Email).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers2.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrUserAssigned.DataSource = lstReportUsers2;
        rptrUserAssigned.DataBind();
    }

    protected void BindAssignGridViewCustom(string type)
    {
        deptId = CurrentUser.DepartmentId();
        GetStartAndEndDate(type, "ticket_assigned");
        txtStartDate.Text = startDate;
        txtEndDate.Text = endDate;
        lblTicketsAssigned.Text = "Tickets Assigned To My Department Users From " + startDate + " To " + endDate + " !!";
        _entity = GetEntity();
        var data2 = from t in _entity.Tickets
                    join ut in _entity.User_Tickets
                    on t.Id equals ut.Ticket_Id
                    join u in _entity.tbl_Users
                    on ut.User_Id equals u.Id
                    where u.Department_Id == deptId && t.Created_At >= startDateT && t.Created_At <= endDateT
                    group t by new { u.Email, t.State } into Grp
                    select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers2 = new List<Report_User>();
        foreach (var x in data2)
        {
            if (lstReportUsers2.Where(y => y.email == x.State.Email).Count() > 0)
            {
                rptUser = lstReportUsers2.Where(y => y.email == x.State.Email).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers2.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrUserAssigned.DataSource = lstReportUsers2;
        rptrUserAssigned.DataBind();
    }

    protected void BindCreatedGridViewFull()
    {
        txtStartDateTC.Text = txtEndDateTC.Text = string.Empty;
        lblTicketsCreated.Text = "Tickets Created By My Department Users !!";
        _entity = GetEntity();
        deptId = CurrentUser.DepartmentId();
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where u.Department_Id == deptId
                   group t by new { u.Email, t.State } into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers = new List<Report_User>();
        foreach (var x in data)
        {
            if (lstReportUsers.Where(y => y.email == x.State.Email).Count() > 0)
            {
                rptUser = lstReportUsers.Where(y => y.email == x.State.Email).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrUser.DataSource = lstReportUsers;
        rptrUser.DataBind();
    }

    protected void BindCreatedGridViewCustom(string type)
    {
        GetStartAndEndDate(type, "ticket_created");
        txtStartDateTC.Text = startDate;
        txtEndDateTC.Text = endDate;
        lblTicketsCreated.Text = "Tickets Created By My Department Users From " + startDate + " To " + endDate + " !!";
        _entity = GetEntity();
        deptId = CurrentUser.DepartmentId();
        var data = from t in _entity.Tickets
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   where u.Department_Id == deptId && t.Created_At >= startDateT && t.Created_At <= endDateT
                   group t by new { u.Email, t.State } into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers = new List<Report_User>();
        foreach (var x in data)
        {
            if (lstReportUsers.Where(y => y.email == x.State.Email).Count() > 0)
            {
                rptUser = lstReportUsers.Where(y => y.email == x.State.Email).First();
            }
            else
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).First();
            rprtState.count = x.Count;
        }
        rptrUser.DataSource = lstReportUsers;
        rptrUser.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAssignGridViewCustom("custom");
    }

    protected void lnkBtnThisWeek_Click(object sender, EventArgs e)
    {
        BindAssignGridViewCustom("this_week");
    }

    protected void lnkBtnLastWeek_Click(object sender, EventArgs e)
    {
        BindAssignGridViewCustom("last_week");
    }

    protected void lnkBtnThisMonth_Click(object sender, EventArgs e)
    {
        BindAssignGridViewCustom("this_month");
    }

    protected void lnkBtnLastMonth_Click(object sender, EventArgs e)
    {
        BindAssignGridViewCustom("last_month");
    }

    protected void lnkBtnAll_Click(object sender, EventArgs e)
    {
        BindAssignGridViewFull();
    }

    protected void btnSearchTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewCustom("custom");
    }

    protected void lnkBtnThisWeekTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewCustom("this_week");
    }

    protected void lnkBtnLastWeekTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewCustom("last_week");
    }

    protected void lnkBtnThisMonthTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewCustom("this_month");
    }

    protected void lnkBtnLastMonthTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewCustom("last_month");
    }

    protected void lnkBtnAllTC_Click(object sender, EventArgs e)
    {
        BindCreatedGridViewFull();
    }

    protected void GetStartAndEndDate(string type, string ticket_type)
    {
        DateTime baseDate = DateTime.Today;
        var today = baseDate;
        var yesterday = baseDate.AddDays(-1);
        var thisWeekStart = baseDate.AddDays(-(int)baseDate.DayOfWeek);
        var thisWeekEnd = thisWeekStart.AddDays(7).AddSeconds(-1);
        var lastWeekStart = thisWeekStart.AddDays(-7);
        var lastWeekEnd = thisWeekStart.AddSeconds(-1);
        var thisMonthStart = baseDate.AddDays(1 - baseDate.Day);
        var thisMonthEnd = thisMonthStart.AddMonths(1).AddSeconds(-1);
        var lastMonthStart = thisMonthStart.AddMonths(-1);
        var lastMonthEnd = thisMonthStart.AddSeconds(-1);
        if (type == "this_week")
        {
            startDate = DateTimeHelper.ConvertToString(thisWeekStart.ToString());
            endDate = DateTimeHelper.ConvertToString(thisWeekEnd.ToString());
            startDateT = thisWeekStart;
            endDateT = thisWeekEnd;
        }
        else if (type == "last_week")
        {
            startDate = DateTimeHelper.ConvertToString(lastWeekStart.ToString());
            endDate = DateTimeHelper.ConvertToString(lastWeekEnd.ToString());
            startDateT = lastWeekStart;
            endDateT = lastWeekEnd;
        }
        else if (type == "this_month")
        {
            startDate = DateTimeHelper.ConvertToString(thisMonthStart.ToString());
            endDate = DateTimeHelper.ConvertToString(thisMonthEnd.ToString());
            startDateT = thisMonthStart;
            endDateT = thisMonthEnd;
        }
        else if (type == "last_month")
        {
            startDate = DateTimeHelper.ConvertToString(lastMonthStart.ToString());
            endDate = DateTimeHelper.ConvertToString(lastMonthEnd.ToString());
            startDateT = lastMonthStart;
            endDateT = lastMonthEnd;
        }
        else
        {
            if (ticket_type == "ticket_created")
            {
                startDate = txtStartDateTC.Text;
                endDate = txtEndDateTC.Text;
            }
            else
            {
                startDate = txtStartDate.Text;
                endDate = txtEndDate.Text;
            }
            hash = DateTimeHelper.GetStartAndEndDateByTwoValues(startDate, endDate);
            startDateT = (DateTime)hash["StartDate"];
            endDateT = (DateTime)hash["EndDate"];
        }
    }
}