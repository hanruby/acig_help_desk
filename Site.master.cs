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
            hprLinkCreateProfile.Visible = false;
        }
        else
        {
            MenuItems(false);
            lblUserName.Visible = false;
            hprLinkLoginStatus.NavigateUrl = Route.GetRootPath("account/login.aspx");
            hprLinkLoginStatus.Text = "Sign In";
            hprLinkCreateProfile.NavigateUrl = Route.GetRootPath("account/create_profile.aspx");
            hprLinkCreateProfile.Visible = true;
        }
        DisplayNotifications();
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
                if (menuItem.Value == "admin" && CurrentUser.Role2() != "admin")
                {
                    toRemoveItems.Add(menuItem);
                }
                else if (menuItem.Value == "engineer" && CurrentUser.Role() != "engineer")
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

    protected void DisplayNotifications()
    {
        //if (Session["NoticeMessage"] != null)
        //{
        //    alertSuccess.Visible = true;
        //    successMessage.InnerText = Session["NoticeMessage"].ToString();
        //    Session["NoticeMessage"] = null;
        //}
        //else
        //{
        //    alertSuccess.Visible = false;
        //}
        //if (Session["ErrorMessage"] != null)
        //{
        //    alertError.Visible = true;
        //    errorMessage.InnerText = Session["ErrorMessage"].ToString();
        //    Session["ErrorMessage"] = null;
        //}
        //else
        //{
        //    alertError.Visible = false;
        //}
    }
}
