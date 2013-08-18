<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Admin_Sub_Categories_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <asp:TextBox ID="txtSubCategoryName" runat="server" MaxLength="200"></asp:TextBox>
    <asp:HiddenField ID="hdnCategoryId" runat="server" />
    <asp:RequiredFieldValidator ID="rfvSubCategoryName" runat="server" ControlToValidate="txtSubCategoryName"
        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewSubCategory">*</asp:RequiredFieldValidator>
    <asp:Button ID="btnSaveSubCategory" runat="server" Text="Save" 
        ValidationGroup="NewSubCategory" onclick="btnSaveSubCategory_Click" />
    <asp:GridView ID="gvSubCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditSubCategory"
        OnRowUpdating="UpdateSubCategory" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSubCategoryNameEdit" runat="server" Text='<%# Eval("Name")%>' MaxLength="200"></asp:TextBox>
                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" Value='<%# Eval("Id") %>' />
                    <asp:RequiredFieldValidator ID="rfvSubCategoryNameEdit" runat="server" ControlToValidate="txtSubCategoryNameEdit"
                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditSubCategory">*</asp:RequiredFieldValidator>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="EditButton" runat="server" CssClass="button" CommandName="Edit"
                        Text="Edit"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                        Text="Update" ValidationGroup="EditSubCategory"/>&nbsp;
                    <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                        Text="Cancel" CausesValidation="false"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

