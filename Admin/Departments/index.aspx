<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Departments_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="updateProgress" runat="server">
        <progresstemplate>
            <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0;
                right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                <asp:Image ID="imgUpdateProgress" runat="server" ImageUrl="~/images/ajax-loader.gif"
                    AlternateText="Loading ..." ToolTip="Loading ..." Style="padding: 10px; position: fixed;
                    top: 39%; left: 42%;" />
            </div>
        </progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">
        <contenttemplate>
            <div class="row-fluid">
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-info-sign"></i>&nbsp; Departments
                        </h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered">
                            <tr>
                                <th>Department Name</th>
                                <th>Manager 1 / Supervisor 1</th>
                                <th>Manager 2 / Supervisor 2</th>
                                <th>Manager 3 / Supervisor 3</th>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtDepartmentName" runat="server" MaxLength="200"
                                     CssClass="input-medium"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ControlToValidate="txtDepartmentName"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewDepartment">*</asp:RequiredFieldValidator>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManagerNew" runat="server" CssClass="input-medium"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManagerNew2" runat="server" CssClass="input-medium"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlManagerNew3" runat="server" CssClass="input-medium"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSaveDepartment" runat="server" Text="Save" ValidationGroup="NewDepartment"
                                    OnClick="btnSaveDepartment_Click" CssClass="btn btn-primary" />
                                </td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false" OnRowEditing="EditDepartment"
                        OnRowUpdating="UpdateDepartment" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                        Width="100%" CssClass="table table-bordered table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtDepartmentNameEdit" runat="server" Text='<%# Eval("Name")%>' MaxLength="200"></asp:TextBox>
                                        <asp:HiddenField ID="hdnDepartmentId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:RequiredFieldValidator ID="rfvDepartmentNameEdit" runat="server" ControlToValidate="txtDepartmentNameEdit"
                                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditDeparmtent">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manager 1 / Supervisor Name 1">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManager" runat="server" Text='<%# Eval("Manager")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlManagerEdit" runat="server"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnManagerId" runat="server" Value='<%# Eval("Manager_Id") %>' />                                        
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manager 2/ Supervisor Name 2">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManager2" runat="server" Text='<%# Eval("Manager_2")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlManagerEdit2" runat="server"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnManagerId2" runat="server" Value='<%# Eval("Manager_Id_2") %>' />                                        
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Manager 3 / Supervisor Name 3">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManager3" runat="server" Text='<%# Eval("Manager_3")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlManagerEdit3" runat="server"></asp:DropDownList>
                                        <asp:HiddenField ID="hdnManagerId3" runat="server" Value='<%# Eval("Manager_Id_3") %>' />                                        
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-info" CommandName="Edit"
                                        Text="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" CommandName="Update"
                                        Text="Update" ValidationGroup="EditDepartment" />&nbsp;
                                        <asp:LinkButton ID="Cancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel"
                                        Text="Cancel" CausesValidation="false"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </contenttemplate>
    </asp:UpdatePanel>
</asp:Content>
