﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h3>
        <asp:LinkButton ID="lnkBtnPending" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/pending.aspx">Tickets Waiting My Response</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnResponded" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/responded.aspx">Tickets Waiting Engineer Response</asp:LinkButton>
        &nbsp;
    </h3>
    <h3 runat="server" id="assignedLnkBtns">
        <asp:LinkButton ID="lnkBtnAssignedPending" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/assigned.aspx">Assigned Tickets Waiting My Response</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnAssignedResponded" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/responded.aspx">Assigned Tickets Waiting User's Response</asp:LinkButton>
        &nbsp;
    </h3>
    <div class="clear">
    </div>
    <br />
</asp:Content>
