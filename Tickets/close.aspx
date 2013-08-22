<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="close.aspx.cs" Inherits="Tickets_close" %>

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
            <table width="100%">
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
    <fieldset class="login">
        <legend>Close Ticket</legend>
        <p>
            <asp:Label ID="lblRating" runat="server" Text="Rating" AssociatedControlID="ddlRating"></asp:Label>
             <asp:DropDownList ID="ddlRating" runat="server">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>Low</asp:ListItem>
                        <asp:ListItem>Medium</asp:ListItem>
                        <asp:ListItem>High</asp:ListItem>
                        <asp:ListItem>Excellent</asp:ListItem>
                    </asp:DropDownList>
            <asp:RequiredFieldValidator ID="rfvRating" runat="server" ControlToValidate="ddlRating"
                ForeColor="#FF3300" SetFocusOnError="True" InitialValue="0">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblDescription" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="95"></asp:TextBox>
        </p>
        <p>
            <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="" ClientValidationFunction="Validate"
                Text="" ForeColor="#FF3300"></asp:CustomValidator>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Close Ticket" CssClass="btn btn-primary" />
        </p>
    </fieldset>
</asp:Content>
