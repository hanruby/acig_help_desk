using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Acig_Help_DeskModel;

public static class CurrentUser
{
    public static int Id()
    {
        var cookie = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData;
        //return int.Parse(HttpContext.Current.Session["currentUserId"].ToString());
        return int.Parse(cookie.Split('#')[0].ToString());
    }

    public static string Role()
    {
        var cookie = FormsAuthentication.Decrypt(HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName].Value).UserData;
        return cookie.Split('#')[1];
    }

    public static long DepartmentId()
    {
        string departmentId = HttpContext.Current.Session["Current_Usr_Department"] as string;
        if (!string.IsNullOrEmpty(departmentId))
        {
            return long.Parse(departmentId);
        }
        var id = CurrentUser.Id();
        var entity = new Acig_Help_DeskEntities();
        var user = entity.tbl_Users.Where(x => x.Id == id).First();
        HttpContext.Current.Session["Current_Usr_Department"] = user.Department.Id;
        return user.Department.Id;
    }

    public static string GetRedirectPath(string role)
    {
        String returnUrl = "";
        if (role == "Admin")
        {
            returnUrl = Route.GetRootPath("default.aspx");
        }
        else if (role == "It_User")
        {
            returnUrl = Route.GetRootPath("default.aspx");
        }
        else
        {
            returnUrl = Route.GetRootPath("default.aspx");
        }
        return returnUrl;
    }
}