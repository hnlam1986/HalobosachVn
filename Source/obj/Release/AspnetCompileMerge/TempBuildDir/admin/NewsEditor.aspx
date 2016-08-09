<%@ Page  Language="C#" AutoEventWireup="true" CodeBehind="NewsEditor.aspx.cs" Inherits="HaloBoSach.admin.NewsEditor" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript" src="/Scripts/jquery.js"></script>
    <script type="text/javascript" src="/Scripts/bootstrap.min.js"></script>
    <link href="Style/Site.css" rel="stylesheet" type="text/css" /><link href="Style/admin.css" rel="stylesheet" type="text/css" />
    <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="Style/admin.css" rel="stylesheet" type="text/css" />
    <link href="Style/PopupStyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="tinymce/tinymce.min.js"></script>
    <script type="text/javascript" src="Scripts/jquery.form-validator.min.js"></script>
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
    <style type="text/css">
        .mce-open[aria-label="Upload image"]
        {
            height: 28px !important;
        }
        body
        {
            background-color: #ffffff!important;    
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="margin: 10px;">
        <div>
        <asp:Button runat="server" ID="btnSubmitTop" CssClass="NewsSubmitButton btn btn-success btn-sm"
            Text="ĐĂNG TIN" OnClientClick="SubmitNews();" OnClick="btnSaveTin_OnClick" /></div>
    <table width="100%">
        <tr>
            <td>
                <h1>
                    Nhập vào tiêu đề</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtTitle" Width="100%" data-validation="required" CssClass="form-control"
                    data-validation-error-msg="Nhập vào tiêu đề"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào phần tin vắn</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtShortText" Width="100%" data-validation="required" TextMode="MultiLine" Height="50px" CssClass="form-control"
                    data-validation-error-msg="Nhập vào phần tin vắn"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Chọn ảnh đại diện</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Image ID="imgNews" runat="server" style="height:125px;" ImageUrl="/images/logo.png"/>
                <asp:FileUpload runat="server" ID="fuAvarta" />
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào nguồn tin</h1>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox runat="server" ID="txtSource" Width="100%" data-validation="required" CssClass="form-control"
                    data-validation-error-msg="Nhập vào nguồn tin"></asp:TextBox>
            </td>
        </tr>
        <div id="divCategory" runat="server">
            <tr>
                <td>
                    <h1>
                        Thuộc danh mục tin</h1>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList runat="server" ID="ddlCategory" Width="100%" CssClass="form-control"> 
                    </asp:DropDownList>
                </td>
            </tr>
        </div>
        <tr>
            <td>
                <asp:CheckBox runat="server" ID="chkSlide" Text="Slide Show" CssClass="mr30"/>
                <asp:CheckBox runat="server" ID="chkHotNews" Text="Tin Hot" CssClass="mr30"/>
                <asp:CheckBox runat="server" ID="chkMostReview" Text="Tin Xem Nhiều" />
            </td>
        </tr>
        <tr>
            <td>
                <h1>
                    Nhập vào nội dung bài viết</h1>
            </td>
        </tr>
        <tr>
            <td>
                <div class="editable" style="width: 100%; height: 550px" id="divContain">
                    Nhập vào nội dung bài viết</div>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="hidContent" ClientIDMode="Static" />
    <div>
        <asp:Button runat="server" ID="btnSaveTin" CssClass="NewsSubmitButton btn btn-success btn-sm"
            Text="ĐĂNG TIN" OnClientClick="SubmitNews();" OnClick="btnSaveTin_OnClick" /></div>
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
            $("#fuAvarta").change(function () {
                readURL(this);
            });
        });
        function readURL(input) {
            if (GetFileSize(input.id) == true) {
                if (input.files && input.files[0]) {
                    var reader = new FileReader();

                    reader.onload = function (e) {
                        $('#imgNews').attr('src', e.target.result);
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
                    JSUtilities.AlertMessageDialog("Upload file", "Kích thước file ảnh lớn hơn 400KB, chọn ảnh nhỏ hơn.", null);
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
    </form>
</body>
</html>
