<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CongNgheCuoi.ascx.cs"
    Inherits="HaloBoSach.ctrl.CongNgheCuoi" %>
<%@ Register Src="ListNewsTopBoldLink.ascx" TagName="ListNewsTopBoldLink" TagPrefix="uc1" %>
<div style="position: relative; height: <%=Height%>; width: <%=Width%>; float: left;
    margin-right: 10px;">
    <div id="titleBar" class="link-news-title-bar">
        <div class="title-frame">
            <div class="title-left bg-dark-grey">
                <%=Title %></div>
            <div class="title-arrow">
                <div class="yellow-arrow">
                    <img src="../images/grey-arrow.jpg" style="margin-left: -7px" /></div>
            </div>
        </div>
        <div class="title-right bg-yellow">
        </div>
    </div>
    <div style="margin-top: 5px">
        <div style="display: inline-block; float: left">
            <a id="topNewsLink" runat="server"><span class="topImageTitle cut-title-460" id="topTitle" runat="server">
            </span>
                <div>
                    <asp:Image ID="Image1" runat="server" Style="display: block; margin-top: 10px" Width="480px" />
                </div>
            </a>
        </div>
        <div style="display: inline-block; float: right;margin-top: 5px;">
            <uc1:ListNewsTopBoldLink ID="ListNewsTopBoldLink1" runat="server" StoreProcedureName="News_GetTop4News" PositionKey="Home_CongNgheCuoi_SubMenu_1" Width="465px" Height="150px" style="display: block!important" Title="CÔ DÂU"/>
            <uc1:ListNewsTopBoldLink ID="ListNewsTopBoldLink2" runat="server" StoreProcedureName="News_GetTop4News" PositionKey="Home_CongNgheCuoi_SubMenu_2" Width="465px" Height="150px" Title="CẨM NANG CƯỚI"/>
        </div>
    </div>
</div>
