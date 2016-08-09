<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HaloBoSach.admin.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/bootstrap.min.css" rel="stylesheet" />
    <link href="Style/Site.css" rel="stylesheet" />
    <link href="/fonts/style.css" rel="stylesheet" />
</head>
<body class="login-bg">
    <form id="form1" runat="server">
        <!--[if lte IE 7]>
    <div class="ie7">
<![endif]-->
        <div class="outer">
            <div class="rightCornerLogo"></div>
            <div class="middle">
                <div class="horizontal-bar">
                    <div class="arrow-div">
                        <div class="login-text">ĐĂNG NHẬP</div>
                        <span class="glyphicon glyphicon-triangle-top"></span>
                    </div>
                </div>
                <div class="inner">
                    <div class="form-group">
                        <label for="txtUserName">Tên đăng nhập</label>
                        <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txtPassword">Mật khẩu</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="Button1" runat="server" Text="Log on" OnClick="Button1_OnClick" CssClass="form-control btn-login btn" /></td>
                    </div>
                    <div class="form-group lable-message">
                        <asp:Label ID="lblMsg" runat="server" Text="Đăng nhập không thành công!" ForeColor="Red" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <!--[if lte IE 7]>
    </div>
<![endif]-->

    </form>
</body>
</html>
