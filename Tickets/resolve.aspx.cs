﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_resolve : MasterAppPage
{
    Comment _comment;
    Ticket _ticket;
    Event _event;
    long _id;
    string routePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindBreadCrumbRepeater("ticket_resolve");
            routePath = Route.GetRootPath("");
            currentUserId = CurrentUser.Id();
            _id = long.Parse(Request.QueryString["id"]);
            hdnFldTicketId.Value = _id.ToString();
            _entity = new Acig_Help_DeskEntities();
            BindCommentsRepeater(rptrComments, _id);
            if (!CanResolve(_id))
            {
                ErrorRedirect(routePath + "not_authorized.aspx", "Not authorized to access that ticket!");
                return;
            }
            var ticketData = from t in _entity.Tickets
                             join ssc in _entity.Sub_Sub_Categories
                             on t.Sub_Sub_Category_Id equals ssc.Id
                             join sc in _entity.Sub_Categories
                             on ssc.Sub_Category_Id equals sc.Id
                             join c in _entity.Categories
                             on sc.Category_Id equals c.Id
                             join u in _entity.tbl_Users
                             on t.Created_By equals u.Id
                             where t.Id == _id && (t.State == "Pending" || t.State == "Not Resolved" || t.State == "Clarified" || t.State == "Clarification")
                             select new
                             {
                                 Id = t.Id,
                                 AssignFromId = u.Id,
                                 AssignFrom = u.Email,
                                 CategoryName = c.Name,
                                 SubCategoryName = sc.Name,
                                 Priority = t.Priority,
                                 State = t.State,
                                 Subject = t.Subject,
                                 Type = t.Type
                             };
            if (ticketData.Count() == 0)
            {
                ErrorRedirect(routePath + "not_authorized.aspx", "Not authorized to access that ticket!");
                return;
            }
            rptrTickets.DataSource = ticketData;
            rptrTickets.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        routePath = Route.GetRootPath("tickets/assigned.aspx");
        _id = long.Parse(hdnFldTicketId.Value);
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();

        _ticket = _entity.Tickets.Where(x => x.Id == _id).First();
        _ticket.Resolved_Date = DateTime.Now;
        _ticket.State = "Resolved";
        _entity.SaveChanges();

        _comment = new Comment
        {
            Created_At = DateTime.Now,
            Created_By = currentUserId,
            Notes = txtDescription.Text,
            Ticket_Id = _id
        };
        HttpPostedFile postedFile = Request.Files["uploadFile"];
        if (postedFile != null && postedFile.ContentLength > 0)
        {
            var outputFile = FileHelper.SaveFile(postedFile, _id);
            _comment.File_Name = (string)outputFile["FileName"];
            _comment.File_Path = (string)outputFile["FilePath"];
        }
        _entity.AddToComments(_comment);
        _entity.SaveChanges();

        _event = new Event
        {
            Created_At = DateTime.Now,
            State = "Resolved",
            Created_By = currentUserId,
            Ticket_Id = _id
        };
        _entity.AddToEvents(_event);
        _entity.SaveChanges();

        HtmlEmailer emailer = new HtmlEmailer(_entity, _ticket);
        emailer.Resoved_Ticket_EMail();

        SuccessRedirect(routePath, "Successfully updated !");
    }
}