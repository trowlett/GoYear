<%@ Page Title="" Language="C#" MasterPageFile="~/SIte.master" AutoEventWireup="true" CodeFile="ParticipationDetails.aspx.cs" Inherits="ParticipationDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
     <link href="Styles/GoY.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<h2>Participation Details for: <%: Name %></h2>
    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>

    <asp:Panel ID="Panel1" runat="server">
        <asp:Label ID="lblResults" runat="server" Font-Bold="True" ForeColor="#0033CC" Height="1.2em"></asp:Label>
        <div id="ShowMember">

    <asp:Panel ID="MemberPanel" runat="server">
        <div id="PlayerEdit">
        <asp:Repeater ID="MemberListRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
				<th class="date">Date</th>
                <th class="type">H/A</th>
				<th class="Mtitle">Title</th>
                <th class="gross">Gross</th>
				<th class="hcp">Hcp.</th>
                <th class="net">Net</th>
                <th class="gross">Points</th> 
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Bind("Details") %>'>
			   <ItemTemplate>
			   <tr>
                   <td class="date"><%# ((MemberEvent)Container.DataItem).Date.ToString("M/d") %></td>
                    <td class="type"><%# ((MemberEvent)Container.DataItem).Type %></td>
					<td class="Mtitle"><%# ((MemberEvent)Container.DataItem).Title %></td>
                   <td class="gross"><%# ((MemberEvent)Container.DataItem).Gross %></td>
					<td class="hcp"><%# ((MemberEvent)Container.DataItem).Hcp %></td> 
                   <td class="net"><%# ((MemberEvent)Container.DataItem).Net %></td>
                   <td class="gross"><%# ((MemberEvent)Container.DataItem).Points.ToString("#0.000") %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>
   </div>
   </asp:Panel>
   </div>
   </asp:Panel>
    <p></p>
    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Standings/standings.aspx">Back to Standings Page</asp:HyperLink>
    <p></p>
</asp:Content>

