<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="clarify.aspx.cs" Inherits="Tickets_clarify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        function Validate(sender, args) {
            args.IsValid = confirm("Are you sure ?");
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:HiddenField ID="hdnFldTicketId" runat="server" />
    <h2>
        Ticket Details
    </h2>
    <hr />
    <h4>
        Details</h4>
    <asp:Repeater ID="rptrTickets" runat="server">
        <HeaderTemplate>
            <table class="table table-bordered ui-table">
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td>
                    <label>
                        ID#:</label>
                    <%# Eval("Id") %>
                </td>
                <td>
                    <label>
                        Created By:
                    </label>
                    <%# Eval("AssignFrom") %>
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
    <table width="100%" class="table table-bordered">
        <tr>
            <th>
                Assigned To
            </th>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAssignedTo" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <hr />
    <fieldset class="login">
        <legend>Clarify Ticket</legend>
        <p>
            <asp:Label ID="lblDescription" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%"
                Height="90px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                ForeColor="#FF3300" SetFocusOnError="True">Required</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
            <input type="file" id="uploadFile" name="uploadFile" />
        </p>
        <p>
            <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="" ClientValidationFunction="Validate"
                Text="" ForeColor="#FF3300"></asp:CustomValidator>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Submit" CssClass="btn btn-primary" />
        </p>
    </fieldset>
</asp:Content>
