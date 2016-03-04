<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ParticipationByEvent.aspx.cs" Inherits="ParticipationByEvent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style type="text/css">
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <h2>
        Participation by Event</h2>
&nbsp;<asp:Panel ID="EventSelectionPanel" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Select Event: "></asp:Label>
        <asp:DropDownList ID="ddlEvents" runat="server"  
            DataTextField="Date" DataValueField="Date" OnSelectedIndexChanged="ddlEvents_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:Button ID="btnSelect" runat="server" OnClick="btnSelect_Click" Text="Select" />
        <br /> <br />
    </asp:Panel>
    <asp:Panel ID="InputPanel" runat="server">
        <asp:Label ID="lblID" runat="server" Text=""></asp:Label>

    </asp:Panel>
    <asp:Panel ID="PriorEntriesPanel" runat="server">
    <asp:Panel ID="PlayerPanel" runat="server">
        <div id="PlayerEdit">
        <asp:Label ID="lblEventPoints" runat="server" Text=""></asp:Label>
        <asp:Repeater ID="PlayersListRepeater" runat="server">
		<ItemTemplate>
		<table>
			<tr>
				<th class="name">Name</th>
                <th class="gross">Gross</th>
				<th class="hcp">Hcp.</th>
                <th class="net">Net</th>
                <th class="gross">Points</th> 
			   </tr>

			   <asp:Repeater ID="Repeater1"  runat="server" DataSource='<%# Bind("Details") %>'>
			   <ItemTemplate>
			   <tr>
					<td class="name"><%# ((GoYEntry)Container.DataItem).EMemberName %></td>
                   <td class="gross"><%# ((GoYEntry)Container.DataItem).EGross %></td>
					<td class="hcp"><%# ((GoYEntry)Container.DataItem).EHcp %></td> 
                   <td class="net"><%# ((GoYEntry)Container.DataItem).ENet %></td>
                   <td class="gross"><%# ((GoYEntry)Container.DataItem).EPoints.ToString("#0.000") %></td>
					</tr>
				</ItemTemplate>
				</asp:Repeater>
			</table>
			</ItemTemplate>
	</asp:Repeater>
   </div>
   </asp:Panel>
    </asp:Panel>
    <asp:Panel ID="ControlPanel" runat="server">
    </asp:Panel>
</asp:Content>

