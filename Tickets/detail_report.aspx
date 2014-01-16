<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="detail_report.aspx.cs" Inherits="Tickets_detail_report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Search
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped" runat="server" id="Table1">
                    <tr>
                        <th>Id</th>
                        <th>Category</th>
                        <th>Created By</th>
                        <th>Resolved By</th>
                        <th>Status</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtId" runat="server" CssClass="input-medium"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCategory" runat="server" CssClass="input-medium"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCreatedBy" runat="server" CssClass="input-medium"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtResolvedBy" runat="server" CssClass="input-medium"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlStatus" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnFilter" runat="server" Text="Search" 
                                CssClass="btn btn-success" onclick="btnFilter_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; My Department Report Details
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered table-striped" runat="server" id="adminTable">
                    <tr>
                        <td>Select Department</td>
                        <td>
                            <asp:DropDownList ID="ddlDept" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                CssClass="btn btn-success" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="right">
                    <asp:Button ID="btnExport" runat="server" Text="Export To Excel" 
                        CssClass="btn btn-primary" onclick="btnExport_Click" /> 
                </div>
                <div class="clear">
                </div>
                <br />
                <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records"
                    Width="100%" ShowFooter="true" CssClass="table table-bordered table-striped">
                    <Columns>
                        <asp:TemplateField HeaderText="Id">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Ticket_Id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created By">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved By">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Resolved_By") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Created At">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Created_At") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved At">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Resolved_At") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Current Status">
                            <ItemTemplate>
                                <asp:Label ID="lbl" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details">
                            <ItemTemplate>
                                <asp:HyperLink ID="hprLnkDetails" runat="server" CssClass="btn btn-primary" NavigateUrl='<%# ShowUrl(Eval("Ticket_Id")) %>'>
                                    <i class="icon-zoom-in icon-white"></i>
                                    Details
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
