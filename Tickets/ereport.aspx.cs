using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Tickets_ereport : MasterAppPage
{
    string role, role2;
    Report_User rptUser;
    Report_State rprtState;
    List<Report_User> lstReportUsers;
    string startDate, endDate;
    DateTime startDateT, endDateT;
    Hashtable hash;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("e_report");
            role = CurrentUser.Role();
            role2 = CurrentUser.Role2();
            if (role != "manager" && role2 != "admin")
            {
                ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
                return;
            }
            BindGridViewCustom("this_week");
        }
    }

    protected void BindGridViewFull()
    {
        lblTicketsCreated.Text = "Tickets Assigned To Engineer !!";
        txtStartDateTC.Text = string.Empty;
        txtEndDateTC.Text = string.Empty;
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   group t by new { u.Email, t.State } into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers = new List<Report_User>();
        foreach (var x in data)
        {
            rptUser = lstReportUsers.Where(y => y.email == x.State.Email).FirstOrDefault();
            if (rptUser == null)
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).FirstOrDefault();
            if (rprtState != null)
            {
                rprtState.count = x.Count;
            }
        }
        rptrFull.DataSource = lstReportUsers;
        rptrFull.DataBind();
    }

    protected void BindGridViewCustom(string type)
    {
        GetStartAndEndDate(type, "ticket_created");
        txtStartDateTC.Text = startDate;
        txtEndDateTC.Text = endDate;
        lblTicketsCreated.Text = "Tickets Assigned to Engineer From " + startDate + " To " + endDate + " !!";
        _entity = GetEntity();
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   where t.Created_At >= startDateT && t.Created_At <= endDateT
                   group t by new { u.Email, t.State } into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };

        lstReportUsers = new List<Report_User>();
        foreach (var x in data)
        {
            rptUser = lstReportUsers.Where(y => y.email == x.State.Email).FirstOrDefault();
            if (rptUser == null)
            {
                rptUser = new Report_User { email = x.State.Email };
                lstReportUsers.Add(rptUser);
            }
            rprtState = rptUser.states.Where(y => y.state == x.State.State).FirstOrDefault();
            if (rprtState != null)
            {
                rprtState.count = x.Count;
            }
        }
        rptrFull.DataSource = lstReportUsers;
        rptrFull.DataBind();
    }

    protected void btnSearchTC_Click(object sender, EventArgs e)
    {
        BindGridViewCustom("custom");
    }

    protected void lnkBtnThisWeekTC_Click(object sender, EventArgs e)
    {
        BindGridViewCustom("this_week");
    }

    protected void lnkBtnLastWeekTC_Click(object sender, EventArgs e)
    {
        BindGridViewCustom("last_week");
    }

    protected void lnkBtnThisMonthTC_Click(object sender, EventArgs e)
    {
        BindGridViewCustom("this_month");
    }

    protected void lnkBtnLastMonthTC_Click(object sender, EventArgs e)
    {
        BindGridViewCustom("last_month");
    }

    protected void lnkBtnAllTC_Click(object sender, EventArgs e)
    {
        BindGridViewFull();
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
            startDate = txtStartDateTC.Text;
            endDate = txtEndDateTC.Text;
            hash = DateTimeHelper.GetStartAndEndDateByTwoValues(startDate, endDate);
            startDateT = (DateTime)hash["StartDate"];
            endDateT = (DateTime)hash["EndDate"];
        }
    }
}