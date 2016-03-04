<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Load_Points.aspx.cs" Inherits="Load_Load_Points" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
	Load Points</h2>
        <asp:Panel ID="AccessPanel" runat="server">
        <asp:Label ID="lblAccess" runat="server" Text="Enter Access Code: " ForeColor="Black" Font-Bold="True" Font-Size="Medium"></asp:Label>
        <asp:TextBox ID="tbAccessCode" runat="server" TextMode="Password"></asp:TextBox>
		<asp:Label ID="lblBadAccess" runat="server" Font-Bold="True" ForeColor="Red" Text="Invalid Access Code.  Try again." Font-Size="Medium"></asp:Label>
        <br />
        <asp:Button ID="btnEnterAccess" runat="server" Text="Enter Access Code" OnClick="btnEnterAccess_Click" />
        <asp:Button ID="btnTryAgain" runat="server" OnClick="btnTryAgain_Click" Text="TryAgain" />
        </asp:Panel> 
	<asp:Panel ID="Panel1" runat="server">
           <asp:Label ID="lblSelectFile" runat="server" Text="Select File from this list that are available: "></asp:Label>
    <asp:DropDownList ID="ddlFiles" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnSelectFile" runat="server" Text="Select" OnClick="btnSelectFile_Click" />
    <br />

	<table><tr>
	<td>
		<asp:Label ID="Label1" runat="server" Text="Event Point File to Load:" 
			Font-Size="Medium"></asp:Label>
		&nbsp;&nbsp;
		<asp:TextBox ID="txtFileToLoad" runat="server" Height="22px" Width="200px" 
			Font-Size="Medium"></asp:TextBox>
		<br />
		<br />

		<asp:Button ID="btnLoad" runat="server" Text="Load File" 
			onclick="btnLoad_Click" />
			&nbsp;&nbsp;
		<asp:Label ID="lblFileName" runat="server" Font-Size="Medium"></asp:Label>
		
		<br />
		<asp:Label ID="lblDbLoadStatus" runat="server"></asp:Label>
		<asp:Button ID="btnApply" runat="server" OnClick="btnApply_Click" Text="Apply Points" />
		<br /><br />

		<div class="point_load">
		<asp:Repeater ID="GoYPointsRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
				<th class="seqno", style="text-align: right">Player</th>  
				<th class="eventid", style="text-align: right">Event ID</th>
				<th class="name", style="text-align: left">Name</th>
				<th class="points", style="text-align: right">Points</th>
				<th class="gross", style="text-align: right">Gross</th>
				<th class="hcp", style="text-align: right">Hcp.</th>
				<th class="net", style="text-align: right">Net</th>
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Eval("Details") %>'>
			   <ItemTemplate>
			   <tr>
					<td class="seqno", style="text-align: right"><%# ((GoYEntry)Container.DataItem).Eseq %></td>    
					<td class="eventid", style="text-align: right"><%# ((GoYEntry)Container.DataItem).EEventID %></td>
					<td class="name", style="text-align: left"><%# ((GoYEntry)Container.DataItem).EMemberName %></td>
					<td class="points", style="text-align: right"><%# ((GoYEntry)Container.DataItem).EPoints.ToString("##,##0.000") %></td>
					<td class="gross", style="text-align: right"><%# ((GoYEntry)Container.DataItem).EGross %></td>
					<td class="hcp", style="text-align: right"><%# ((GoYEntry)Container.DataItem).EHcp %></td>
					<td class="net", style="text-align: right"><%# ((GoYEntry)Container.DataItem).ENet %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>
	<br />

	</div>

	</td>

	<td style="font-size: medium">
	<h2>Point files already loaded  
		</h2>
		<br />
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
			SelectCommand="SELECT [Name], [Date_Loaded] FROM [GoYFiles] ORDER BY [Date_Loaded]"></asp:SqlDataSource>       
		<asp:GridView ID="GridView1" runat="server" 
			AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="Name" 
			DataSourceID="SqlDataSource1">
		<Columns>
			<asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
				SortExpression="Name" >
			<ItemStyle HorizontalAlign="Left" Width="100px" />
			</asp:BoundField>
			<asp:BoundField DataField="Date_Loaded" HeaderText="Date Loaded" 
				SortExpression="Date_Loaded" DataFormatString="{0:d}" >
			<ItemStyle HorizontalAlign="Left" Width="100px" />
			</asp:BoundField>
		</Columns>
		</asp:GridView>
	</td></tr></table>
        	    </asp:Panel>

</asp:Content>

