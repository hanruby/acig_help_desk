﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="assigned_responded.aspx.cs" Inherits="Tickets_assigned_responded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>
        <asp:LinkButton ID="lnkBtnMainHeader" runat="server">
            <asp:Label ID="lblMainHeader" runat="server" Text=""></asp:Label>
        </asp:LinkButton>
    </h2>
    <hr />
    <h3>
        <asp:Label ID="lblClarification" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsClarification" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsClarification_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="gvTicketsClarification_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
    <h3>
        <asp:Label ID="lblResolved" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsResolved" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsResolved_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="gvTicketsResolved_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
    <h3>
        <asp:Label ID="lblClosed" runat="server" Text="Label"></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsClosed" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsClosed_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="gvTicketsClosed_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
</asp:Content>

