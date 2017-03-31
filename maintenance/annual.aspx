<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="annual.aspx.cs" Inherits="Maintenance_annual" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Annual File Maintenance</h2>
    <p>
        Purge all prior year entries from Golfer of the Year Database</p>
    <p>
        &nbsp;</p>
        <table style="width: 100%;">
            <tr>
                <td>
                    <asp:Button ID="btnFiles" runat="server" Text="Files" width ="100" OnClick="btnFiles_Click" />
                    &nbsp;</td>
                <td><asp:Label ID="lblFiles" runat="server" Text="Purge all entries in GoYFiles"></asp:Label>
                    &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:Button ID="Button1" runat="server" Text="Summary" width ="100" OnClick="Button1_Click" />
                    &nbsp;</td>
                <td><asp:Label ID="lblSummary" runat="server" Text="Purge all entries in GoYSummary" /> &nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:Button ID="btnDetail" runat="server" Text="Detail" width ="100" OnClick="btnDetail_Click" /> &nbsp;</td>
                <td><asp:Label ID="lblDetail" runat="server" Text="Purge all entries in GoYDetail" />&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
<p>Last updated March 27, 2017.</p>

</asp:Content>

