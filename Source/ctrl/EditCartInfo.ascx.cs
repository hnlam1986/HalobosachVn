using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;
using HaloBoSach.entity;

namespace HaloBoSach.ctrl
{
    public partial class EditCartInfo : System.Web.UI.UserControl
    {
        private const string head = @"<table class=""table table-bordered"" id=""cart_summary"">
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
                                        <td class='cart_quantity text-center'><span class='glyphicon glyphicon-refresh update-quantity' onclick='ProductDetailEvent.ViewOrderUpdateItemInCartSummary({5})'></span>
                                        <div class='form-group'>
                                        <input class='input-text qty form-control' maxlength='12' size='4' value='{3}' id='txtQuantity_{5}' name='number' ></input> x 100gr
                                        </div>
                                        </td>
                                        <td class='cart_total' data-title='Total' id='cartSummaryItemTotal_{5}'>{4}</td>
                                        <td class='cart_delete text-center' data-title='Delete'><span class='glyphicon glyphicon-remove pointer' onclick='ProductDetailEvent.ViewOrderDeleteItemInCartSummary({5})'></span></td>
                                    </tr>";
        private const string foot = @"</table>";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            Page.ClientScript.RegisterClientScriptInclude("key1", "/Scripts/bootstrapValidator.js");
            string guidId = Page.RouteData.Values["guid"].ToString();
            StringBuilder sb = new StringBuilder();
            SqlParameter paramGuidId = new SqlParameter("GuidId", SqlDbType.VarChar);
            paramGuidId.Value = guidId;
            DataSet ds = DataAccessLayer.ExecuteDataSet("ShopCart_GetOrderByGuidId", paramGuidId);
            List<ShoppingCart> lstShoppingCart = BuildListPendingCart(ds);
            ShoppingCart cart = lstShoppingCart != null ? lstShoppingCart.FirstOrDefault() : null;
            divInfo.Visible = false;
            processedMessage.Visible = false;
            if (cart != null)
            {
                divInfo.Visible = true;
                processedMessage.Visible = false;
                hdShopCartId.Value = cart.ShopCartId.ToString();
                txtCustomerName.Text = cart.CustomerName;
                txtShipAddress.Text = cart.ShipAddress;
                txtPhone.Text = cart.Phone;
                txtEmail.Text = cart.Email;
                txtNote.Text = cart.Note;
                Session["ShoppingCartAdmin"] = cart;
                string header = string.Format(head, Utilities.FormatCurrency(cart.TotalPrice));
                sb.Append(header);
                if (cart.ListProduct.Count > 0)
                {
                    sb.Append("<tbody>");
                }
                foreach (CartItem item in cart.ListProduct)
                {
                    string orderItem = string.Format(body, item.ImagePath, item.ProductName, Utilities.FormatCurrency(item.Price), item.Amount, Utilities.FormatCurrency(item.Total), item.CartDetailId);
                    sb.Append(orderItem);
                }
                if (cart.ListProduct.Count > 0)
                {
                    sb.Append("</tbody>");
                }

                sb.Append(foot);
                divContent.InnerHtml = sb.ToString();
            }
            else
            {
                processedMessage.Visible = true;
                divInfo.Visible = false;
                if (DisplayCompletedButton == true)
                {
                    processedMessage.InnerText = "Đơn hàng đã hoàn tất, vui lòng chọn đơn hàng tiếp theo";
                }
            }
        }
        public bool DisplayCompletedButton { get; set; }
        private List<ShoppingCart> BuildListPendingCart(DataSet ds)
        {
            List<ShoppingCart> lstShoppingCart = new List<ShoppingCart>();
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int ShopCartId = int.Parse(row["ShopCartId"].ToString());
                    string CustomerName = row["CustomerName"].ToString();
                    string Phone = row["Phone"].ToString();
                    string Email = row["Email"].ToString();
                    string Address = row["ShipAddress"].ToString();
                    string Note = row["Note"].ToString();
                    DateTime OrderDate = DateTime.Parse(row["OrderDate"].ToString());
                    int CartDetailId = int.Parse(row["CartDetailId"].ToString());
                    int ProductId = int.Parse(row["ProductId"].ToString());
                    string ProductName = row["ProductName"].ToString();
                    double Price = double.Parse(row["Price"].ToString());
                    int Amount = int.Parse(row["Amount"].ToString());
                    string CartGuidId = row["GuidId"].ToString();
                    string ImagePath = row["Image"].ToString();
                    ShoppingCart cart = lstShoppingCart.Where(p => p.ShopCartId == ShopCartId).FirstOrDefault();
                    if (cart != null)
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.CartDetailId = CartDetailId;
                        cartItem.ProductId = ProductId;
                        cartItem.ProductName = ProductName;
                        cartItem.Price = Price;
                        cartItem.Amount = Amount;
                        cartItem.ImagePath = "/product/" + ImagePath;
                        if (cart.ListProduct == null)
                        {
                            cart.ListProduct = new List<CartItem>();
                        }
                        cart.ListProduct.Add(cartItem);
                    }
                    else
                    {
                        ShoppingCart newCart = new ShoppingCart();
                        newCart.ShopCartId = ShopCartId;
                        newCart.CustomerName = CustomerName;
                        newCart.Phone = Phone;
                        newCart.Email = Email;
                        newCart.OrderDate = OrderDate;
                        newCart.GuidId = CartGuidId;
                        newCart.ShipAddress = Address;
                        newCart.Note = Note;
                        newCart.ListProduct = new List<CartItem>();
                        CartItem cartItem = new CartItem();
                        cartItem.CartDetailId = CartDetailId;
                        cartItem.ProductId = ProductId;
                        cartItem.ProductName = ProductName;
                        cartItem.Price = Price;
                        cartItem.Amount = Amount;
                        cartItem.ImagePath = "/product/"+ImagePath;
                        newCart.ListProduct.Add(cartItem);
                        lstShoppingCart.Add(newCart);
                    }
                }
            }
            return lstShoppingCart;
        }
    }
}