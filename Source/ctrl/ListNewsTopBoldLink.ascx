<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsTopBoldLink.ascx.cs" Inherits="HaloBoSach.ctrl.ListNewsTopBoldLink" %>
<%@ Import Namespace="HaloBoSach" %>
<div style="position: relative;height: <%=Height%>;width: <%=Width%>;display: block">
 <div style="float: left;display: inline-block;margin-top: 30px;width: 325px">
 <a id="topNewsLink" runat="server">
 <span class="topImageTitle cut-title-310" id="topTitle" runat="server"></span>
 </a>
 <div style="display: inline-block;">
 <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <a style="display: block" class="link-news-separate" href="/NewsDetail/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>/<%#Eval("NewsID") %>"><div class="bullet-yellow"></div><span class="cut-title-280"><%#Eval("NewsTitle") %></span></a>
        </ItemTemplate>
     </asp:Repeater>
     </div>
 </div>

 <div id="titleBar" class="link-news-title-bar" style="display: inline-block; float: right;width: 137px">
    <div class="title-frame" style="position: relative!important;display: block;float:right">
        <div style="display: inline-block;float: left;">
            <div class="yellow-arrow" style="margin-left: 0!important"><img src="../images/grey-arrow2.jpg" style="margin-top:8px"/></div>

        </div>
        <div class="title-left bg-dark-grey" style="display: inline-block;float: left;width:130px;text-align: center"><%=Title %></div>
        
    </div>
    <div style="display: inline-block">
        <asp:Image ID="Image1" runat="server" style="display: block;margin-top: 10px" Width="137px"/>
    </div>
 </div>

</div>