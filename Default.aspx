<%@ Page Title="Home Page" Language="C#" Debug="true" MasterPageFile="~/Site.master" AutoEventWireup="true"
	CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
	<h2>
		Golfer of the Year
		Rankings as <%= DateTime.Now.ToLongDateString() %>
	</h2>
	<p>
		&nbsp;</p>
	<p>

	</p>
</asp:Content>
