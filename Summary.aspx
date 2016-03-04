<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Summary.aspx.cs" Inherits="Summary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
	Summary</h2>
		<asp:Panel ID="Panel1" runat="server">
	<div id="goy_ranking">
		
		<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
			ConnectionString="<%$ ConnectionStrings:GoYConnect %>" 
			
			SelectCommand="SELECT MemberName, TotalPoints, Rank, HomeCount, AwayCount, MISGACount FROM mrgoydb.GoYSummary WHERE (TotalPoints &gt; 0.0) ORDER BY Rank, MemberName" 
			onselecting="SqlDataSource1_Selecting" ProviderName="System.Data.SqlClient">
		</asp:SqlDataSource>
		
		</div>
			<asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
				AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MemberName" 
				DataSourceID="SqlDataSource1" PageSize="20" HorizontalAlign="Center">
				<Columns>
					<asp:BoundField DataField="Rank" HeaderText="Standing" 
						SortExpression="Rank" >
					<ItemStyle HorizontalAlign="Center" Width="50px" />
					</asp:BoundField>
					<asp:BoundField DataField="MemberName" HeaderText="Name" 
						SortExpression="MemberName" ReadOnly="True" >
					<ItemStyle HorizontalAlign="Left" Width="200px" />
					</asp:BoundField>
					<asp:BoundField DataField="TotalPoints" DataFormatString="{0:###,##0.000}  " 
						HeaderText="Points" SortExpression="TotalPoints">
					<ItemStyle HorizontalAlign="Right" Width="80px" />
					</asp:BoundField>
					<asp:BoundField DataField="HomeCount" HeaderText="Home" 
						SortExpression="HomeCount" >
					<ItemStyle HorizontalAlign="Right" Width="50px" />
					</asp:BoundField>
					<asp:BoundField DataField="AwayCount" HeaderText="Away" 
						SortExpression="AwayCount" >
					<ItemStyle HorizontalAlign="Right" Width="50px" />
					</asp:BoundField>
				</Columns>
			</asp:GridView>
		<br /><br /><br />
					</asp:Panel> 

</asp:Content>

