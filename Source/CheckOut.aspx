<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="CheckOut.aspx.cs" Inherits="HaloBoSach.CheckOut" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/bootstrapValidator.js"></script>
    <script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TitleBar runat="server" ID="TitleBar" Title="XÁC NHẬN THÔNG TIN ĐƠN HÀNG"/>
    <div id="divCustomerInfo" class="check-out">
        <div class="form-horizontal">
            <div class="form-group">
                <label for="txtCustomerName" class="col-sm-2 control-label">Họ và tên</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" id="txtCustomerName" name="require">
                </div>
            </div>
            <div class="form-group">
                <label for="txtShipAddress" class="col-sm-2 control-label">Địa chỉ giao hàng</label>
                <div class="col-sm-10">
                    <input type="text" class="form-control" id="txtShipAddress" name="require">
                </div>
            </div>
            <div class="form-group">
                <label for="txtPhone" class="col-sm-2 control-label">Số điện thoại</label>
                <div class="col-sm-10">
                    <input type="phone" class="form-control" id="txtPhone" name="phone">
                </div>
            </div>
            <div class="form-group">
                <label for="txtEmail" class="col-sm-2 control-label" >Email</label>
                <div class="col-sm-10">
                    <input type="email" class="form-control" id="txtEmail" name="email">
                </div>
            </div>
        </div>

            <div class="form-group">
                <label for="txtNote">Nếu bạn muốn thêm ghi chú về đơn hàng của bạn, xin vui lòng nhập vào đây.</label>
                
                    <textarea type="text" class="form-control" id="txtNote"></textarea>
               
            </div>

    </div>
    <div id="divShoppingCartInfo">
        <div id="divCartInfo">
            <a href="/gio-hang" class="btn btn-success ">QUAY LẠI GIỎ HÀNG</a>
            <a class="btn btn-success float-right" onclick="validForm();">XÁC NHẬN THÔNG TIN ĐƠN HÀNG</a>
        </div>
    </div>
    
    <script>
        function validForm() {
            var bootstrapValidator = $("#divCustomerInfo").data('bootstrapValidator');
            bootstrapValidator.validate();
            if (bootstrapValidator.isValid()) {
                ProductDetailEvent.SendShopCartInfo();
            }
            return false;
        }
        $(document).ready(function () {
            JSUtilities.ValidationForm("#divCustomerInfo");
        });
    </script>
</asp:Content>
