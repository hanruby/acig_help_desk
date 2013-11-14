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
        }
        return list;
    }
}