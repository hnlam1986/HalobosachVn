<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="HaloBoSach.admin.Upload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/Site.css" rel="stylesheet" type="text/css" /><link href="Style/admin.css" rel="stylesheet" type="text/css" /><link href="Style/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" style="margin: 10px;">
    <table>
            <tr>
                <td>
                    <div style="margin: 0 10px">
                        Ten album
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Danh muc Album
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" CssClass="form-control"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkHot" runat="server" Text="Hot Album" style="margin: 0 10px;"/>
                     <asp:Button ID="Button1" runat="server" Text="Update Album" 
        style="width: 110px"  class="NewsSubmitButton btn btn-success btn-sm" 
        onclick="Button1_Click"/>
                </td>
            </tr>
        </table>
     
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" />
    <div style="margin-top: 10px">
        <asp:AjaxFileUpload ID="AjaxFileUpload1" runat="server" AllowedFileTypes="jpg,png" 
            onuploadcomplete="AjaxFileUpload1_UploadComplete" />
    </div>
    </form>
</body>
</html>
