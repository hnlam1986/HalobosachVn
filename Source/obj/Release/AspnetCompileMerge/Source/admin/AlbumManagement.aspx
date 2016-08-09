<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true"
    EnableEventValidation="false" CodeBehind="AlbumManagement.aspx.cs" Inherits="HaloBoSach.admin.AlbumManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/AlbumManagement.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 10px">
        <table>
            <tr>
                <td>
                    <div style="margin: 0 10px">
                        Ten album
                    </div>
                </td>
                <td>
                    <asp:TextBox ID="txtAlbum" runat="server" Width="200px" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Danh muc Album
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" CssClass="form-control">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chkHot" runat="server" Text="Sách" Style="margin: 0 10px;" />
                    <asp:Button runat="server" ID="btnSubmitTop" CssClass="NewsSubmitButton btn btn-success btn-sm"
                        Text="Tao Album" Width="100px" OnClick="btnSubmitTop_Click" />
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <table class="custom-table">
        <thead>
            <td>
                Ten Album
            </td>
            <td>
                Danh muc
            </td>
            <td>
                Tong so anh
            </td>
            <td style="width: 300px">
            </td>
        </thead>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr id="Album_<%#Eval("AlbumID") %>">
                    <td>
                        <a href="javascript:void(0);">
                            <%#Eval("AlbumTitle") %></a>
                    </td>
                    <td>
                        <%#Eval("NewsCateName")%>
                    </td>
                    <td>
                        <%#Eval("Total") %> 
                    </td>
                    <td>
                        <input type="button" id="btnDel" style="width: 70px" class="NewsSubmitButton btn btn-danger btn-sm"
                            value="Xoa Album" width="70px" onclick="AlbumManagementEvent.DeleteAlbum(this)"
                            data-albumid="<%#Eval("AlbumID") %>" data-folder="<%#Eval("FolderPath") %>" />
                        <input type="button" id="btnEdit" style="width: 70px" class="NewsSubmitButton btn btn-success btn-sm"
                            value="Edit Album" onclick="OpenUpload(<%#Eval("AlbumID") %>,'<%#Eval("FolderPath") %>','','<%#Eval("HotAlbum") %>');" />
                        <img id="album_indicator_<%#Eval("AlbumID") %>" src="img/throbber.gif" style="display: none;
                            margin-left: 10px" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    <script>
        function OpenUpload(id, path, title, hot) {
            var encode = encodeURIComponent(title);
            var popup = window.open('Upload.aspx?action=edit&id=' + id + '&f=' + path + '&title=' + encode + '&hot=' + hot, 'EditAdvWindow', 'toolbar=no, scrollbars=yes, resizable=yes, width=550px, height=350px'); popup.focus();
        }
    </script>
</asp:Content>
