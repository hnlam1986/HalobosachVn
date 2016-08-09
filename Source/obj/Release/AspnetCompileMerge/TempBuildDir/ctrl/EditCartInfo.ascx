<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EditCartInfo.ascx.cs" Inherits="HaloBoSach.ctrl.EditCartInfo" %>
<div id="processedMessage" runat="server">
    Đơn hàng đã hoàn tất, chúng tôi xin chân thành cảm ơn quý khách hàng tin dùng dịch vụ của chúng tôi
</div>
<div id="divInfo" runat="server">
    <div id="divValidate">
        <h3>Thông tin khách hàng</h3>
        <div id="divCustomerInfo" class="check-out">
            <div class="form-horizontal">
                <div class="form-group">
                    <label for="txtCustomerName" class="col-sm-2 control-label">Họ và tên</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtCustomerName" runat="server" ClientIDMode="Static" CssClass="form-control" data-type-validate="require"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtShipAddress" class="col-sm-2 control-label">Địa chỉ giao hàng</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtShipAddress" runat="server" ClientIDMode="Static" CssClass="form-control" data-type-validate="require"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtPhone" class="col-sm-2 control-label">Số điện thoại</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtPhone" runat="server" ClientIDMode="Static" CssClass="form-control" data-type-validate="phone"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label for="txtEmail" class="col-sm-2 control-label">Email</label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtEmail" runat="server" ClientIDMode="Static" CssClass="form-control" data-type-validate="email"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <label for="txtNote">Nếu bạn muốn thêm ghi chú về đơn hàng của bạn, xin vui lòng nhập vào đây.</label>
                <asp:TextBox ID="txtNote" runat="server" ClientIDMode="Static" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

            </div>

        </div>
        <a class="btn btn-warning" onclick="validForm();">CẬP NHẬT THÔNG TIN KHÁCH HÀNG</a>
        <%if (DisplayCompletedButton)
          { %>
        <a class="btn btn-warning complete-order-with-check" onclick="ProductDetailEvent.CompletedOrder();">HOÀN TẤT ĐƠN HÀNG</a>
        <%} %>
        <h3>Thông tin đơn hàng</h3>
        <div runat="server" id="divContent"></div>
        <asp:HiddenField ID="hdShopCartId" runat="server" ClientIDMode="Static" />
    </div>
</div>
<script>
    function validForm() {
        var bootstrapValidator = $("#divValidate").data('bootstrapValidator');
        bootstrapValidator.validate();
        if (bootstrapValidator.isValid()) {
            ProductDetailEvent.UpdateCustomerInfo();
        }
        return false;
    }
    function SetNameForServerControl() {
        var validateControls = $("*[data-type-validate]");
        $(validateControls).each(function () {
            var validateType = $(this).attr("data-type-validate");
            $(this).attr("name", validateType);
        });
    }
    $(document).ready(function () {
        SetNameForServerControl();
        JSUtilities.ValidationForm("#divValidate");
    });
</script>
