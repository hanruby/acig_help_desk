using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : MasterAppPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!User.Identity.IsAuthenticated)
        {
            ErrorRedirect(Route.GetRootPath("account/login.aspx"), "Please login to continue");
            return;
        }
        assignedLnkBtns.Visible = CurrentUser.Is_Engineer();
    }
}