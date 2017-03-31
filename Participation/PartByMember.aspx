<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PartByMember.aspx.cs" Inherits="PartByMember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>Event Participation by Member</h2>
<p>Select Member:&nbsp;
    <asp:DropDownList ID="ddlMembers" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Select" Enabled="False" Visible="False" />
    <br />
    <asp:Label ID="lblNoMembers" runat="server" Text="No Members to select" Visible="False"></asp:Label>
</p>
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
</asp:Content>

