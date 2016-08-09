<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="HaloBoSach._default" %>

<%@ Register Src="~/ctrl/ProductListControl.ascx" TagPrefix="uc1" TagName="ProductListControl" %>
<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>
<%@ Register Src="~/ctrl/ShowTopNewsImage.ascx" TagPrefix="uc1" TagName="ShowTopNewsImage" %>
<%@ Register Src="~/ctrl/QuangCaoNgang.ascx" TagPrefix="uc1" TagName="QuangCaoNgang" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/jquery.als-1.7.min.js"></script>
    <link rel="stylesheet" href="http://blueimp.github.io/Gallery/css/blueimp-gallery.min.css">
    <link rel="stylesheet" href="/Styles/bootstrap-image-gallery.min.css">
    <script src="http://blueimp.github.io/Gallery/js/jquery.blueimp-gallery.min.js"></script>
    <script src="/Scripts/bootstrap-image-gallery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#lista1").als({
                visible_items: 4,
                scrolling_items: 1,
                orientation: "horizontal",
                circular: "yes",
                autoscroll: "yes",
                interval: 4000
            });
        });
		</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="home-banner"></div>
    <div class="san-pham-noi-bat">
        <uc1:ProductListControl runat="server" ID="ProductListControl" ProductTitle="sản phẩm nổi bật" />
    </div>
    <div class="home-news-section">
        <div class="col-xs-6 home-tin-khuyen-mai">
            <uc1:TitleBar runat="server" ID="TitleBar" Title="KHUYẾN MÃI" />
            <div class="khuyen-mai-content">
                <uc1:ShowTopNewsImage runat="server" ID="ShowTopNewsImage" StoreProcedureName="News_GetTopNewsByConfigKeyId" PositionKey="Promotion" RoutePath="xem-tin"  ShowType="BigNewsTop" RecordNumber="3" SmallNewsColumn="2" ShowBigNews="true"/>
            </div>
        </div>
        <div class="col-xs-6 home-tin-tuc">
            <uc1:TitleBar runat="server" ID="TitleBar1" Title="TIN TỨC" />
            <div class="tin-tuc-content">
                <uc1:ShowTopNewsImage runat="server" ID="ShowTopNewsImage1" StoreProcedureName="News_GetTopNewsByConfigKeyId" PositionKey="Promotion" RoutePath="xem-tin" ShowType="WideNewsTop" RecordNumber="5" SmallNewsColumn="2" ShowBigNews="true"/>
            </div>
        </div>
    </div>
    <div class="home-working-picture">
        <uc1:TitleBar runat="server" ID="TitleBar2" Title="HÌNH ẢNH HOẠT ĐỘNG"/>
        <uc1:QuangCaoNgang runat="server" ID="QuangCaoNgang" Width="965px"/>
    </div>
</asp:Content>
