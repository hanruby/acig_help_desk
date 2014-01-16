using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;
using System.Text;

public partial class Tickets_detail_report : MasterAppPage
{
    List<Manager_Report> lst;
    Manager_Report obj;
    List<long> userIds;
    string fileName, excelData;
    long searchId;
    string searchCategory, searchCreatedBy, searchResolvedBy, searchStatus;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!CurrentUser.Is_Admin() && !CurrentUser.Is_Manager())
        {
            ErrorRedirect("not_authorized.aspx", "Not Authorized");
            return;
        }
        if (!IsPostBack)
        {
            adminTable.Visible = false;
            if (CurrentUser.Is_Admin())
            {
                BindDdlDepartmentsRoot(ddlDept);
                adminTable.Visible = true;
            }
            GetUsersIds();
            BindData();
            BindDdlStatus(ddlStatus);
        }
    }

    void BindData(bool export = false, bool search = false)
    {
        var data = from t in _entity.Tickets
                   join tsc in _entity.Sub_Sub_Categories
                   on t.Sub_Sub_Category_Id equals tsc.Id
                   join sc in _entity.Sub_Categories
                   on tsc.Sub_Category_Id equals sc.Id
                   join c in _entity.Categories
                   on sc.Category_Id equals c.Id
                   join e in _entity.Events
                   on t.Id equals e.Ticket_Id
                   join u in _entity.tbl_Users
                   on t.Created_By equals u.Id
                   join r in _entity.tbl_Users
                   on e.Created_By equals r.Id
                   where userIds.Contains(t.Created_By)
                   orderby e.Id
                   select new
                   {
                       Id = t.Id,
                       Subject = t.Subject,
                       Created_By = u.User_Name,
                       Created_At = t.Created_At,
                       Resolved_By = r.User_Name,
                       Resolved_At = e.Created_At,
                       Status = t.State,
                       Event_Status = e.State,
                       Category = c.Name + " - " + sc.Name + " - " + c.Name
                   };
        if (search)
        {
            if (searchId != 0)
            {
                data = data.Where(x => x.Id == searchId);
            }

            if (!string.IsNullOrEmpty(searchCategory))
            {
                data = data.Where(x => x.Category.Contains(searchCategory));
            }

            if (!string.IsNullOrEmpty(searchCreatedBy))
            {
                data = data.Where(x => x.Created_By.Contains(searchCreatedBy));
            }

            if (!string.IsNullOrEmpty(searchResolvedBy))
            {
                data = data.Where(x => x.Resolved_By.Contains(searchResolvedBy));
            }

            if (searchStatus != "0")
            {
                data = data.Where(x => x.Status.Contains(searchStatus));
            }
        }
        lst = new List<Manager_Report>();
        foreach (var x in data)
        {
            obj = lst.Where(y => y.Ticket_Id == x.Id).FirstOrDefault();
            if (obj == null)
            {
                obj = new Manager_Report
                {
                    Created_At = x.Created_At.ToString(),
                    Created_By = x.Created_By,
                    Status = x.Status,
                    Subject = x.Subject,
                    Ticket_Id = x.Id,
                    Category = x.Category
                };
                lst.Add(obj);
            }
            else
            {
                if (x.Event_Status == "Resolved")
                {
                    obj.Resolved_At = x.Resolved_At.ToString();
                    obj.Resolved_By = x.Resolved_By;
                }
            }
        }
        if (!export)
        {
            gvReport.DataSource = lst;
            gvReport.DataBind();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        GetUsersIds();
        BindData();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        GetUsersIds();
        BindData(true);
        fileName = "report_" + DateTimeHelper.ToTimeStamp() + ".xls";
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
        excelData += "Ticket ID\tCategory\tCreated By\tResolved By\tCreated At\tResolved At\tCurrent Status\t\n";
        foreach (var x in lst)
        {
            excelData += x.Ticket_Id + "\t" + x.Category + "\t" + x.Created_By + "\t" + x.Created_At + "\t" +
                x.Resolved_At + "\t" + x.Status + "\t\n";
        }
    }

    protected string ShowUrl(object obj)
    {
        return "show.aspx?id=" + obj.ToString();
    }

    void GetUsersIds()
    {
        _entity = GetEntity();
        userIds = new List<long>();
        var id = CurrentUser.Id();
        if (CurrentUser.Is_Manager())
        {
            var deptIds = _entity.Departments.Where(x => x.Manager_Id == id || x.Manager_Id_2 == id || x.Manager_Id_3 == id).Select(x => x.Id).ToList();
            userIds = _entity.tbl_Users.Where(x => deptIds.Contains(x.Department_Id)).Select(x => x.Id).ToList();
        }
        else
        {
            var deptId = long.Parse(ddlDept.SelectedValue);
            userIds = _entity.tbl_Users.Where(x => x.Department_Id == deptId).Select(x => x.Id).ToList();
        }
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        searchId = 0;
        long.TryParse(txtId.Text, out searchId);
        searchCategory = txtCategory.Text.Trim();
        searchCreatedBy = txtCreatedBy.Text.Trim();
        searchResolvedBy = txtResolvedBy.Text.Trim();
        searchStatus = ddlStatus.SelectedValue;
        GetUsersIds();
        BindData(false, true);
    }
}