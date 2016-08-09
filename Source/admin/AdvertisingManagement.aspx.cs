using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class AdvertisingManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAllImage", null);
            //gvAdvertising.DataSource = ds;
            //gvAdvertising.DataBind();
            DataSet ds = DataAccessLayer.ExecuteDataSet("Adv_GetAllImage", null);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table class='custom-table' id='tblAdv'>");
            sb.Append("<thead><tr>");
            //sb.Append("<td>Anh</td><td>Bat Dau</td><td>Ket Thuc</td><td>Link</td><td></td>");
            sb.Append("<td>Anh</td><td>Link</td><td></td>");
            sb.Append("</tr></thead><tbody>");
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
               
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    string id = dr["AdvID"].ToString();
                    string imgPath = dr["AdvImagePath"].ToString();
                    string fromdate = DateTime.Parse(dr["DisplayFrom"].ToString()).ToString("dd/MM/yyyy");
                    string todate = DateTime.Parse(dr["DisplayTo"].ToString()).ToString("dd/MM/yyyy");
                    string url = dr["LinkURL"].ToString();
                    string template = Utilities.GetTempleAdvItem();
                    sb.AppendLine(string.Format(template, imgPath, fromdate, todate, url, id));

                }
                
            }
            sb.Append("</tbody></table>");
            divAdv.InnerHtml = sb.ToString();

        }
    }
}