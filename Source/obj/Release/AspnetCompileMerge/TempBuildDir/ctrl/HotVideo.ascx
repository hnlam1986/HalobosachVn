<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HotVideo.ascx.cs" Inherits="HaloBoSach.ctrl.HotVideo" %>
<%@ Import Namespace="HaloBoSach" %>
<div style="position: relative; height: <%=Height%>; width: <%=Width%>; float: left;
    margin-right: 10px;">
    <div id="titleBar" class="link-news-title-bar">
        <div class="title-frame">
            <div class="title-left bg-dark-grey cut-title">
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
        <a id="topVideoLink" runat="server"><span class="topImageTitle cut-title" id="topTitle" runat="server">
        </span></a>
            <div style="overflow: hidden; height: 245px;margin-top: 5px">
                <%--<asp:Image ID="Image1" runat="server" Style="display: block; margin-top: 10px" Width="352px" />--%>
                <asp:Label ID="lblVideo" runat="server" Text="Label"></asp:Label>
            </div>
        
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div id="hotvideo" style="float: left;margin-top: 10px;">
                <a href="/PlayVideo.aspx?id=<%#Eval("VideoID") %>">
                    <div style="overflow: hidden; height: 100px; position: relative">
                    <img src="<%#GetImageSource(Eval("SourceURL").ToString()) %>" style="display: block;width: 173px;position: absolute;top:-15px"  />
                    <span class="play-icon"></span>
                    </div>
                    <span style="font-weight: bold; display: block;width: 173px" class="cut-title-height">
                        <%#Eval("VideoTitle")%></span>
                        </a>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<script>
    $(document).ready(function () {
        var index = 0;
        $("div[id=hotvideo]").each(function () {
            if (index == 0) {
                $(this).css("margin-right", "6px");
                index = 1;
            } else {
                index = 0;
            }
        });
    });
</script>