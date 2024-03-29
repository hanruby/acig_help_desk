﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="Admin_Categories_index" %>

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
                            <i class="icon-info-sign"></i>&nbsp; Category
                        </h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered">
                            <tr>
                                <td>
                                    Category Name
                                    <br />
                                    <asp:TextBox ID="txtCategoryName" runat="server" MaxLength="200" CssClass="input-large span4 inline-block"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvCategoryName" runat="server" ControlToValidate="txtCategoryName"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewCategory">*</asp:RequiredFieldValidator>
                                    <asp:Button ID="btnSaveCategory" runat="server" Text="Save" ValidationGroup="NewCategory"
                                    OnClick="btnSaveCategory_Click" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                        </table>    
                        <asp:GridView ID="gvCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditCategory"
                        OnRowUpdating="UpdateCategory" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                        Width="100%" CssClass="table table-bordered table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtCategoryNameEdit" runat="server" Text='<%# Eval("Name")%>' 
                                        MaxLength="200"></asp:TextBox>
                                        <asp:HiddenField ID="hdnCategoryId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:RequiredFieldValidator ID="rfvCategoryNameEdit" runat="server" ControlToValidate="txtCategoryNameEdit"
                                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditCategory">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-info" CommandName="Edit"
                                        Text="Edit" />
                                        &nbsp;
                                        <asp:LinkButton ID="lnkBtnViewSubCategories" runat="server" CommandArgument='<%# Eval("Id")%>'
                                        Text="View Sub Categories" OnClick="ViewSubCategories" CausesValidation="false"
                                        CssClass="btn btn-info"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" CommandName="Update"
                                        Text="Update" ValidationGroup="EditCategory" />&nbsp;
                                        <asp:LinkButton ID="Cancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel"
                                        Text="Cancel" CausesValidation="false"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <div id="subCategoryDiv" runat="server">
                    <div class="box span12">
                        <div class="box-header well">
                            <h2>
                                <i class="icon-info-sign"></i>&nbsp; Sub Category
                            </h2>
                        </div>
                        <div class="box-content">
                            <table class="table table-bordered">
                                <tr>
                                    <td>
                                        Sub Category Name
                                        <br />
                                        <asp:TextBox ID="txtSubCategoryName" runat="server" MaxLength="200" 
                                        CssClass="input-large span4 inline-block"></asp:TextBox>
                                        <asp:HiddenField ID="hdnCategoryId" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvSubCategoryName" runat="server" ControlToValidate="txtSubCategoryName"
                                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewSubCategory">*</asp:RequiredFieldValidator>
                                        <asp:Button ID="btnSaveSubCategory" runat="server" Text="Save" ValidationGroup="NewSubCategory"
                                        OnClick="btnSaveSubCategory_Click" CssClass="btn btn-primary" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvSubCategories" runat="server" AutoGenerateColumns="false" OnRowEditing="EditSubCategory"
                            OnRowUpdating="UpdateSubCategory" OnRowCancelingEdit="CancelSubCategoryEdit" EmptyDataText="No Records"
                            Width="100%" CssClass="table table-bordered table-striped">
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
                                            <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-info" CommandName="Edit"
                                            Text="Edit" />
                                            <asp:LinkButton ID="lnkBtnViewSubSubCategories" runat="server" CommandArgument='<%# Eval("Id")%>'
                                            Text="View Target Sub Categories" OnClick="ViewSubSubCategories" CausesValidation="false"
                                            CssClass="btn btn-info"></asp:LinkButton>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" CommandName="Update"
                                            Text="Update" ValidationGroup="EditSubCategory" />&nbsp;
                                            <asp:LinkButton ID="Cancel" runat="server" CssClass="btn btn-danger" CommandName="Cancel"
                                            Text="Cancel" CausesValidation="false"></asp:LinkButton>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div id="subSubCategoryDiv" runat="server">
                <div class="box span12">
                    <div class="box-header well">
                        <h2>
                            <i class="icon-info-sign"></i>&nbsp; 
                            <span id="tgtSubCategory" runat="server"></span>
                        </h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered">
                            <tr>
                                <td>
                                    Target Sub Category Name
                                    <br />
                                    <asp:TextBox ID="txtSubSubCategoryName" runat="server" MaxLength="200"
                                     CssClass="input-large span4 inline-block"></asp:TextBox>
                                    <asp:HiddenField ID="hdnSubCategoryId" runat="server" />
                                    <asp:RequiredFieldValidator ID="rfvSubSubCategoryName" runat="server" ControlToValidate="txtSubSubCategoryName"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="NewSubSubCategory">*</asp:RequiredFieldValidator>
                                    <asp:Button ID="btnSaveSubSubCategory" runat="server" Text="Save" ValidationGroup="NewSubSubCategory"
                                    OnClick="btnSaveSubSubCategory_Click" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvSubSubCategories" runat="server" AutoGenerateColumns="false"
                        OnRowEditing="EditSubSubCategory" OnRowUpdating="UpdateSubSubCategory" OnRowCancelingEdit="CancelEditSubSubCategory"
                        EmptyDataText="No Records" Width="100%" CssClass="table table-bordered table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtSubSubCategoryNameEdit" runat="server" Text='<%# Eval("Name")%>'
                                        MaxLength="200"></asp:TextBox>
                                        <asp:HiddenField ID="hdnSubSubCategoryId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:RequiredFieldValidator ID="rfvSubSubCategoryNameEdit" runat="server" ControlToValidate="txtSubSubCategoryNameEdit"
                                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="EditSubSubCategory">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-info" CommandName="Edit"
                                        Text="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" CommandName="Update"
                                        Text="Update" ValidationGroup="EditSubSubCategory" />&nbsp;
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
