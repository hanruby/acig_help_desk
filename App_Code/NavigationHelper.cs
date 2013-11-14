using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Acig_Help_DeskModel;

public class NavigationHelper
{
    public string Name { get; set; }
    public string Href { get; set; }
    public string ImagePath { get; set; }

    public static List<NavigationHelper> GetMainMenu(tbl_Users user)
    {
        var role = user.Role;
        if (user.Role2 == "admin")
        {
            role = user.Role2;
        }
        NavigationHelper obj = new NavigationHelper(); ;
        List<NavigationHelper> lst = new List<NavigationHelper>();
        string rootPath = Route.GetRootPath("");
        obj = new NavigationHelper { Name = "Dashboard ( " + 0 + " )", Href = rootPath + "dashboard.aspx", ImagePath = "icon-home" };
        lst.Add(obj);
        switch (role)
        {
            case "admin":
                lst = GetAdminMenu(obj, lst, rootPath);
                break;
            case "engineer":
                lst = GetEngineerMenu(obj, lst, rootPath);
                break;
            case "user":
                lst = GetUserMenu(obj, lst, rootPath);
                break;
            case "supervisor":
                lst = GetUserMenu(obj, lst, rootPath);
                break;
        }
        if (role == "manager" || role == "vp" || role == "coo" || role == "ceo")
        {
            lst = GetUserMenu(obj, lst, rootPath);
        }
        lst = AddCommonListItems(obj, lst, rootPath);
        if (role == "supervisor")
        {
            lst = GetSuprvisorMenu(obj, lst, rootPath);
        }
        else if (role == "admin" || role == "manager" || role == "vp" || role == "coo" || role == "ceo")
        {
            lst = GetFullReportsMenu(obj, lst, rootPath);
        }
        obj = new NavigationHelper { Name = "Log out", Href = rootPath + "account/log_out.aspx", ImagePath = "icon-off" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetAdminMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Users", Href = rootPath + "admin/users/index.aspx", ImagePath = "icon-home" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Categories", Href = rootPath + "admin/categories/index.aspx", ImagePath = "icon-home" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Departments", Href = rootPath + "admin/departments/index.aspx", ImagePath = "icon-home" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "New Ticket", Href = rootPath + "tickets/new.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Reassign Ticket", Href = rootPath + "tickets/reassign.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetEngineerMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "New Ticket", Href = rootPath + "tickets/new.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "New Call Ticket", Href = rootPath + "tickets/call.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetUserMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "New Ticket", Href = rootPath + "tickets/new.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> AddCommonListItems(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Search Ticket", Href = rootPath + "tickets/search.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report", Href = rootPath + "tickets/report.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetSuprvisorMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "My Department Report", Href = rootPath + "tickets/sreport.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetFullReportsMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Full Report", Href = rootPath + "tickets/full_report.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report By User", Href = rootPath + "tickets/full_report2.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report By Engineer", Href = rootPath + "tickets/ereport.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

}