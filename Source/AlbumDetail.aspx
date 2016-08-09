<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true"
    CodeBehind="AlbumDetail.aspx.cs" Inherits="HaloBoSach.AlbumDetail" %>

<%@ Register Src="ctrl/BigAds.ascx" TagName="BigAds" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="http://netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css">
    <link rel="stylesheet" href="http://blueimp.github.io/Gallery/css/blueimp-gallery.min.css">
    <link rel="stylesheet" href="/Styles/bootstrap-image-gallery.min.css">
    <script src="http://blueimp.github.io/Gallery/js/jquery.blueimp-gallery.min.js"></script>
    <script src="/Scripts/bootstrap-image-gallery.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div id="titleBar" class="link-news-title-bar" style="margin-top: 10px">
            <div class="title-frame">
                <div class="title-left bg-dark-grey">
                    <%=Title %>
                </div>
                <div class="title-arrow">
                    <div class="yellow-arrow">
                        <img src="/images/grey-arrow.jpg" style="margin-left: -7px" />
                    </div>
                </div>
            </div>
            <div class="title-right bg-yellow">
            </div>
        </div>
        <div id="blueimp-gallery" class="blueimp-gallery">
            <!-- The container for the modal slides -->
            <div class="slides"></div>
            <!-- Controls for the borderless lightbox -->
            <h3 class="title"></h3>
            <a class="prev">‹</a>
            <a class="next">›</a>
            <a class="close">×</a>
            <a class="play-pause"></a>
            <ol class="indicator"></ol>
            <!-- The modal dialog, which will be used to wrap the lightbox content -->
            <div class="modal fade">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" aria-hidden="true">&times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body next"></div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default pull-left prev">
                                <i class="glyphicon glyphicon-chevron-left"></i>
                                Previous
                            </button>
                            <button type="button" class="btn btn-default next">
                                Next
                        <i class="glyphicon glyphicon-chevron-right"></i>
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="margin-top: 10px; float: left; width: 725px" id="links">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div style="overflow: hidden; height: 239px; position: relative; margin-top: 5px; margin-bottom: 5px; float: left; margin-right: 10px;">
                        <a href="/Albums/<%#Eval("FolderPath") %>/<%#Eval("ImageUrl") %>" title="<%=Title %>"
                            data-gallery>
                            <img src="/Albums/<%#Eval("FolderPath") %>/<%#Eval("ImageUrl") %>" style="width: 350px;" />
                        </a>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="mt10">
            <uc1:BigAds ID="BigAds1" runat="server" />
        </div>
</asp:Content>
