using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Tickets_assign_vendor : MasterAppPage
{
    long id, subCatId;
    Ticket ticket;
    User_Tickets userTicket;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CurrentUser.Is_Admin())
        {
            ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access!");
            return;
        }
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("assign_vendor");
            Reset();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        id = long.Parse(txtSearch.Text);
        _entity = GetEntity();
        ticket = _entity.Tickets.Where(x => x.Id == id).FirstOrDefault();
        if (ticket == null)
        {
            Reset();
            return;
        }
        hdnFldTicketId.Value = ticket.Id.ToString();
        lblId.Text = "Id# " + ticket.Id.ToString();
        lblState.Text = "State# " + ticket.State;
        lblCreatedBy.Text = "Created By# " + ticket.tbl_Users.User_Name;
        lblAssignedTo.Text = "Currently Assigned to# " + ticket.Assigned_To_Emails;
        reassignDiv.Visible = true;
        BindDdlAssignTo();
    }

    private void BindCategories(object sender, EventArgs e)
    {
    }

    protected void BindDdlAssignTo()
    {
        subCatId = ticket.Sub_Sub_Category_Id;
        var lst = ticket.User_Tickets.ToList().Select(x => x.User_Id);
        var data = from u in _entity.tbl_Users
                   //join us in _entity.User_Sub_Sub_Categories
                   //on u.Id equals us.User_Id
                   where u.Role == "vendor" && u.Active == true
                   select new { UserId = u.Id, UserName = u.User_Name };
        DataTable table = new DataTable();
        table.Columns.Add("Text");
        table.Columns.Add("Value");
        DataRow dr;
        dr = table.NewRow();
        dr["Text"] = "Select";
        dr["Value"] = "0";
        table.Rows.Add(dr);
        foreach (var x in data)
        {
            dr = table.NewRow();
            dr["Text"] = x.UserName;
            dr["Value"] = x.UserId;
            table.Rows.Add(dr);
        }
        ddlAssignTo.DataSource = table;
        ddlAssignTo.DataTextField = table.Columns["Text"].ColumnName;
        ddlAssignTo.DataValueField = table.Columns["Value"].ColumnName;
        ddlAssignTo.DataBind();
        ddlAssignTo.SelectedIndexChanged += new System.EventHandler(BindCategories);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        id = long.Parse(hdnFldTicketId.Value);
        var vendorId = long.Parse(ddlAssignTo.SelectedValue);
        _entity = GetEntity();
        ticket = _entity.Tickets.Where(x => x.Id == id).First();
        string emails = string.Empty;
        var newUserLst = new List<long>();
        var deleteUserLst = new List<long>();

        foreach (var x in ticket.User_Tickets)
        {
            if (x.tbl_Users.Role == "vendor")
            {
                deleteUserLst.Add(x.User_Id);
            }
        }

        deleteUserLst.Add(vendorId);
        foreach (var x in deleteUserLst)
        {
            userTicket = _entity.User_Tickets.Where(y => y.User_Id == x && y.Ticket_Id == ticket.Id).FirstOrDefault();
            if (userTicket != null)
            {
                _entity.DeleteObject(userTicket);
                _entity.SaveChanges();
            }
        }

        newUserLst.Add(vendorId);

        foreach (var x in newUserLst)
        {
            userTicket = new User_Tickets
            {
                Ticket_Id = ticket.Id,
                User_Id = x
            };
            _entity.AddToUser_Tickets(userTicket);
            _entity.SaveChanges();
        }

        foreach (var x in ticket.User_Tickets)
        {
            emails += x.tbl_Users.Email + ", ";
        }
        ticket.Assigned_To_Emails = emails;
        _entity.SaveChanges();

        HtmlEmailer emailer = new HtmlEmailer(_entity, ticket);
        emailer.Assign_Vendor_Ticket_EMail(_entity.tbl_Users.Where(x => x.Id == vendorId).First());
        SuccessRedirect(Route.GetRootPath("tickets/assign_vendor.aspx"), "Successfully assigned !");
    }

    protected void Reset()
    {
        hdnFldTicketId.Value = string.Empty;
        lblId.Text = string.Empty;
        lblState.Text = string.Empty;
        lblCreatedBy.Text = string.Empty;
        lblAssignedTo.Text = string.Empty;
        reassignDiv.Visible = false;
    }
}