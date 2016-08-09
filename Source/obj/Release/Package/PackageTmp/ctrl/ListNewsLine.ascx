<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsLine.ascx.cs" Inherits="HaloBoSach.ListNewsLine" %>
<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>

<uc1:TitleBar runat="server" ID="TitleBar" />
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <div>
            <div class="mt3 clearfix">
                <a title="<%#Eval("NewsTitle") %>" href="/<%=RoutePath%>/<%#Eval("NewsID") %>/<%#HaloBoSach.Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>.html">
                    <img src="<%#GetImageUrl(Eval("ThumnailImagePath").ToString()) %>" class="img130" alt="<%#Eval("NewsTitle") %>"></a>
                <div class="mr1">
                    <h2 style="margin: 0!important">
                        <a title="<%#Eval("NewsTitle") %>" href="/<%=RoutePath%>/<%#Eval("NewsID") %>/<%#HaloBoSach.Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>.html" class="news-title"><%#Eval("NewsTitle") %></a>
                    </h2>
                    <span><%#Eval("ShortContent") %>
                    </span>
                </div>
            </div>
            <div class="line1 mt1">
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>

