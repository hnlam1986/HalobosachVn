<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="Cooking.aspx.cs" Inherits="HaloBoSach.Cooking" %>

<%@ Register Src="~/ctrl/bookflip/bookflip.ascx" TagPrefix="uc1" TagName="bookflip" %>
<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/ctrl/bookflip/css/magazine.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="book-banner"></div>
    <div class="div-book-shelf">
        <uc1:bookflip runat="server" ID="bookflip" />
    </div>
</asp:Content>
