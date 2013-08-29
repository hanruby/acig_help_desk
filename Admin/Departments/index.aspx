<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Admin_Departments_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
            <fieldset>
                <legend>New Department</legend>
                <table class="table table-bordered" style="width:30%">
                    <tr>
                        <td>
                            Department Name
                            <br />
                            <asp:TextBox ID="txtDepartmentName" runat="server" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDepartmentName" runat="server" ControlToValidate="txtDepartmentName"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewDepartment">*</asp:RequiredFieldValidator>
                            <br />
                            <asp:Button ID="btnSaveDepartment" runat="server" Text="Save" ValidationGroup="NewDepartment"
                                OnClick="btnSaveDepartment_Click" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <h3>All Departments</h3>
            <asp:GridView ID="gvDepartment" runat="server" AutoGenerateColumns="false" OnRowEditing="EditDepartment"
                OnRowUpdating="UpdateDepartment" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                Width="100%" CssClass="table table-bordered">
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
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CssClass="button" CommandName="Edit"
                                Text="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                                Text="Update" ValidationGroup="EditDepartment" />&nbsp;
                            <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                                Text="Cancel" CausesValidation="false"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

