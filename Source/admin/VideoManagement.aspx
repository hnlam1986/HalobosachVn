<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="VideoManagement.aspx.cs" Inherits="HaloBoSach.admin.VideoManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="Scripts/AlbumManagement.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="margin-top: 10px">
        <table>
            <tr>
                <td style="width: 120px">
                    Tieu de
                </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" Width="500px" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Link
                </td>
                <td>
                    <asp:TextBox ID="txtLink" runat="server" Width="500px" CssClass="form-control"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Danh muc
                </td>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" Width="500px" CssClass="form-control">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkHot" runat="server" Text="Hot Video" Style="margin: 0 10px;" />
                </td>
                <td>
                    <asp:Button runat="server" ID="btnSubmitTop" CssClass="NewsSubmitButton btn btn-success btn-sm"
                        Text="Them Video" Width="100px" OnClick="btnSubmitTop_OnClick" />
                        <asp:Button runat="server" ID="Button1" CssClass="NewsSubmitButton btn btn-success btn-sm"
                        Text="Tao moi" Width="100px" OnClientClick="window.location.href='/admin/VideoManagement.aspx';return false;" />
                </td>
            </tr>
        </table>
    </div>
    <hr />
    <table class="custom-table">
        <thead>
            <td>
                Tieu de
            </td>
            <td>
                Danh muc
            </td>
            <td>
                Anh dai dien
            </td>
            <td>
                Hot Video
            </td>
            <td style="width: 300px">
            </td>
        </thead>
        <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
                <tr id="Video_<%#Eval("VideoID") %>">
                    <td>
                        <a href="<%#Eval("SourceURL")%>">
                            <%#Eval("VideoTitle")%>
                        </a>
                    </td>
                    <td>
                        <%#Eval("NewsCateName")%>
                    </td>
                    <td>
                        <img src="<%#GetImageSource(Eval("SourceURL").ToString()) %>" />
                    </td>
                    <td>
                        <input id="Checkbox1" type="checkbox" <%#Eval("HotVideo").ToString()=="True"?"checked":"" %>
                            disabled="disabled" />
                    </td>
                    <td>
                        <input type="button" id="btnDel" style="width: 70px" class="NewsSubmitButton btn btn-danger btn-sm"
                            value="Xoa" width="70px" onclick="AlbumManagementEvent.DeleteVideo(this)" data-videoid="<%#Eval("VideoID") %>" />
                        <input type="button" id="btnEdit" style="width: 70px" class="NewsSubmitButton btn btn-success btn-sm"
                            value="Edit Album" onclick="window.location.href='/admin/VideoManagement.aspx?id=<%#Eval("VideoID") %>'" />
                        <img id="video_indicator_<%#Eval("VideoID") %>" src="img/throbber.gif" style="display: none;
                            margin-left: 10px" />
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
</asp:Content>
