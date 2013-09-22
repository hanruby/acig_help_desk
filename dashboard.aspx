<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="dashboard.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <h3>
        <asp:LinkButton ID="lnkBtnPending" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/pending.aspx">Pending Waiting My Response</asp:LinkButton>
        &nbsp;
        <asp:LinkButton ID="lnkBtnResponded" runat="server" CausesValidation="false" CssClass="btn btn-inverse"
            PostBackUrl="~/tickets/responded.aspx">Report By Supervisor</asp:LinkButton>
        &nbsp;
    </h3>
    <div class="clear">
    </div>
    <br />
</asp:Content>
