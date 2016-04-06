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
		In order to update the Golfer of the Year points, first use the Excel Workbook to enter the scores and handicaps for each individual that played in an event and calculate the points to be awarded.&nbsp; Once you have calculated the points to be awarded for the individuials and saved them to a file that has the EventID as the file name with an extendion of txt.&nbsp; Then transfer using FTP the txt file to the appropriate place on the server (ftp://mrsga.org/goy/App_Data) and initiate this web site (http://mrsga.org/goy).&nbsp; Then select the menu item &quot;Input Points&quot;. Once the points are input and applied, then the standings should be available.&nbsp; The &quot;Result&quot; pages of the Musket Ridge pages can be updated to point to the Standings.</p>
	<p>

	</p>
</asp:Content>
