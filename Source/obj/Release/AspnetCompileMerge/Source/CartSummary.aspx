<%@ Page Title="" Language="C#" MasterPageFile="~/themes/halobosach/HaloBoSach.Master" AutoEventWireup="true" CodeBehind="CartSummary.aspx.cs" Inherits="HaloBoSach.CartSummary" %>

<%@ Register Src="~/ctrl/TitleBar.ascx" TagPrefix="uc1" TagName="TitleBar" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Scripts/bootstrapValidator.js"></script>
    <script type="text/javascript" src="/Scripts/ProductDetail.Event.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <uc1:TitleBar runat="server" ID="TitleBar" />
    <div class="div-cart-summary">
        <div runat="server" id="divContent"></div>
        <a class="btn btn-success float-right" href="/gui-don-hang" onclick="validForm();">GỬI ĐƠN HÀNG</a>
    </div>
    <script>
        function validForm() {
            var bootstrapValidator = $("#cart_summary").data('bootstrapValidator');
            bootstrapValidator.validate();
            if (bootstrapValidator.isValid()) {
                return true;
            }
            return false;
        }
        $(document).ready(function () {
            JSUtilities.ValidationForm("#divCartSummary");
        });
    </script>
</asp:Content>
