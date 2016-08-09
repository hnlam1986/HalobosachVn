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
    public partial class QuangCao : System.Web.UI.UserControl
    {
        public bool BigSize { get; set; }
        public bool OnTop { get; set; }
        public string Height { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
           
             if (BigSize)
             {
                 Height = "330px";
             }
             else
             {
                 Height = "330px";
             }
            SqlParameter pBig = new SqlParameter("Big",SqlDbType.Bit);
            pBig.Value = BigSize;
            SqlParameter pPos = new SqlParameter("Pos", SqlDbType.Bit);
            pPos.Value = OnTop;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetImageBy", pBig, pPos);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            
        }
    }
}