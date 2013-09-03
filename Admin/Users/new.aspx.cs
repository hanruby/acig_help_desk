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
        _user.Role = ddlRole.SelectedValue;
        _user.Role2 = "user";
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
        Session["NoticeMessage"] = "Successfully created new user profile!";
        Response.Redirect(Route.GetRootPath("admin/users/index.aspx"));
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlRole.SelectedValue == "engineer")
        {
            lstBoxSubSubCategory.Visible = true;
        }
        else
        {
            lstBoxSubSubCategory.Visible = false;
        }
    }
}