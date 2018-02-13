<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Repeater.aspx.cs" Inherits="Jan2018DemoWebSite.SamplePages.Repeater" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Repeater for nested link query</h1>
    <asp:CompareValidator ID="TrackCountLimitcompare" runat="server" ErrorMessage="invalid limit value" ControlToValidate="trackCountLimit" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
    
    <asp:RangeValidator ID="TrackCountLimitrange" runat="server" ErrorMessage="Error!!" MinimumValue="0" MaximumValue="100" ControlToValidate="TrackCountLimit" Type="Integer"></asp:RangeValidator>
    <asp:Label ID="Label1" runat="server" Text="Enter pplaylist track lower limit: "></asp:Label>&nbsp;
    <asp:TextBox ID="TrackCountLimit" runat="server" TextMode="Number"></asp:TextBox>&nbsp;
    <asp:Button ID="DisplayClientPlaylist" runat="server" Text="Display Client Playlist" Class="btn btn-primary" /><br />







    <asp:Repeater ID="ClientPlaylist" runat="server" DataSourceID="ODSClientPlaylist" ItemType="Chinook.Data.DTO.ClientPlaylist"> <%--add item type to your repeater--%>
        <HeaderTemplate>
            <h3>Client Playlist</h3>
            </HeaderTemplate>
        <ItemTemplate>
          <h4>  <%# Item.playlist %> </h4>
            <asp:Repeater ID="PlaylistSongs" runat="server" DataSource="<%# Item.tracklist %>" ItemType="Chinook.Data.POCOs.TracksAndGenre">
                <ItemTemplate>
                    <%# Item.tracks %> (<%# Item.genre %>)<br />

                </ItemTemplate>
   
                      <AlternatingItemTemplate>
                                   <span style="color:aquamarine">
                    <%# Item.tracks %>(<%# Item.genre %>)<br />
                            </span>
                </AlternatingItemTemplate>
                  
            </asp:Repeater>

        </ItemTemplate>

    </asp:Repeater>

    <asp:ObjectDataSource ID="ODSClientPlaylist" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Playlist_ClientPlaylist" TypeName="ChinookSystem.BLL.PlayListController">
        <SelectParameters>
            <asp:ControlParameter ControlID="trackCountLimit" PropertyName="Text" DefaultValue="" Name="trackcountlimit" Type="Int32"></asp:ControlParameter>
        </SelectParameters>
    </asp:ObjectDataSource>
















</asp:Content>
