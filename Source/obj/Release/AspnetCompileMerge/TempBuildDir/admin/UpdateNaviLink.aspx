<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateNaviLink.aspx.cs" Inherits="HaloBoSach.admin.UpdateNaviLink" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body style="background-color:#eeeeee;">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Chuyển đến</td>
                    <td>
                        <asp:TextBox ID="txtchuyen" runat="server" Width="345px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>RoutePath</td>
                    <td>
                        <asp:TextBox ID="txtRoutePath" runat="server" Width="345px" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_OnClick" class="btn btn-success btn-sm"/></td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
