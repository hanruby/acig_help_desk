<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Tickets_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnFldScope" runat="server" />
    <h3>
        <asp:Label ID="lblHeading" runat="server" Text="Label"></asp:Label>
    </h3>
    <asp:GridView ID="gvTickets" runat="server" Width="100%" CssClass="table table-bordered"
        EmptyDataText="No Records" OnRowDataBound="gvTickets_RowDataBound">
    </asp:GridView>
</asp:Content>
