using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class UpdateNaviLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            SqlParameter AdvID = new SqlParameter("NewsCateID", SqlDbType.Int);
            AdvID.Value = Request.QueryString["id"];
            DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetDirectLinkByCateId", AdvID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string url = dr[0].ToString();
                string route = dr[1].ToString();
                txtchuyen.Text = url;
                txtRoutePath.Text = route;
            }
        }

        protected void Button1_OnClick(object sender, EventArgs e)
        {
            SqlParameter id = new SqlParameter("NewsCateID", SqlDbType.Int);
            id.Value = Request.QueryString["id"];
            SqlParameter url = new SqlParameter("DirectURL", SqlDbType.VarChar);
            url.Value = txtchuyen.Text;
            SqlParameter route = new SqlParameter("RoutePath", SqlDbType.VarChar);
            route.Value = txtRoutePath.Text;
            DataAccessLayer.ExcuteNoneQuery("Cate_SaveDirectLinkByCateId", id, url, route);
        }
    }
}