<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="full_report.aspx.cs" Inherits="Tickets_full_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
   <asp:Repeater ID="rptrFull" runat="server">
        <HeaderTemplate>
            <h3><a href="#">Tickets Created By Department !!</a></h3>
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
                        <td><%# Eval("state") %></td>
                        <td><%# Eval("count") %></td>
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
            </table>
            </hr>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="rptrFullReportAssigned" runat="server">
        <HeaderTemplate>
            <h3><a href="#">Tickets Assigned To Department !!</a></h3>
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
                        <td><%# Eval("state") %></td>
                        <td><%# Eval("count") %></td>
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
            </table>
            </hr>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>

