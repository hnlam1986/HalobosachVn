<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShowTopNewsImage.ascx.cs" Inherits="HaloBoSach.ctrl.ShowTopNewsImage" %>
<div id="divShowTopNewsImage" runat="server">
<%if (ShowBigNews)
  { %>
<%if (ShowType == ShowNewsType.BigNewsTop)
  { %>
<div class="bigNewsImage <%=GetCssOfType%> news-top<%=GetRightCaptionCss %>">
    <a id="firstNewsLink" runat="server">
        <div class="overflow-image">
            <asp:Image ID="Image1" runat="server" />
        </div>
        <div class="short-description" id="firstDescription" runat="server"></div>
    </a>
</div>
<%}
  else
  { %>
<div class="bigNewsImageWide <%=GetCssOfType%> news-top<%=GetRightCaptionCss %>">
    <a id="firstNewsLinkWide" runat="server">
        <div class="overflow-small-image-wide">
            <asp:Image ID="Image2" runat="server" />
        </div>
        <div class="short-description-wide" id="firstDescriptionWide" runat="server"></div>
    </a>
</div>
<%} %>
<%} %>
<div class="below-small-news">
    <%if (SmallNewsColumn > 2)
      { %>
    <ul>
    <%} %>
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <%if (SmallNewsColumn > 2)
              { %>
            <li>
                <%}
              else
              { %>
                <div class="col-xs-<%=GetCSSColumn %> small-news <%=OddEven() %> <%=GetRightCaptionCss %>">
                    <%} %>
                    <a href="/<%=RoutePath %>/<%#Eval("NewsID") %>/<%#HaloBoSach.Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>.html">
                        <div class="overflow-small-image">
                            <img src="<%#GetImageUrl(Eval("ThumnailImagePath").ToString()) %>" />
                        </div>
                        <div class="caption"><%#Eval("NewsTitle") %></div>
                    </a>
                    <%if (SmallNewsColumn > 2)
                      { %>
            </li>
            <%}
                      else
                      { %>
</div>
            <%} %>
        </ItemTemplate>
    </asp:Repeater>
        <%if (SmallNewsColumn > 2)
      { %>
    </ul>
    <%} %>
</div>
<%if(!HideViewMoreButton){ %>
<div class="btn-view-all-outbound"><a id="moreNewsLink" runat="server" class="btn-view-all">XEM TẤT CẢ</a></div>
<%} %>
</div>