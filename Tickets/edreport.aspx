<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="edreport.aspx.cs" Inherits="Tickets_edreport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Tickets Detail By Engineer
                </h2>
            </div>
            <div class="box-content">
                <asp:LinkButton ID="lnkBtnDownload" runat="server" CssClass="btn btn-primary right"
                    CausesValidation="false" OnClick="lnkBtnDownload_Click">
                            <i class="icon-download-alt icon-white"></i>
                            Download
                </asp:LinkButton>
                <div class="clear">
                </div>
                <br />
                <table style="width: 100%;" class="table table-bordered">
                    <tr>
                        <th>
                            State
                        </th>
                        <th>
                            Engineer
                        </th>
                        <th>
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlTicketStatus" runat="server" CssClass="input-xlarge">
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                                <asp:ListItem Value="Closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEngineer" runat="server" AutoPostBack="True" CssClass="input-xlarge">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gvTickets" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" AutoGenerateColumns="false" AllowPaging="true" PageSize="20"
                    OnPageIndexChanging="gvTickets_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Ticket ID">
                            <ItemTemplate>
                                <a href='show.aspx?id=<%# Eval("Id") %>'>
                                    <%# Eval("Id") %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Open At">
                            <ItemTemplate>
                                <asp:Label ID="lblTicketOpenAt" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">
                            <ItemTemplate>
                                <asp:Label ID="lblSubject" runat="server" Text='<%# Eval("Subject")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Open By">
                            <ItemTemplate>
                                <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("CreatedBy")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Assigned To">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("AssignedTo")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("Status")%>' CssClass="label label-info">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
