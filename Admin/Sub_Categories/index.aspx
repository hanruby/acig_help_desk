<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Sub_Categories_index" %>

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
            <fieldset>
                <legend>New Sub Category</legend>
                <table class="table table-bordered">
                    <tr>
                        <td>
                            Sub Category Name
                            <br />
                            <asp:TextBox ID="txtSubCategoryName" runat="server" MaxLength="200"></asp:TextBox>
                            <asp:HiddenField ID="hdnCategoryId" runat="server" />
                            <asp:RequiredFieldValidator ID="rfvSubCategoryName" runat="server" ControlToValidate="txtSubCategoryName"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewSubCategory">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveSubCategory" runat="server" Text="Save" ValidationGroup="NewSubCategory"
                                OnClick="btnSaveSubCategory_Click" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:GridView ID="gvSubCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditSubCategory"
                OnRowUpdating="UpdateSubCategory" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                Width="100%" CssClass="table table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSubCategoryNameEdit" runat="server" Text='<%# Eval("Name")%>'
                                MaxLength="200"></asp:TextBox>
                            <asp:HiddenField ID="hdnSubCategoryId" runat="server" Value='<%# Eval("Id") %>' />
                            <asp:RequiredFieldValidator ID="rfvSubCategoryNameEdit" runat="server" ControlToValidate="txtSubCategoryNameEdit"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditSubCategory">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CssClass="button" CommandName="Edit"
                                Text="Edit" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                                Text="Update" ValidationGroup="EditSubCategory" />&nbsp;
                            <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                                Text="Cancel" CausesValidation="false"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
