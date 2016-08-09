<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuangCaoNgang.ascx.cs"
    Inherits="HaloBoSach.ctrl.QuangCaoNgang" %>

<div style="position: relative; width: <%=Width%>; float: left;" class="div-scroll-image">
    <div style="margin-top: 10px">
        <div id="lista1" class="als-container">
            <div class="als-viewport">
                <ul class="als-wrapper">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <li class="als-item">
                                <a href="<%#Eval("ImagePath")%>" data-gallery>
                                    <div class="overfloat-scrolling-image">
                                        <img src="<%#Eval("ImagePath")%>" />
                                    </div>
                                </a>

                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
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

