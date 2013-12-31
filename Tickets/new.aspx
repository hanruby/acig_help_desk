<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="new.aspx.cs" Inherits="Tickets_new" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
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
    <asp:HiddenField ID="hdnFldId" runat="server" />
        <div class="row-fluid">
            <div class="box span12">
                <div class="box-header well">
                    <h2>
                        <i class="icon-info-sign"></i>&nbsp; New Ticket
                    </h2>
                </div>
                <div class="box-content">
                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
                        <contenttemplate>
                            <table style="width: 100%;" class="table table-bordered">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text="Type" AssociatedControlID="ddlType"></asp:Label>
                                        <asp:DropDownList ID="ddlType" runat="server" CssClass="input-xlarge">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Selected="True">Issue</asp:ListItem>
                                            <asp:ListItem>New Feature</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvType" runat="server" ControlToValidate="ddlType"
                                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblPriority" runat="server" Text="Priority" AssociatedControlID="ddlPriority">
                                        </asp:Label>
                                        <asp:DropDownList ID="ddlPriority" runat="server" CssClass="input-xlarge">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                            <asp:ListItem Selected="True">Low</asp:ListItem>
                                            <asp:ListItem>Medium</asp:ListItem>
                                            <asp:ListItem>High</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ControlToValidate="ddlPriority"
                                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSubject" runat="server" Text="Subject" AssociatedControlID="txtSubject">
                                        </asp:Label>
                                        <asp:TextBox ID="txtSubject" runat="server" MaxLength="100" CssClass="input-xlarge"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject"
                                        ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text="Category" AssociatedControlID="ddlCategory"></asp:Label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                        DataSourceID="SqlDataSource2" DataTextField="Name" DataValueField="Id" CssClass="input-xlarge">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                        SelectCommand="SELECT [Id], [Name] FROM [Categories]"></asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="rfvCategory" runat="server" ControlToValidate="ddlCategory"
                                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSubCategory" runat="server" Text="Sub Category" AssociatedControlID="ddlSubCategory"></asp:Label>
                                        <asp:DropDownList ID="ddlSubCategory" runat="server" AppendDataBoundItems="True"
                                        DataSourceID="SqlDataSource3" DataTextField="Name" DataValueField="Id" EnableViewState="False"
                                        AutoPostBack="true" CssClass="input-xlarge">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                        SelectCommand="SELECT [Id], [Name] FROM [Sub_Categories] WHERE ([Category_Id] = @Category_Id)">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlCategory" Name="Category_Id" PropertyName="SelectedValue"
                                                Type="Int64" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="rfvSubCategory" runat="server" ControlToValidate="ddlSubCategory"
                                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSubSubCategory" runat="server" Text='Target Sub Category'></asp:Label>
                                        <br />
                                        <asp:DropDownList ID="ddlSubSubCategory" runat="server" AppendDataBoundItems="True" CssClass="input-xlarge"
                                        DataSourceID="SqlDataSource5" DataTextField="Name" DataValueField="Id" EnableViewState="False">
                                            <asp:ListItem Value="0">Select</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:Acig_Help_DeskConnectionString %>"
                                        SelectCommand="SELECT Id, Name FROM Sub_Sub_Categories WHERE (Sub_Category_Id = @Sub_Category_Id) AND (Id IN (SELECT User_Sub_Sub_Categories.Sub_Sub_Category_Id FROM User_Sub_Sub_Categories INNER JOIN tbl_Users ON User_Sub_Sub_Categories.User_Id = tbl_Users.Id WHERE (tbl_Users.Active = 'True')))">
                                            <SelectParameters>
                                                <asp:ControlParameter ControlID="ddlSubCategory" Name="Sub_Category_Id" PropertyName="SelectedValue"
                                                Type="Int64" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                        <asp:RequiredFieldValidator ID="rfvSubSubCategory" runat="server" ControlToValidate="ddlSubSubCategory"
                                        ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblNotes" runat="server" Text="Description" AssociatedControlID="txtNotes">
                                        </asp:Label>
                                        <CKEditor:CKEditorControl ID="txtNotes" runat="server"></CKEditor:CKEditorControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="lblUploadFile" runat="server" Text="File">
                                        </asp:Label>
                                        <br />
                                        <input type="file" id="uploadFile" name="uploadFile"  CssClass="input-xlarge" />
                                    </td>
                                </tr>
                            </table>
                        </contenttemplate>
                    </asp:UpdatePanel>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />
                </div>
            </div>
        </div>
</asp:Content>
