using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Admin_Users_index : MasterAppPage
{
    long _id;
    DataTable table;
    DataRow dr;
    tbl_Users _user;
    DropDownList ddlCategory;
    string _email;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDataToGridView();
        }
    }

    protected void EditUser(object sender, GridViewEditEventArgs e)
    {
        gvUsers.EditIndex = e.NewEditIndex;
        BindDataToGridView();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvUsers.EditIndex = -1;
        BindDataToGridView();
    }

    protected void UpdateUser(object sender, GridViewUpdateEventArgs e)
    {
        ddlCategory = ((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlCategoryEdit")));
        _id = long.Parse(((HiddenField)(gvUsers.Rows[e.RowIndex].FindControl("hdnUserId"))).Value.ToString());
        _entity = GetEntity();
        _user = _entity.tbl_Users.Where(x => x.Id == _id).First();
        _email = ((TextBox)(gvUsers.Rows[e.RowIndex].FindControl("txtEmailEdit"))).Text.Trim();
        if (_user.Email != _email)
        {
            if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
            {
                Session["ErrorMessage"] = "Email already taken!";
                return;
            }
        }
        _user.User_Name = ((TextBox)(gvUsers.Rows[e.RowIndex].FindControl("txtUserNameEdit"))).Text;
        _user.Email = _email;
        _user.Active = bool.Parse(((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlActiveEdit"))).SelectedValue);
        if (ddlCategory.SelectedValue == "0")
        {
            _user.Category_Id = null;
        }
        else
        {
            _user.Category_Id = long.Parse(ddlCategory.SelectedValue);
        }
        _user.Role = ((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlRoleEdit"))).SelectedValue;
        _user.Updated_At = DateTime.Now;
        _entity.SaveChanges();
        gvUsers.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _entity = GetEntity();
        var data = from u in _entity.tbl_Users
                   join c in _entity.Categories
                   on u.Category_Id equals c.Id into cu
                   from x in cu.DefaultIfEmpty()
                   select new
                   {
                       Id = u.Id,
                       User_Name = u.User_Name,
                       Email = u.Email,
                       Category_Id = x == null ? 0 : x.Id,
                       Role = u.Role,
                       Active = u.Active,
                       Role_Text = u.Role == "admin" ? "Admin" : "Normal",
                       Category_Name = x.Name
                   };
        gvUsers.DataSource = data;
        gvUsers.DataBind();
    }

    protected void RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && gvUsers.EditIndex == e.Row.RowIndex)
        {
            DropDownList ddlCategories = (DropDownList)e.Row.FindControl("ddlCategoryEdit");
            HiddenField hdnFldId = (HiddenField)e.Row.FindControl("hdnFldCategoryId");
            BindCategoriesDdl(ddlCategories);
            ddlCategories.SelectedValue = hdnFldId.Value;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            BindCategoriesDdl((DropDownList)e.Row.FindControl("ddlCategoryNew"));
        }
    }

    private void BindCategories(object sender, EventArgs e)
    {
    }

    void BindCategoriesDdl(DropDownList ddl)
    {
        _entity = GetEntity();
        table = new DataTable();
        table.Columns.Add("Text");
        table.Columns.Add("Value");
        dr = table.NewRow();
        dr["Text"] = "Select";
        dr["Value"] = 0;
        table.Rows.Add(dr);
        foreach (var x in _entity.Categories)
        {
            dr = table.NewRow();
            dr["Text"] = x.Name;
            dr["Value"] = x.Id.ToString();
            table.Rows.Add(dr);
        }
        ddl.DataSource = table;
        ddl.DataTextField = table.Columns["Text"].ColumnName;
        ddl.DataValueField = table.Columns["Value"].ColumnName;
        ddl.DataBind();
        ddl.SelectedIndexChanged += new System.EventHandler(BindCategories);
    }

    protected void AddNewUser(object sender, EventArgs e)
    {
        _entity = GetEntity();
        var txtUserName = ((TextBox)(gvUsers.FooterRow.FindControl("txtUserNameNew")));
        var txtEmail = ((TextBox)(gvUsers.FooterRow.FindControl("txtEmailNew")));
        var ddlActive = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlActiveNew")));
        ddlCategory = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlCategoryNew")));
        var ddlRole = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlRoleNew")));
        _email = txtEmail.Text.Trim();
        if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
        {
            Session["ErrorMessage"] = "Email already taken!";
            return;
        }
        _user = new tbl_Users();
        _user.User_Name = txtUserName.Text;
        _user.Email = _email;
        _user.Active = bool.Parse(ddlActive.SelectedValue);
        if (ddlCategory.SelectedValue == "0")
        {
            _user.Category_Id = null;
        }
        else
        {
            _user.Category_Id = long.Parse(ddlCategory.SelectedValue);
        }
        _user.Role = ddlRole.SelectedValue;
        _user.Created_At = DateTime.Now;
        _user.Updated_At = DateTime.Now;
        _entity.AddTotbl_Users(_user);
        _entity.SaveChanges();
        BindDataToGridView();

        txtUserName.Text = txtEmail.Text = string.Empty;
        ddlActive.SelectedValue = ddlCategory.SelectedValue = ddlRole.SelectedValue = "0";
    }
}