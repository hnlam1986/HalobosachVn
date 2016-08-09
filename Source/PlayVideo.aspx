<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true"
    CodeBehind="PlayVideo.aspx.cs" Inherits="HaloBoSach.PlayVideo" %>

<%@ Register Src="ctrl/PlayOneVideo.ascx" TagName="PlayOneVideo" TagPrefix="uc1" %>
<%@ Register Src="ctrl/BigAds.ascx" TagName="BigAds" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="float: left; margin-top: 10px">
        <asp:Panel ID="Panel1" runat="server">
            <uc1:PlayOneVideo ID="PlayOneVideo1" runat="server" Width="716px" />
        </asp:Panel>
    </div>
    <div class="mt10">
        <uc2:BigAds ID="BigAds1" runat="server" />
    </div>
</asp:Content>
