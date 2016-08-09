<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true"
    CodeBehind="ConfigDisplayNewsCate.aspx.cs" Inherits="HaloBoSach.admin.ConfigDisplayNewsCate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="form-horizontal">
        <div class="form-group">
            <label for="DropDownList1" class="col-sm-2 control-label">Tin khuyến mãi</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="DropDownList1" runat="server" ClientIDMode="Static" CssClass="form-control">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="DropDownList2" class="col-sm-2 control-label">Tin tức</label>
            <div class="col-sm-10">
                <asp:DropDownList ID="DropDownList2" runat="server" ClientIDMode="Static" CssClass="form-control">
                    </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label for="txtTime" class="col-sm-2 control-label">Thời gian hoạt động</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtTime" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="txtHow" class="col-sm-2 control-label">Hướng dẫn đặt hàng</label>
            <div class="col-sm-10">
                <asp:TextBox ID="txtHow" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label for="Button1" class="col-sm-2 control-label"></label>
            <div class="col-sm-10">
                <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" CssClass="btn btn-success"/>
            </div>
        </div>
        <div class="form-group">
            <label for="Button1" class="col-sm-2 control-label">Cập nhật liên kết trên menu</label>
            <div class="col-sm-10">
                <asp:Button ID="Button2" runat="server" Text="Bắt đầu cập nhật" CssClass="btn btn-success" OnClick="Button2_Click"/>
            </div>
        </div>
    </div>
    
</asp:Content>
