<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSQuery.aspx.cs" Inherits="Jan2018DemoWebSite.SamplePages.ODSQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Serious Business</h3>
    <div class="row">
        <asp:GridView ID="AlbumList" runat="server" AutoGenerateColumns="False" DataSourceID="AlbumListODS" AllowPaging="True" PageSize="15" BorderStyle="Double" GridLines="Horizontal" CellPadding="150" CellSpacing="150" HorizontalAlign="Center" OnSelectedIndexChanged="AlbumList_SelectedIndexChanged">
            <Columns>
                <asp:CommandField SelectText="View" ShowSelectButton="True"></asp:CommandField>
                <asp:TemplateField HeaderText="Id" SortExpression="AlbumId">
                   
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("AlbumId") %>' ID="AlbumId"></asp:Label>&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title" SortExpression="Title">
                   
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("Title") %>' ID="Label2"></asp:Label>&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Artist" SortExpression="ArtistId">
                   
                    <ItemTemplate>
                        <asp:DropDownList ID="ArtistList" runat="server" DataSourceID="ArtistListODS" DataTextField="Name" DataValueField="ArtistId" selectedvalue='<%# Eval("ArtistId") %>' Width="300px"></asp:DropDownList>&nbsp;&nbsp;
                       <%-- <asp:Label runat="server" Text='<%# Bind("ArtistId") %>' ID="Label3"></asp:Label>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Year" SortExpression="ReleaseYear">
                   
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ReleaseYear") %>' ID="Label4"></asp:Label>&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Label" SortExpression="ReleaseLabel">
                   
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Eval("ReleaseLabel") %>' ID="Label5"></asp:Label>&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:ObjectDataSource ID="AlbumListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Albums_List" TypeName="ChinookSystem.BLL.AlbumController"></asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ArtistListODS" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Artists_List" TypeName="ChinookSystem.BLL.ArtistController"></asp:ObjectDataSource>
    </div>

</asp:Content>
