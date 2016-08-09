<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="CustViewOrderDetail.aspx.cs" Inherits="HaloBoSach.admin.ViewOrderDetail" %>

<%@ Register Src="~/ctrl/EditCartInfo.ascx" TagPrefix="uc1" TagName="EditCartInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
    <script type="text/javascript" src="/Scripts/JSUtilities.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:EditCartInfo runat="server" ID="EditCartInfo" DisplayCompletedButton="False"/>
</asp:Content>
