using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Admin_Users_new : MasterAppPage
{
    long _id;
    tbl_Users _user;
    User_Sub_Sub_Categories _sc;
    string _email;
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        _email = txtEmail.Text.Trim();
        _entity = GetEntity();
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
        if (ddlCategory.SelectedValue == "0" || ddlSubCategory.SelectedValue == "0")
        {
            _user.Category_Id = null;
        }
        else
        {
            _user.Category_Id = long.Parse(ddlCategory.SelectedValue);
        }
        _user.Role = ddlRole.SelectedValue;
        _user.Department_Id = long.Parse(ddlDepartment.SelectedValue);
        _user.Created_At = DateTime.Now;
        _user.Updated_At = DateTime.Now;
        _entity.AddTotbl_Users(_user);
        _entity.SaveChanges();
        foreach (ListItem x in lstBoxSubSubCategory.Items)
        {
            if (x.Selected)
            {
                _sc = new User_Sub_Sub_Categories();
                _sc.User_Id = _user.Id;
                _sc.Sub_Sub_Category_Id = long.Parse(x.Value);
                _entity.AddToUser_Sub_Sub_Categories(_sc);
                _entity.SaveChanges();
            }
        }
        Session["NoticeMessage"] = "Successfully created new user!";
        Response.Redirect(Route.GetRootPath("admin/users/index.aspx"));
    }



    //protected void UpdateUser(object sender, GridViewUpdateEventArgs e)
    //{
    //    ddlCategory = ((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlCategoryEdit")));
    //    _id = long.Parse(((HiddenField)(gvUsers.Rows[e.RowIndex].FindControl("hdnUserId"))).Value.ToString());
    //    _entity = GetEntity();
    //    _user = _entity.tbl_Users.Where(x => x.Id == _id).First();
    //    _email = ((TextBox)(gvUsers.Rows[e.RowIndex].FindControl("txtEmailEdit"))).Text.Trim();
    //    if (_user.Email != _email)
    //    {
    //        if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
    //        {
    //            Session["ErrorMessage"] = "Email already taken!";
    //            return;
    //        }
    //    }
    //    _user.User_Name = ((TextBox)(gvUsers.Rows[e.RowIndex].FindControl("txtUserNameEdit"))).Text;
    //    _user.Email = _email;
    //    _user.Active = bool.Parse(((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlActiveEdit"))).SelectedValue);
    //    _user.Department_Id = long.Parse(((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlDepartmentEdit"))).SelectedValue);
    //    if (ddlCategory.SelectedValue == "0")
    //    {
    //        _user.Category_Id = null;
    //    }
    //    else
    //    {
    //        _user.Category_Id = long.Parse(ddlCategory.SelectedValue);
    //    }
    //    _user.Role = ((DropDownList)(gvUsers.Rows[e.RowIndex].FindControl("ddlRoleEdit"))).SelectedValue;
    //    _user.Updated_At = DateTime.Now;
    //    _entity.SaveChanges();
    //    gvUsers.EditIndex = -1;
    //    BindDataToGridView();
    //}

    //{
    //    _entity = GetEntity();
    //    var txtUserName = ((TextBox)(gvUsers.FooterRow.FindControl("txtUserNameNew")));
    //    var txtEmail = ((TextBox)(gvUsers.FooterRow.FindControl("txtEmailNew")));
    //    var ddlActive = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlActiveNew")));
    //    ddlCategory = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlCategoryNew")));
    //    var ddlRole = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlRoleNew")));
    //    var ddlDeparment = ((DropDownList)(gvUsers.FooterRow.FindControl("ddlDepartmentNew")));
    //    _email = txtEmail.Text.Trim();
    //    if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
    //    {
    //        Session["ErrorMessage"] = "Email already taken!";
    //        return;
    //    }
    //    _user = new tbl_Users();
    //    _user.User_Name = txtUserName.Text;
    //    _user.Email = _email;
    //    _user.Active = bool.Parse(ddlActive.SelectedValue);
    //    if (ddlCategory.SelectedValue == "0")
    //    {
    //        _user.Category_Id = null;
    //    }
    //    else
    //    {
    //        _user.Category_Id = long.Parse(ddlCategory.SelectedValue);
    //    }
    //    _user.Role = ddlRole.SelectedValue;
    //    _user.Department_Id = long.Parse(ddlDeparment.SelectedValue);
    //    _user.Created_At = DateTime.Now;
    //    _user.Updated_At = DateTime.Now;
    //    _entity.AddTotbl_Users(_user);
    //    _entity.SaveChanges();
    //    BindDataToGridView();

    //    txtUserName.Text = txtEmail.Text = string.Empty;
    //    ddlActive.SelectedValue = ddlCategory.SelectedValue = ddlRole.SelectedValue = ddlDeparment.SelectedValue = "0";
}