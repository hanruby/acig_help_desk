<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="edit.aspx.cs" Inherits="Admin_Users_edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; Edit User Profile
                </h2>
            </div>
            <div class="box-content">
                <asp:HiddenField ID="hdnFldUserId" runat="server" />
                <table class="table table-bordered">
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server" Text="User Name" AssociatedControlID="txtUserName"></asp:Label>
                            <asp:TextBox ID="txtUserName" runat="server" MaxLength="200" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="200" CssClass="input-xlarge"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblRole" runat="server" Text="Role" AssociatedControlID="ddlRole"></asp:Label>
                            <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" CssClass="input-xlarge">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="user">User</asp:ListItem>
                                <asp:ListItem Value="engineer">Engineer</asp:ListItem>
                                <asp:ListItem Value="supervisor">Supervisor</asp:ListItem>
                                <asp:ListItem Value="manager">Manager</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lblActive" runat="server" Text='Active'></asp:Label>
                            <br />
                            <asp:DropDownList ID="ddlActive" runat="server"  CssClass="input-xlarge">
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
                            <asp:Label ID="lblSubSubCategory" runat="server" Text='Category(ies)'></asp:Label>
                            <br />
                            <asp:ListBox ID="lstBoxSubSubCategory" runat="server" SelectionMode="Multiple"
                             CssClass="input-xlarge" Height="500px">
                            </asp:ListBox>
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
                            <asp:Button ID="btnUpdateUser" runat="server" Text="Update" CssClass="btn btn-primary"
                                OnClick="btnUpdateUser_Click" />
                        </td>
                        <td>
                            <asp:HyperLink ID="hprLnkBack" runat="server" CssClass="btn btn-danger" 
                            NavigateUrl="index.aspx">Cancel</asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
