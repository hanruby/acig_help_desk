<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="ereport.aspx.cs" Inherits="Tickets_ereport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../scripts/datepicker-range.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnFldFilterType" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnFldFilterStartDate" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="hdnFldFilterEndDate" runat="server"></asp:HiddenField>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Report By Engineer
                </h2>
            </div>
            <div class="box-content">
                <div>
                    <h3 style="margin: 0;" class="left">
                        <a href="#">
                            <asp:Label ID="lblTicketsCreated" runat="server" Text=""></asp:Label>
                        </a>
                    </h3>
                    <asp:LinkButton ID="lnkBtnDownload" runat="server" CssClass="btn btn-primary right"
                        CausesValidation="false" OnClick="lnkBtnDownload_Click">
                                <i class="icon-download-alt icon-white"></i>
                                Download</asp:LinkButton>
                    <div class="clear">
                    </div>
                </div>
                <br />
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
                        <table class="table table-bordered table-striped">
                            <tr>
                                <th>
                                    Engineer
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
                                            <asp:Label ID="lblTecketsCountByDept" runat="server" Text='<%# Eval("Count") %>'
                                                CssClass="label label-success"></asp:Label>
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
</asp:Content>
