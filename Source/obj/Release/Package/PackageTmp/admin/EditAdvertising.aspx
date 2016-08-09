<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditAdvertising.aspx.cs" Inherits="HaloBoSach.admin.EditAdvertising" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/admin.css" rel="stylesheet" type="text/css" />
    <link href="Style/jquery.datetimepicker.css" rel="stylesheet" type="text/css" />
<link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="Scripts/jquery.js"></script>
<script type="text/javascript" src="Scripts/jquery.form-validator.min.js"></script>
<script type="text/javascript" src="Scripts/jquery.datetimepicker.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div><asp:Button runat="server" ID="btnSubmitTop"  
            CssClass="NewsSubmitButton btn btn-success btn-sm" Text="ĐĂNG TIN" 
             onclick="btnSubmitTop_Click" /></div>
    <div>
    <table>
    <tbody>
        <thead>
            <tr>
                <td>Chon Anh Quang Cao</td>
                <td>
                    <asp:FileUpload ID="fuLogo" runat="server" /></td>
            </tr>
            <tr style="display:none">
                <td>Bat dau</td>
                <td>
                    <asp:TextBox ID="dtStart" runat="server" ClientIDMode="Static" data-validation="required" data-validation-error-msg="Nhập vào ngay bat dau" CssClass="form-control" Text="1/1/2016"></asp:TextBox></td>
            </tr>
            <tr style="display:none">
                <td>Ket thuc</td>
                <td>
                    <asp:TextBox ID="dtEnd" runat="server" ClientIDMode="Static" data-validation="required" data-validation-error-msg="Nhập vào ngay ket thuc" CssClass="form-control" Text="1/1/2017"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Link lien ket</td>
                <td>
                    <asp:TextBox ID="txtNavigation" runat="server"  CssClass="form-control"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Vi Tri</td>
                <td>
                    <asp:CheckBox ID="chkViTri" runat="server" Text="Ben Tren" style="margin-right: 30px" Visible="false"/>
                    <asp:CheckBox ID="chkKichThuoc" runat="server" Text="Kich Thuoc Lon" Visible="false"/>
                    <asp:RadioButton ID="rdTop" runat="server"  GroupName="position" Text="Banner" Checked="true" Enabled="false"/>
                    <asp:RadioButton ID="rdInside" runat="server" Checked="true" GroupName="position" Text="Dọc" Visible="false"/>
                    <asp:RadioButton ID="rdFloat" runat="server" GroupName="position" Text="Hai bên" Visible="false"/>
                    <asp:RadioButton ID="rdAnhLon" runat="server" GroupName="position" Text="1 Ảnh lớn" Visible="false"/>
                    <asp:RadioButton ID="rdAnhNgang" runat="server" GroupName="position" Text="1 Ảnh ngang" Visible="false"/>
                   </td> 
            </tr>
        </thead>
    </tbody>
    </table>
    </div>
    <div><asp:Button runat="server" ID="Button1"  
            CssClass="NewsSubmitButton btn btn-success btn-sm" Text="ĐĂNG TIN" 
             onclick="btnSubmitTop_Click" /></div>
    </div>
    <script type="text/javascript">
        $(function () {
            $('#dtStart').datetimepicker({
                    closeOnDateSelect:true,
                    format: 'Y/m/d',
                onShow: function (ct) {
                    this.setOptions({
                        maxDate: $('#dtEnd').val() ? $('#dtEnd').val() : false
                    });
                },
                timepicker: false
            });
            $('#dtEnd').datetimepicker({
                    closeOnDateSelect:true,
                    format: 'Y/m/d',
                onShow: function (ct) {
                    this.setOptions({
                        minDate: $('#dtStart').val() ? $('#dtStart').val() : false
                        });
                },
                timepicker: false
            });
        });
        $(document).ready(function () {
            $.validate({
                validateOnBlur: false, // disable validation when input looses focus
                errorMessagePosition: 'top', // Instead of 'element' which is default
                scrollToTopOnError: false // Set this property to true if you have a long form
            });
        });
    </script>
    <asp:HiddenField runat="server" ID="hdImage"/>
    </form>
</body>
</html>
