using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Admin_Departments_index : MasterAppPage
{
    Department _department;
    DropDownList ddl, ddl2, ddl3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("department");
            ddl = ddlManagerNew;
            ddl2 = ddlManagerNew2;
            ddl3 = ddlManagerNew3;
            BindDdls();
            BindDataToGridView();
        }
    }

    protected void EditDepartment(object sender, GridViewEditEventArgs e)
    {
        gvDepartment.EditIndex = e.NewEditIndex;
        BindDataToGridView();
        ddl = (DropDownList)(gvDepartment.Rows[e.NewEditIndex].FindControl("ddlManagerEdit"));
        ddl2 = (DropDownList)(gvDepartment.Rows[e.NewEditIndex].FindControl("ddlManagerEdit2"));
        ddl3 = (DropDownList)(gvDepartment.Rows[e.NewEditIndex].FindControl("ddlManagerEdit3"));
        BindDdls();
        ddl.SelectedValue = ((HiddenField)gvDepartment.Rows[e.NewEditIndex].FindControl("hdnManagerId")).Value;
        ddl2.SelectedValue = ((HiddenField)gvDepartment.Rows[e.NewEditIndex].FindControl("hdnManagerId2")).Value;
        ddl3.SelectedValue = ((HiddenField)gvDepartment.Rows[e.NewEditIndex].FindControl("hdnManagerId3")).Value;
    }

    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvDepartment.EditIndex = -1;
        BindDataToGridView();
    }

    protected void UpdateDepartment(object sender, GridViewUpdateEventArgs e)
    {
        currentUserId = CurrentUser.Id();
        var id = long.Parse(((HiddenField)(gvDepartment.Rows[e.RowIndex].FindControl("hdnDepartmentId"))).Value.ToString());
        _entity = GetEntity();
        _department = _entity.Departments.Where(x => x.Id == id).First();
        _department.Updated_At = DateTime.Now;
        _department.Updated_By = currentUserId;
        _department.Name = ((TextBox)gvDepartment.Rows[e.RowIndex].FindControl("txtDepartmentNameEdit")).Text;
        _department.Manager_Id = long.Parse(((DropDownList)gvDepartment.Rows[e.RowIndex].FindControl("ddlManagerEdit")).SelectedValue);
        _department.Manager_Id_2 = long.Parse(((DropDownList)gvDepartment.Rows[e.RowIndex].FindControl("ddlManagerEdit2")).SelectedValue);
        _department.Manager_Id_3 = long.Parse(((DropDownList)gvDepartment.Rows[e.RowIndex].FindControl("ddlManagerEdit3")).SelectedValue);
        _entity.SaveChanges();
        gvDepartment.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _entity = GetEntity();
        var data = from d in _entity.Departments
                   join u in _entity.tbl_Users
                   on d.Manager_Id equals u.Id
                   into subUsers
                   from subUser in subUsers.DefaultIfEmpty()
                   join u2 in _entity.tbl_Users
                   on d.Manager_Id_2 equals u2.Id
                   into subUsers2
                   from subUser2 in subUsers2.DefaultIfEmpty()
                   join u3 in _entity.tbl_Users
                   on d.Manager_Id_3 equals u3.Id
                   into subUsers3
                   from subUser3 in subUsers3.DefaultIfEmpty()
                   orderby d.Name
                   select new { 
                       Id = d.Id,
                       Name = d.Name,
                       Manager = (subUser == null) ? "-" : subUser.User_Name,
                       Manager_Id = (subUser == null) ? 0 : subUser.Id,
                       Manager_2 = (subUser2 == null) ? "-" : subUser2.User_Name,
                       Manager_Id_2 = (subUser2 == null) ? 0 : subUser2.Id,
                       Manager_3 = (subUser3 == null) ? "-" : subUser3.User_Name,
                       Manager_Id_3 = (subUser3 == null) ? 0 : subUser3.Id
                   };
        gvDepartment.DataSource = data;
        gvDepartment.DataBind();
    }

    protected void btnSaveDepartment_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();
        _department = new Department
        {
            Name = txtDepartmentName.Text,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _department.Manager_Id = long.Parse(ddlManagerNew.SelectedValue);
        _department.Manager_Id_2 = long.Parse(ddlManagerNew2.SelectedValue);
        _department.Manager_Id_3 = long.Parse(ddlManagerNew3.SelectedValue);
        _entity.AddToDepartments(_department);
        _entity.SaveChanges();
        BindDataToGridView();
        txtDepartmentName.Text = string.Empty;
        ddlManagerNew.SelectedValue = ddlManagerNew2.SelectedValue = ddlManagerNew3.SelectedValue = "0";
    }

    void BindDdls()
    {
        BindDdlManagersRoot(ddl, "engineer");
        BindDdlManagersRoot(ddl2, "engineer");
        BindDdlManagersRoot(ddl3, "engineer");
    }
}