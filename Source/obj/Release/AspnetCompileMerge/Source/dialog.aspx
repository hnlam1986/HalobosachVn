<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dialog.aspx.cs" Inherits="HaloBoSach.dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery.js"></script>
    <script type="text/javascript" src="/Scripts/jtree/jstree.js"></script>
     <link href="/Scripts/jtree/themes/default/style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <div id="treeFolder" style="float: left;width: 350px;height: 560px;border: 1px gray solid;margin: 10px 5px 10px 10px">
         <div id="treeViewCate">
             <asp:Literal ID="litDirectory" runat="server"></asp:Literal>
         </div>
            <script type="text/javascript">
                $(function () {
                    $("#treeViewCate").on('changed.jstree', function (e, data) {
                        var _path = data.node.data.path;
                        $.ajax({
                            type: "POST",
                            url: "/Ajax.aspx?action=getimage",
                            dataType: 'text',
                            data: { path: _path },
                            success: function (data) {
                                if (data != 'False') {
                                    $("#fileViewer").get(0).innerHTML = data;
                                }

                            },
                            error: function (msg) {
                                this.result = false;
                            }
                        });
                    }).jstree();
                });
            </script>
        </div>
        <div id="fileViewer" style="float: left;width: 460px;height: 540px;border: 1px gray solid;margin: 10px 10px 10px 5px;padding:10px"></div>
    </div>
    <script>
    function apply_img(file){
               
                    var target = window.parent.document.getElementsByClassName('mce-img_divContain');
                    var closed = window.parent.document.getElementsByClassName('mce-tinyfilemanager.net');
                    $(target).val(file);
                    $(closed).find('.mce-close').trigger('click');
               
            }
    
    </script>
    </form>
</body>
</html>
