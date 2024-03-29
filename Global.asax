﻿<%@ Application Language="C#" %>
<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // Code that runs on application startup

    }

    void Application_End(object sender, EventArgs e)
    {
        //  Code that runs on application shutdown

    }

    void Application_Error(object sender, EventArgs e)
    {
        Exception exc = Server.GetLastError();

        if (exc is HttpUnhandledException)
        {
            if (exc.InnerException != null)
            {
                exc = new Exception(exc.InnerException.Message);
                var path = Route.GetRootPath("error.aspx");
                Server.Transfer(path + "?handler=Application_Error%20-%20Global.asax", true);
            }
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // Code that runs when a new session is started

    }

    void Session_End(object sender, EventArgs e)
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.

    }

    protected void FormsAuthentication_OnAuthenticate(Object sender, FormsAuthenticationEventArgs e)
    {
        if (FormsAuthentication.CookiesSupported == true)
        {
            if (Request.Cookies[FormsAuthentication.FormsCookieName] != null)
            {
                try
                {
                    //email
                    var cookie = FormsAuthentication.Decrypt(Request.Cookies[FormsAuthentication.FormsCookieName].Value);
                    string username = cookie.Name;

                    //role
                    string roles = cookie.UserData.Split('#')[1];
                    //string roles = roles;

                    //
                    e.User = new System.Security.Principal.GenericPrincipal(
                      new System.Security.Principal.GenericIdentity(username, "Forms"), roles.Split(';'));
                }
                catch (Exception)
                {
                    //somehting went wrong
                }
            }
        }
    }
       
</script>
