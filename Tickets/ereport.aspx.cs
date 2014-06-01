using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Tickets_ereport : MasterAppPage
{
    string role, role2, query, filterCondition;
    Report_User rptUser;
    Report_State rprtState;
    List<Report_User> lstReportUsers;
    string startDate, endDate;
    DateTime startDateT, endDateT;
    Hashtable hash;
    SqlConnection conn = null;
    SqlCommand cmd = null;
    IDataReader dr = null;
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

        lstReportUsers = new List<Report_User>();
        AssignPendingQuery();
        ExecuteQuery();
        AssignResolvedQuery();
        ExecuteQuery();
        rptrFull.DataSource = lstReportUsers;
        rptrFull.DataBind();
    }

    protected void BindGridViewCustom(string type)
    {
        GetStartAndEndDate(type, "ticket_created");
        txtStartDateTC.Text = startDate;
        txtEndDateTC.Text = endDate;
        lblTicketsCreated.Text = "Tickets Assigned to Engineer From " + startDate + " To " + endDate + " !!";

        lstReportUsers = new List<Report_User>();
        AssignPendingQuery(true);
        ExecuteQuery(true);
        AssignResolvedQuery(true);
        ExecuteQuery(true);
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

    void AssignPendingQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Active = 'True' ";
        filterCondition += (filter ? "AND evc.Created_At >= @Start_Date AND evc.Created_At <= @End_Date" : string.Empty);
        query = "SELECT t.State, r.User_Name, evc.Created_At FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id, e.State, e.Created_At FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID WHERE e.State = 'Open') evc ON t.Id = evc.Ticket_Id " +
            "INNER JOIN User_Tickets ut ON t.Id = ut.Ticket_Id INNER JOIN tbl_Users r " +
            "ON ut.User_Id = r.Id {0} ORDER BY t.Id, r.User_Name";
        query = string.Format(query, filterCondition);
    }

    void AssignResolvedQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Active = 'True' ";
        filterCondition += (filter ? "AND evc.Created_At >= @Start_Date AND evc.Created_At <= @End_Date" : string.Empty);
        query = "SELECT t.State, r.User_Name, evc.Created_At FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id, e.State, e.Created_At FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events WHERE State = 'Resolved' GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID) evc ON t.Id = evc.Ticket_Id INNER JOIN tbl_Users r ON evc.Created_By = r.Id " +
            "{0} ORDER BY t.Id, r.User_Name";
        query = string.Format(query, filterCondition);
    }

    void ExecuteQuery(bool filter = false)
    {
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Acig_Help_DeskConnectionString"].ToString());
        cmd = new SqlCommand();
        try
        {
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.CommandText = query;
            if (filter)
            {
                cmd.Parameters.AddWithValue("@Start_Date", startDateT);
                cmd.Parameters.AddWithValue("@End_Date", endDateT);
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rptUser = lstReportUsers.Where(y => y.email == dr["User_Name"].ToString()).FirstOrDefault();
                if (rptUser == null)
                {
                    rptUser = new Report_User { email = dr["User_Name"].ToString() };
                    lstReportUsers.Add(rptUser);
                }
                rprtState = rptUser.states.Where(y => y.state == dr["State"].ToString()).FirstOrDefault();
                if (rprtState == null)
                {
                    rprtState = new Report_State { state = dr["State"].ToString(), count = 0 };
                    rptUser.states.Add(rprtState);
                }
                rprtState.count += 1;
            }
        }
        catch
        {

        }
        finally
        {
            cmd.Parameters.Clear();
            conn.Close();
        }
    }
}