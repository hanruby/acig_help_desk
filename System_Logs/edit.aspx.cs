using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class System_Logs_edit : MasterAppPage
{
    System_Incident_Logs obj;
    int id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.Role2() != "admin" && CurrentUser.Role() != "engineer")
        {
            ErrorRedirect(Route.GetRootPath("not_authorized.aspx"), "Not authorized");
        }
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("system_logs_new");
            BindDdlLogSystemsRoot(ddlLogSystems);
            hdnFldId.Value = Request.QueryString["id"];
            BindText();
            BindData();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        currentUserId = CurrentUser.Id();
        id = int.Parse(hdnFldId.Value);
        _entity = GetEntity();
        obj = _entity.System_Incident_Logs.Where(x => x.Id == id).First();
        obj.Comment = txtComment.Text;
        obj.Created_At = DateTime.Now;
        obj.Created_By = currentUserId;
        obj.Error_Long_Description = txtErrorLongDesc.Text;
        obj.Error_Short_Desc = txtErrorShortDesc.Text;
        obj.Incident_Date = DateTimeHelper.ConvertToDateWithoutNull(txtIncidentDate.Text);
        obj.Resolved_Date = DateTimeHelper.ConvertToDate(txtResolvedDate.Text);
        obj.Resolved_Description = txtResolvedDescription.Text;
        obj.System_Id = int.Parse(ddlLogSystems.SelectedValue);
        obj.Time_Difference = txtTimeDifference.Text;
        obj.Updated_At = DateTime.Now;
        obj.Updated_By = currentUserId;
        _entity.SaveChanges();
        SuccessRedirect("index.aspx", "Successfully created new incident log");
    }

    protected void BindText()
    {
        lblComment.Text = "Comment";
        lblErrorLongDesc.Text = "Error Description";
        lblErrorShortDesc.Text = "Error Type";
        lblIncidentDate.Text = "Incident Date";
        lblResolvedDate.Text = "Resolved Date";
        lblResolvedDescription.Text = "Resolved Description";
        lblSystem.Text = "System";
        lblTimeDifference.Text = "Time Difference";
    }

    protected void BindData()
    {
        id = int.Parse(hdnFldId.Value);
        _entity = GetEntity();
        obj = _entity.System_Incident_Logs.Where(x => x.Id == id).First();
        txtComment.Text = obj.Comment;
        txtErrorLongDesc.Text = obj.Error_Long_Description;
        txtErrorShortDesc.Text = obj.Error_Short_Desc;
        txtIncidentDate.Text = DateTimeHelper.ConvertToString(obj.Incident_Date.ToString());
        txtResolvedDate.Text = DateTimeHelper.ConvertToString(obj.Resolved_Date.ToString());
        txtResolvedDescription.Text = obj.Resolved_Description;
        ddlLogSystems.SelectedValue = obj.System_Id.ToString();
        txtTimeDifference.Text = obj.Time_Difference;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
}