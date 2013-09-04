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
            return;
        }
        if (!user.Active)
        {
            Session["ErrorMessage"] = "Your account has been deactivated !";
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
                user.Id.ToString() + "#" + user.Role2 // role assignment is stored
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
            _entity = GetEntity();
            var email = dsresult.Properties["mail"][0].ToString();
            //var email = "ubaid@acig.com.sa";
            var customUser = _entity.tbl_Users.Where(x => x.Email == email).FirstOrDefault();
            //var customUser = _entity.tbl_Users.Where(x => x.Email.Contains(txtUserName.Text)).FirstOrDefault();
            if (customUser == null)
            {
                Session["ErrorMessage"] = "Your profile does not exist in this system please create Profile!";
                return customUser;
            }
            return customUser;
        }
        catch (Exception e)
        {
            Session["ErrorMessage"] = "Username or Password is incorrect !";
            return null;
        }
    }

}
