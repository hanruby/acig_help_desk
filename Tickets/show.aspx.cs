﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Acig_Help_DeskModel;

public partial class Tickets_show : MasterAppPage
{
    Ticket _ticket;
    Comment _comment;
    Event _event;
    long _id;
    string routePath;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lnkBtnClarification.Visible = lnkBtnClarify.Visible = lnkBtnResolve.Visible = lnkBtnClose.Visible = lnkBtnReOpen.Visible = false;
            routePath = Route.GetRootPath("");
            _id = long.Parse(Request.QueryString["id"]);
            hdnFldTicketId.Value = _id.ToString();
            _entity = new Acig_Help_DeskEntities();
            var ticketData = from t in _entity.Tickets
                             join ssc in _entity.Sub_Sub_Categories
                             on t.Sub_Sub_Category_Id equals ssc.Id
                             join sc in _entity.Sub_Categories
                             on ssc.Sub_Category_Id equals sc.Id
                             join c in _entity.Categories
                             on sc.Category_Id equals c.Id
                             join u in _entity.tbl_Users
                             on t.Created_By equals u.Id
                             where t.Id == _id
                             select new
                             {
                                 Id = t.Id,
                                 AssignFromId = u.Id,
                                 BehalfOf = t.On_Behalf,
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

            var data = from ev in _entity.Events
                       where ev.Ticket_Id == _id
                       orderby ev.Created_At
                       select new
                       {
                           CreatedAt = ev.Created_At,
                           State = ev.State
                       };
            gvEvents.DataSource = data;
            gvEvents.DataBind();

            BindAssignedUsers();

            BindGVComments();

            currentUserId = CurrentUser.Id();
            foreach (var x in ticketData)
            {
                if ((currentUserId == x.AssignFromId || currentUserId == x.BehalfOf) && x.State == "Resolved")
                {
                    lnkBtnClose.Visible = true;
                    lnkBtnClose.PostBackUrl = routePath + "tickets/close.aspx?id=" + x.Id;

                    lnkBtnReOpen.Visible = true;
                    lnkBtnReOpen.PostBackUrl = routePath + "tickets/re_open.aspx?id=" + x.Id;
                }
                else if ((currentUserId == x.AssignFromId || currentUserId == x.BehalfOf) && x.State == "Clarification")
                {
                    lnkBtnClarify.Visible = true;
                    lnkBtnClarify.PostBackUrl = routePath + "tickets/clarify.aspx?id=" + x.Id;
                }
                else if (CanResolve(_id))
                {
                    NewCommentDiv.Visible = true;
                    lnkBtnClarification.Visible = true;
                    lnkBtnClarification.PostBackUrl = routePath + "tickets/clarification.aspx?id=" + x.Id;

                    lnkBtnResolve.Visible = true;
                    lnkBtnResolve.PostBackUrl = routePath + "tickets/resolve.aspx?id=" + x.Id;
                }
                ShowOrHideCommentDiv(x.AssignFromId, x.BehalfOf);
            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        _id = long.Parse(hdnFldTicketId.Value);
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();
        _comment = new Comment
        {
            Created_At = DateTime.Now,
            Created_By = currentUserId,
            Notes = txtDescription.Text
        };
        _comment.Ticket_Id = _id;
        HttpPostedFile postedFile = Request.Files["uploadFile"];
        if (postedFile != null && postedFile.ContentLength > 0)
        {
            var outputFile = FileHelper.SaveFile(postedFile, _id);
            _comment.File_Name = (string)outputFile["FileName"];
            _comment.File_Path = (string)outputFile["FilePath"];
        }
        _entity.AddToComments(_comment);
        _entity.SaveChanges();
        txtDescription.Text = string.Empty;
        BindGVComments();
        HtmlEmailer emailer = new HtmlEmailer(_entity, _comment.Ticket);
        emailer.New_Comment_TicketEMail();
    }

    void BindGVComments()
    {
        BindCommentsRepeater(rptrComments, _id);
    }

    protected void btnCloseTicket_Click(object sender, EventArgs e)
    {
        _id = long.Parse(hdnFldTicketId.Value);
        currentUserId = CurrentUser.Id();
        _entity = GetEntity();
        _ticket = _entity.Tickets.Where(x => x.Id == _id).First();
        _ticket.State = "Closed";
        _ticket.Closed_Date = DateTime.Now;
        _event = new Event
        {
            Created_At = DateTime.Now,
            State = "Closed",
            Created_By = currentUserId,
            Ticket_Id = _id
        };
        _entity.AddToEvents(_event);
        _entity.SaveChanges();
        SuccessRedirect(Route.GetRootPath("tickets/index.aspx"), "Successfully Closed Ticket!");
    }

    protected void BindAssignedUsers()
    {
        var data = from ut in _entity.User_Tickets
                   join u in _entity.tbl_Users
                   on ut.User_Id equals u.Id
                   where ut.Ticket_Id == _id
                   select new { Name = u.Email };
        rptrAssignedUsers.DataSource = data;
        rptrAssignedUsers.DataBind();
    }

    void ShowOrHideCommentDiv(long createdBy, long? behalfOf)
    {
        if (currentUserId == createdBy || currentUserId == behalfOf)
        {
            NewCommentDiv.Visible = true;
            return;
        }
        NewCommentDiv.Visible = _entity.User_Tickets.Where(x => x.User_Id == currentUserId && x.Ticket_Id == _id).Count() > 0;
    }
}