<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="ViewVideo.aspx.cs" Inherits="HaloBoSach.ViewVideo" %>
<%@ Register src="ctrl/ListCateVideo.ascx" tagname="ListCateVideo" tagprefix="uc1" %>
<%@ Register src="ctrl/BigAds.ascx" tagname="BigAds" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Panel ID="PanelGroupCate" runat="server" Width="725px" style="float: left;">
    
        <asp:Repeater ID="rpGroupCate" runat="server">
            <ItemTemplate>
                <%--<uc1:GroupNewsTopImage ID="ListNewsTopImage1" runat="server" Width="720px" StoreProcedureName="News_GetTop6NewsByCateID"
                    PositionKey='<%#Eval("NewsCateID") %>' />--%>
                    <div class="mt10" style="display: inline-block">
                    <uc1:ListCateVideo ID="ListCateVideo1" runat="server" Width="720px" ParentID='<%#Eval("NewsCateParentID") %>' CateID='<%#Eval("NewsCateID") %>' Title='<%#Eval("NewsCateName") %>'/>
                    </div>
            </ItemTemplate>
        </asp:Repeater>
        
    </asp:Panel>
    <div class="mt10">
    <uc2:BigAds ID="BigAds1" runat="server" />
    </div>
</asp:Content>
