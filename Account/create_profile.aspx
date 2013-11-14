<%@ Page Title="" Language="C#" MasterPageFile="~/login.master" AutoEventWireup="true"
    CodeFile="create_profile.aspx.cs" Inherits="Account_create_profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container-fluid">
        <div class="row-fluid">
            <div class="row-fluid">
                <div class="span12 center login-header">
                    <h2>
                        Welcome to Quotation Tracking System!</h2>
                </div>
            </div>
            <div class="row-fluid">
                <div class="well span5 center login-box">
                    <div class="alert alert-info">
                        Please create your profile.
                    </div>
                    <fieldset>
                        <div id="errorDiv" runat="server" class="alert alert-error">
                            <asp:Label ID="errorLabel" runat="server" Text="Email already taken!"></asp:Label>
                        </div>
                        <div class="input-prepend" title="User Name" data-rel="tooltip">
                            <span class="add-on"><i class="icon-user"></i></span>
                            <asp:TextBox ID="txtUserName" runat="server" MaxLength="20" CssClass="input-large span4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="input-prepend" title="Email" data-rel="tooltip">
                            <span class="add-on"><i class="icon-envelope"></i></span>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="30" CssClass="input-large span4"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </div>
                        <div class="clearfix">
                        </div>
                        <div class="input-prepend" title="Department" data-rel="tooltip">
                            <span class="add-on"><i class="icon-th"></i></span>
                            <asp:DropDownList ID="ddlDepartment" runat="server" DataSourceID="SqlDataSource4"
                                DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" CssClass="input-large span4">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                SelectCommand="SELECT [Id], [Name] FROM [Departments]"></asp:SqlDataSource>
                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </div>
                        <p class="center span5">
                            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Create Profile"
                                CssClass="btn btn-primary" />
                        </p>
                        <p class="center span5">
                            <a href="login.aspx">Login</a>
                        </p>
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
