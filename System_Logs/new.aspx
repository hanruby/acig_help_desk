<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="new.aspx.cs" Inherits="System_Logs_new" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="../scripts/datepicker-range.js"></script>
    <script type="text/javascript">
        $(function () {
            pageLoad();
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp; New System Log
                </h2>
            </div>
            <div class="box-content">
                <table style="width: 100%;" class="table table-bordered">
                    <tr>
                        <td>
                            <asp:Label ID="lblSystem" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlLogSystems" runat="server">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfcLogSystems" runat="server" ControlToValidate="ddlLogSystems" InitialValue ="0"
                                 ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblIncidentDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtIncidentDate" runat="server" CssClass="input-xlarge date-picker start-date"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvIncidentDate" runat="server" ControlToValidate="txtIncidentDate"
                                 ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorShortDesc" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtErrorShortDesc" runat="server" CssClass="input-xlarge" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvErrorShortDesc" runat="server" ControlToValidate="txtErrorShortDesc"
                                 ForeColor="#FF3300" SetFocusOnError="True">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblErrorLongDesc" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtErrorLongDesc" runat="server" CssClass="input-xlarge" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResolvedDate" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtResolvedDate" runat="server" CssClass="input-xlarge date-picker end-date"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResolvedDescription" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtResolvedDescription" runat="server" CssClass="input-xlarge" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTimeDifference" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTimeDifference" runat="server" CssClass="input-xlarge" MaxLength="30"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblComment" runat="server"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtComment" runat="server" CssClass="input-xlarge" MaxLength="250"
                            TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn btn-primary" />  
                        </td>
                        <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn" CausesValidation = "false" />  
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
