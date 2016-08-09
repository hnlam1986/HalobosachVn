<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true" CodeBehind="ViewOrderDetail.aspx.cs" Inherits="HaloBoSach.admin.ViewOrderDetail" %>
<%@ Register src="../ctrl/EditCartInfo.ascx" tagname="EditCartInfo" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
    <script type="text/javascript" src="/Scripts/JSUtilities.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:EditCartInfo ID="EditCartInfo1" runat="server" DisplayCompletedButton="true"/>
</asp:Content>
