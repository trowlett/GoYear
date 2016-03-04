<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="load_scores.aspx.cs" Inherits="Load_load_scores" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Load Scores from a MISGA Event</h2>
    <asp:Panel ID="Panel2" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Enter Event ID: "></asp:Label>
        <asp:TextBox ID="tbEventID" runat="server" Width="100px"></asp:TextBox>

    </asp:Panel>
    <asp:Panel ID="Panel1" runat="server">
    <asp:Label ID="lblSelectFile" runat="server" Text="Select File from this list that are available: "></asp:Label>
    <asp:DropDownList ID="ddlFiles" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnSelectFile" runat="server" Text="Select" OnClick="btnSelectFile_Click" />
    <br />
    <asp:Label ID="lblFileName" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Label ID="lblItems" runat="server" Text="Label"></asp:Label>
    </asp:Panel>

</asp:Content>

