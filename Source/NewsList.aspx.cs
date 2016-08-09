using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using HaloBoSach.Dal;

namespace HaloBoSach
{
    public partial class NewsList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            object _id = Page.RouteData.Values["id"];
            object _parent = Page.RouteData.Values["parent"];
            if (IsPostBack)
                return;
            if (_parent != null && _id != null)
            {
                string id = _id.ToString();
                string parent = _parent.ToString();
                if (parent == "0")
                {
                    divHomeNews.Visible = true;
                    divListNewsInLine.Visible = false;
                }
                else
                {
                    divHomeNews.Visible = false;
                    divListNewsInLine.Visible = true;
                    ListNewsLine.CategoryId = _id.ToString();
                }
            }
        }
        private void GetAllCateByParentID(string ID)
        {
            //SqlParameter key = new SqlParameter("CateID", ID);
            //DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetAllGroupByCateID", key);
            //rpGroupCate.DataSource = ds;
            //rpGroupCate.DataBind();
            
        }

        private void GetListNews(string ID)
        {
            //SqlParameter cateId = new SqlParameter("NewsCateID", SqlDbType.Int);
            //cateId.Value = ID;
            //DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetNewsByCategoryId", cateId);
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    rpNews.DataSource = ds;
            //    rpNews.DataBind();
            //}
        }
    }
}