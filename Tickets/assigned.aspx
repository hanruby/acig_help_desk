<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="assigned.aspx.cs" Inherits="Tickets_assigned" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblOpen" runat="server" Text=""></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gvTicketsOpen" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" OnRowDataBound="gvTicketsOpen_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="gvTicketsOpen_PageIndexChanging" PageSize="10">
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblClarified" runat="server" Text=""></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:GridView ID="gvTicketsClarified" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" OnRowDataBound="gvTicketsClarified_RowDataBound" AllowPaging="true"
                    OnPageIndexChanging="gvTicketsClarified_PageIndexChanging" PageSize="10">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
