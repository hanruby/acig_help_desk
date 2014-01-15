using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Web.Security;
using System.Data;

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

    protected string SetLogin(tbl_Users user)
    {
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
        return returnUrl;
    }

    protected void SetLogout()
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
        cookie1.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie1);
        HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "");
        cookie2.Expires = DateTime.Now.AddYears(-1);
        Response.Cookies.Add(cookie2);
        FormsAuthentication.RedirectToLoginPage();
    }

    protected void BindCommentsRepeater(Repeater rptCust, long custId)
    {
        _entity = GetEntity();
        var commentData = from c in _entity.Comments
                          join u in _entity.tbl_Users
                          on c.Created_By equals u.Id
                          where c.Ticket_Id == custId
                          orderby c.Created_At descending
                          select new
                          {
                              CreatedBy = u.Email,
                              CreatedAt = c.Created_At,
                              Notes = c.Notes,
                              Visible = c.File_Path,
                              Url = c.Id
                          };
        rptCust.DataSource = commentData;
        rptCust.DataBind();
    }

    private void BindCategoriesRoot(object sender, EventArgs e)
    {
    }

    protected void BindDdlLogSystemsRoot(DropDownList ddlRoot)
    {
        _entity = GetEntity();
        List<Log_Systems> lst = _entity.Log_Systems.OrderBy(x => x.Name).ToList();
        DataTable table = new DataTable();
        table.Columns.Add("Text");
        table.Columns.Add("Value");
        DataRow dr;
        dr = table.NewRow();
        dr["Text"] = "Select";
        dr["Value"] = "0";
        table.Rows.Add(dr);
        foreach (var x in lst)
        {
            dr = table.NewRow();
            dr["Text"] = x.Name;
            dr["Value"] = x.Id;
            table.Rows.Add(dr);
        }
        ddlRoot.DataSource = table;
        ddlRoot.DataTextField = table.Columns["Text"].ColumnName;
        ddlRoot.DataValueField = table.Columns["Value"].ColumnName;
        ddlRoot.DataBind();
        ddlRoot.SelectedIndexChanged += new System.EventHandler(BindCategoriesRoot);
    }

    protected void BindDdlManagersRoot(DropDownList ddlRoot)
    {
        _entity = GetEntity();
        var highUsers = High_Users();
        var lst = _entity.tbl_Users.Where(x => highUsers.Contains(x.Role)).OrderBy(x => x.User_Name).ToList();
        DataTable table = new DataTable();
        table.Columns.Add("Text");
        table.Columns.Add("Value");
        DataRow dr;
        dr = table.NewRow();
        dr["Text"] = "Select";
        dr["Value"] = "0";
        table.Rows.Add(dr);
        foreach (var x in lst)
        {
            dr = table.NewRow();
            dr["Text"] = x.User_Name;
            dr["Value"] = x.Id;
            table.Rows.Add(dr);
        }
        ddlRoot.DataSource = table;
        ddlRoot.DataTextField = table.Columns["Text"].ColumnName;
        ddlRoot.DataValueField = table.Columns["Value"].ColumnName;
        ddlRoot.DataBind();
        ddlRoot.SelectedIndexChanged += new System.EventHandler(BindCategoriesRoot);
    }

    protected bool FileLinkVisibile(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        return !string.IsNullOrEmpty(obj.ToString());
    }

    protected string FileDownloadUrl(object obj)
    {
        return Route.GetRootPath("download.aspx?id=" + obj.ToString());
    }

    string[] High_Users()
    {
        return new string[] { "supervisor", "manager" };
    }
}