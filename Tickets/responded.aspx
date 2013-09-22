<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="responded.aspx.cs" Inherits="Tickets_responded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        <asp:LinkButton ID="lnkBtnMainHeader" runat="server">
            <asp:Label ID="lblMainHeader" runat="server" Text=""></asp:Label>
        </asp:LinkButton>
    </h2>
    <hr />
    <h3>
        <asp:Label ID="lblOpen" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsOpen" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsOpen_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="gvTicketsOpen_PageIndexChanging" PageSize="10">
    </asp:GridView>
    <hr />
    <h3>
        <asp:Label ID="lblClarified" runat="server" Text=""></asp:Label>
    </h3>
    <asp:GridView ID="gvTicketsClarified" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTicketsClarified_RowDataBound" AllowPaging="true"
        OnPageIndexChanging="gvTicketsClarified_PageIndexChanging" PageSize="10">
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

