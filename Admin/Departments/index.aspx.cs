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
    DropDownList ddl;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("department");
            BindDdlManagersRoot(ddlManagerNew);
            BindDataToGridView();
        }
    }

    protected void EditDepartment(object sender, GridViewEditEventArgs e)
    {
        gvDepartment.EditIndex = e.NewEditIndex;
        BindDataToGridView();
        ddl = (DropDownList)(gvDepartment.Rows[e.NewEditIndex].FindControl("ddlManagerEdit"));
        BindDdlManagersRoot(ddl);
        ddl.SelectedValue = ((HiddenField)gvDepartment.Rows[e.NewEditIndex].FindControl("hdnManagerId")).Value;
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
                   orderby d.Created_At
                   select new { 
                       Id = d.Id,
                       Name = d.Name,
                       Manager = (subUser == null) ? "-" : subUser.User_Name,
                       Manager_Id = (subUser == null) ? 0 : subUser.Id
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
        _entity.AddToDepartments(_department);
        _entity.SaveChanges();
        BindDataToGridView();
        txtDepartmentName.Text = string.Empty;
    }
}