using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

public partial class Tickets_report : MasterAppPage
{
    string query, filterCondition;
    int pendingCount, resolvedCount, closedCount, totalCount, count;
    List<TextValue> lstText;
    TextValue objText;
    SqlConnection conn = null;
    SqlCommand cmd = null;
    IDataReader dr = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        BindBreadCrumbRepeater("report");
        _entity = GetEntity();
        currentUserId = CurrentUser.Id();
        lblTotalPending.Text = _entity.Tickets.Where(x => x.State == "Pending" && x.Created_By == currentUserId).Count().ToString();
        lblTotalResolved.Text = _entity.Tickets.Where(x => x.State == "Resolved" && x.Created_By == currentUserId).Count().ToString();
        lblTotalClosed.Text = _entity.Tickets.Where(x => x.State == "Closed" && x.Created_By == currentUserId).Count().ToString();
        lblTotalCount.Text = (long.Parse(lblTotalPending.Text) + long.Parse(lblTotalResolved.Text) + long.Parse(lblTotalClosed.Text)).ToString();
        if (!CurrentUser.Is_Engineer())
        {
            divEngineer1.Visible = false;
            return;
        }
        AssignPendingQuery();
        ExecuteQuery();
        AssignResolvedQuery();
        ExecuteQuery();
        lblAssignedPending.Text = lblAssignedResolved.Text = lblAssignedClosed.Text = "0";
        lblAssignedPending.Text = pendingCount.ToString();
        lblAssignedResolved.Text = resolvedCount.ToString();
        lblAssignedClosed.Text = closedCount.ToString();
        lblAssignedTotal.Text = totalCount.ToString();
        AssignDeptPendingQuery();
        ExecuteQuery(true);
        AssignDeptResolvedQuery();
        ExecuteQuery(true);
        rptrDeptTickets.DataSource = lstText;
        rptrDeptTickets.DataBind();
    }

    void AssignPendingQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Id='" + currentUserId + "'";
        query = "SELECT t.State, COUNT(*) AS Tickets_Count FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID WHERE e.State = 'Open') evc ON t.Id = evc.Ticket_Id " +
            "INNER JOIN User_Tickets ut ON t.Id = ut.Ticket_Id INNER JOIN tbl_Users r " +
            "ON ut.User_Id = r.Id " +
            "{0} GROUP BY r.Id, t.State";
        query = string.Format(query, filterCondition);
    }

    void AssignResolvedQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Id='" + currentUserId + "'";
        query = "SELECT t.State, COUNT(*) AS Tickets_Count FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events WHERE State = 'Resolved' GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID) evc ON t.Id = evc.Ticket_Id INNER JOIN tbl_Users r ON evc.Created_By = r.Id " +
            "{0} GROUP BY r.Id, t.State";
        query = string.Format(query, filterCondition);
    }

    void AssignDeptPendingQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Id='" + currentUserId + "'";
        query = "SELECT d.Name, COUNT(*) AS Tickets_Count FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID WHERE e.State = 'Open') evc ON t.Id = evc.Ticket_Id " +
            "INNER JOIN User_Tickets ut ON t.Id = ut.Ticket_Id INNER JOIN tbl_Users r " +
            "ON ut.User_Id = r.Id INNER JOIN tbl_Users c ON t.Created_By = c.Id " +
            "INNER JOIN departments d ON c.Department_Id = d.Id " + 
            "{0} GROUP BY d.Name";
        query = string.Format(query, filterCondition);
    }

    void AssignDeptResolvedQuery(bool filter = false)
    {
        filterCondition = "WHERE r.Id='" + currentUserId + "'";
        query = "SELECT d.Name, COUNT(*) AS Tickets_Count FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events WHERE State = 'Resolved' GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID) evc ON t.Id = evc.Ticket_Id INNER JOIN tbl_Users r ON evc.Created_By = r.Id " +
            "INNER JOIN tbl_Users c ON t.Created_By = c.Id " +
            "INNER JOIN departments d ON c.Department_Id = d.Id " + 
            "{0} GROUP BY d.Name";
        query = string.Format(query, filterCondition);
    }

    void ExecuteQuery(bool forDept = false)
    {
        totalCount = 0;
        lstText = new List<TextValue>();
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Acig_Help_DeskConnectionString"].ToString());
        cmd = new SqlCommand();
        try
        {
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.CommandText = query;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (forDept)
                {
                    objText = lstText.Where(x => x.Text == dr["Name"].ToString()).FirstOrDefault();
                    if (objText == null)
                    {
                        objText = new TextValue { Text = dr["Name"].ToString(), Value = "0" };
                    }
                    objText.Value = (int.Parse(dr["Tickets_Count"].ToString()) + int.Parse(objText.Value)).ToString();
                    lstText.Add(objText);
                }
                else
                {
                    count = 0;
                    if (dr["State"].ToString() == "Pending")
                    {
                        count = int.Parse(dr["Tickets_Count"].ToString());
                        pendingCount += count;
                    }
                    else if (dr["State"].ToString() == "Resolved")
                    {
                        count = int.Parse(dr["Tickets_Count"].ToString());
                        resolvedCount += count;
                    }
                    else if (dr["State"].ToString() == "Closed")
                    {
                        count = int.Parse(dr["Tickets_Count"].ToString());
                        closedCount += count;
                    }
                    totalCount += count;
                }
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