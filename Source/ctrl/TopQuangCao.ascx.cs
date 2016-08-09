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
    public partial class TopQuangCao : System.Web.UI.UserControl
    {
        public bool BigSize { get; set; }
        public bool OnTop { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public int Index = 0;
        public int GetIndex()
        {
            Index++;
            return Index;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Width = "190px";
            Height = "116px";
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetTopAdv");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }

        }
    }
}