<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="index.aspx.cs" Inherits="System_Logs_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="row-fluid">
        <div class="box span12">
            <div class="box-header well">
                <h2>
                    <i class="icon-info-sign"></i>&nbsp;
                    <asp:Label ID="lblIncidents" runat="server"></asp:Label>
                </h2>
            </div>
            <div class="box-content">
                <asp:LinkButton ID="lnkBtnDownload" runat="server" CssClass="btn btn-primary left"
                    CausesValidation="false" onclick="lnkBtnDownload_Click">
                        <i class="icon-download-alt icon-white"></i>
                        Download</asp:LinkButton>
                <asp:LinkButton ID="lnkBtnNewIncident" runat="server" CssClass="btn btn-primary right"
                    CausesValidation="false">
                        <i class="icon-plus icon-white"></i>
                        New System Incident Log</asp:LinkButton>
                <div class="clear">
                </div>
                <hr />
                <asp:GridView ID="gvIncidents" runat="server" Width="100%" CssClass="table table-bordered table-striped"
                    EmptyDataText="No Records" AutoGenerateColumns="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Incident Date">
                            <ItemTemplate>
                                <asp:Label ID="lblIncidentDate" runat="server" Text='<%# Eval("Incident_Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System">
                            <ItemTemplate>
                                <asp:Label ID="lblSystem" runat="server" Text='<%# Eval("System")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error Type">
                            <ItemTemplate>
                                <asp:Label ID="lblErrorShortDesc" runat="server" Text='<%# Eval("Error_Short_Desc")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error Description">
                            <ItemTemplate>
                                <asp:Label ID="lblErrorLongDesc" runat="server" Text='<%# Eval("Error_Long_Desc")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved Date">
                            <ItemTemplate>
                                <asp:Label ID="lblResolvedDate" runat="server" Text='<%# Eval("Resolved_Date")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Resolved Description">
                            <ItemTemplate>
                                <asp:Label ID="lblResolvedDescription" runat="server" Text='<%# Eval("Resolved_Description")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time Difference">
                            <ItemTemplate>
                                <asp:Label ID="lblTimeDifference" runat="server" Text='<%# Eval("Time_Difference")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Comment">
                            <ItemTemplate>
                                <asp:Label ID="lblComment" runat="server" Text='<%# Eval("Comment")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:HyperLink ID="hprLnkEdit" runat="server" CssClass="btn btn-info" NavigateUrl='<%# EditUrl(Eval("Id")) %>'>
                                    <i class="icon-edit icon-white"></i>
                                    Edit
                                </asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
