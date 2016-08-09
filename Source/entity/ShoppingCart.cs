using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HaloBoSach.entity
{
    [Serializable()]
    public class ShoppingCart
    {
        public List<CartItem> ListProduct { get; set; }
        public int TotalItem
        {
            get
            {
                return ListProduct.Count;
            }
        }
        public double TotalPrice
        {
            get
            {
                return ListProduct.Sum(item => item.Total);
            }
        }
        public int ShopCartId { get; set; }
        public string CustomerName { get; set; }
        public string ShipAddress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public string GuidId { get; set; }
        public DateTime OrderDate { get; set; }
    }
    [Serializable()]
    public class CartItem
    {

        public int CartDetailId { get; set; }
        public int ProductId { get; set; }
        public string ImagePath { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public double Total
        {
            get
            {
                return Price * Amount;
            }
        }

    }
    [Serializable()]
    public class ReturnCartSummary
    {
        public int TotalItem { get; set; }
        public string TotalPrice { get; set; }
        public string UpdatedTotalPrice { get; set; }
        public ReturnCartSummary(ShoppingCart cart)
        {
            TotalItem = cart.TotalItem;
            TotalPrice =Utilities.FormatCurrency( cart.TotalPrice);
        }
    }
}