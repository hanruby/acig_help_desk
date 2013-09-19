<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="report.aspx.cs" Inherits="Tickets_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h3>
    <asp:LinkButton ID="lnkBtnReport" runat="server" CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/report.aspx">Report</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="lnkBtnReportSuperVisor" CommandArgument="supervisor" runat="server"
     CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/sreport.aspx">Report By Supervisor</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="lnkBtnFullReport" CommandArgument="manager,vp,coo,ceo" runat="server"
     CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/full_report.aspx">Full Report</asp:LinkButton>
    &nbsp;
    <asp:LinkButton ID="lnkBtnReportByUser" CommandArgument="manager,vp,coo,ceo" runat="server" 
    CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/full_report2.aspx">Report By User</asp:LinkButton>
    </h3>
    <div class="clear"></div>
    <br />
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
