<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsTopImage.ascx.cs"
    Inherits="HaloBoSach.ctrl.ListNewsTopImage" %>
<%@ Import Namespace="HaloBoSach" %>
<div style="position: relative; height: <%=Height%>; width: <%=Width%>; float: left;
    margin-right: 10px;">
    <div id="titleBar" class="link-news-title-bar">
        <div class="title-frame">
            <a href="/NewsList/<%=ParentID %>/<%=CateID%>">
                <div class="title-left bg-dark-grey">
                    <%=Title %></div>
            </a>
            <div class="title-arrow">
                <div class="yellow-arrow">
                    <img src="/images/grey-arrow.jpg" style="margin-left: -7px" /></div>
            </div>
        </div>
        <div class="title-right bg-yellow">
        </div>
    </div>
    <div style="margin-top: 5px">
        <a id="topNewsLink" runat="server">
            <div style="overflow: hidden; height: 190px;">
                <asp:Image ID="Image1" runat="server" Style="display: block; margin-top: 10px" Width="352px" />
            </div>
            <span class="topImageTitle cut-title" id="topTitle" runat="server"></span></a>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <a style="display: block" class="link-news-separate" href="/NewsDetail/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>/<%#Eval("NewsID") %>">
                    <div class="bullet-yellow" style="float: left">
                    </div>
                    <div class="cut-title" style="padding-left: 25px;">
                        <%#Eval("NewsTitle") %></div>
                </a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
