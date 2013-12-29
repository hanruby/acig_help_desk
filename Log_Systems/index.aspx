 <%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="Log_Systems_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
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
                            <i class="icon-info-sign"></i>&nbsp; Log Systems
                        </h2>
                    </div>
                    <div class="box-content">
                        <table class="table table-bordered">
                            <tr>
                                <td>
                                    Name
                                    <br />
                                    <asp:TextBox ID="txtName" runat="server" MaxLength="200"
                                     CssClass="input-large span4 inline-block"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName"
                                    ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="New">*</asp:RequiredFieldValidator>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="New"
                                    OnClick="btnSave_Click" CssClass="btn btn-primary" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gvLogSystems" runat="server" AutoGenerateColumns="false" OnRowEditing="Edit"
                        OnRowUpdating="Update" OnRowCancelingEdit="CancelEdit" EmptyDataText="No Records"
                        Width="100%" CssClass="table table-bordered table-striped">
                            <Columns>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtNameEdit" runat="server" Text='<%# Eval("Name")%>' MaxLength="20"></asp:TextBox>
                                        <asp:HiddenField ID="hdnId" runat="server" Value='<%# Eval("Id") %>' />
                                        <asp:RequiredFieldValidator ID="rfvNameEdit" runat="server" ControlToValidate="txtNameEdit"
                                        ForeColor="#FF3300" SetFocusOnError="True" ValidationGroup="Edit">*</asp:RequiredFieldValidator>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="EditButton" runat="server" CssClass="btn btn-info" CommandName="Edit"
                                        Text="Edit" />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="UpdateButton" runat="server" CssClass="btn btn-primary" CommandName="Update"
                                        Text="Update" ValidationGroup="Edit" />&nbsp;
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

