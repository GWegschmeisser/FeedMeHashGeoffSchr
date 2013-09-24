<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="FeedMeHashGeoffSchr._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Feed Me Hash</title>
    <link rel="Stylesheet" href="Styles/StyleSheet1.css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div>
        <h2>Feed Me Hash</h2>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:TextBox ID="txtHashTag" CssClass="inputBox" runat="server">Hashtag</asp:TextBox>&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnGetTweets" CssClass="tBtn" runat="server" Text="Find Tweets" OnClick="btnGetTweets_Click" />
                <br /><br />

                <asp:TextBox ID="txtFilter" CssClass="inputBox" runat="server">Filter</asp:TextBox>&nbsp;&nbsp;&nbsp;
                Sort by:  <asp:DropDownList ID="ddlSort" runat="server">
                                <asp:ListItem Text="Date(descending)" Value="Date descending"></asp:ListItem>
                                <asp:ListItem Text="Date(ascending)" Value="Date ascending"></asp:ListItem>
                          </asp:DropDownList>
                <br /><br />
                <div id="dispDiv1" class="displayDiv">
                    <asp:Label ID="lblDebug" runat="server" Text="Debug"></asp:Label>
                    <br /><br /></div>
                <br /><br />
                <div id="dispDiv2" class="displayDiv"><br /><br /></div>
                <br /><br />
                <div id="dispDiv3" class="displayDiv"><br /><br /></div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    </form>
</body>
</html>
