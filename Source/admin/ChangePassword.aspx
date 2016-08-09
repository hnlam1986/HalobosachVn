<%@ Page Title="" Language="C#" MasterPageFile="~/admin/Admin.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="HaloBoSach.admin.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../Scripts/bootstrapValidator.min.js"></script>
    <script src="/Scripts/JSUtilities.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="form-change-password" id="divValidate">
        <div class="form-group">
            <label for="txtPassword">Mật khẩu mới</label>
            <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" data-type-validate="password" ClientIDMode="Static"></asp:TextBox> 
        </div>
        <div class="form-group">
            <label for="txtRetypePassword">Nhập lại mật khẩu</label>
            <asp:TextBox ID="txtRetypePassword" runat="server" TextMode="Password" CssClass="form-control" data-type-validate="confirmPassword"
                data-fv-field="confirmPassword"></asp:TextBox>
        </div>
        <div class="form-group">
            <asp:Button ID="btnSubmit" runat="server" Text="Cập nhật" CssClass="btn btn-success" OnClientClick="return validForm();" ClientIDMode="Static"/>
        </div>
        <asp:HiddenField ID="hdUserName" runat="server" ClientIDMode="Static"/>
    </div>
    <script>
        function validForm() {
            var bootstrapValidator = $("#divValidate").data('bootstrapValidator');
            bootstrapValidator.validate();
            if (bootstrapValidator.isValid()) {
                JSUtilities.ChangePassword();
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
</asp:Content>
