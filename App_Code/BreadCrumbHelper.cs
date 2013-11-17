using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class BreadCrumbHelper
{
    public string Text { get; set; }
    public string Href { get; set; }
    public string Divider_Class { get; set; }

    public List<BreadCrumbHelper> GetList(string currentPage)
    {
        string rootPath = Route.GetRootPath("");
        BreadCrumbHelper obj;
        List<BreadCrumbHelper> list = new List<BreadCrumbHelper>();
        obj = new BreadCrumbHelper { Divider_Class = currentPage == "dashboard" ? "hide" : "divider", Href = rootPath + "dashboard.aspx", Text = "Home" };
        list.Add(obj);
        switch (currentPage)
        {
            case "user_index":
                rootPath += "admin/users/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "index.aspx", Text = "Users" };
                list.Add(obj);
                break;
            case "user_new":
                rootPath += "admin/users/";
                obj = new BreadCrumbHelper { Divider_Class = "divider", Href = rootPath + "index.aspx", Text = "Users" };
                list.Add(obj);
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = "#", Text = "New" };
                list.Add(obj);
                break;
            case "user_edit":
                rootPath += "admin/users/";
                obj = new BreadCrumbHelper { Divider_Class = "divider", Href = rootPath + "index.aspx", Text = "Users" };
                list.Add(obj);
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = "#", Text = "Edit" };
                list.Add(obj);
                break;
            case "category":
                rootPath += "admin/categories/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "index.aspx", Text = "Categories" };
                list.Add(obj);
                break;
            case "department":
                rootPath += "admin/departments/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "index.aspx", Text = "Departments" };
                list.Add(obj);
                break;
            case "ticket_pending":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "pending.aspx", Text = "Tickets Waiting My Response" };
                list.Add(obj);
                break;
            case "ticket_responded":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "responded.aspx", Text = "Tickets Waiting Engineer's Response" };
                list.Add(obj);
                break;
            case "ticket_assigned":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "assigned.aspx", Text = "Assigned Tickets Waiting My Response" };
                list.Add(obj);
                break;
            case "ticket_assigned_responded":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "assigned_responded.aspx", Text = "Assigned Tickets Waiting User's Response" };
                list.Add(obj);
                break;
            case "new_ticket":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "new.aspx", Text = "New Ticket" };
                list.Add(obj);
                break;
            case "reassign_ticket":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "reassign.aspx", Text = "Reassign Ticket" };
                list.Add(obj);
                break;
            case "search_ticket":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "search.aspx", Text = "Search Ticket" };
                list.Add(obj);
                break;
            case "report":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "report.aspx", Text = "Report" };
                list.Add(obj);
                break;
            case "full_report":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "full_report.aspx", Text = "Full Report" };
                list.Add(obj);
                break;
            case "s_report":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "sreport.aspx", Text = "My Department Report" };
                list.Add(obj);
                break;
            case "e_report":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "ereport.aspx", Text = "Report By Engineer" };
                list.Add(obj);
                break;
            case "u_report":
                rootPath += "tickets/";
                obj = new BreadCrumbHelper { Divider_Class = "hide", Href = rootPath + "full_report2.aspx", Text = "Report By User" };
                list.Add(obj);
                break;
        }
        return list;
    }
}