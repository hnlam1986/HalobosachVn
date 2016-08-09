using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.FriendlyUrls;
using HaloBoSach.Dal;

namespace HaloBoSach.ctrl
{
    public partial class ViewAlbum : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public int CateID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> lst = Request.GetFriendlyUrlSegments();
            string id = lst[1];
            SqlParameter pCate = new SqlParameter("CateID", SqlDbType.Int);
            pCate.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadViewAllAlbum", pCate);
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}