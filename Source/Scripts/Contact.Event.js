var ContactEvent = {
    SendRequestEmail: function () {
        var name = $("#txtCustomerName").val();
        var phone = $("#txtPhone").val();
        var email = $("#txtEmail").val();
        var request = $("#txtRequestDetail").val();
        var Url = "/ajax.aspx?action=sendrequestemail";
        $.ajax({
            type: "POST",
            url: Url,
            dataType: 'text',
            data: { CustomerName: name,Phone:phone, Email: email,Request: request},
            error: function (msg) {
                this.result = false;
            },
            success: function (data) {
                try {
                    if (data == "True") {
                        $("#txtCustomerName").val("");
                        $("#txtPhone").val("");
                        $("#txtEmail").val("");
                        $("#txtRequestDetail").val("");
                        JSUtilities.AlertMessageDialog("LIÊN HỆ VỚI CHÚNG TÔI", "Xin cảm ơn quý khách đã liên hệ với chúng tôi, chúng tôi sẽ liên lạc với quý khách trong thời gian sớm nhất. <br/> Xin cảm ơn!", null);
                    } else {
                        JSUtilities.AlertMessageDialog("LIÊN HỆ VỚI CHÚNG TÔI", "Rất tiết, Hiện tại chúng tôi không thể nhận được yêu cầu của quý khách vào lúc này, Xin vui lòng liên hệ lại sau. <br/> Xin cảm ơn!", null);
                    }

                } catch (e) { }
            }
        });
    }

}