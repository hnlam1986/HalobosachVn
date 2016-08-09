<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductEditor.aspx.cs" Inherits="HaloBoSach.admin.ProductEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="Style/admin.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Style/PopupStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jquery.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.form-validator.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.datetimepicker.js"></script>
    <script type="text/javascript" src="tinymce/tinymce.min.js"></script>
    <script type="text/javascript" src="/Scripts/JSUtilities.js"></script>
    <script type="text/javascript">
        tinymce.init({
            selector: "#txtTitle.editable",
            inline: true,
            toolbar: "undo redo",
            menubar: false
        });
        tinymce.init({
            selector: "#txtShortText.editable",
            inline: true,
            toolbar: "undo redo",
            menubar: false
        });
        tinymce.init({
            selector: "div.editable",
            theme: "modern",
            plugins: [
                "advlist autolink lists link image charmap print preview hr anchor pagebreak",
                "searchreplace wordcount visualblocks visualchars code fullscreen",
                "insertdatetime nonbreaking save table contextmenu directionality",
                "emoticons template paste textcolor"
            ],
            file_browser_callback: RoxyFileBrowser,
            toolbar1: "insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",
            toolbar2: "print preview media | forecolor backcolor emoticons",
            image_advtab: true,
            templates: [
                { title: 'Test template 1', content: 'Test 1' },
                { title: 'Test template 2', content: 'Test 2' }
            ]
        });
        function RoxyFileBrowser(field_name, url, type, win) {
            var roxyFileman = '/admin/tinymce/plugins/fileman/index.html';
            if (roxyFileman.indexOf("?") < 0) {
                roxyFileman += "?type=" + type;
            }
            else {
                roxyFileman += "&type=" + type;
            }
            roxyFileman += '&input=' + field_name + '&value=' + win.document.getElementById(field_name).value;
            if (tinyMCE.activeEditor.settings.language) {
                roxyFileman += '&langCode=' + tinyMCE.activeEditor.settings.language;
            }
            tinyMCE.activeEditor.windowManager.open({
                file: roxyFileman,
                title: 'Roxy Fileman',
                width: 850,
                height: 650,
                resizable: "yes",
                plugins: "media",
                inline: "yes",
                close_previous: "no"
            }, { window: win, input: field_name });
            return false;
        }
    </script>
</head>
<body style="padding: 10px;">
    <form id="form1" runat="server">
        <div>
            <div>
                <asp:Button runat="server" ID="btnSubmitTop"
                    CssClass="NewsSubmitButton btn btn-success btn-sm" Text="CẬP NHẬT SẢN PHẨM" OnClick="btnSubmitTop_Click" OnClientClick="SubmitNews();"/>
            </div>
            <div style="margin: 10px 0;">
                <table style="width:100%">
                    <tbody >
                        <thead>
                            <tr>
                                <td style="width:100px">Chọn ảnh SP</td>
                                <td>
                                    <asp:Image ID="imgProduct" runat="server" style="height:125px;" ImageUrl="/images/logo.png"/>
                                    <asp:FileUpload ID="fuLogo" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Tên SP</td>
                                <td>
                                    <asp:TextBox ID="txtProductName" CssClass="form-control" runat="server" ClientIDMode="Static" data-validation="required" data-validation-error-msg="Nhập vào tên SP"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Giá</td>
                                <td>
                                    <asp:TextBox ID="txtGia" runat="server" CssClass="form-control" ClientIDMode="Static" data-validation="number" data-validation-error-msg="Nhập vào giá"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>Mô tả SP</td>
                                <td>
                                    <div class="editable" style="width: 100%; height: 550px" id="divContain">
                                        Nhập vào nội dung bài viết
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>SP nổi bật</td>
                                <td>
                                    <asp:CheckBox ID="chkNoiBat" runat="server" Text="" Style="margin-right: 30px" />
                                </td>
                            </tr>
                        </thead>
                    </tbody>
                </table>
            </div>
            <div>
                <asp:Button runat="server" ID="Button1"
                    CssClass="NewsSubmitButton btn btn-success btn-sm" Text="CẬP NHẬT SẢN PHẨM"
                    OnClick="btnSubmitTop_Click" OnClientClick="SubmitNews();"/>
            </div>
        </div>
        <script type="text/javascript">
            function SubmitNews() {
                $("#hidContent").val($("#divContain_ifr").contents().find("body").html());
            }
            $(document).ready(function () {

                $.validate({
                    validateOnBlur: false, // disable validation when input looses focus
                    errorMessagePosition: 'top', // Instead of 'element' which is default
                    scrollToTopOnError: false // Set this property to true if you have a long form
                });

                if ($("#hidContent").val() != "")
                    $("#divContain").html($("#hidContent").val());
                $("#fuLogo").change(function () {
                    readURL(this);
                });
            });
            function readURL(input) {
                if (GetFileSize(input.id) == true) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $('#imgProduct').attr('src', e.target.result);
                        }

                        reader.readAsDataURL(input.files[0]);
                    }
                }
            }
            function GetFileSize(fileid) {
                try {
                    var fileSize = 0;
                    //for IE
                    if (checkIE()) {//we could use this $.browser.msie but since it's depracted we'll use this function
                        //before making an object of ActiveXObject, 
                        //please make sure ActiveX is enabled in your IE browser
                        var objFSO = new ActiveXObject("Scripting.FileSystemObject"); var filePath = $("#" + fileid)[0].value;
                        var objFile = objFSO.getFile(filePath);
                        var fileSize = objFile.size; //size in byte
                        fileSize = fileSize / 1024; //size in kb 
                    }
                        //for FF, Safari, Opeara and Others
                    else {
                        fileSize = $("#" + fileid)[0].files[0].size //size in kb
                        fileSize = fileSize / 1024; //size in mb 
                    }
                    if (fileSize > 400) {
                        JSUtilities.AlertMessageDialog("Upload file","Kích thước file ảnh lớn hơn 400KB, chọn ảnh nhỏ hơn.", null);
                        return false;
                    }
                    return true;
                }
                catch (e) {
                    alert("Error is :" + e);
                    return false;
                }
            }
            function checkIE() {
                var ua = window.navigator.userAgent;
                var msie = ua.indexOf("MSIE ");

                if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {
                    // If Internet Explorer, return version number
                    return true;
                } 
                return false;
            }
            
        </script>
        <asp:HiddenField runat="server" ID="hdImage" />
        <asp:HiddenField runat="server" ID="hidContent" ClientIDMode="Static" />
    </form>
</body>
</html>
