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
    public partial class ProductList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            object _id = Page.RouteData.Values["id"];
            if (_id != null)
            {
                string cateId = _id.ToString();
                SqlParameter paramParentNodeID = new SqlParameter("NewsCateID", SqlDbType.Int);
                paramParentNodeID.Value = cateId;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Product_GetProductByCategoryId", paramParentNodeID);
                ProductListControl.Products = ds;
            }
        }
    }
}