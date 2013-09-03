<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="new.aspx.cs" Inherits="Admin_Users_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>New User Profile</legend>
        <table class="table table-bordered">
            <tr>
                <td>
                    <asp:Label ID="lblUserName" runat="server" Text="User Name" AssociatedControlID="txtUserName"></asp:Label>
                    <asp:TextBox ID="txtUserName" runat="server" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName"
                        ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="Email" AssociatedControlID="txtEmail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" MaxLength="200"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                        ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblRole" runat="server" Text="Role" AssociatedControlID="ddlRole"></asp:Label>
                    <asp:DropDownList ID="ddlRole" runat="server" Width="150px" AutoPostBack="True" 
                        onselectedindexchanged="ddlRole_SelectedIndexChanged">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="user">User</asp:ListItem>
                        <asp:ListItem Value="engineer">Engineer</asp:ListItem>
                        <asp:ListItem Value="manager">Manager</asp:ListItem>
                        <asp:ListItem Value="supervisor">Supervisor</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                        ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblActive" runat="server" Text='Active'></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlActive" runat="server" Width="150px">
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
                    <asp:ListBox ID="lstBoxSubSubCategory" runat="server" DataSourceID="SqlDataSource5" 
                        DataTextField="TEXT" DataValueField="ID" Width="75%" SelectionMode="Multiple"></asp:ListBox>
                    <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>" 
                        SelectCommand="select  (c.name + ' - ' + s.Name + ' - ' + ss.Name) as TEXT, ss.Id as ID  from Categories c inner join Sub_Categories s on c.Id = s.Category_Id inner join Sub_Sub_Categories ss on s.Id = ss.Sub_Category_Id order by c.Name, s.Name, ss.Name">
                    </asp:SqlDataSource>
                </td>
                <td>
                    <asp:Label ID="lblDepartment" runat="server" Text='Department'></asp:Label>
                    <br />
                    <asp:DropDownList ID="ddlDepartment" runat="server" 
                        DataSourceID="SqlDataSource4" DataTextField="Name" DataValueField="Id" AppendDataBoundItems="true">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>" 
                        SelectCommand="SELECT [Id], [Name] FROM [Departments]"></asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="rfvDepartment" runat="server" ControlToValidate="ddlDepartment"
                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnAddNewUser" runat="server" Text="Add New User" CssClass="btn btn-primary"
                        OnClick="btnAddNewUser_Click" />
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>
