﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default2 : MasterAppPage
{
    NavigationHelper obj;
    List<NavigationHelper> list;
    int pendCount = 0, resolCount = 0, closedCount = 0;
    string rootPath;
    protected void Page_Load(object sender, EventArgs e)
    {
        rootPath = Route.GetRootPath("tickets/");
        if (!User.Identity.IsAuthenticated)
        {
            ErrorRedirect(Route.GetRootPath("account/login.aspx"), "Please login to continue");
            return;
        }
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("dashboard");
        }
        _entity = GetEntity();
        currentUserId = CurrentUser.Id();
        list = new List<NavigationHelper>();
        if (!CurrentUser.Is_Vendor())
        {
            obj = new NavigationHelper();
            obj.Href = rootPath + "responded.aspx";
            obj.ImagePath = _entity.Tickets.Where(x => x.State == "Pending" && x.Created_By == currentUserId).Count().ToString();
            obj.Name = "Pending Tickets";
            list.Add(obj);
            obj = new NavigationHelper();
            obj.Href = rootPath + "pending.aspx";
            obj.ImagePath = _entity.Tickets.Where(x => x.State == "Resolved" && x.Created_By == currentUserId).Count().ToString();
            obj.Name = "Resolved Tickets";
            list.Add(obj);
            obj = new NavigationHelper();
            obj.Href = rootPath + "responded.aspx";
            obj.ImagePath = _entity.Tickets.Where(x => x.State == "Closed" && x.Created_By == currentUserId).Count().ToString();
            obj.Name = "Closed Tickets";
            list.Add(obj);
            rptrTickets.DataSource = list;
            rptrTickets.DataBind();
        }
        if (!CurrentUser.Is_Engineer() && !CurrentUser.Is_Vendor())
        {
            return;
        }
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   where ut.User_Id == currentUserId
                   group t by t.State into Grp
                   select new { Count = Grp.Count(), State = Grp.Key };
        list = new List<NavigationHelper>();
        foreach (var x in data)
        {
            if (x.State == "Pending")
            {
                pendCount += x.Count;
            }
            else if (x.State == "Resolved")
            {
                resolCount += x.Count;
            }
            else
            {
                closedCount += x.Count;
            }
        }
        obj = new NavigationHelper();
        obj.Name = "Assigned Pending Tickets";
        obj.Href = rootPath + "assigned.aspx";
        obj.ImagePath = pendCount.ToString();
        list.Add(obj);
        obj = new NavigationHelper();
        obj.Name = "Assigned Resolved Tickets";
        obj.Href = rootPath + "assigned_responded.aspx";
        obj.ImagePath = resolCount.ToString();
        list.Add(obj);
        obj = new NavigationHelper();
        obj.Name = "Assigned Closed Tickets";
        obj.Href = rootPath + "assigned_responded.aspx";
        obj.ImagePath = closedCount.ToString();
        list.Add(obj);
        rptrAssignedTickets.DataSource = list;
        rptrAssignedTickets.DataBind();
    }
}