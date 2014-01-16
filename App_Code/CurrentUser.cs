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
        if (role == "engineer")
        {
            returnUrl = Route.GetRootPath("dashboard.aspx");
        }
        else
        {
            returnUrl = Route.GetRootPath("dashboard.aspx");
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
        if (_entity == null)
        {
            _entity = new Acig_Help_DeskEntities();
        }
        tbl_Users user = _entity.tbl_Users.Where(x => x.Id == id).First();
        return user.Department.Name;
    }

    public static tbl_Users User()
    {
        tbl_Users user = HttpContext.Current.Session["Current_Usr"] as tbl_Users;
        if (user != null)
        {
            return user;
        }
        var id = CurrentUser.Id();
        var entity = new Acig_Help_DeskEntities();
        user = entity.tbl_Users.Where(x => x.Id == id).First();
        HttpContext.Current.Session["Current_Usr"] = user.Department.Id;
        return user;
    }

    public static bool Is_Engineer()
    {
        return CurrentUser.Role() == "engineer";
    }

    public static bool Is_IT_Consultant(tbl_Users user = null)
    {
        var id = user == null ? CurrentUser.Id() : user.Id;
        return CurrentUser.DepartmentName(null, id) == "IT Consultant";
    }

    public static bool Is_Admin()
    {
        return CurrentUser.Role2() == "admin";
    }

    public static bool Is_Manager()
    {
        return CurrentUser.Role() == "manager";
    }
}