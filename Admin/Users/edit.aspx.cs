using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Data;

public partial class Admin_Users_edit : MasterAppPage
{
    tbl_Users _user;
    Category _category;
    User_Sub_Sub_Categories _user_Sub_Sub_Categories;
    Sub_Categories _sub_Category;
    Sub_Sub_Categories _sub_Sub_Category;
    long _id, _category_Id, _sub_CategoryId, _sub_Sub_CategoryId;
    bool email_Changed;
    string old_Email, new_Email;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            _entity = GetEntity();
            _id = long.Parse(Request.QueryString["id"]);
            hdnFldUserId.Value = _id.ToString();
            _user = _entity.tbl_Users.Where(x => x.Id == _id).First();
            txtUserName.Text = _user.User_Name;
            txtEmail.Text = _user.Email;
            ddlRole.SelectedValue = _user.Role;
            ddlActive.SelectedValue = _user.Active.ToString();
            ddlDepartment.SelectedValue = _user.Department_Id.ToString();
            var _subSubCategoryIds = _entity.User_Sub_Sub_Categories.Where(x => x.User_Id == _id).ToList().Select(x => x.Sub_Sub_Category_Id).ToList();
            _category_Id = _sub_CategoryId = _sub_Sub_CategoryId = 0;
            if (_subSubCategoryIds.Count > 0)
            {
                _sub_Sub_CategoryId = _subSubCategoryIds.First();
                _sub_Sub_Category = _entity.Sub_Sub_Categories.Where(x => x.Id == _sub_Sub_CategoryId).First();
                _sub_Category = _sub_Sub_Category.Sub_Categories;
                _category = _sub_Category.Category;
                _category_Id = _category.Id;
                _sub_CategoryId = _sub_Category.Id;
            }
            BindCategoryDdl();
            BindSubCategoryDdl(_category_Id);
            BindSubSubCategoryDdl(_sub_CategoryId);
            if (_subSubCategoryIds.Count > 0)
            {
                ddlCategory.SelectedValue = _category.Id.ToString();
                ddlSubCategory.SelectedValue = _sub_Category.Id.ToString();
                foreach (ListItem x in lstBoxSubSubCategory.Items)
                {
                    if (_subSubCategoryIds.Contains(long.Parse(x.Value)))
                    {
                        x.Selected = true;
                    }
                }
            }
        }
    }

    private void BindCategories(object sender, EventArgs e)
    {
    }

    protected void BindCategoryDdl()
    {
        _entity = GetEntity();
        DataTable table = new DataTable();

        table.Columns.Add("Text");
        table.Columns.Add("Value");
        DataRow defaultRow = table.NewRow();
        defaultRow["Text"] = "Select";
        defaultRow["Value"] = 0;
        table.Rows.Add(defaultRow);
        foreach (var x in _entity.Categories)
        {
            DataRow dr1 = table.NewRow();
            dr1["Value"] = x.Id;
            dr1["Text"] = x.Name;
            table.Rows.Add(dr1);
        }
        ddlCategory.DataSource = table;
        ddlCategory.DataTextField = table.Columns["Text"].ColumnName;
        ddlCategory.DataValueField = table.Columns["Value"].ColumnName;
        ddlCategory.DataBind();
        ddlCategory.AutoPostBack = true;
        ddlCategory.SelectedIndexChanged += new System.EventHandler(BindCategories);
    }

    protected void BindSubCategoryDdl(long categoryId)
    {
        _entity = GetEntity();
        DataTable table = new DataTable();

        table.Columns.Add("Text");
        table.Columns.Add("Value");
        DataRow defaultRow = table.NewRow();
        defaultRow["Text"] = "Select";
        defaultRow["Value"] = 0;
        table.Rows.Add(defaultRow);
        foreach (var x in _entity.Sub_Categories.Where(x => x.Category_Id == categoryId))
        {
            DataRow dr1 = table.NewRow();
            dr1["Value"] = x.Id;
            dr1["Text"] = x.Name;
            table.Rows.Add(dr1);
        }
        ddlSubCategory.DataSource = table;
        ddlSubCategory.DataTextField = table.Columns["Text"].ColumnName;
        ddlSubCategory.DataValueField = table.Columns["Value"].ColumnName;
        ddlSubCategory.DataBind();
        ddlSubCategory.AutoPostBack = true;
        ddlSubCategory.SelectedIndexChanged += new System.EventHandler(BindCategories);
    }

    protected void BindSubSubCategoryDdl(long SubCategoryId)
    {
        _entity = GetEntity();
        DataTable table = new DataTable();

        table.Columns.Add("Text");
        table.Columns.Add("Value");
        foreach (var x in _entity.Sub_Sub_Categories.Where(x => x.Sub_Category_Id == SubCategoryId))
        {
            DataRow dr1 = table.NewRow();
            dr1["Value"] = x.Id;
            dr1["Text"] = x.Name;
            table.Rows.Add(dr1);
        }
        lstBoxSubSubCategory.DataSource = table;
        lstBoxSubSubCategory.DataTextField = table.Columns["Text"].ColumnName;
        lstBoxSubSubCategory.DataValueField = table.Columns["Value"].ColumnName;
        lstBoxSubSubCategory.DataBind();
    }

    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubCategoryDdl(long.Parse(ddlCategory.SelectedValue));
    }

    protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindSubSubCategoryDdl(long.Parse(ddlSubCategory.SelectedValue));
    }

    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {
        _id = long.Parse(hdnFldUserId.Value);
        _entity = GetEntity();
        _user = _entity.tbl_Users.Where(x => x.Id == _id).First();
        new_Email = txtEmail.Text.Trim();
        old_Email = _user.Email;
        email_Changed = false;
        if (old_Email != new_Email)
        {
            email_Changed = true;
            if (_entity.tbl_Users.Where(x => x.Email == new_Email).Count() > 0)
            {
                Session["ErrorMessage"] = "Email already taken!";
                return;
            }
        }
        var ids = _user.User_Sub_Sub_Categories.Select(x => x.Sub_Sub_Category_Id).ToList();
        foreach (ListItem x in lstBoxSubSubCategory.Items)
        {
            _sub_Sub_CategoryId = long.Parse(x.Value);
            if (!x.Selected && ids.Contains(_sub_Sub_CategoryId))
            {
                _entity.DeleteObject(_entity.User_Sub_Sub_Categories.Where(y => y.Sub_Sub_Category_Id == _sub_Sub_CategoryId && y.User_Id == _user.Id).First());
            }
            else if (x.Selected && !ids.Contains(_sub_Sub_CategoryId))
            {
                _user_Sub_Sub_Categories = new User_Sub_Sub_Categories { 
                    Sub_Sub_Category_Id = _sub_Sub_CategoryId,
                    User_Id = _user.Id
                };
                _entity.AddToUser_Sub_Sub_Categories(_user_Sub_Sub_Categories);
                _entity.SaveChanges();
            }
        }
        _user.Email = new_Email;
        _user.User_Name = txtUserName.Text;
        _user.Active = bool.Parse(ddlActive.SelectedValue);
        _user.Department_Id = long.Parse(ddlDepartment.SelectedValue);
        _user.Role = ddlRole.SelectedValue;
        _user.Updated_At = DateTime.Now;
        _entity.SaveChanges();
        if (email_Changed)
        {
            UpdateTicketsAssignedEmails();
        }
        Session["NoticeMessage"] = "Successfully updated information!";
        Response.Redirect(Route.GetRootPath("admin/users/index.aspx"));
    }

    protected void UpdateTicketsAssignedEmails()
    {
        var data = from t in _entity.Tickets
                   join ut in _entity.User_Tickets
                   on t.Id equals ut.Ticket_Id
                   where ut.User_Id == _user.Id
                   select new {Ticket = t};
        var dataLst = data.ToList();
        foreach (var x in dataLst)
        {
            x.Ticket.Assigned_To_Emails = x.Ticket.Assigned_To_Emails.Replace(old_Email, new_Email);
            _entity.SaveChanges();
        }
    }
}