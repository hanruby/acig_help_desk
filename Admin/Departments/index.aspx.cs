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
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("department");
            BindDataToGridView();
        }
    }

    protected void EditDepartment(object sender, GridViewEditEventArgs e)
    {
        gvDepartment.EditIndex = e.NewEditIndex;
        BindDataToGridView();
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
        _entity.SaveChanges();
        gvDepartment.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _entity = GetEntity();
        gvDepartment.DataSource = _entity.Departments.OrderBy(x => x.Created_At).ToList();
        gvDepartment.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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
        _entity.AddToDepartments(_department);
        _entity.SaveChanges();
        BindDataToGridView();
        txtDepartmentName.Text = string.Empty;
    }
}