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
    public partial class ProductCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Category_GetCategoryIsProductCate");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder();
                str.Append("<ul class='list-category'>");
                string liTemplate = @"<li class={3}><a href='{1}'><figure><img src='/Product/{0}'></img></figure>
                                    <div class='category-title'>{2}
                                    <div class='btn-view-all-product'>XEM TẤT CẢ SẢN PHẨM</div>
                                    </div>
                                    </a></li>";
                bool odd = true;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string ParentId = dataRow["ParentId"].ToString();
                    string ParentName = dataRow["ParentName"].ToString();
                    string ProductCategoryId = dataRow["ProductCategoryId"].ToString();
                    string ProductCategoryName = dataRow["ProductCategoryName"].ToString();
                    string ImagePath = dataRow["ImagePath"].ToString();
                    string href = string.Format("/{0}-{1}/{2}/{3}", Utilities.ConvertUnicodeToAscii(ParentName).ToLower(),
                        Utilities.ConvertUnicodeToAscii(ProductCategoryName).ToLower(),
                        ParentId, ProductCategoryId);
                    string li = string.Format(liTemplate, ImagePath, href, ProductCategoryName,odd?"odd":"even");
                    str.Append(li);
                    odd = !odd;
                }
                str.Append("</ul>");
                divContent.InnerHtml = str.ToString();
            }
        }
    }
}