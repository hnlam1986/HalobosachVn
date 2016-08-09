<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="FAQ.aspx.cs" Inherits="HaloBoSach.FAQ" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TitleBar runat="server" ID="TitleBar" Title="CÂU HỎI THƯỜNG GẶP"/>
    <div class="faq">
        <div id="faqContent" runat="server"></div>
    </div>
</asp:Content>
