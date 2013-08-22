<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div style="float: left; margin-left: 147px; margin-top: 15px;">
        <img src="../images/acig_logo_full.png" alt="Acig Logo" />
    </div>
    <fieldset class="login" style="float: right; width: 39%; margin-right: 94px; height: 360px;">
        <legend>Account Information</legend>
        <p>
            <asp:Label ID="lblUserName" runat="server" Text="Login Id" AssociatedControlID="txtUserName"></asp:Label>
            <asp:TextBox ID="txtUserName" runat="server" MaxLength="45"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"></asp:Label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" MaxLength="45"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Sign in" CssClass="btn btn-primary" />
        </p>
    </fieldset>
</asp:Content>
