using System;
using System.Collections;
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
    public partial class ListNewsLink : System.Web.UI.UserControl
    {
        public string Title { get; set; }
        public string Height { get; set; }
        public string StoreProcedureName { get; set; }
        public int NewID { get; set; }
        public int CateID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet ds = null;
            if(NewID!=0 && CateID!=0)
            {
                SqlParameter pCateID = new SqlParameter("CateID",SqlDbType.Int);
                pCateID.Value = CateID;
                SqlParameter pNewsID = new SqlParameter("NewsID", SqlDbType.Int);
                pNewsID.Value = NewID;
                ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName, pCateID, pNewsID);
            }else
            {
                ds = DataAccessLayer.ExecuteDataSet(StoreProcedureName);
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
        }
    }
}