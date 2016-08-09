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
    public partial class ListNewsTopBoldLink : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string StoreProcedureName { get; set; }
        public string PositionKey { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlParameter key = new SqlParameter("Key", PositionKey);
            DataSet ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName, key);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string image = ds.Tables[0].Rows[0]["ThumnailImagePath"].ToString();
                string title = ds.Tables[0].Rows[0]["NewsTitle"].ToString();
                string id = ds.Tables[0].Rows[0]["NewsID"].ToString();
                string cate_title = ds.Tables[0].Rows[0]["ParentName"].ToString();
                Title = cate_title;
                string url = "/NewsDetail/"+Utilities.ConvertUnicodeToAscii(title)+"/"+id;
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