<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Categories_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="200"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName"
        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewCategory">*</asp:RequiredFieldValidator>
    <asp:Button ID="btnSaveCategory" runat="server" Text="Save" 
        ValidationGroup="NewCategory" onclick="btnSaveCategory_Click" />
    <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditCategory"
        OnRowUpdating="UpdateCategory" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records" Width="100%">
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
                        Text="Edit"/>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:LinkButton ID="UpdateButton" runat="server" CssClass="button" CommandName="Update"
                        Text="Update" ValidationGroup="EditCategory"/>&nbsp;
                    <asp:LinkButton ID="Cancel" runat="server" CssClass="button" CommandName="Cancel"
                        Text="Cancel" CausesValidation="false"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
