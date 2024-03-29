﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="new.aspx.cs" Inherits="Admin_Users_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; New User Profile
                </h2>
            </div>
            <div class="box-content">
                <table class="table table-bordered">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text="User Name" AssociatedControlID="txtUserName"></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" MaxLength="200" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"
                                CssClass="input-xlarge"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRole" runat="server" Text="Role" AssociatedControlID="ddlRole"></asp:Label>
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged"
                                CssClass="input-xlarge">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="user">User</asp:ListItem>
                                <asp:ListItem Value="engineer" Selected="True">Engineer</asp:ListItem>
                                <asp:ListItem Value="supervisor">Supervisor</asp:ListItem>
                                <asp:ListItem Value="manager">Manager</asp:ListItem>
                                <asp:ListItem Value="vendor">Vendor</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblActive" runat="server" Text='Active'></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlActive" runat="server" CssClass="input-xlarge">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="True">True</asp:ListItem>
                                <asp:ListItem Value="False">False</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvActiveNew" runat="server" ControlToValidate="ddlActive"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAccountType" runat="server" Text='Account Type'></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="input-xlarge" 
                                onselectedindexchanged="ddlAccountType_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvAccountType" runat="server" ControlToValidate="ddlAccountType"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblPassword" runat="server" Text="Password" AssociatedControlID="txtPassword"
                                CssClass="input-xlarge"></asp:Label>
                            <asp:TextBox ID="txtPassword" runat="server" MaxLength="20" CssClass="input-xlarge" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="categoryDiv" runat="server">
                                <asp:Label ID="lblSubSubCategory" runat="server" Text='Category(ies)'></asp:Label>
                                <br />
                                <asp:ListBox ID="lstBoxSubSubCategory" runat="server" DataSourceID="SqlDataSource5"
                                    DataTextField="TEXT" DataValueField="ID" SelectionMode="Multiple" CssClass="input-xlarge"
                                    Height="500px"></asp:ListBox>
                                <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                    SelectCommand="select  (c.name + ' - ' + s.Name + ' - ' + ss.Name) as TEXT, ss.Id as ID  from Categories c inner join Sub_Categories s on c.Id = s.Category_Id inner join Sub_Sub_Categories ss on s.Id = ss.Sub_Category_Id order by c.Name, s.Name, ss.Name">
                                </asp:SqlDataSource>
                            </div>
                            <div id="vendorEmailsDiv" runat="server">
                                <asp:Label ID="lblVendorEmails" runat="server" Text="Emails ( ',' separated )"></asp:Label>
                                <br />
                                <asp:TextBox ID="txtVendorEmails" runat="server" MaxLength="250"></asp:TextBox>
                            </div>
                        </td>
                        <td>
                            <asp:Label ID="lblDepartment" runat="server" Text='Department'></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlDepartment" runat="server" DataSourceID="SqlDataSource4"
                                DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true" CssClass="input-xlarge">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                SelectCommand="SELECT [Id], [Name] FROM [Departments] ORDER BY [Name]"></asp:SqlDataSource>
                            <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnAddNewUser" runat="server" Text="Save" CssClass="btn btn-primary"
                                OnClick="btnAddNewUser_Click" />
                        </td>
                        <td>
                            <asp:HyperLink ID="hprLnkBack" runat="server" CssClass="btn btn-danger" NavigateUrl="index.aspx">Cancel</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
