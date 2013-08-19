<%@ Page Title="Log In" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Log In
    </h2>
    <p>
        Please enter your username and password.
    </p>
    <div class="accountInfo">
        <fieldset class="login">
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
    </div>
</asp:Content>
