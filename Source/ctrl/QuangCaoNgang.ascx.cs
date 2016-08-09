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
    public partial class QuangCaoNgang : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_Load15AlbumDetail");
            Repeater1.DataSource = ds;
            Repeater1.DataBind();
            //if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //{
            //    ImageUrl = ds.Tables[0].Rows[0]["AdvImagePath"].ToString();
            //    Link = ds.Tables[0].Rows[0]["LinkURL"].ToString();

            //}
        }
    }
}