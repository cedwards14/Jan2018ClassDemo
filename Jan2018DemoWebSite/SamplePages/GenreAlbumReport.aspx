<%@ Page Title="Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GenreAlbumReport.aspx.cs" Inherits="Jan2018DemoWebSite.SamplePages.GenreAlbumReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <rsweb:ReportViewer ID="GenreAlbumReportViewer" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Height="800px" Width="100%" style="margin-top:200px;">
        <LocalReport ReportPath="Report\GenreAlbumReport.rdlc">
            <DataSources>
                <rsweb:ReportDataSource Name="GenreAlbumDS" DataSourceId="GenreAlbumReportViewerODS">
                </rsweb:ReportDataSource>
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="GenreAlbumReportViewerODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GenreAlbumReport_Get" TypeName="ChinookSystem.BLL.TrackController">
    </asp:ObjectDataSource>
</asp:Content>
