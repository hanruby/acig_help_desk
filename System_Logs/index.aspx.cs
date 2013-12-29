using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Text;

public partial class System_Logs_index : MasterAppPage
{
    List<Custom_Logs> lst;
    Custom_Logs obj;
    string fileName, excelData;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (CurrentUser.Role2() != "admin" && CurrentUser.Role() != "engineer")
        {
            ErrorRedirect(Route.GetRootPath("not_authorized.aspx"), "Not authorized");
        }
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("system_logs");
            lblIncidents.Text = "System Incident Logs";
            lnkBtnNewIncident.PostBackUrl = "new.aspx";
            BindData();
        }
    }

    protected void BindData(bool bind = true)
    {
        lst = new List<Custom_Logs>();
        _entity = GetEntity();
        var data = from sl in _entity.System_Incident_Logs
                   join ls in _entity.Log_Systems
                   on sl.System_Id equals ls.Id
                   orderby sl.Created_At
                   select new
                   {
                       Id = sl.Id,
                       Comment = sl.Comment,
                       Incident_Date = sl.Incident_Date,
                       Error_Long_Desc = sl.Error_Long_Description,
                       Error_Short_Desc = sl.Error_Short_Desc,
                       System = ls.Name,
                       Resolved_Date = sl.Resolved_Date,
                       Resolved_Description = sl.Resolved_Description,
                       Time_Difference = sl.Time_Difference
                   };
        foreach (var x in data)
        {
            obj = new Custom_Logs
            {
                Comment = x.Comment,
                Error_Long_Desc = x.Error_Long_Desc,
                Error_Short_Desc = x.Error_Short_Desc,
                Id = x.Id,
                Incident_Date = DateTimeHelper.ConvertToString(x.Incident_Date.ToString()),
                Resolved_Date = DateTimeHelper.ConvertToString(x.Resolved_Date.ToString()),
                Resolved_Description = x.Resolved_Description,
                System = x.System,
                Time_Difference = x.Time_Difference
            };
            lst.Add(obj);
        }
        if (bind)
        {
            gvIncidents.DataSource = lst;
            gvIncidents.DataBind();
        }
    }

    protected string EditUrl(object id)
    {
        return "edit.aspx?id=" + id.ToString();
    }

    protected void lnkBtnDownload_Click(object sender, EventArgs e)
    {
        fileName = "System_Incident_Logs_" + DateTimeHelper.ToTimeStamp() + ".xls";
        BindData(false);
        BuildExcelData();
        Response.ClearContent();
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        Response.ContentType = "application/ms-excel";
        Response.ContentEncoding = Encoding.Unicode;
        Response.BinaryWrite(Encoding.Unicode.GetPreamble());
        Response.Write(excelData);
        Response.End();
    }
        
    protected void BuildExcelData()
    {
        excelData = string.Empty;
        excelData += "Incident Date\tSystem\tError Type\tError Description\tResolved Date\tResolved Description\tTime Difference\tComment\t\n";
        foreach (var x in lst)
        {
            excelData += x.Incident_Date + "\t" + x.System + "\t" + x.Error_Short_Desc + "\t" + x.Error_Long_Desc + "\t" + 
                x.Resolved_Date + "\t" + x.Resolved_Description + "\t" + x.Time_Difference + "\t" + x.Comment + "\t\n";
        }
    }
}