﻿using System;
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
    User_Sub_Sub_Categories _user_Sub_Sub_Categories;
    long _id, _sub_Sub_CategoryId;
    bool email_Changed, acntType;
    string old_Email, new_Email, userName;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindDdlAccountTypes(ddlAccountType);
            BindBreadCrumbRepeater("user_edit");
            _entity = GetEntity();
            _id = long.Parse(Request.QueryString["id"]);
            hdnFldUserId.Value = _id.ToString();
            _user = _entity.tbl_Users.Where(x => x.Id == _id).First();
            txtUserName.Text = _user.User_Name;
            txtEmail.Text = _user.Email;
            ddlRole.SelectedValue = _user.Role;
            ddlActive.SelectedValue = _user.Active.ToString();
            ddlDepartment.SelectedValue = _user.Department_Id.ToString();
            txtVendorEmails.Text = _user.Vendor_Emails;
            ddlAccountType.SelectedValue = _user.Account_Type;
            hdnFldAccountType.Value = _user.Account_Type;
            var _subSubCategoryIds = _entity.User_Sub_Sub_Categories.Where(x => x.User_Id == _id).ToList().Select(x => x.Sub_Sub_Category_Id).ToList();
            BindSubSubCategoryDdl();
            if (_subSubCategoryIds.Count > 0)
            {
                foreach (ListItem x in lstBoxSubSubCategory.Items)
                {
                    if (_subSubCategoryIds.Contains(long.Parse(x.Value)))
                    {
                        x.Selected = true;
                    }
                }
            }
            UpdateCategoryBox();
            if (_user.Account_Type == Enum_Helper.AccountTypes.NON_ACIG.ToString())
            {
                UpdateAccountDiv(true, false);
            }
            else
            {
                UpdateAccountDiv(false, false);
            }
        }
    }

    private void BindCategories(object sender, EventArgs e)
    {
    }

    protected void BindSubSubCategoryDdl()
    {
        _entity = GetEntity();
        DataTable table = new DataTable();

        table.Columns.Add("Text");
        table.Columns.Add("Value");
        var data = from c in _entity.Categories
                   join s in _entity.Sub_Categories
                   on c.Id equals s.Category_Id
                   join ss in _entity.Sub_Sub_Categories
                   on s.Id equals ss.Sub_Category_Id
                   orderby c.Name, s.Name, ss.Name
                   select new { Text = c.Name + " - " + s.Name + " - " + ss.Name, Id = ss.Id };

        foreach (var x in data)
        {
            DataRow dr1 = table.NewRow();
            dr1["Value"] = x.Id;
            dr1["Text"] = x.Text;
            table.Rows.Add(dr1);
        }
        lstBoxSubSubCategory.DataSource = table;
        lstBoxSubSubCategory.DataTextField = table.Columns["Text"].ColumnName;
        lstBoxSubSubCategory.DataValueField = table.Columns["Value"].ColumnName;
        lstBoxSubSubCategory.DataBind();
    }

    protected void btnUpdateUser_Click(object sender, EventArgs e)
    {
        _id = long.Parse(hdnFldUserId.Value);
        userName = txtUserName.Text.Trim();
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
        if (_user.User_Name != userName)
        {
            if (_entity.tbl_Users.Where(x => x.User_Name == userName).Count() > 0)
            {
                Session["ErrorMessage"] = "Username already taken!";
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
                _user_Sub_Sub_Categories = new User_Sub_Sub_Categories
                {
                    Sub_Sub_Category_Id = _sub_Sub_CategoryId,
                    User_Id = _user.Id
                };
                _entity.AddToUser_Sub_Sub_Categories(_user_Sub_Sub_Categories);
                _entity.SaveChanges();
            }
        }
        _user.Account_Type = ddlAccountType.SelectedValue;
        _user.Email = new_Email;
        _user.User_Name = txtUserName.Text;
        _user.Active = bool.Parse(ddlActive.SelectedValue);
        _user.Department_Id = long.Parse(ddlDepartment.SelectedValue);
        _user.Role = ddlRole.SelectedValue;
        _user.Updated_At = DateTime.Now;
        if (ddlRole.SelectedValue == "vendor")
        {
            _user.Vendor_Emails = txtVendorEmails.Text;
        }
        else
        {
            _user.Vendor_Emails = string.Empty;
        }
        if (_user.Account_Type == Enum_Helper.AccountTypes.NON_ACIG.ToString())
        {
            _user.Role = "vendor";
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                _user.Password = String_Helper.Encrypt(txtPassword.Text.Trim());
            }
        }
        _entity.SaveChanges();
        if (email_Changed)
        {
            UpdateTicketsAssignedEmails();
        }
        if (ddlRole.SelectedValue != "engineer")
        {
            foreach (var x in _user.User_Sub_Sub_Categories.ToList())
            {
                _entity.DeleteObject(x);
                _entity.SaveChanges();
            }
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
                   select new { Ticket = t };
        var dataLst = data.ToList();
        foreach (var x in dataLst)
        {
            x.Ticket.Assigned_To_Emails = x.Ticket.Assigned_To_Emails.Replace(old_Email, new_Email);
            _entity.SaveChanges();
        }
    }

    protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdateCategoryBox();
    }

    protected void UpdateCategoryBox()
    {
        vendorEmailsDiv.Visible = ddlRole.SelectedValue == "vendor";
        categoryDiv.Visible = ddlRole.SelectedValue == "engineer";
    }

    protected void ddlAccountType_SelectedIndexChanged(object sender, EventArgs e)
    {
        acntType = ddlAccountType.SelectedValue == Enum_Helper.AccountTypes.NON_ACIG.ToString();
        if (hdnFldAccountType.Value == Enum_Helper.AccountTypes.ACIG.ToString() && acntType)
        {
            UpdateAccountDiv(true, true);
        }
        else if (hdnFldAccountType.Value == Enum_Helper.AccountTypes.NON_ACIG.ToString() && acntType)
        {
            UpdateAccountDiv(true, false);
        }
        else
        {
            UpdateAccountDiv(false, false);
        }
    }

    void UpdateAccountDiv(bool visible, bool enabled)
    {
        txtPassword.Visible = lblPassword.Visible = visible;
        rfvPassword.Enabled = enabled;
    }
}