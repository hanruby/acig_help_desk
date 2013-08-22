<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="resolve.aspx.cs" Inherits="Tickets_resolve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
 <script type = "text/javascript">
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
        <legend>Resolve Ticket</legend>
        <p>
            <asp:Label ID="lblDescription" runat="server" Text="Description" AssociatedControlID="txtDescription"></asp:Label>
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="5" Columns="95"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription"
                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
        </p>
        <p>
            <asp:Label ID="lblFile" runat="server" Text="File"></asp:Label>
            <input type="file" id="uploadFile" name="uploadFile" />
        </p>
        <p>
            <asp:CustomValidator ID="CustomValidator" runat="server" ErrorMessage="" 
            ClientValidationFunction = "Validate" Text=""  ForeColor="#FF3300"></asp:CustomValidator>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Add" CssClass="btn btn-primary"/>
        </p>
    </fieldset>
</asp:Content>
