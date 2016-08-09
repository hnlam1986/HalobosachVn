<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="test.aspx.cs" Inherits="HaloBoSach.test" %>

<%@ Register Src="~/ctrl/bookflip/bookflip.ascx" TagPrefix="uc1" TagName="bookflip" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">
    <link href="/Styles/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="/Styles/responsive.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Scripts/jquery.js"></script>
    <script type="text/javascript" src="/ctrl/bookflip/extras/modernizr.2.5.3.min.js"></script>
    <script type="text/javascript" src="/ctrl/bookflip/lib/hash.js"></script>
</head>
<body>
    <div>
        <span>THÔNG TIN XÁC NHẬN ĐƠN HÀNG</span><br />
        <p>Xin chào khách hàng {0},
        <br />
        Công ty HaloFreshBeef xin chân thành cảm ơn quý khách đã tín nhiệm và sử dụng dịch vụ của chúng tôi.<br />
        Chúng tôi vừa nhận được 1 đơn hàng từ quý khách vào lúc: {1}<br />
        Với tổng số món hàng: {2}<br />
        Đơn hàng của quý khách có trị giá: {3}<br /><br />

        Chúng tôi sẽ liên hệ với quý khách để xác nhận đơn hàng trong thời gian sớm nhất.<br /><br />

        Xin cảm ơn.</p>
    </div>

</body>
</html>
