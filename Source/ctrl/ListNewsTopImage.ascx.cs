using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.ctrl
{
    public partial class ListNewsTopImage : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string StoreProcedureName { get; set; }
        public string PositionKey { get; set; }
        public string ParentID { get; set; }
        public string CateID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = null;
            SqlParameter key = new SqlParameter("Key", PositionKey);
            ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName, key);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string image = ds.Tables[0].Rows[0]["ThumnailImagePath"].ToString();
                string title = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
                string id = ds.Tables[0].Rows[0]["NewsID"].ToString();
                string parent_title = ds.Tables[0].Rows[0]["ParentName"].ToString();
                string cate_id = ds.Tables[0].Rows[0]["ParentID"].ToString();
                string parent_id = ds.Tables[0].Rows[0]["NewsCateParentID"].ToString();
                Title = parent_title;
                ParentID = parent_id;
                CateID = cate_id;
                string url = "/NewsDetail/" + Utilities.ConvertUnicodeToAscii(title) + "/" + id;
                Image1.ImageUrl = "/NewsAvarta/" + image;
                topNewsLink.HRef = url;

                topTitle.InnerText = title;
                DataRow dr = ds.Tables[0].Rows[0];
                dr.Delete();
                int index = 0;

                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
        
    }
}