<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="full_report.aspx.cs" Inherits="Tickets_full_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../scripts/datepicker-range.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                    top: 39%; left: 42%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Repeater ID="rptrFull" runat="server">
                <HeaderTemplate>
                    <h3>
                        <a href="#">Tickets Created By Department !!</a></h3>
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
                                        <%# Eval("count") %>
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
                    </table> </hr>
                </FooterTemplate>
            </asp:Repeater>
            <div class="left">
                <h3 style="margin: 0;">
                    <a href="#">
                        <asp:Label ID="lblTicketsAssigned" runat="server" Text=""></asp:Label></a></h3>
            </div>
            <div class="right">
                <asp:LinkButton ID="lnkBtnAll" runat="server" 
                    onclick="lnkBtnAll_Click">All</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkBtnThisWeek" runat="server" 
                    onclick="lnkBtnThisWeek_Click">This Week</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkBtnLastWeek" runat="server" 
                    onclick="lnkBtnLastWeek_Click">Last Week</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkBtnThisMonth" runat="server" 
                    onclick="lnkBtnThisMonth_Click">This Month</asp:LinkButton>
                &nbsp;
                <asp:LinkButton ID="lnkBtnLastMonth" runat="server" 
                    onclick="lnkBtnLastMonth_Click">Last Month</asp:LinkButton>
                &nbsp;
                <asp:TextBox ID="txtStartDate" runat="server" CssClass="input-small start-date"></asp:TextBox>
                 <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ControlToValidate="txtStartDate"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assigned">*</asp:RequiredFieldValidator>
                <asp:TextBox ID="txtEndDate" runat="server" CssClass="input-small end-date"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ControlToValidate="txtEndDate"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assigned">*</asp:RequiredFieldValidator>
                <asp:Button ID="btnSearch" runat="server" Text="Search" 
                    CssClass="btn btn-success" onclick="btnSearch_Click" ValidationGroup="assigned" />
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
                                        <%# Eval("count") %>
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
                    </table> </hr>
                </FooterTemplate>
            </asp:Repeater>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
