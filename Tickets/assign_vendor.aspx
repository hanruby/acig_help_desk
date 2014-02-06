<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="assign_vendor.aspx.cs" Inherits="Tickets_assign_vendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Assign Ticket To Vendor
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
                                <asp:Label ID="lblReassignTo" runat="server" Text="Assign To" AssociatedControlID="ddlAssignTo"></asp:Label>
                                <asp:DropDownList ID="ddlAssignTo" runat="server">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rfvAssignTo" runat="server" ControlToValidate="ddlAssignTo"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assign" InitialValue="0">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnSave" runat="server" Text="Assign" CssClass="btn btn-primary" ValidationGroup="assign"
                                    OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

