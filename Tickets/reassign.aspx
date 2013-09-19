<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="reassign.aspx.cs" Inherits="Tickets_reassign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h2>
        Reassign Ticket
    </h2>
    <fieldset class="login">
        <legend>Search Ticket</legend>
        <table style="width: 50%;" class="table table-bordered">
            <tr>
                <th>
                    Id
                </th>
                <th>
                </th>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch"
                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="search">*</asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revSearch" runat="server" ControlToValidate="txtSearch"
                        ForeColor="#FF3300" SetFocusOnError="True" ValidationExpression="^\d*$" ValidationGroup="search">
                        Please enter id in correct format!</asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                        OnClick="btnSearch_Click" ValidationGroup="search" />
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset class="login" runat="server" id="reassignDiv">
        <legend>Reassign Ticket</legend>
        <table style="width: 100%;" class="table table-bordered">
            <tr>
                <td>
                    <asp:HiddenField ID="hdnFldTicketId" runat="server" />
                    <asp:Label ID="lblId" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblState" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCreatedBy" runat="server" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblAssignedTo" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblReassignTo" runat="server" Text="Reassign To" AssociatedControlID="lstBxAssignTo"></asp:Label>
                    <asp:ListBox ID="lstBxAssignTo" runat="server"></asp:ListBox>
                    <asp:RequiredFieldValidator ID="rfvAssignTo" runat="server" ControlToValidate="lstBxAssignTo"
                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="assign" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Reassign" 
                        CssClass="btn btn-primary" onclick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
