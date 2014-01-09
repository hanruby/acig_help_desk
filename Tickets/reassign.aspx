<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="reassign.aspx.cs" Inherits="Tickets_reassign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Reassign Ticket
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered">
                    <tr>
                        <th>
                            Search by ID
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtSearch" runat="server" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="search">*</asp:RequiredFieldValidator>
                            <br />
                            <asp:RegularExpressionValidator ID="revSearch" runat="server" ControlToValidate="txtSearch"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationExpression="^\d*$" ValidationGroup="search">
                        Please enter id in correct format!</asp:RegularExpressionValidator>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                OnClick="btnSearch_Click" ValidationGroup="search" />
                        </td>
                    </tr>
                </table>
                <div id="reassignDiv" runat="server">
                    <table style="width: 100%;" class="table table-bordered">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnFldTicketId" runat="server" />
                                <asp:Label ID="lblId" runat="server" Text="" CssClass="label label-success"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblState" runat="server" Text="" CssClass="label label-success"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCreatedBy" runat="server" Text="" CssClass="label label-success"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAssignedTo" runat="server" Text="" CssClass="label label-success"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblReassignTo" runat="server" Text="Reassign To" AssociatedControlID="lstBxAssignTo"></asp:Label>
                                <asp:ListBox ID="lstBxAssignTo" runat="server" SelectionMode="Multiple" CssClass="input-xlarge" Height="300px">
                                </asp:ListBox>
                                <asp:RequiredFieldValidator ID="rfvAssignTo" runat="server" ControlToValidate="lstBxAssignTo"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assign" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Reassign" CssClass="btn btn-primary"
                                    OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
