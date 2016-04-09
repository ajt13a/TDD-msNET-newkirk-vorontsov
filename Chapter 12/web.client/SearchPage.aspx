<%@ Page language="c#" Codebehind="SearchPage.aspx.cs" AutoEventWireup="false" Inherits="WebClient.SearchPage" %>
<HTML>
	<HEAD>
		<title>Page</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<DIV style="WIDTH: 601px; HEIGHT: 160px" ms_positioning="FlowLayout">
				<DIV style="WIDTH: 600px; POSITION: relative; HEIGHT: 288px" ms_positioning="GridLayout"><asp:textbox id="recordingId" style="Z-INDEX: 101; LEFT: 168px; POSITION: absolute; TOP: 16px"
						runat="server" Width="88px"></asp:textbox><asp:label id="idLabel" style="Z-INDEX: 102; LEFT: 59px; POSITION: absolute; TOP: 16px" runat="server">Recording ID:</asp:label>
					<asp:Label id="titleLabel" style="Z-INDEX: 103; LEFT: 115px; POSITION: absolute; TOP: 56px"
						runat="server">Title:</asp:Label>
					<asp:Label id="artistNameLabel" style="Z-INDEX: 104; LEFT: 67px; POSITION: absolute; TOP: 96px"
						runat="server">Artist Name:</asp:Label>
					<asp:Label id="averageRatingLabel" style="Z-INDEX: 105; LEFT: 48px; POSITION: absolute; TOP: 136px"
						runat="server">Average Rating:</asp:Label>
					<asp:Label id="labelNameLabel" style="Z-INDEX: 106; LEFT: 66px; POSITION: absolute; TOP: 176px"
						runat="server">Label Name:</asp:Label>
					<asp:TextBox id="title" style="Z-INDEX: 107; LEFT: 168px; POSITION: absolute; TOP: 55px" runat="server"
						Width="256px"></asp:TextBox>
					<asp:TextBox id="artistName" style="Z-INDEX: 108; LEFT: 168px; POSITION: absolute; TOP: 94px"
						runat="server" Width="224px"></asp:TextBox>
					<asp:RadioButtonList id="averageRating" style="Z-INDEX: 109; LEFT: 168px; POSITION: absolute; TOP: 133px"
						runat="server" Width="368px" RepeatDirection="Horizontal">
						<asp:ListItem Value="1">1 Star</asp:ListItem>
						<asp:ListItem Value="2">2 Stars</asp:ListItem>
						<asp:ListItem Value="3">3 Stars</asp:ListItem>
						<asp:ListItem Value="4">4 Stars</asp:ListItem>
						<asp:ListItem Value="5">5 Stars</asp:ListItem>
					</asp:RadioButtonList>
					<asp:TextBox id="labelName" style="Z-INDEX: 110; LEFT: 168px; POSITION: absolute; TOP: 176px"
						runat="server"></asp:TextBox>
					<asp:Button id="searchButton" style="Z-INDEX: 111; LEFT: 160px; POSITION: absolute; TOP: 240px"
						runat="server" Width="61px" Text="Search"></asp:Button>
					<asp:Button id="cancelButton" style="Z-INDEX: 112; LEFT: 248px; POSITION: absolute; TOP: 240px"
						runat="server" Width="61" Text="Cancel"></asp:Button></DIV>
			</DIV>
			<DIV style="WIDTH: 602px; HEIGHT: 208px" ms_positioning="FlowLayout"><asp:repeater id="searchResults" runat="server">
					<ItemTemplate>
						<tr>
							<td><%# DataBinder.Eval(Container.DataItem, "Title")%></td>
							<td><%# DataBinder.Eval(Container.DataItem, "ArtistName") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "AverageRating") %></td>
							<td><%# DataBinder.Eval(Container.DataItem, "LabelName") %></td>
						</tr>
					</ItemTemplate>
					<HeaderTemplate>
						<table cellpadding="3" cellspacing="0" bordercolor="black" border="1" style="background-color:#CCCCFF;border-color:Black;font-family:Verdana;font-size:8pt;width:600px;border-collapse:collapse;">
							<tr bgcolor="#aaaadd">
								<td>Title</td>
								<td>Artist Name</td>
								<td>Average Rating</td>
								<td>Label Name</td>
							</tr>
					</HeaderTemplate>
					<FooterTemplate>
						</table>
					</FooterTemplate>
				</asp:repeater></DIV>
		</form>
	</body>
</HTML>
