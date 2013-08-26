<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Categories_index" %>

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
                <legend>New Category</legend>
                <table class="table table-bordered">
                    <tr>
                        <td>
                            Category Name
                            <br />
                            <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="200"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewCategory">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btnSaveCategory" runat="server" Text="Save" ValidationGroup="NewCategory"
                                OnClick="btnSaveCategory_Click" CssClass="btn btn-primary" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <h3>
                All Categories</h3>
            <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditCategory"
                OnRowUpdating="UpdateCategory" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                Width="100%" CssClass="table table-bordered">
                <Columns>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCategoryNameEdit" runat="server" Text='<%# Eval("Name")%>' MaxLength="200"></asp:TextBox>
                            <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%# Eval("Id") %>' />
                            <asp:RequiredFieldValidator ID="rfvCategoryNameEdit" runat="server" ControlToValidate="txtCategoryNameEdit"
                                ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditCategory">*</asp:RequiredFieldValidator>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="EditButton" runat="server" CssClass="button" CommandName="Edit"
                                Text="Edit" />
                            | <a href='<%# Route.GetRootPath("admin/sub_categories/index.aspx") %>?id=<%# Eval("Id") %>'>
                                Sub Categories</a> |
                            <asp:LinkButton ID="lnkBtnViewSubCategories" runat="server" CommandArgument='<%# Eval("Id")%>'
                                Text="View Sub Categories" OnClick="ViewSubCategories" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                                Text="Update" ValidationGroup="EditCategory" />&nbsp;
                            <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                                Text="Cancel" CausesValidation="false"></asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div id="subCategoryDiv" runat="server">
                <fieldset>
                    <legend runat="server">Sub Category</legend>
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
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
