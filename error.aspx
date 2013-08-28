<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="error.aspx.cs" Inherits="error" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <fieldset>
        <legend>Error</legend>
        <asp:Label ID="FriendlyErrorMsg" runat="server" Text="Label" Font-Size="Large" Style="color: red"></asp:Label>
        <asp:Panel ID="DetailedErrorPanel" runat="server" Visible="false">
            <p>
                Detailed Error:
                <br />
                <asp:Label ID="ErrorDetailedMsg" runat="server" Font-Bold="true" Font-Size="Large" /><br />
            </p>
            <p>
                Error Handler:
                <br />
                <asp:Label ID="ErrorHandler" runat="server" Font-Bold="true" Font-Size="Large" /><br />
            </p>
            <p>
                Detailed Error Message:
                <br />
                <asp:Label ID="InnerMessage" runat="server" Font-Bold="true" Font-Size="Large" /><br />
            </p>
            <pre>
            <asp:Label ID="InnerTrace" runat="server" />
        </pre>
        </asp:Panel>
    </fieldset>
</asp:Content>
