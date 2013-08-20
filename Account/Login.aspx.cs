using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.DirectoryServices;
using Acig_Help_DeskModel;

public partial class Account_Login : MasterAppPage
{
    tbl_Users user;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.Identity.IsAuthenticated && Request.QueryString["ReturnUrl"] != null)
        {
            Response.Redirect(Route.GetRootPath("not_authorized.aspx"));
        }
        else if (User.Identity.IsAuthenticated)
        {
            Response.Redirect(CurrentUser.GetRedirectPath(CurrentUser.Role()));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        user = ActiveDirectoryAuthentication();
        if (user == null)
        {
            Session["ErrorMessage"] = "Username or Password is incorrect !";
            return;
        }
        FormsAuthentication.SetAuthCookie(user.User_Name, false);
        FormsAuthenticationTicket ticket1 =
           new FormsAuthenticationTicket(
                1,                                   // version
                user.User_Name,   // get username  from the form
                DateTime.Now,                        // issue time is now
                DateTime.Now.AddHours(11),         // expires in 10 minutes
                false,      // cookie is not persistent
                user.Id.ToString() + "#" + user.Role // role assignment is stored
                );
        HttpCookie cookie1 = new HttpCookie(
          FormsAuthentication.FormsCookieName,
          FormsAuthentication.Encrypt(ticket1));
        Response.Cookies.Add(cookie1);
        string returnUrl = CurrentUser.GetRedirectPath(user.Role);
        Session["NoticeMessage"] = "Successfully logged in !";
        Response.Redirect(returnUrl);
    }

    protected tbl_Users ActiveDirectoryAuthentication()
    {
        DirectoryEntry d1 = new DirectoryEntry("LDAP://acig.local/dc=acig,dc=local", txtUserName.Text, txtPassword.Text, AuthenticationTypes.Secure);
        try
        {
            DirectorySearcher ds = new DirectorySearcher(d1);
            ds.FindOne();
            ds.Filter = "(sAMAccountName=" + txtUserName.Text + ")";
            SearchResult sr = ds.FindOne();
            DirectoryEntry dsresult = sr.GetDirectoryEntry();
            var customUser = CheckUser(dsresult.Properties["mail"][0].ToString());
            return customUser;
        }
        catch (Exception e)
        {
            return null;
        }
    }

    protected tbl_Users CheckUser(string email)
    {
        _entity = GetEntity();
        var customUser = _entity.tbl_Users.Where(x => x.Email == email).FirstOrDefault();
        if (customUser != null)
        {
            return customUser;
        }
        customUser = CreateUser(email);
        return customUser;
    }

    protected tbl_Users CreateUser(string email)
    {
        var userName = txtUserName.Text;
        var array = userName.Split('\\');
        userName = array[array.Length - 1].Trim();
        _entity = GetEntity();
        var customUser = new tbl_Users { 
            Active = true,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Department = "non_it",
            Email = email,
            Role = "normal_user",
            User_Name = userName
        };
        _entity.AddTotbl_Users(customUser);
        _entity.SaveChanges();
        return customUser;
    }
}
