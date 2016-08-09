using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HaloBoSach.ctrl
{
    public partial class ProductListControl : System.Web.UI.UserControl
    {
        public DataSet Products { get; set; }
        public string ProductTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            string cateName = "";
            StringBuilder html = new StringBuilder();
            html.Append("<section class='main'><ul class='ch-grid' id='circle-list-product'>");
            string itemTemplate = @"<li class='col-xs-3' id=productList{4} data-product-id='{4}'>
                                            <div id='productImage' class='ch-item' style=""background-image:url('{0}');background-position:center;background-repeat:no-repeat;background-size: cover;"" data-image='/product/{0}'>
	                                            <div class='ch-info'>
                                                    <div class='cart-icon-outbound'>
		                                            <a onclick='ProductDetailEvent.AddQuickToCartFromList({4});'><div class='cart-icon'></div></a>
                                                    </div>
                                                    <span>MUA HÀNG NHANH</span>
                                                    <p></p>
                                                    <span>XEM CHI TIẾT</span>
                                                    <div class='view-detail-icon-outbound'>
                                                    <a href='{1}'><div class='view-detail-icon'></div></a>
                                                    </div>
	                                            </div>
                                            </div>
                                            <h3>
                                            <div class='box_inner'>
                                            <a style='height:52px;' title='{2}' href='{1}' id='productName'>{2}</a>
                                            </div>
                                            </h3>
                                            <p class='product-price'>
                                            <span class='price' data-price='{5}' id='productPrice'>{3}</span>
                                            /100gr
                                            </p>    
                                            <a class='take-to-cart' onclick='ProductDetailEvent.AddToCartFromList({4});'><div class='select-product'></div></a>
                                    </li>";
            if (Products != null && Products.Tables != null && Products.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in Products.Tables[0].Rows)
                {
                    string id = dataRow["ProductId"].ToString();
                    string title = dataRow["ProductName"].ToString();
                    cateName = dataRow["NewsCateName"].ToString();
                    double price = double.Parse(dataRow["Price"].ToString());
                    string image = dataRow["Image"].ToString();
                    string href = "/chi-tiet-san-pham/" + id + "/" + Utilities.ConvertUnicodeToAscii(title) + ".halobosach.vn";
                    string imgUrl = !string.IsNullOrWhiteSpace(image) ? "/product/" + image : "/Images/logo.png";
                    html.Append(string.Format(itemTemplate, imgUrl, href, title, Utilities.FormatCurrency(price), id, price));
                }
            }
            else
            {
                divProductList.Visible = false;
            }
            html.Append("</ul></section>");
            divProductListContent.InnerHtml = html.ToString();
            if (!string.IsNullOrWhiteSpace(ProductTitle))
                TitleBar.Title = ProductTitle;
            else
                TitleBar.Title = cateName;
        }
    }
}