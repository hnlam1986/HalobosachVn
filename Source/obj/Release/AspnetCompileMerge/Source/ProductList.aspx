<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="ProductList.aspx.cs" Inherits="HaloBoSach.ProductList" %>

<%@ Register Src="~/ctrl/ProductListControl.ascx" TagPrefix="uc1" TagName="ProductListControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:ProductListControl runat="server" ID="ProductListControl" />
</asp:Content>
