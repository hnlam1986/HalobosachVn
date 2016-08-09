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

namespace HaloBoSach
{
    public partial class ProductDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            object _id = Page.RouteData.Values["id"];
            if (_id != null)
            {
                string id = _id.ToString();
                SqlParameter productId = new SqlParameter("ProductId", SqlDbType.Int);
                productId.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Product_GetProductById", productId);
                if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string ProductName = row["ProductName"].ToString();
                        string CateName = row["NewsCateName"].ToString();
                        string Description = row["Description"].ToString();
                        string Price = row["Price"].ToString();
                        string Image = row["Image"].ToString();
                        hdProductImage.Value ="/product/"+ Image;
                        lblProductName.Text = ProductName;
                        lblDescription.Text = Description;
                        lblPrice.Text = Utilities.FormatCurrency(double.Parse( Price));
                        lblPrice.Attributes.Add("data-price", Price);
                        hdProductId.Value = id;
                        lblTotal.Text = Utilities.FormatCurrency(double.Parse(Price));
                        TitleBar.Title = CateName + "<div class='icon'><span class='glyphicon glyphicon-play'></span></div>" + ProductName;
                    }
                }
            }
        }
    }
}