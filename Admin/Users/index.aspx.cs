using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Admin_Users_index : MasterAppPage
{
    string routePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lnkBtnNewUser.PostBackUrl = Route.GetRootPath("admin/users/new.aspx");
            BindDataToGridView();
        }
    }

    protected void BindDataToGridView()
    {
        routePath = Route.GetRootPath("admin/users/edit.aspx?id=");
        _entity = GetEntity();
        var data = from u in _entity.tbl_Users
                   join c in _entity.Categories
                   on u.Category_Id equals c.Id into cu
                   from x in cu.DefaultIfEmpty()
                   select new
                   {
                       Id = u.Id,
                       User_Name = u.User_Name,
                       Email = u.Email,
                       Category_Id = x == null ? 0 : x.Id,
                       Role = u.Role,
                       Active = u.Active,
                       Role_Text = u.Role == "admin" ? "Admin" : "Normal",
                       Category_Name = x.Name,
                       Department_Text = u.Department.Name,
                       Department_Id = u.Department_Id
                   };
        gvUsers.DataSource = data;
        gvUsers.DataBind();
    }

    protected string EditUrl(object obj)
    {
        routePath = Route.GetRootPath("admin/users/edit.aspx?id=");
        return routePath + obj.ToString();
    }
}