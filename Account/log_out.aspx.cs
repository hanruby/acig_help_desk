using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class Account_log_out : MasterAppPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetLogout();
        var _homePath = Route.GetRootPath("account/login.aspx");
        Response.Redirect(_homePath);
    }
}