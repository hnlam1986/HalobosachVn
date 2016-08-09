var ProductDetailEvent = {
    ValidateInputAmount: function () {
        var res = false;
        var amount = $("#txtAmount").val();
        try {
            var intAmount = parseInt(amount);
            if ($.isNumeric(intAmount) && intAmount > 0) {
                res = true;
            }
        } catch (e) {
        }
        return res;
    },

    AddToCart: function () {
        var product = this;
        if (product.ValidateInputAmount()) {
            $("#txtAmount").closest(".form-group").removeClass("has-error");
            var data = {};
            data.ProductId = $("#hdProductId").val();
            data.ProductName = $(".product-name").text();
            data.Price = $(".product-price").attr("data-price");
            data.Amount = $("#txtAmount").val();
            data.ImagePath = $("#hdProductImage").val();
            var Url = "/ajax.aspx?action=addtocart";
            $.ajax({
                type: "POST",
                url: Url,
                dataType: 'text',
                data: data,
                error: function (msg) {
                    this.result = false;
                },
                success: function (data) {
                    try {
                        cartSummary = JSON.parse(data);
                        product.RefreshShoppingCart(cartSummary);
                        $("#confirmOrder").show();
                    } catch (e) { }
                }
            });
        } else {
            $("#txtAmount").closest(".form-group").addClass("has-error");
        }
    },
    AddToCartFromList: function (id) {
        var product = this;
        var data = {};
        var liProduct = $("#productList" + id);
        data.ProductId = $(liProduct).attr("data-product-id");
        data.ProductName = $("#productName", $(liProduct)).text();
        data.Price = $("#productPrice", $(liProduct)).attr("data-price");
        data.Amount = 1;
        data.ImagePath = $("#productImage", $(liProduct)).attr('data-image');
        var Url = "/ajax.aspx?action=addtocart";
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: data,
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    cartSummary = JSON.parse(data);
                    product.RefreshShoppingCart(cartSummary);
                } catch (e) { }
            }
        });

    },
    AddQuickToCartFromList: function (id) {
        var product = this;
        var data = {};
        var liProduct = $("#productList" + id);
        data.ProductId = $(liProduct).attr("data-product-id");
        data.ProductName = $("#productName", $(liProduct)).text();
        data.Price = $("#productPrice", $(liProduct)).attr("data-price");
        data.Amount = 1;
        data.ImagePath = $("#productImage", $(liProduct)).attr('data-image');
        var Url = "/ajax.aspx?action=addtocart";
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: data,
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    window.location.href = "/gio-hang";
                } catch (e) { }
            }
        });

    },
    RefreshShoppingCart: function (data) {
        $("#lblItem.shop-cart-item").html(data.TotalItem);
        $("#lblTotal.shop-cart-total").html(data.TotalPrice);
    },
    UpdateItemInCartSummary: function (id) {
        var product = this;
        var Url = "/ajax.aspx?action=updatecartitem";
        var quantity = $("#txtQuantity_" + id).val();
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { id: id, quantity: quantity },
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    cartSummary = JSON.parse(data);
                    product.RefreshShoppingCart(cartSummary);
                    $("#cartSummaryItemTotal_" + id).html(cartSummary.UpdatedTotalPrice);
                    $("#cartSummaryTotalAll").html(cartSummary.TotalPrice);
                } catch (e) { }
            }
        });
    },
    ViewOrderUpdateItemInCartSummary: function (id) {
        var product = this;
        var Url = "/ajax.aspx?action=vieworderupdatecartitem";
        var quantity = $("#txtQuantity_" + id).val();
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { id: id, quantity: quantity },
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    cartSummary = JSON.parse(data);
                    product.RefreshShoppingCart(cartSummary);
                    $("#cartSummaryItemTotal_" + id).html(cartSummary.UpdatedTotalPrice);
                    $("#cartSummaryTotalAll").html(cartSummary.TotalPrice);
                } catch (e) { }
            }
        });
    },
    DeleteItemInCartSummary: function (id) {
        var product = this;
        JSUtilities.ConfirmDialog("Xóa hàng hóa", "Bạn có muốn xóa món hàng đang chọn không?", function () {
            var Url = "/ajax.aspx?action=deletecartitem";
            $.ajax({
                type: "POST",
                url: Url,
                dataType: 'text',
                data: { id: id },
                error: function (msg) {
                    this.result = false;
                },
                success: function (data) {
                    try {
                        cartSummary = JSON.parse(data);
                        product.RefreshShoppingCart(cartSummary);
                        $("#cartSummaryTotalAll").html(cartSummary.TotalPrice);
                        $("#divTotalItem").html("Giỏ Hàng (" + cartSummary.TotalItem + " SP)");
                        if (cartSummary.TotalItem == "0") {
                            $("#cartSummaryItem_" + id).closest("tbody").remove();
                        } else {
                            $("#cartSummaryItem_" + id).remove();
                        }

                    } catch (e) { }
                }
            });
        });
    },
    ViewOrderDeleteItemInCartSummary: function (id) {
        var product = this;
        JSUtilities.ConfirmDialog("Xóa hàng hóa", "Bạn có muốn xóa món hàng đang chọn không?", function () {
            var Url = "/ajax.aspx?action=vieworderdeletecartitem";
            $.ajax({
                type: "POST",
                url: Url,
                dataType: 'text',
                data: { id: id },
                error: function (msg) {
                    this.result = false;
                },
                success: function (data) {
                    try {
                        cartSummary = JSON.parse(data);
                        product.RefreshShoppingCart(cartSummary);
                        $("#cartSummaryTotalAll").html(cartSummary.TotalPrice);
                        $("#divTotalItem").html("Giỏ Hàng (" + cartSummary.TotalItem + " SP)");
                        if (cartSummary.TotalItem == "0") {
                            $("#cartSummaryItem_" + id).closest("tbody").remove();
                        } else {
                            $("#cartSummaryItem_" + id).remove();
                        }

                    } catch (e) { }
                }
            });
        });
    },
    SendShopCartInfo: function () {
        var product = this;
        var Url = "/ajax.aspx?action=sendshopcart";
        var CustomerName = $("#txtCustomerName").val();
        var ShipAddress = $("#txtShipAddress").val();
        var Phone = $("#txtPhone").val();
        var Email = $("#txtEmail").val();
        var Note = $("#txtNote").val();
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { CustomerName: CustomerName, ShipAddress: ShipAddress, Phone: Phone, Email: Email, Note: Note },
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                if (data == "True") {
                    window.location.href = "/hoan-tat";
                }
            }
        });
    },
    UpdateCustomerInfo: function () {
        var product = this;
        var Url = "/ajax.aspx?action=updatecustomerinfo";
        var CustomerName = $("#txtCustomerName").val();
        var ShipAddress = $("#txtShipAddress").val();
        var Phone = $("#txtPhone").val();
        var Email = $("#txtEmail").val();
        var Note = $("#txtNote").val();
        var ShopCartId = $("#hdShopCartId").val();
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { CustomerName: CustomerName, ShipAddress: ShipAddress, Phone: Phone, Email: Email, Note: Note, ShopCartId: ShopCartId },
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                if (data == "True") {
                    JSUtilities.AlertMessageDialog("Xác nhận đơn hàng", "Cập nhật thông tin khách hàng thành công", null);
                }
            }
        });
    },
    CompletedOrder: function () {
        var product = this;
        var Url = "/ajax.aspx?action=processedorder";
        var ShopCartId = $("#hdShopCartId").val();
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { ShopCartId: ShopCartId },
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                if (data == "True") {
                    //window.location.href = "/hoan-tat";
                    JSUtilities.AlertMessageDialog("Xác nhận đơn hàng", "Hoàn tất đơn hàng, quay về trang Quản Lý Đơn Hàng", function () { window.location.href = '/admin/OrderManagement.aspx' });
                }
            }
        });
    }

}