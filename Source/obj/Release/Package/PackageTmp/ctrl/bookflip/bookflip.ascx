<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="bookflip.ascx.cs" Inherits="HaloBoSach.ctrl.bookflip.bookflip" %>

<div class="bookshefl-skin">
<div id="bookSheftContent" runat="server" class="bookshefl-cut"></div>
</div>
<div id='divIframe' class='fade hide' style='position: fixed; top: 0px; left: 0px; z-index:99999;width:100%;height:100%'>
    <iframe width="100%" height="100%" id="bookIframe" allowtransparency="true"></iframe>
</div>
<script type="text/javascript">
    var bookId = 0;
    function OpenBook(modal) {
        $.blockUI({message:"Đang tải sách..."});
        
        $("iframe").attr("src", "/viewbook.aspx?id=" + modal);
        $("body").css("overflow", "hidden");
        
    }
    function DisplayContent() {
        var book = $("#divIframe");
        $(book).addClass("in").removeClass("hide");
        $(".blockMsg").hide();
        
    }
    function CloseBook() {
        $.unblockUI();
        var book = $("#divIframe");
        $(book).addClass("hide").removeClass("in");
        $("body").css("overflow", "");
    }


</script>