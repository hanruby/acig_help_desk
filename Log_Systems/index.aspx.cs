using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Log_Systems_index : MasterAppPage
{
    Log_Systems obj;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("log_systems");
            BindDataToGridView();
        }
    }

    protected void Edit(object sender, GridViewEditEventArgs e)
    {
        gvLogSystems.EditIndex = e.NewEditIndex;
        BindDataToGridView();
    }
    protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvLogSystems.EditIndex = -1;
        BindDataToGridView();
    }

    protected void Update(object sender, GridViewUpdateEventArgs e)
    {
        currentUserId = CurrentUser.Id();
        var id = long.Parse(((HiddenField)(gvLogSystems.Rows[e.RowIndex].FindControl("hdnId"))).Value.ToString());
        _entity = GetEntity();
        obj = _entity.Log_Systems.Where(x => x.Id == id).First();
        obj.Updated_At = DateTime.Now;
        obj.Updated_By = currentUserId;
        obj.Name = ((TextBox)gvLogSystems.Rows[e.RowIndex].FindControl("txtNameEdit")).Text;
        _entity.SaveChanges();
        gvLogSystems.EditIndex = -1;
        BindDataToGridView();
    }

    protected void BindDataToGridView()
    {
        _entity = GetEntity();
        gvLogSystems.DataSource = _entity.Log_Systems.OrderBy(x => x.Created_At).ToList();
        gvLogSystems.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();
        obj = new Log_Systems
        {
            Name = txtName.Text,
            Created_At = DateTime.Now,
            Updated_At = DateTime.Now,
            Created_By = currentUserId,
            Updated_By = currentUserId
        };
        _entity.AddToLog_Systems(obj);
        _entity.SaveChanges();
        BindDataToGridView();
        txtName.Text = string.Empty;
    }
}