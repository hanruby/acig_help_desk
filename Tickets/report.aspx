<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="report.aspx.cs" Inherits="Tickets_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="divEngineer1" runat="server" class="left" style="width: 30%; margin-right: 10px;">
        <h3 style="display: inline;">
            <a href="#">My Assigned Tickets !!</a>
        </h3>
        <table class="table table-bordered">
            <tr>
                <td>
                    Pending
                </td>
                <td>
                    <asp:Label ID="lblAssignedPending" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Resolved
                </td>
                <td>
                    <asp:Label ID="lblAssignedResolved" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Closed
                </td>
                <td>
                    <asp:Label ID="lblAssignedClosed" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total
                </td>
                <td>
                    <asp:Label ID="lblAssignedTotal" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="left" style="width: 30%;">
        <h3 style="display: inline;">
            <a href="#">My Created Tickets !!</a>
        </h3>
        <table class="table table-bordered">
            <tr>
                <td>
                    Pending
                </td>
                <td>
                    <asp:Label ID="lblTotalPending" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Resolved
                </td>
                <td>
                    <asp:Label ID="lblTotalResolved" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Closed
                </td>
                <td>
                    <asp:Label ID="lblTotalClosed" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    Total
                </td>
                <td>
                    <asp:Label ID="lblTotalCount" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div class="clear">
    </div>
    <hr />
    <div runat="server" id="divEngineer2">
        <h3>
            <a href="#">My Assigned Tickets By Department !!</a></h3>
        <asp:Repeater ID="rptrDeptTickets" runat="server">
            <HeaderTemplate>
                <table class="table table-bordered ui-table" style="width: 50%;">
                    <tr>
                        <th>
                            Department
                        </th>
                        <th>
                            Count
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td>
                        <%# Eval("Department") %>
                    </td>
                    <td>
                        <%# Eval("Count") %>
                    </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <hr />
    </div>
</asp:Content>
