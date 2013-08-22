using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.User.Identity.IsAuthenticated)
        {
            MenuItems(true);
            lblUserName.Visible = true;
            lblUserName.Text = "Logged in as " + Page.User.Identity.Name + " | ";
            hprLinkLoginStatus.NavigateUrl = Route.GetRootPath("account/log_out.aspx");
            hprLinkLoginStatus.Text = "Sign Out";
        }
        else
        {
            MenuItems(false);
            lblUserName.Visible = false;
            hprLinkLoginStatus.NavigateUrl = Route.GetRootPath("account/login.aspx");
            hprLinkLoginStatus.Text = "Sign In";
        }
        if (Session["NoticeMessage"] != null)
        {
            lblNotification.Text = Session["NoticeMessage"].ToString();
            lblNotification.Visible = true;
            Session["NoticeMessage"] = null;
        }
        if (Session["ErrorMessage"] != null)
        {
            lblNotification.Text = Session["ErrorMessage"].ToString();
            lblNotification.Visible = true;
            Session["ErrorMessage"] = null;
        }
    }

    protected void MenuItems(bool authenticated)
    {
        MenuItemCollection menuItems = NavigationMenu.Items;
        List<MenuItem> toRemoveItems = new List<MenuItem>();
        foreach (MenuItem menuItem in menuItems)
        {
            if (!authenticated)
            {
                toRemoveItems.Add(menuItem);
            }
            else
            {
                if (CurrentUser.Role() == "normal_user" && menuItem.Value == "admin")
                {
                    toRemoveItems.Add(menuItem);
                }
                else if (CurrentUser.Department() == "non_it" && menuItem.Value == "it")
                {
                    toRemoveItems.Add(menuItem);
                }
            }
        }
        DeleteMenuItems(menuItems, toRemoveItems);
    }


    protected void DeleteMenuItems(MenuItemCollection menuItems, List<MenuItem> toRemoveItems)
    {
        foreach (MenuItem menuItemx in toRemoveItems)
        {
            menuItems.Remove(menuItemx);
        }
    }
}
