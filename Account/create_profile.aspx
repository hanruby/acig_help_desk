<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="create_profile.aspx.cs" Inherits="Account_create_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Create Profile</legend>
        <p>
            <asp:Label ID="lblUserName" runat="server" Text="User Name" AssociatedControlID="txtUserName"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
            <asp:TextBox ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblDepartment" runat="server" Text='Department'></asp:Label>
            <br />
            <asp:DropDownList ID="ddlDepartment" runat="server" DataSourceID="SqlDataSource4"
                DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                <asp:ListItem Value="0">Select</asp:ListItem>
            </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                SelectCommand="SELECT [Id], [Name] FROM [Departments]"></asp:SqlDataSource>
            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Button ID="btnAddNewUser" runat="server" Text="Create Profile" CssClass="btn btn-primary"
                OnClick="btnAddNewUser_Click" />
        </p>
    </fieldset>
</asp:Content>
