<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:Repeater ID="rptrAssignedTickets" runat="server">
        <HeaderTemplate>
            <div class="sortable row-fluid">
        </HeaderTemplate>
        <ItemTemplate>
            <a data-rel="tooltip" title="" class="well span3 top-block" href='<%# Eval("Href") %>'><span
                class="icon-red icon-leaf"></span>
                <div>
                    <%# Eval("Name") %></div>
                <div><%# Eval("ImagePath")%></div>
                <span class="notification"><%# Eval("ImagePath")%></span>
            </a>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="rptrTickets" runat="server">
        <HeaderTemplate>
            <div class="sortable row-fluid">
        </HeaderTemplate>
        <ItemTemplate>
            <a data-rel="tooltip" title="" class="well span3 top-block" href='<%# Eval("Href") %>'><span
                class="icon-red icon-leaf"></span>
                <div>
                    <%# Eval("Name") %></div>
                <div><%# Eval("ImagePath")%></div>
                <span class="notification"><%# Eval("ImagePath")%></span>
            </a>
        </ItemTemplate>
        <FooterTemplate>
            </div>
        </FooterTemplate>
    </asp:Repeater>
</asp:Content>
