<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true"
    CodeBehind="NewsList.aspx.cs" Inherits="HaloBoSach.NewsList" %>

<%@ Register Src="ctrl/BigAds.ascx" TagName="BigAds" TagPrefix="uc1" %>
<%@ Import Namespace="HaloBoSach" %>
<%@ Register Src="ctrl/GroupNewsTopImage.ascx" TagName="GroupNewsTopImage" TagPrefix="uc1" %>
<%@ Register Src="~/ctrl/ListNewsTopImage.ascx" TagPrefix="uc1" TagName="ListNewsTopImage" %>
<%@ Register Src="~/ctrl/ShowTopNewsImage.ascx" TagPrefix="uc1" TagName="ShowTopNewsImage" %>
<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>
<%@ Register Src="~/ctrl/ListNewsLine.ascx" TagPrefix="uc1" TagName="ListNewsLine" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divHomeNews" runat="server">
        <div class="home-banner"></div>
        <div class="news-home">
            <div class="listNewsOnTop">
                <uc1:ShowTopNewsImage runat="server" ID="ShowTopNewsImage" StoreProcedureName="News_GetTop5NewsHot" PositionKey="HotNews" RoutePath="xem-tin" ShowType="BigWideNewsTop" RecordNumber="5" SmallNewsColumn="4" ShowBigNews="true" HideViewMoreButton="true"/>
            </div>
            <div>
                <div class="col-xs-6 listNewsOnLeft">
                    <uc1:TitleBar runat="server" ID="TitleBar" Title="KHUYẾN MÃI" />
                    <uc1:ShowTopNewsImage runat="server" ID="ShowTopNewsImage1" StoreProcedureName="News_GetTopNewsByConfigKeyId" RoutePath="xem-tin" PositionKey="Promotion" ShowType="BigNewsTop" RecordNumber="5" SmallNewsColumn="2" ShowBigNews="true" />
                </div>
                <div class="col-xs-6 listNewsOnRight">
                    <uc1:TitleBar runat="server" ID="TitleBar1" Title="TIN TỨC" />
                    <uc1:ShowTopNewsImage runat="server" ID="ShowTopNewsImage2" StoreProcedureName="News_GetTopNewsByConfigKeyId" RoutePath="xem-tin" PositionKey="News" ShowType="WideNewsTop" RecordNumber="5" SmallNewsColumn="1" ShowBigNews="true" ShowRightCaption="true" />
                </div>
            </div>
        </div>
    </div>
    <div id="divListNewsInLine" runat="server">
        <uc1:ListNewsLine runat="server" id="ListNewsLine" StoreProcedureName="News_GetNewsByCategoryId" RoutePath="xem-tin" />
    </div>
</asp:Content>
