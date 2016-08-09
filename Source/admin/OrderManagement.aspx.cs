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

namespace HaloBoSach.admin
{
    public partial class OrderManagement : System.Web.UI.Page
    {
        string head = @"<table class=""table table-bordered"">
                            <thead>
                                <tr>
                                    <th>Ngày lập</th>
                                    <th>Tên KH</th>
                                    <th>Số điện thoại</th>
                                    <th>Tổng số lượng</th>
                                    <th>Giá trị đơn hàng</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tfoot>
                                <tr>
                                    <td colspan='4' class='text-right'>Tổng số đơn hàng chờ xác nhận</td>
                                    <td>{0}</td>
                                </tr>
                            </tfoot>";
        string body = @"<tr>
                            <td>{0}</td>
                            <td>{1}</td>
                            <td>{2}</td>
                            <td>{3}</td>
                            <td>{4}</td>
                            <td><a class='btn btn-primary btn-view-order' href='/xac-nhan-don-hang/{5}'>Xem chi tiết</a></td>
                        </tr>";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            DataSet ds = DataAccessLayer.ExecuteDataSet("ShopCart_GetAllOrder");
            StringBuilder sb = new StringBuilder();
            List<ShoppingCart> lstShoppingCart = BuildListPendingCart(ds);
            string header = string.Format(head, lstShoppingCart.Count);
            sb.Append(header);
            if (lstShoppingCart.Count > 0)
            {
                sb.Append("<tbody>");

                foreach (ShoppingCart cart in lstShoppingCart)
                {
                    string item = string.Format(body, cart.OrderDate.ToString("dd/MM/yyyy"), cart.CustomerName, cart.Phone, cart.TotalItem,Utilities.FormatCurrency(cart.TotalPrice), cart.GuidId);
                    sb.Append(item);
                }
            }
            if (lstShoppingCart.Count > 0)
            {
                sb.Append("</tbody>");
            }
            sb.Append("</table>");
            divContent.InnerHtml = sb.ToString();
        }

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
                    string Note = row["Note"].ToString();
                    DateTime OrderDate = DateTime.Parse(row["OrderDate"].ToString());
                    int CartDetailId = int.Parse(row["CartDetailId"].ToString());
                    int ProductId = int.Parse(row["ProductId"].ToString());
                    string ProductName = row["ProductName"].ToString();
                    double Price = double.Parse(row["Price"].ToString());
                    int Amount = int.Parse(row["Amount"].ToString());
                    string CartGuidId = row["GuidId"].ToString();
                    ShoppingCart cart = lstShoppingCart.Where(p => p.ShopCartId == ShopCartId).FirstOrDefault();
                    if (cart != null)
                    {
                        CartItem cartItem = new CartItem();
                        cartItem.CartDetailId = CartDetailId;
                        cartItem.ProductId = ProductId;
                        cartItem.ProductName = ProductName;
                        cartItem.Price = Price;
                        cartItem.Amount = Amount;
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
                        newCart.ListProduct = new List<CartItem>();
                        CartItem cartItem = new CartItem();
                        cartItem.CartDetailId = CartDetailId;
                        cartItem.ProductId = ProductId;
                        cartItem.ProductName = ProductName;
                        cartItem.Price = Price;
                        cartItem.Amount = Amount;
                        newCart.ListProduct.Add(cartItem);
                        lstShoppingCart.Add(newCart);
                    }
                }
            }
            return lstShoppingCart;
        }
    }
}