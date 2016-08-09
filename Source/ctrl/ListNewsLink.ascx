<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListNewsLink.ascx.cs" Inherits="HaloBoSach.ctrl.ListNewsLink" %>
<%@ Import Namespace="HaloBoSach" %>
<div style="position: relative;height: <%=Height%>">
 <div style="margin-top: 5px">
     <asp:Repeater ID="Repeater1" runat="server">
     
        <ItemTemplate>
            <a style="display: block" class="link-news-separate" href="/xem-tin/<%#Eval("NewsID") %>/<%#Utilities.ConvertUnicodeToAscii(Eval("NewsTitle").ToString()) %>.halobosach.vn"><div class="bullet"></div><span class="cut-title"><%#Eval("NewsTitle") %></span></a>
        </ItemTemplate>
     </asp:Repeater>
 </div>
</div>