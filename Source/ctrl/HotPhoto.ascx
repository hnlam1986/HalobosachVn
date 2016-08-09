<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotPhoto.ascx.cs" Inherits="HaloBoSach.ctrl.HotPhoto" %>
<%@ Import Namespace="HaloBoSach" %>
<div style="position: relative;height: <%=Height%>;width: <%=Width%>;float: left;margin-right: 10px;">
 <div id="titleBar" class="link-news-title-bar">
    <div class="title-frame">
        <div class="title-left bg-dark-grey"><%=Title %></div>
        <div class="title-arrow">
            <div class="yellow-arrow"><img src="/images/grey-arrow.jpg" style="margin-left: -7px"/></div>
        </div>
    </div>
    <div class="title-right bg-yellow"></div>
 </div>
 <div style="margin-top: 10px">
     <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
        <a href="/AlbumDetail.aspx?id=<%#Eval("AlbumID") %>">
        <div style="overflow: hidden; height: 236px; position: relative;margin-top: 5px">
            <img src="Albums/<%#Eval("FolderPath") %>/<%#Eval("ImageUrl") %>" style="width:350px;" />
            </div>
            <span class="albumTitle cut-title">
                    <%#Eval("AlbumTitle") %>
                    </span>
                    </a>
        </ItemTemplate>
     </asp:Repeater>
 </div>
</div>