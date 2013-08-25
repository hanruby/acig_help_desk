<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="show.aspx.cs" Inherits="Tickets_show" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnFldTicketId" runat="server" />
    <h2>
        Ticket Details
        <asp:LinkButton ID="lnkBtnResolve" runat="server" CausesValidation="false">Resolve Ticket</asp:LinkButton>
        <asp:LinkButton ID="lnkBtnClose" runat="server" CausesValidation="false">Close Ticket</asp:LinkButton>
    </h2>
    <hr />
    <h4>
        Ticket Events</h4>
    <asp:GridView ID="gvEvents" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
        ShowHeader="true" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="State">
                <ItemTemplate>
                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("State")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created At">
                <ItemTemplate>
                    <asp:Label ID="lblCountry" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <hr />
    <asp:Repeater ID="rptrAssignedUsers" runat="server">
        <HeaderTemplate>
            <table width="100%" class="table table-bordered">
                <tr>
                    <th>
                        Assigned To
                    </th>
                </tr>
                <tr>
                    <td>
        </HeaderTemplate>
        <ItemTemplate>
            <%# Eval("Name") %>
        </ItemTemplate>
        <SeparatorTemplate>
            ,
        </SeparatorTemplate>
        <FooterTemplate>
            </td> </tr> </table>
        </FooterTemplate>
    </asp:Repeater>
    <hr />
    <h4>
        Details</h4>
    <asp:Repeater ID="rptrTickets" runat="server">
        <HeaderTemplate>
            <table class="table table-bordered">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <label>
                        Created By:
                    </label>
                    <%# Eval("AssignFrom") %>
                </td>
                <td>
                    <label>
                        Assigned To:
                    </label>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Category:
                    </label>
                    <%# Eval("CategoryName") %>
                </td>
                <td>
                    <label>
                        Sub Category:
                    </label>
                    <%# Eval("SubCategoryName") %>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Type:
                    </label>
                    <%# Eval("Type") %>
                </td>
                <td>
                    <label>
                        Priority:
                    </label>
                    <%# Eval("Priority") %>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Subject:
                    </label>
                    <%# Eval("Subject") %>
                </td>
                <td>
                    <label>
                        Current State:
                    </label>
                    <%# Eval("State") %>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <hr />
    <h4>
        Comments</h4>
    <asp:Repeater ID="rptrComments" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            Created At:  <asp:Label ID="lblCreatedAt" runat="server" Text='<%# Eval("CreatedAt")%>'></asp:Label>
            <br />
            <asp:HyperLink ID="hprLinkFile" runat="server" Visible='<%# FileLinkVisibile(Eval("Visible")) %>'
                NavigateUrl='<%# FileDownloadUrl(Eval("Url")) %>'>Download</asp:HyperLink>
            <br />
            <asp:Label ID="lblNotes" runat="server" Text='<%# Eval("Notes")%>'></asp:Label>
        </ItemTemplate>
        <SeparatorTemplate>
            <hr />
        </SeparatorTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
    <hr />
    <div id="NewCommentDiv" runat="server">
        <fieldset class="login">
            <legend>New Comment</legend>
            <p>
                <asp:Label ID="lblDescription" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="95" Width="100%"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                    ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
            </p>
            <p>
                <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
                <input type="file" id="uploadFile" name="uploadFile" />
            </p>
            <p>
                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" CssClass="btn btn-primary" />
            </p>
        </fieldset>
    </div>
</asp:Content>
