<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="ProductDetail.aspx.cs" Inherits="HaloBoSach.ProductDetail" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/bootstrapValidator.js"></script>
    <script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TitleBar runat="server" ID="TitleBar" />


    <asp:Label ID="lblDescription" runat="server" Text="Label"></asp:Label>
    <div class="loi-gioi-thieu">
        <span class="bold-text green-text">HALOBOSACH</span> <b></b>cam kết:</b>        </br>- <b>Xuất xứ rõ ràng:</b> Sản phẩm có nguồn gốc rõ ràng, có chứng từ nhập khẩu và chứng nhận an toàn thực phẩm.        </br>- <b>Giá cạnh tranh:</b> Luôn cố gắng mang sản phẩm đến tay khách hàng với giá tốt nhất so với các sản phẩm cùng loại trên thị trường.        </br>- <b>Phục vụ mọi lúc, mọi nơi:</b> Bất cứ khi nào cần bạn cũng có thể mua được các sản phẩm của chúng tôi thông qua ba kênh: mua hàng trực tiếp; mua hàng qua điện thoại; mua hàng trực tuyến tại website: halofreshbeef.com
        </br>
    </div>
    <div class="order-info">
        <div class="text-center bold-text green-text">THÔNG TIN ĐẶT HÀNG</div>

        <div class="row">
            <div class="col-xs-6 text-right green-text">Tên sản phẩm:</div>
            <div class="col-xs-6">
                <asp:Label ID="lblProductName" runat="server" CssClass="product-name"></asp:Label>
            </div>
        </div>
        <div class="row">
            <div class="col-xs-6 text-right green-text">Giá bán HALOBOSACH (100gr):</div>
            <div class="col-xs-6">
                <asp:Label ID="lblPrice" runat="server" Text="Label" CssClass="product-price"></asp:Label>
            </div>
        </div>
        <div class="row amount">
            <div class="col-xs-6 text-right green-text">Số lượng bạn cần mua:</div>
            <div class="col-xs-6">
                <div id="divProductDetail">
                    <div class="form-group">
                        <input id="txtAmount" class="order-amount form-control" name="number" value="1" />
                    </div>
                </div>
                <div class="gram">x 100gr</div>
            </div>
        </div>
        <div class="row last">
            <div class="col-xs-6 text-right green-text">Tổng thanh toán:</div>
            <div class="col-xs-6" id="divTotal">
                <asp:Label ID="lblTotal" runat="server" Text="Label" ClientIDMode="Static"></asp:Label></div>
        </div>
        <div class="text-center">
            <button type="button" class="btn btn-success" onclick="validForm();">THÊM VÀO GIỎ</button>
        </div>
    </div>
    <div id="confirmOrder" style="display:none">
        <div class="row text-center bold-text confirm-message">Bạn muốn gửi đơn hàng ngay bây giờ không?</div>
        <div class="col-xs-6 text-right">
            <a href="/san-pham/0/1" class="btn btn-success">Không, tôi muốn mua thêm hàng!</a>
        </div>
        <div class="col-xs-6">
            <a href="/gio-hang" class="btn btn-success">Gửi đơn hàng đến HALOBOSACH!</a>
        </div>
    </div>

    <asp:HiddenField ID="hdProductId" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="hdProductImage" runat="server" ClientIDMode="Static" />
    <script>
        function validForm() {
            var bootstrapValidator = $("#divProductDetail").data('bootstrapValidator');
            bootstrapValidator.validate();
            if (bootstrapValidator.isValid()) {
                ProductDetailEvent.AddToCart();
            }
        }
        $(document).ready(function () {
            JSUtilities.ValidationForm("#divProductDetail");
        });
    </script>
</asp:Content>
