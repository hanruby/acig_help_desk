<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="sreport.aspx.cs" Inherits="Tickets_sreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  <asp:Repeater ID="rptrUserAssigned" runat="server">
        <HeaderTemplate>
            <h3><a href="#">Tickets Assigned To My Department Users !!</a></h3>
            <table class="table table-bordered">
                <tr>
                    <th>
                        Email
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
    <asp:Repeater ID="rptrUser" runat="server">
        <HeaderTemplate>
            <h3><a href="#">Tickets Created By My Department Users !!</a></h3>
            <table class="table table-bordered">
                <tr>
                    <th>
                        Email
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
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
