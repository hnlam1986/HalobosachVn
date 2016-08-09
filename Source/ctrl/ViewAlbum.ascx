<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewAlbum.ascx.cs" Inherits="HaloBoSach.ctrl.ViewAlbum" %>
<%@ Import Namespace="HaloBoSach" %>
<asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <div style="position: relative; height: <%=Height%>; width: <%=Width%>; float: left;
            margin-right: 10px;;margin-top: 10px;margin-bottom: 20px">
            <span class="ListAlbumTitle"><%#Eval("AlbumTitle") %></span>
            <div style="margin-top: 10px">
            <a href="/AlbumDetail.aspx?id=<%#Eval("AlbumID") %>">
                <div style="overflow: hidden; height: 236px; position: relative; margin-top: 5px">
                    <img src="/Albums/<%#Eval("FolderPath") %>/<%#Eval("ImageUrl") %>" style="width: 350px;" />
                </div>
                    </a>
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>
