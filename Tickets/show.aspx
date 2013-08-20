﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="show.aspx.cs" Inherits="Tickets_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Ticket Details</h2>
    <hr />
    <h4>
        Ticket Events</h4>
    <asp:GridView ID="gvEvents" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
        ShowHeader="true" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="State">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("State")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created At">
                <ItemTemplate>
                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <h4>
        Details</h4>
    <asp:Repeater ID="rptrTickets" runat="server">
        <HeaderTemplate>
            <table width="100%">
        </HeaderTemplate>
        <ItemTemplate>
           <tr>
             <td><label>Created By: </label><%# Eval("AssignFrom") %></td>
             <td><label>Assigned To: </label><%# Eval("AssignTo") %></td>
           </tr>
           <tr>
             <td><label>Category: </label><%# Eval("CategoryName") %></td>
             <td><label>Sub Category: </label><%# Eval("SubCategoryName") %></td>
           </tr>
           <tr>
             <td><label>Type: </label><%# Eval("Type") %></td>
             <td><label>Priority: </label><%# Eval("Priority") %></td>
           </tr>
           <tr>
             <td><label>Subject: </label><%# Eval("Subject") %></td>
             <td><label>Current State: </label><%# Eval("State") %></td>
           </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <hr />
    <h4>
        Comments</h4>
    <asp:GridView ID="gvComments" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
        ShowHeader="true" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Created At">
                <ItemTemplate>
                    <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Notes">
                <ItemTemplate>
                    <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Notes")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="File">
                <ItemTemplate>
                    <asp:HyperLink ID="hprLinkFile" runat="server" Visible='<%# FileLinkVisibile(Eval("Visible")) %>'
                        NavigateUrl='<%# FileDownloadUrl(Eval("Url")) %>'>Download</asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>