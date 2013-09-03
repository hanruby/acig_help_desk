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
        string role = HttpContext.Current.Session["Current_Usr_Role"] as string;
        if (!string.IsNullOrEmpty(role))
        {
            return role.Trim();
        }
        var id = CurrentUser.Id();
        var entity = new Acig_Help_DeskEntities();
        var user = entity.tbl_Users.Where(x => x.Id == id).First();
        HttpContext.Current.Session["Current_Usr_Role"] = user.Role.Trim();
        return user.Role.Trim();
    }

    public static string Role2()
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
        String returnUrl = string.Empty;
        if (role == "admin")
        {
            returnUrl = Route.GetRootPath("admin/users/index.aspx");
        }
        else
        {
            returnUrl = Route.GetRootPath("tickets/new.aspx");
        }
        return returnUrl;
    }

    public static string DepartmentName(Acig_Help_DeskEntities _entity, long id)
    {
        string departmentName = HttpContext.Current.Session["Current_Usr_Department_Name"] as string;
        if (!string.IsNullOrEmpty(departmentName))
        {
            return departmentName;
        }
        tbl_Users user = _entity.tbl_Users.Where(x => x.Id == id).First();
        return user.Department.Name;
    }
}