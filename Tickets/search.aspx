<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="search.aspx.cs" Inherits="Tickets_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endRequestHandler);

            function endRequestHandler(sender, args) {
                $(".datePicker").datepicker({
                    constrainInput: true,
                    dateFormat: "dd-mm-yy",
                    changeMonth: true,
                    changeYear: true
                });

                $(".datePicker").attr("readonly", true);
            }

        });
    </script>
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
    <asp:UpdatePanel ID="updatePanelCity" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <fieldset class="login">
                <legend>Search Ticket</legend>
                <table style="width: 100%;" class="table table-bordered">
                    <tr>
                        <th>
                            State
                        </th>
                        <th>
                            Search Field
                        </th>
                        <th>
                            Search Value
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddlTicketType" runat="server">
                                <asp:ListItem Value="Pending">Pending</asp:ListItem>
                                <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                                <asp:ListItem Value="Closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSearchField" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchField_SelectedIndexChanged">
                                <asp:ListItem>Created By</asp:ListItem>
                                <asp:ListItem>Assigned To</asp:ListItem>
                                <asp:ListItem>Subject</asp:ListItem>
                                <asp:ListItem>Open Date</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txtString" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvString" runat="server" ControlToValidate="txtString"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                            <asp:TextBox ID="txtDate" runat="server" CssClass="datePicker"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvDate" runat="server" ControlToValidate="txtDate"
                                ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary"
                                OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:HiddenField ID="hdnFldTicketType" runat="server" />
            <asp:GridView ID="gvTickets" runat="server" Width="100%" CssClass="table table-bordered"
                EmptyDataText="No Records" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderText="Ticket ID">
                        <ItemTemplate>
                            <asp:Label ID="lblTicketId" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Open At">
                        <ItemTemplate>
                            <asp:Label ID="lblTicketOpenAt" runat="server" Text='<%# Eval("Opened_At")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Open By">
                        <ItemTemplate>
                            <asp:Label ID="lblCreatedBy" runat="server" Text='<%# Eval("Opened_By")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Assigned To">
                        <ItemTemplate>
                            <asp:Label ID="lblAssignedTo" runat="server" Text='<%# Eval("Assigned_To")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category">
                        <ItemTemplate>
                            <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Full_Category")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Details">
                        <ItemTemplate>
                            <asp:Label ID="lblDetails" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
