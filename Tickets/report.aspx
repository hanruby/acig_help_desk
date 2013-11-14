<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="report.aspx.cs" Inherits="Tickets_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h3>
        <asp:LinkButton ID="lnkBtnReport" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/Tickets/report.aspx">Report</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnReportSuperVisor" CommandArgument="supervisor" runat="server"
            CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/sreport.aspx">Report By Supervisor</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnFullReport" CommandArgument="manager,vp,coo,ceo" runat="server"
            CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/full_report.aspx">Full Report</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnReportByUser" CommandArgument="manager,vp,coo,ceo" runat="server"
            CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/full_report2.aspx">Report By User</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnReportByEngineer" CommandArgument="manager,vp,coo,ceo"
            runat="server" CausesValidation="false" CssClass="btn btn-inverse" PostBackUrl="~/Tickets/ereport.aspx">Report By Engineer</asp:LinkButton>
    </h3>
    <div class="clear">
    </div>
    <br />
    <div class="row-fluid">
        <div id="divEngineer1" runat="server">
            <div class="box span6">
                <div class="box-header well">
                    <h2>
                        <i class="icon-info-sign"></i>&nbsp; My Assigned Tickets
                    </h2>
                </div>
                <div class="box-content">
                    <table class="table">
                        <tr>
                            <td>
                                Pending
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedPending" runat="server" Text="Label" CssClass="label label-warning"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Resolved
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedResolved" runat="server" Text="Label" CssClass="label label-inverse"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Closed
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedClosed" runat="server" Text="Label" CssClass="label label-success"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Total
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedTotal" runat="server" Text="Label" CssClass="label"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="box span6">
                <div class="box-header well">
                    <h2>
                        <i class="icon-info-sign"></i>&nbsp; My Assigned Tickets By Department !!
                    </h2>
                </div>
                <div class="box-content">
                    <asp:Repeater ID="rptrDeptTickets" runat="server">
                        <HeaderTemplate>
                            <table class="table">
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
                                    <asp:Label ID="lblTecketsByDept" runat="server" Text='<%# Eval("Count") %>' CssClass="label label-success"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; My Created Tickets
                </h2>
            </div>
            <div class="box-content">
                <table class="table">
                    <tr>
                        <td>
                            Pending
                        </td>
                        <td>
                            <asp:Label ID="lblTotalPending" runat="server" Text="Label" CssClass="label label-warning"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Resolved
                        </td>
                        <td>
                            <asp:Label ID="lblTotalResolved" runat="server" Text="Label" CssClass="label label-inverse"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Closed
                        </td>
                        <td>
                            <asp:Label ID="lblTotalClosed" runat="server" Text="Label" CssClass="label label-success"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Total
                        </td>
                        <td>
                            <asp:Label ID="lblTotalCount" runat="server" Text="Label" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
