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

    public static List<NavigationHelper> GetUserMenu()
    {
        List<NavigationHelper> lst = new List<NavigationHelper>();
        string rootPath = Route.GetRootPath("");
        NavigationHelper obj = new NavigationHelper { Name = "Logout", Href = rootPath + "account/log_out.aspx", ImagePath = "icon-off" };
        lst.Add(obj);
        return lst;
    }

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
        obj = new NavigationHelper { Name = "Dashboard", Href = rootPath + "dashboard.aspx", ImagePath = "icon-home" };
        lst.Add(obj);
        switch (role)
        {
            case "admin":
                lst = GetAdminMenu(obj, lst, rootPath);
                if (user.Role == "engineer")
                {
                    lst = GetEngineerMenu(obj, lst, rootPath, true);
                }
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
            case "vendor":
                lst = GetVendorMenu(obj, lst, rootPath);
                break;
        }
        if (role == "admin" && user.Role != "engineer")
        {
            lst = GetUserMenu(obj, lst, rootPath);
            if (CurrentUser.Is_IT_Consultant(user))
            {
                lst = GetReassignTicketMenu(obj, lst, rootPath);
            }
        }
        if (role == "admin")
        {
            lst = GetAssignTicketVendorMenu(obj, lst, rootPath);
        }
        if (role == "manager" || role == "vp" || role == "coo" || role == "ceo")
        {
            lst = GetUserMenu(obj, lst, rootPath);
        }
        if (role != "vendor")
        {
            lst = AddCommonListItems(obj, lst, rootPath);
        }
        if (role == "supervisor" || user.Role == "supervisor")
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

    public static List<NavigationHelper> GetVendorMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Assigned Tickets Waiting My Response", Href = rootPath + "tickets/assigned.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Assigned  Tickets Waiting User's Response", Href = rootPath + "tickets/assigned_responded.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report", Href = rootPath + "tickets/report.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetAdminMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Users", Href = rootPath + "admin/users/index.aspx", ImagePath = "icon-user" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Categories", Href = rootPath + "admin/categories/index.aspx", ImagePath = "icon-cog" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Departments", Href = rootPath + "admin/departments/index.aspx", ImagePath = "icon-cog" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetEngineerMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath, bool admin = false)
    {
        obj = new NavigationHelper { Name = "Tickets Waiting My Response", Href = rootPath + "tickets/pending.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Tickets Waiting Engineer's Response", Href = rootPath + "tickets/responded.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Assigned Tickets Waiting My Response", Href = rootPath + "tickets/assigned.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Assigned  Tickets Waiting User's Response", Href = rootPath + "tickets/assigned_responded.aspx", ImagePath = "icon-th-large" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "New Ticket", Href = rootPath + "tickets/new.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "New Call Ticket", Href = rootPath + "tickets/call.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        GetReassignTicketMenu(obj, lst, rootPath);
        if (admin)
        {
            obj = new NavigationHelper { Name = "Log Systems", Href = rootPath + "log_systems/index.aspx", ImagePath = "icon-th" };
            lst.Add(obj);
            obj = new NavigationHelper { Name = "System Incident Logs", Href = rootPath + "system_logs/index.aspx", ImagePath = "icon-pencil" };
            lst.Add(obj);
        }
        return lst;
    }

    public static List<NavigationHelper> GetUserMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Tickets Waiting My Response", Href = rootPath + "tickets/pending.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Ticekts Waiting Engineer's Response", Href = rootPath + "tickets/responded.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "New Ticket", Href = rootPath + "tickets/new.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> AddCommonListItems(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Search Ticket", Href = rootPath + "tickets/search.aspx", ImagePath = "icon-search" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report", Href = rootPath + "tickets/report.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetSuprvisorMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "My Department Report", Href = rootPath + "tickets/sreport.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetReassignTicketMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Reassign Ticket", Href = rootPath + "tickets/reassign.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetAssignTicketVendorMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Assign Ticket To Vendor", Href = rootPath + "tickets/assign_vendor.aspx", ImagePath = "icon-leaf" };
        lst.Add(obj);
        return lst;
    }

    public static List<NavigationHelper> GetFullReportsMenu(NavigationHelper obj, List<NavigationHelper> lst, string rootPath)
    {
        obj = new NavigationHelper { Name = "Details Report", Href = rootPath + "tickets/detail_report.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Full Report", Href = rootPath + "tickets/full_report.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report By User", Href = rootPath + "tickets/full_report2.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        obj = new NavigationHelper { Name = "Report By Engineer", Href = rootPath + "tickets/ereport.aspx", ImagePath = "icon-book" };
        lst.Add(obj);
        return lst;
    }

}