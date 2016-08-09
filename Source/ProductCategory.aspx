<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="HaloBoSach.ProductCategory" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .list-category figure img
        {
            -webkit-filter:  blur(0);
            filter:  blur(0);
            -webkit-transition: .3s ease-in-out;
            transition: .3s ease-in-out;
        }

        .list-category a:hover img
        {
            -webkit-filter:  blur(3px);
            filter:  blur(3px);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TitleBar runat="server" id="TitleBar" Title="DANH MỤC SẢN PHẨM"/>
    <div runat="server" id="divContent"></div>
</asp:Content>
