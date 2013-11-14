using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Account_create_profile : MasterAppPage
{
    tbl_Users _user;
    string _email;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            errorDiv.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _email = txtEmail.Text.Trim();
        _entity = GetEntity();
        _email = txtEmail.Text.Trim();
        if (_entity.tbl_Users.Where(x => x.Email == _email).Count() > 0)
        {
            errorDiv.Visible = true;
            return;
        }
        _user = new tbl_Users();
        _user.User_Name = txtUserName.Text;
        _user.Email = _email;
        _user.Active = true;
        _user.Role = "user";
        _user.Role2 = "user";
        _user.Department_Id = long.Parse(ddlDepartment.SelectedValue);
        _user.Created_At = DateTime.Now;
        _user.Updated_At = DateTime.Now;
        _entity.AddTotbl_Users(_user);
        _entity.SaveChanges();
        HtmlEmailer.New_User_Sign_Up(_entity, _user);
        Session["NoticeMessage"] = "Successfully created profile, now you can login!";
        Response.Redirect(Route.GetRootPath("account/login.aspx"));
    }
}