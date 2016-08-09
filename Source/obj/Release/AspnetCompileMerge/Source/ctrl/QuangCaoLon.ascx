<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuangCaoLon.ascx.cs"
    Inherits="HaloBoSach.ctrl.QuangCaoLon" %>
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
    <div style="margin-top: 10px">
        <a href="<%=Link %>">
            <img style="width: 960px; height: 324px" src="/AdvImage/<%=ImageUrl %>"/>
        </a>
    </div>
</div>
