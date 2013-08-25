<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Users_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <ProgressTemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                    top: 39%; left: 42%;" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h3>All Users</h3>
            <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" OnRowEditing="EditUser"
                OnRowUpdating="UpdateUser" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                Width="100%" OnRowDataBound="RowDataBound" ShowFooter="true" CssClass="table table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="User Name">
                        <ItemTemplate>
                            <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("User_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:HiddenField ID="hdnUserId" runat="server" Value='<%# Eval("Id") %>' />
                            <asp:HiddenField ID="hdnFldCategoryId" runat="server" Value='<%# Eval("Category_Id") %>' />
                            <asp:HiddenField ID="hdnFldDepartmentId" runat="server" Value='<%# Eval("Department_Id") %>' />
                            <asp:TextBox ID="txtUserNameEdit" runat="server" Text='<%# Eval("User_Name")%>' MaxLength="100" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserNameEdit" runat="server" ControlToValidate="txtUserNameEdit"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtUserNameNew" runat="server" Text='<%# Eval("User_Name")%>' MaxLength="100" Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvUserNameNew" runat="server" ControlToValidate="txtUserNameNew"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewUser">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Email">
                        <ItemTemplate>
                            <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEmailEdit" runat="server" MaxLength="200" Text='<%# Eval("Email")%>' Width="150px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailEdit" runat="server" ControlToValidate="txtEmailEdit"  Width="150px"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:TextBox ID="txtEmailNew" runat="server" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvEmailNew" runat="server" ControlToValidate="txtEmailNew" Width="150px"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewUser">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category_Name")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCategoryEdit" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlCategoryNew" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Role">
                        <ItemTemplate>
                            <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role_Text")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlRoleEdit" runat="server" SelectedValue='<%# Eval("Role") %>' Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="admin">Admin</asp:ListItem>
                                <asp:ListItem Value="normal_user">Normal</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRoleEdit" runat="server" ControlToValidate="ddlRoleEdit"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlRoleNew" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="admin">Admin</asp:ListItem>
                                <asp:ListItem Value="normal_user">Normal</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvRoleNew" runat="server" ControlToValidate="ddlRoleNew"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0" ValidationGroup="NewUser">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate>
                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department_Text")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlDepartmentEdit" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDeparmentEdit" runat="server" ControlToValidate="ddlDepartmentEdit"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlDepartmentNew" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvDepartmentNew" runat="server" ControlToValidate="ddlDepartmentNew"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0" ValidationGroup="NewUser">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Active?">
                        <ItemTemplate>
                            <asp:Label ID="lblActive" runat="server" Text='<%# Eval("Active")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlActiveEdit" runat="server" SelectedValue='<%# Eval("Active") %>' Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="True">True</asp:ListItem>
                                <asp:ListItem Value="False">False</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvActiveEdit" runat="server" ControlToValidate="ddlActiveEdit"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:DropDownList ID="ddlActiveNew" runat="server" Width="150px">
                                <asp:ListItem Value="0">Select</asp:ListItem>
                                <asp:ListItem Value="True">True</asp:ListItem>
                                <asp:ListItem Value="False">False</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvActiveNew" runat="server" ControlToValidate="ddlActiveNew"
                                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0" ValidationGroup="NewUser">*</asp:RequiredFieldValidator>
                        </FooterTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CssClass="button" CommandName="Edit"
                                Text="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                                Text="Update" ValidationGroup="EditUser" />&nbsp;
                            <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                                Text="Cancel" CausesValidation="false"></asp:LinkButton>
                        </EditItemTemplate>
                        <FooterTemplate>
                            <asp:Button ID="btnAddUser" runat="server" Text="Add User" OnClick="AddNewUser" ValidationGroup="NewUser" CssClass="btn btn-primary"/>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
      </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
