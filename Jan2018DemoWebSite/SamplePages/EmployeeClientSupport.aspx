<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmployeeClientSupport.aspx.cs" Inherits="Jan2018DemoWebSite.SamplePages.EmployeeClientSupport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Repeater for nest Query</h1>

    <asp:Repeater ID="EmployeecontextList" runat="server" DataSourceID="EmployeeClientListODs" ItemType="Chinook.Data.DTO.EmployeeClients">
        <HeaderTemplate>
            <h3>Employee Custome Support</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <div class="row">
                <div class="col-md-5">
                    <%# Item.fullname %> (<%# Item.Client %>)

                        <asp:GridView ID="GridView1" runat="server" DataSource="<%# Item.ClientList%>"></asp:GridView>
                </div>
                <div class="col-md-5">
                    <asp:ListView ID="CLientLsitLV" runat="server" DataSource="<%# Item.ClientList%>">
                        <ItemTemplate>
                            <tr>
                            <td>
                                <asp:Label Text='<%# Eval("Client") %>' runat="server" ID="Client" /></td>
                            <td>
                                <asp:Label Text='<%# Eval("Phone") %>' runat="server" ID="NameLabel" /></td>
                            </tr>
                        </ItemTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                            <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                                <th runat="server">Phone</th>
                                                <th runat="server">Client</th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholder"></tr>
                                        </table>
                                    </td>
                                </tr>
                    <%--            <tr runat="server">
                                    <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                        <asp:DataPager runat="server" ID="DataPager1">
                                            <Fields>
                                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                            </Fields>
                                        </asp:DataPager>
                                    </td--%>>
<%--                                </tr>--%>
                            </table>

                        </LayoutTemplate>


                    </asp:ListView>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>




    <asp:ObjectDataSource ID="EmployeeClientListODs" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="Employee_GetClientList" TypeName="ChinookSystem.BLL.EmployeeController"></asp:ObjectDataSource>
</asp:Content>
