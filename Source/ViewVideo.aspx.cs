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

namespace HaloBoSach
{
    public partial class ViewVideo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IList<string> lst= Request.GetFriendlyUrlSegments();
            if (IsPostBack)
                return;
            if (lst.Count >= 2)
            {
                string id = lst[1];
                string parent = lst[0];
                if (parent == "0")
                {
                    PanelGroupCate.Visible = true;
                    GetAllCateByParentID(id);
                }
            }
        }
        private void GetAllCateByParentID(string ID)
        {
            SqlParameter key = new SqlParameter("CateID", ID);
            DataSet ds = DataAccessLayer.ExecuteDataSet("News_GetAllVideoGroupByCateID", key);
            rpGroupCate.DataSource = ds;
            rpGroupCate.DataBind();

        }
    }
}