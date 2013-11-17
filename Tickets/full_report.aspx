<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="full_report.aspx.cs" Inherits="Tickets_full_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
<script type="text/javascript" src="../scripts/datepicker-range.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <progresstemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                    top: 39%; left: 42%;" />
            </div>
        </progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <div class="row-fluid">
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-info-sign"></i>&nbsp; Full Report
                        </h2>
                    </div>
                    <div class="box-content">
                        <div>
                            <h3 style="margin: 0;">
                                <a href="#">
                                    <asp:Label ID="lblTicketsCreated" runat="server" Text=""></asp:Label></a></h3>
                        </div>
                        <div class="right">
                            <asp:LinkButton ID="lnkBtnAllTC" runat="server" OnClick="lnkBtnAllTC_Click">All</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnThisWeekTC" runat="server" OnClick="lnkBtnThisWeekTC_Click">This Week</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnLastWeekTC" runat="server" OnClick="lnkBtnLastWeekTC_Click">Last Week</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnThisMonthTC" runat="server" OnClick="lnkBtnThisMonthTC_Click">This Month</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnLastMonthTC" runat="server" OnClick="lnkBtnLastMonthTC_Click">Last Month</asp:LinkButton>
                            &nbsp;
                            <asp:TextBox ID="txtStartDateTC" runat="server" CssClass="input-small start-date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStartDateTC" runat="server" ControlToValidate="txtStartDateTC"
                            ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="created">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEndDateTC" runat="server" CssClass="input-small end-date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEndDateTC" runat="server" ControlToValidate="txtEndDateTC"
                            ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="created">*</asp:RequiredFieldValidator>
                            <asp:Button ID="btnSearchTC" runat="server" Text="Search" CssClass="btn btn-success"
                            OnClick="btnSearchTC_Click" ValidationGroup="created" />
                        </div>
                        <div class="clear">
                        </div>
                        <asp:Repeater ID="rptrFull" runat="server">
                            <HeaderTemplate>
                                <table class="table table-bordered">
                                    <tr>
                                        <th>
                                            Department
                                        </th>
                                        <th>
                                            Tickets
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("email") %>
                                    </td>
                                    <asp:Repeater ID="rptrStates" runat="server" DataSource='<%# Eval("states") %>'>
                                        <HeaderTemplate>
                                            <td>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>
                                                            State
                                                        </th>
                                                        <th>
                                                            Count
                                                        </th>
                                                    </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("state") %>
                                                </td>
                                                <td>
                                                     <asp:Label ID="lblTecketsCountByDept" runat="server" Text='<%# Eval("Count") %>' CssClass="label label-success"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                            </td>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table> </hr>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="row-fluid">
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-info-sign"></i>&nbsp; Full Report
                        </h2>
                    </div>
                    <div class="box-content">
                        <div>
                            <h3 style="margin: 0;">
                                <a href="#">
                                    <asp:Label ID="lblTicketsAssigned" runat="server" Text=""></asp:Label></a></h3>
                        </div>
                        <div class="right">
                            <asp:LinkButton ID="lnkBtnAll" runat="server" OnClick="lnkBtnAll_Click">All</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnThisWeek" runat="server" OnClick="lnkBtnThisWeek_Click">This Week</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnLastWeek" runat="server" OnClick="lnkBtnLastWeek_Click">Last Week</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnThisMonth" runat="server" OnClick="lnkBtnThisMonth_Click">This Month</asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="lnkBtnLastMonth" runat="server" OnClick="lnkBtnLastMonth_Click">Last Month</asp:LinkButton>
                            &nbsp;
                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="input-small start-date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                            ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assigned">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="input-small end-date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                            ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assigned">*</asp:RequiredFieldValidator>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success"
                            OnClick="btnSearch_Click" ValidationGroup="assigned" />
                        </div>
                        <div class="clear">
                        </div>
                        <asp:Repeater ID="rptrFullReportAssigned" runat="server">
                            <HeaderTemplate>
                                <table class="table table-bordered">
                                    <tr>
                                        <th>
                                            Department
                                        </th>
                                        <th>
                                            Tickets
                                        </th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Eval("email") %>
                                    </td>
                                    <asp:Repeater ID="rptrStates" runat="server" DataSource='<%# Eval("states") %>'>
                                        <HeaderTemplate>
                                            <td>
                                                <table class="table table-bordered">
                                                    <tr>
                                                        <th>
                                                            State
                                                        </th>
                                                        <th>
                                                            Count
                                                        </th>
                                                    </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <%# Eval("state") %>
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblTecketsCount" runat="server" Text='<%# Eval("Count") %>' CssClass="label label-success"></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table> </td>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table> 
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
