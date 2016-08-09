<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SlideShow.ascx.cs" Inherits="HaloBoSach.ctrl.SlideShow" %>
<%@ Import Namespace="HaloBoSach" %>
<%if (IsIncludeScript)
  { %>
<script type='text/javascript' src='/Scripts/jquery.min.js'></script>
<script type='text/javascript' src='/scripts/jquery.mobile.customized.min.js'></script>
<script type='text/javascript' src='/scripts/jquery.easing.1.3.js'></script>
<script type='text/javascript' src='/scripts/camera.js'></script>
<link href="/Styles/camera.css" rel="stylesheet" type="text/css" />
<%} %>
<div id="divSlideShow" runat="server">
    <div id="<%=ID%>" class="camera_wrap">
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <div data-src="/AdvImage/<%#Eval("AdvImagePath") %>" data-link="<%#Eval("LinkURL") %>"></div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<script>
    jQuery(function () {

        jQuery('#<%=ID%>').camera({
            height: "<%=Height%>",//435
            fx: "<%=Effect%>",
            playPause: false,
            loader: 'none',
            navigation: false,
            time: 3000,
            pagination: false
        });
    });
</script>

