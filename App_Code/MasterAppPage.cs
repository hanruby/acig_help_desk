﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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

    protected bool CanResolve(long ticketId)
    {
        var count = (from t in _entity.Tickets
                     join ut in _entity.User_Tickets
                     on t.Id equals ut.Ticket_Id
                     where ut.User_Id == currentUserId && ut.Ticket_Id == ticketId && (t.State == "Pending" || t.State == "Clarified" || t.State == "Clarification")
                     select new { tckt = t }).Count();
        return count > 0;
    }

    protected bool CanClarify(long ticketId)
    {
        var count = (from t in _entity.Tickets
                     join ut in _entity.User_Tickets
                     on t.Id equals ut.Ticket_Id
                     where ut.User_Id == currentUserId && ut.Ticket_Id == ticketId && (t.State == "Pending" || t.State == "Clarification" || t.State == "Clarified")
                     select new { tckt = t }).Count();
        return count > 0;
    }

    protected void BindBreadCrumbRepeater(string pageName)
    {
        var rptr = (Repeater)Master.FindControl("rptrBreadCrumb");
        BreadCrumbHelper obj = new BreadCrumbHelper();
        rptr.DataSource = obj.GetList(pageName);
        rptr.DataBind();
    }
}