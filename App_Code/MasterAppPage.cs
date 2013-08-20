using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acig_Help_DeskModel;

public class MasterAppPage : System.Web.UI.Page
{
    protected Acig_Help_DeskEntities _entity;
    protected long currentUserId;
    protected Acig_Help_DeskEntities GetEntity()
    {
        if (_entity == null)
        {
            _entity = new Acig_Help_DeskEntities();
        }
        return _entity;
    }

    protected void ErrorRedirect(string path, string message)
    {
        Session["ErrorMessage"] = message;
        Response.Redirect(path);
    }

    protected void SuccessRedirect(string path, string message)
    {
        Session["NoticeMessage"] = message;
        Response.Redirect(path);
    }
}