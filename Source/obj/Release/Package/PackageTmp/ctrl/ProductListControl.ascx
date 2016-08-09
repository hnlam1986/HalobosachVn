<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductListControl.ascx.cs" Inherits="HaloBoSach.ctrl.ProductListControl" %>
<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>

<link type="text/css" rel="stylesheet" href="/ctrl/circle-effect/css/common.css" />
<link type="text/css" rel="stylesheet" href="/ctrl/circle-effect/css/style2.css" />
<script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
<script src="/ctrl/circle-effect/js/modernizr.js" type="text/javascript"></script>
<div class="control-product-list" id="divProductList" runat="server">
    <uc1:TitleBar runat="server" ID="TitleBar" />
    <div runat="server" id="divProductListContent"></div>
</div>
