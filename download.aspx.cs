using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class download : MasterAppPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        _entity = new Acig_Help_DeskEntities();
        var _commentId = long.Parse(Request.QueryString["id"]);
        var _comment = _entity.Comments.Where(x => x.Id == _commentId).FirstOrDefault();
        if ((_comment == null) || (string.IsNullOrEmpty(_comment.File_Path)))
        {
            var path = CurrentUser.GetRedirectPath(CurrentUser.Role());
            var message = _comment == null ? "Comment does not exists in the system !" : "Comment  does not have any file attached !";
            Session["ErrorMessage"] = message;
            Response.Redirect(path);
            return;
        }
        System.Web.HttpResponse response = System.Web.HttpContext.Current.Response;
        response.ClearContent();
        response.Clear();
        response.ContentType = "text/plain";
        response.AddHeader("Content-Disposition", "attachment; filename=" + _comment.File_Name + ";");
        response.TransmitFile(_comment.File_Path);
        response.Flush();
        response.End();
    }
}