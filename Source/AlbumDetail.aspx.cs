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
    public partial class AlbumDetail : System.Web.UI.Page
    {
        public string Title { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"]??"0";
            SqlParameter pId = new SqlParameter("AlbumID", SqlDbType.Int);
            pId.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadAlbumDetailByID", pId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Title = ds.Tables[0].Rows[0]["AlbumTitle"].ToString();
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}