<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="assigned.aspx.cs" Inherits="Tickets_assigned" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h3>
        <asp:Label ID="lblOpen" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsOpen" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsOpen_RowDataBound"
        AllowPaging="true" OnPageIndexChanging="gvTicketsOpen_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
    <h3>
        <asp:Label ID="lblResolved" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsResolved" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsResolved_RowDataBound"
        AllowPaging="true" OnPageIndexChanging="gvTicketsResolved_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
    <h3>
        <asp:Label ID="lblClosed" runat="server" Text="Label"></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsClosed" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsClosed_RowDataBound" 
        AllowPaging="true" OnPageIndexChanging="gvTicketsClosed_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
</asp:Content>

