using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach
{
    public partial class ListNewsLine : System.Web.UI.UserControl
    {
        public string RoutePath { get; set; }
        public string StoreProcedureName { get; set; }
        public string CategoryId { get; set; }
        public string GetImageUrl(string url)
        {
            return !string.IsNullOrWhiteSpace(url) ? "/NewsAvarta/" + url : "/Images/logo.png";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = null;
            SqlParameter Cateid = new SqlParameter("NewsCateID", CategoryId);
            ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName, Cateid);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string image = ds.Tables[0].Rows[0]["ThumnailImagePath"].ToString();
                string shortContent = ds.Tables[0].Rows[0]["ShortContent"].ToString();
                string title = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
                string id = ds.Tables[0].Rows[0]["NewsID"].ToString();
                string cateName = ds.Tables[0].Rows[0]["NewsCateName"].ToString();
                string url = "/" + RoutePath + "/" + id + "/" + Utilities.ConvertUnicodeToAscii(title) + ".halobosach.vn";
                TitleBar.Title = cateName;
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}