<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="standings.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:GoYConnect %>" 
            SelectCommand="SELECT [MemberName], [TotalPoints], [Rank], [HomeCount], [AwayCount], [MISGACount] FROM [GoYSummary] ORDER BY [Rank], [MemberName]">
        </asp:SqlDataSource>
    <h2>
        <%= DateTime.Now.Year %>
    Golfer of the Year Standings</h2>
    <asp:Label ID="lblLastEvent" runat="server" Text="" 
            CssClass="style_bold" ForeColor="#000099"></asp:Label>
    <p >

    </p>        
    <p class="center">
        <span style="font-size:small;text-align:center;font-weight:bold;">
        Click on the column heading to sort data by that column.
            <br />
            To view an individual's points earned and the events they participated,
            click on their name.
        </span>
        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" 
            AutoGenerateColumns="False" DataKeyNames="MemberName" 
            DataSourceID="SqlDataSource1" HorizontalAlign="Center" 
            CaptionAlign="Right" onselectedindexchanged="GridView1_SelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="Rank" HeaderText="Standing" SortExpression="Rank">
                <HeaderStyle HorizontalAlign="Center" />
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:HyperLinkField DataTextField="MemberName"
                    datanavigateurlfields="MemberName" DataNavigateUrlFormatString="ParticipationDetails.aspx?Name={0}"
                    HeaderText="Name" SortExpression="MemberName" >
                <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Width="200px" />
                </asp:HyperLinkField>
                <asp:BoundField DataField="TotalPoints" DataFormatString="{0:###,##0.000}" 
                    HeaderText="Points" SortExpression="TotalPoints">
                <ItemStyle HorizontalAlign="Right" Width="80px" />
                </asp:BoundField>
                <asp:BoundField DataField="HomeCount" HeaderText="Home" 
                    SortExpression="HomeCount">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
                <asp:BoundField DataField="AwayCount" HeaderText="Away" 
                    SortExpression="AwayCount">
                <ItemStyle HorizontalAlign="Center" Width="50px" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </p>
    <p>
        <asp:Label ID="lblSelectedName" runat="server" Text="Label"></asp:Label>
        </p>
</asp:Content>

