using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.entity;

namespace HaloBoSach
{
    public partial class CartSummary : System.Web.UI.Page
    {
        private const string head = @"<div id='divCartSummary'><table class=""table table-bordered"" id=""cart_summary"">
                                        <thead>
                                            <tr>
                                                <th class=""cart_product first_item"">Ảnh</th>
                                                <th class=""cart_description item"">Tên sản phẩm</th>
                                                <th class=""cart_unit item"">Đơn giá</th>
                                                <th class=""cart_quantity item"">Số lượng</th>
                                                <th class=""cart_total item"">Thành tiền</th>
                                                <th class=""cart_delete last_item"">Xóa</th>
                                            </tr>
                                        </thead>
                                        <tfoot>
                                            <tr>
                                                <td colspan=""5"" class='text-right'>Tổng cộng</td><td><span id='cartSummaryTotalAll'>{0}</span></td>
                                            </tr>
                                        </tfoot>";
                                        
        private const string body = @"<tr id='cartSummaryItem_{5}' class='cart_item first_item address_0 odd'>
                                        <td class='cart_product'><img src='{0}' class='cart-summary-img'></img></td>
                                        <td class='cart_description'>{1}</td>
                                        <td class='cart_unit' data-title='Unit price'>{2}/100gr</td>
                                        <td class='cart_quantity text-center'><span class='glyphicon glyphicon-refresh update-quantity' onclick='ProductDetailEvent.UpdateItemInCartSummary({5})'></span>
                                        <div class='form-group'>
                                        <input type='text' class='form-control' maxlength='12' size='4' value='{3}' id='txtQuantity_{5}' name='number_womsg'></input>
                                        </div>
                                        x 100gr</td>
                                        <td class='cart_total' data-title='Total' id='cartSummaryItemTotal_{5}'>{4}</td>
                                        <td class='cart_delete text-center' data-title='Delete'><span class='glyphicon glyphicon-remove pointer' onclick='ProductDetailEvent.DeleteItemInCartSummary({5})'></span></td>
                                    </tr>";
        private const string foot = @"</table></div>";
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            StringBuilder sb = new StringBuilder();
            ShoppingCart cart = Utilities.GetShopCartFromSession(Session);
            string header = string.Format(head, Utilities.FormatCurrency(cart.TotalPrice));
            sb.Append(header);
            if (cart.ListProduct.Count > 0)
            {
                sb.Append("<tbody>");
            }
            foreach (CartItem item in cart.ListProduct)
            {
                string orderItem = string.Format(body, item.ImagePath, item.ProductName,Utilities.FormatCurrency(item.Price), item.Amount, Utilities.FormatCurrency(item.Total), item.ProductId);
                sb.Append(orderItem);
            }
            if (cart.ListProduct.Count > 0)
            {
                sb.Append("</tbody>");
            }
            
            sb.Append(foot);
            TitleBar.Title = "Giỏ Hàng (" + cart.TotalItem + " SP)";
            divContent.InnerHtml = sb.ToString();
        }
    }
}