using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Users_auto_login : MasterAppPage
{
    string routePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        var user = CurrentUser.User();
        if (user.Email != "ubaid@acig.com.sa")
        {
            ErrorRedirect(Route.GetRootPath("") + "not_authorized.aspx", "Not authorized to access that page !!");
            return;
        }
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("user_auto_login");
            BindDataToGridView();
        }
    }

    protected void BindDataToGridView()
    {
        routePath = Route.GetRootPath("admin/users/edit.aspx?id=");
        _entity = GetEntity();
        var data = from u in _entity.tbl_Users
                   orderby u.User_Name
                   select new
                   {
                       Id = u.Id,
                       User_Name = u.User_Name,
                       Email = u.Email,
                       Role = u.Role,
                       Active = u.Active,
                       Role_Text = u.Role,
                       Department_Text = u.Department.Name,
                       Department_Id = u.Department_Id
                   };
        gvUsers.DataSource = data;
        gvUsers.DataBind();
    }

    protected void lnkBtnLogin_Click(object sender, EventArgs e)
    {
        LinkButton btn = (LinkButton)(sender);
        var id = int.Parse(btn.CommandArgument);
        _entity = GetEntity();
        SetLogout();
        var user =  _entity.tbl_Users.Where(x => x.Id == id).First();
        string returnUrl = SetLogin(user);
        Session["NoticeMessage"] = "Successfully logged in !";
        Response.Redirect(returnUrl);
    }   
}