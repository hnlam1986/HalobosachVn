<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="ViewPhoto.aspx.cs" Inherits="HaloBoSach.ViewPhoto" %>
<%@ Register src="ctrl/ViewAlbum.ascx" tagname="ViewAlbum" tagprefix="uc1" %>
<%@ Register src="ctrl/BigAds.ascx" tagname="BigAds" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >

<div style="float: left;width: 725px">

    <uc1:ViewAlbum ID="ViewAlbum1" runat="server" Width="352px" />
    </div>
    <div style="float: right; margin-top: 10px">
         <uc2:BigAds ID="BigAds1" runat="server" />
    </div>
</asp:Content>
