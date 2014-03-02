<%@ Page Title="Log In" Language="C#" MasterPageFile="~/login.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="row-fluid">
                <div class="span12 center login-header">
                    <h2>
                        Welcome to ACIG IT Help Desk System!</h2>
                </div>
                <!--/span-->
            </div>
            <!--/row-->
            <div class="row-fluid">
                <div class="span4">
                    <asp:Image ID="imgLogo" runat="server" />
                </div>
                <div class="well span5 center login-box">
                    <div class="alert alert-info">
                        Please login with your Username and Password.
                    </div>
                    <fieldset>
                        <div id="errorDiv" runat="server" class="alert alert-error">
                            <asp:Label ID="errorLabel" runat="server" Text="Incorrect username or password!"></asp:Label>
                        </div>
                        <div class="input-prepend" title="Username" data-rel="tooltip">
                            <span class="add-on"><i class="icon-user"></i></span>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="input-large span4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="input-prepend" title="Password" data-rel="tooltip">
                            <span class="add-on"><i class="icon-lock"></i></span>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="input-large span4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                          <div class="clearfix">
                        </div>
                        <div class="input-prepend" title="Account Type" data-rel="tooltip" style="padding-right: 11px;">
                            <span class="add-on"><i class="icon-th"></i></span>
                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="input-large span4">
                            </asp:DropDownList>
                        </div>
                        <p class="center span5">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Sign in" CssClass="btn btn-primary" />
                        </p>
                        <p class="center span5">
                            <a href="create_profile.aspx">Create Profile</a>
                        </p>
                    </fieldset>
                </div>
                <!--/span-->
            </div>
            <!--/row-->
        </div>
        <!--/fluid-row-->
    </div>
    <!--/.fluid-container-->
</asp:Content>
