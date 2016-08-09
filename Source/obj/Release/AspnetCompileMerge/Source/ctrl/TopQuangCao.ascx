<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopQuangCao.ascx.cs"
    Inherits="HaloBoSach.ctrl.TopQuangCao" %>
<div id="TopQuangCao" style="width: 1000px; height: 116px;">
    <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
            <a href="<%#Eval("LinkURL") %>" target="_blank">
                <img id="TopQuangCao<%=GetIndex() %>" src="/AdvImage/<%#Eval("AdvImagePath") %>" height="116px" width="192px"
                    style="margin-right: 10px; float: left"></a>
        </ItemTemplate>
    </asp:Repeater>
</div>
<script>
    $(document).ready(function () { $("#TopQuangCao5").css("margin-right","0");});
</script>