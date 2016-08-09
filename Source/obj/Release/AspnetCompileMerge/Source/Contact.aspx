<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="HaloBoSach.Contact" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="/Scripts/bootstrapValidator.js"></script>
    <script type="text/javascript" src="/Scripts/Contact.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <uc1:TitleBar runat="server" ID="TitleBar" Title="ĐỊA CHỈ LIÊN HỆ" />
    <div class="contact">
        <div class="col-xs-4 contact-bg"></div>
        <div class="col-xs-8 contact-info">
            <div class="contact-info-text">* Liên hệ ngay với chúng tôi theo địa chỉ:</div>
            <div class="company-info">

                <span class="bold-text">CÔNG TY TNHH THANH NHÂN FOOD</span>
                <br />
                <b>Địa chỉ:</b> 111/17 Lũy Bán Bích, Phường Tân Thới Hòa, Quận Tân Phú, TP.HCM           
                <br />
                <br />
                <span class="bold-text">HALO FRESH BEEF</span>
                <br />
                Số 6, đường Cây Keo, P. Hiệp Tân, Q. Tân Phú                           
            <br />
                <b>Điện thoại:</b> 08 6681 4414  |  <b>Hotline:</b> 090 99 66 033                           
            <br />
                <b>Website:</b> halobosach.vn  |  halofreshbeef.com
            </div>
            <div class="contact-info-text">*  Hoặc gửi cho chúng tôi yêu cầu của bạn tại đây, HALOBOSACH sẽ liên hệ lại ngay:</div>
            <div id="divCustomerInfo" class="check-out">
                <div class="form-horizontal">
                    <div class="form-group">
                        <label for="txtCustomerName" class="col-sm-2 control-label">Họ và tên</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="txtCustomerName" name="require" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtPhone" class="col-sm-2 control-label">Số điện thoại</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="txtPhone" name="phone" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtEmail" class="col-sm-2 control-label">Email</label>
                        <div class="col-sm-10">
                            <input type="text" class="form-control" id="txtEmail" name="email" />
                        </div>
                    </div>
                    <div class="form-group">
                        <label for="txtRequestDetail" class="col-sm-2 control-label">Yêu cầu của bạn</label>
                        <div class="col-sm-10">
                            <textarea class="form-control" id="txtRequestDetail"></textarea>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <a class="btn btn-success" onclick="validForm();">GỬI YÊU CẦU</a>
                </div>
            </div>

        </div>
    </div>
    <uc1:TitleBar runat="server" ID="TitleBar1" Title="BẢN ĐỒ ĐƯỜNG ĐI"/>
    <a href="https://www.google.com/maps/place/111%2F17+L%C5%A9y+B%C3%A1n+B%C3%ADch,+T%C3%A2n+Th%E1%BB%9Bi+Ho%C3%A0,+T%C3%A2n+Ph%C3%BA,+H%E1%BB%93+Ch%C3%AD+Minh,+Vi%E1%BB%87t+Nam/@10.7624907,106.63231,17z/data=!4m5!3m4!1s0x31752e9d00758451:0x4dc306931f1f0eb5!8m2!3d10.7629439!4d106.631972">
        <img src="/images/map.jpg" />
    </a>



    <script>
        function validForm() {
            var bootstrapValidator = $("#divCustomerInfo").data('bootstrapValidator');
            bootstrapValidator.validate();
            if (bootstrapValidator.isValid()) {
                ContactEvent.SendRequestEmail();
            }
        }
        $(document).ready(function () {
            JSUtilities.ValidationForm("#divCustomerInfo");
        });
    </script>
</asp:Content>
