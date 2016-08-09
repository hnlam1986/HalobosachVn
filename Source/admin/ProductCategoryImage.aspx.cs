using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class ProductCategoryImage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            string id = Request.QueryString["id"];
            SqlParameter NewsCateID = new SqlParameter("NewsCateID", SqlDbType.Int);
            NewsCateID.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Cate_LoadProductCateImageAndColor", NewsCateID);
            if (ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    string path = row["ImagePath"].ToString();
                    string color = row["CategoryColor"].ToString();
                    hdPath.Value = path;
                    txtcolor.Text = color;
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string filename = FileUpload1.FileName;
            string extention = string.IsNullOrEmpty(filename) ? "" : filename.Substring(filename.IndexOf("."));
            string newFileName = string.IsNullOrEmpty(filename) ? "" : Guid.NewGuid().ToString() + extention;
            SqlParameter id = new SqlParameter("NewsCateID", SqlDbType.Int);
            id.Value = Request.QueryString["id"];
            SqlParameter ImagePath = new SqlParameter("ImagePath", SqlDbType.VarChar);
            ImagePath.Value = newFileName;
            SqlParameter color = new SqlParameter("Color", SqlDbType.VarChar);
            color.Value = txtcolor.Text;


            if (string.IsNullOrEmpty(newFileName))
            {
                ImagePath.Value = hdPath.Value;
            }
            bool res = DataAccessLayer.ExcuteNoneQuery("Cate_UpdateProductCateImage", id, ImagePath, color);
            if (res && !string.IsNullOrEmpty(newFileName))
            {
                if(File.Exists(Server.MapPath("/Product/" + hdPath.Value))){
                    File.Delete(Server.MapPath("/Product/" + hdPath.Value));
                }
                FileUpload1.SaveAs(Server.MapPath("/Product/" + newFileName));
            }

        }
    }
}