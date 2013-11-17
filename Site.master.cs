using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    string rootPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        rootPath = Route.GetRootPath("");
        if (!Page.User.Identity.IsAuthenticated) return;
        if (!IsPostBack)
        {
            var _currentUserId = CurrentUser.Id();
            footerCopy.InnerHtml = " &copy; " + DateTime.Now.Year + " ACIG";
            var user = CurrentUser.User();
            displayName.InnerText = user.User_Name;
            mainLogo.Src = rootPath + "images/logo20.png";
            rptrMainNav.DataSource = NavigationHelper.GetMainMenu(user);
            rptrMainNav.DataBind();
            rptrUserNav.DataSource = NavigationHelper.GetUserMenu();
            rptrUserNav.DataBind();
        }
    }
}
