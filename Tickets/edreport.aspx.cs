using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Acig_Help_DeskModel;
using System.Data.SqlClient;
using System.Configuration;

public partial class Tickets_edreport : MasterAppPage
{
    SqlConnection conn = null;
    SqlCommand cmd = null;
    IDataReader dataReader = null;
    CustomReport objCustom;
    List<CustomReport> lstCustom;
    string query, filterCondition;
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
            BindDdlUsersRoot(ddlEngineer, "engineer");
        }
    }

    protected void gvTickets_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvTickets.PageIndex = e.NewPageIndex;
        ExecuteQuery();
    }

    protected void gvTickets_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        ExecuteQuery();
    }

    void AssignPendingQuery()
    {
        query = "SELECT t.Id, t.Created_At, t.Subject, t.State, r.User_Name as 'Engineer', c.User_Name as 'User', " +
            "cat.Name + ' >>> ' + sc.Name + ' >>> ' + ssc.Name as 'Category' FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id, e.State, e.Created_At FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID WHERE e.State = 'Open') evc ON t.Id = evc.Ticket_Id " +
            "INNER JOIN User_Tickets ut ON t.Id = ut.Ticket_Id " +
            "INNER JOIN tbl_Users r ON ut.User_Id = r.Id " +
            "INNER JOIN tbl_Users c on t.Created_By = c.Id " +
            "INNER JOIN Sub_Sub_Categories ssc ON t.Sub_Sub_Category_Id = ssc.Id " +
            "INNER JOIN Sub_Categories sc ON ssc.Sub_Category_Id = sc.Id " +
            "INNER JOIN Categories cat ON sc.Category_Id = cat.Id " +
            "{0} ORDER BY t.Id, r.User_Name";
        query = string.Format(query, filterCondition);
    }

    void AssignResolvedQuery(bool filter = false)
    {
        query = "SELECT t.Id, t.Created_At, t.Subject, t.State, r.User_Name as 'Engineer', c.User_Name as 'User', " +
            "cat.Name + ' >>> ' + sc.Name + ' >>> ' + ssc.Name as 'Category' FROM TICKETS t INNER JOIN " +
            "(SELECT e.Created_By, e.Ticket_Id, e.State, e.Created_At FROM events e INNER JOIN " +
            "(SELECT MAX(id) as CID FROM events WHERE State = 'Resolved' GROUP BY ticket_id  ) ec " +
            "ON e.Id = ec.CID) evc ON t.Id = evc.Ticket_Id INNER JOIN tbl_Users r ON evc.Created_By = r.Id " +
            "INNER JOIN tbl_Users c on t.Created_By = c.Id " +
            "INNER JOIN Sub_Sub_Categories ssc ON t.Sub_Sub_Category_Id = ssc.Id " +
            "INNER JOIN Sub_Categories sc ON ssc.Sub_Category_Id = sc.Id " +
            "INNER JOIN Categories cat ON sc.Category_Id = cat.Id " +
            "{0} ORDER BY t.Id, r.User_Name";
        query = string.Format(query, filterCondition);
    }

    void ExecuteQuery(bool filter = false)
    {
        _entity = GetEntity();
        filterCondition = "WHERE r.Active = 'True' AND t.State = '" + ddlTicketStatus.SelectedValue + "'";
        if (ddlEngineer.SelectedValue != "0")
        {
            filterCondition += " AND r.Id = '" + ddlEngineer.SelectedValue + "'";
        }
        if (ddlTicketStatus.SelectedValue == "Pending")
        {
            AssignPendingQuery();
        }
        else
        {
            AssignResolvedQuery();
        }
        lstCustom = new List<CustomReport>();
        conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Acig_Help_DeskConnectionString"].ToString());
        cmd = new SqlCommand();
        try
        {
            conn.Open();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.CommandText = query;
            dataReader = cmd.ExecuteReader();
            while (dataReader.Read())
            {
                objCustom = new CustomReport();
                objCustom.AssignedTo = dataReader["Engineer"].ToString();
                objCustom.Category = dataReader["Category"].ToString();
                objCustom.CreatedAt = dataReader["Created_At"].ToString();
                objCustom.CreatedBy = dataReader["User"].ToString();
                objCustom.Id = int.Parse(dataReader["Id"].ToString());
                objCustom.Status = dataReader["State"].ToString();
                objCustom.Subject = dataReader["Subject"].ToString();
                lstCustom.Add(objCustom);
            }
            gvTickets.DataSource = lstCustom;
            gvTickets.DataBind();
        }
        catch
        {

        }
        finally
        {
            conn.Close();
        }
    }
}