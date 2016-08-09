<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProductCategoryImage.aspx.cs" Inherits="HaloBoSach.admin.ProductCategoryImage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="Scripts/jscolor.min.js"></script>
</head>
<body style="background-color:#eeeeee;">
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>Chọn ảnh đại diện</td>
                    <td>
                        <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    
                </tr>
                <tr>
                    <td>Chọn màu đại diện</td>
                    <td>
                        <asp:TextBox ID="txtcolor" runat="server" CssClass="jscolor form-control"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="Button1_Click" Width="100px"/></td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdPath" runat="server" />
    </form>
</body>
</html>
