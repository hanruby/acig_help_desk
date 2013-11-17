<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="assigned_responded.aspx.cs" Inherits="Tickets_assigned_responded" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblClarification" runat="server" Text=""></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gvTicketsClarification" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" OnRowDataBound="gvTicketsClarification_RowDataBound"
                    AllowPaging="true" OnPageIndexChanging="gvTicketsClarification_PageIndexChanging"
                    PageSize="10">
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblResolved" runat="server" Text=""></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gvTicketsResolved" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" OnRowDataBound="gvTicketsResolved_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="gvTicketsResolved_PageIndexChanging" PageSize="10">
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblClosed" runat="server" Text="Label"></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gvTicketsClosed" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" OnRowDataBound="gvTicketsClosed_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="gvTicketsClosed_PageIndexChanging" PageSize="10">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
