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
    string _email, _userName;
    bool acntType;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDdlAccountTypes(ddlAccountType);
            UpdateAccountDiv(false, false);
            BindBreadCrumbRepeater("user_new");
            vendorEmailsDiv.Visible = false;
        }
    }

    protected void btnAddNewUser_Click(object sender, EventArgs e)
    {
        _email = txtEmail.Text.Trim();
        _entity = GetEntity();
        _email = txtEmail.Text.Trim();
        _userName = txtUserName.Text.Trim();
        if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
        {
            Session["ErrorMessage"] = "Email already taken!";
            return;
        }
        if (_entity.tbl_Users.Where(x => x.User_Name == _userName).Count() > 0)
        {
            Session["ErrorMessage"] = "User Name already taken!";
            return;
        }
        _user = new tbl_Users();
        _user.User_Name = txtUserName.Text;
        _user.Account_Type = ddlAccountType.SelectedValue;
        _user.Email = _email;
        _user.Active = bool.Parse(ddlActive.SelectedValue);
        _user.Role = ddlRole.SelectedValue;
        _user.Role2 = "user";
        _user.Department_Id = long.Parse(ddlDepartment.SelectedValue);
        _user.Created_At = DateTime.Now;
        _user.Updated_At = DateTime.Now;
        if (ddlRole.SelectedValue == "vendor")
        {
            _user.Vendor_Emails = txtVendorEmails.Text;
        }
        if (_user.Account_Type == Enum_Helper.AccountTypes.NON_ACIG.ToString())
        {
            _user.Role = "vendor";
            _user.Password = String_Helper.Encrypt(txtPassword.Text.Trim());
        }
        _entity.AddTotbl_Users(_user);
        _entity.SaveChanges();
        if (ddlRole.SelectedValue == "engineer")
        {
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
        }
        Session["NoticeMessage"] = "Successfully created new user profile!";
        Response.Redirect(Route.GetRootPath("admin/users/index.aspx"));
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        vendorEmailsDiv.Visible = ddlRole.SelectedValue == "vendor";
        categoryDiv.Visible = ddlRole.SelectedValue == "engineer";
    }

    protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        acntType = ddlAccountType.SelectedValue == Enum_Helper.AccountTypes.NON_ACIG.ToString();
        UpdateAccountDiv(acntType, acntType);
    }

    void UpdateAccountDiv(bool visible, bool enabled)
    {
        txtPassword.Visible = lblPassword.Visible = visible;
        rfvPassword.Enabled = enabled;
    }
    
}