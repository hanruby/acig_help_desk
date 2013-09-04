<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="report.aspx.cs" Inherits="Tickets_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h3>
        <a href="#">My Assigned Tickets !</a>
    </h3>
    <table runat="server" id="tblAssigned" class="table table-bordered">
        <tr>
            <th>
                Total Assigned Pending Tickets
            </th>
            <th>
                Total Assigned Resolved Tickets
            </th>
            <th>
                Total Assigned Closed Tickets
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAssignedPending" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAssignedResolved" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblAssignedClosed" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <hr />
    <h3>
        <a href="#">My Created Tickets !</a>
    </h3>
    <table class="table table-bordered">
        <tr>
            <th>
                Total Pending
            </th>
            <th>
                Total Resolved
            </th>
            <th>
                Total Closed
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotalPending" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalResolved" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lblTotalClosed" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
