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
                                <asp:ListItem Value="Open">Pending</asp:ListItem>
                                <asp:ListItem Value="Resolved">Resolved</asp:ListItem>
                                <asp:ListItem Value="Closed">Closed</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlSearchField" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSearchField_SelectedIndexChanged">
                                <asp:ListItem>Created By</asp:ListItem>
                                <asp:ListItem>Assigned To</asp:ListItem>
                                <asp:ListItem>Subject</asp:ListItem>
                                <asp:ListItem Value="date">Open Date</asp:ListItem>
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
                            <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                CssClass="btn btn-primary" onclick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </fieldset>
            <asp:HiddenField ID="hdnFldTicketType" runat="server" />
            <asp:GridView ID="gvTickets" runat="server" Width="100%" CssClass="table table-bordered"
                EmptyDataText="No Records" OnRowDataBound="gvTickets_RowDataBound">
            </asp:GridView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
