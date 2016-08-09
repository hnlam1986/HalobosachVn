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
    public partial class ProductEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
                return;
            if (Request.QueryString["action"] != null && Request.QueryString["action"] == "edit")
            {
                LoadProduct();
                string id = Request.QueryString["id"];
                if (Request.QueryString["action"] == "edit" && Request.QueryString["reload"] != null && Request.QueryString["reload"] == "true")
                {
                    string cateID = Request.QueryString["cateID"].ToString();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "HaloBoSachUpdateProduct", "$(window.opener.document.getElementById('" + cateID + "')).children(\"a\").get(0).click();", true);
                }
            }
        }

        protected void btnSubmitTop_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {
                if (Request.QueryString["action"] == "new")
                {
                    InsertProduct();
                }
                else
                {
                    if (Request.QueryString["id"] != null)
                    {
                        UpdateProduct();
                    }
                }

            }
        }

        private void InsertProduct()
        {
            string id = Request.QueryString["id"];
            SqlParameter ImagePath = new SqlParameter("ImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }

            ImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter ProductName = new SqlParameter("ProductName", SqlDbType.NVarChar);
            ProductName.Value = txtProductName.Text;
            SqlParameter Gia = new SqlParameter("Gia", SqlDbType.Float);
            Gia.Value = txtGia.Text;
            SqlParameter Description = new SqlParameter("Description", SqlDbType.NVarChar);
            Description.Value = hidContent.Value;
            SqlParameter NoiBat = new SqlParameter("NoiBat", SqlDbType.VarChar);
            NoiBat.Value = chkNoiBat.Checked;
            SqlParameter CateId = new SqlParameter("NewsCateID", SqlDbType.Int);
            CateId.Value = int.Parse(id);
            

            

            SqlParameter ProductId = new SqlParameter("ProductId", SqlDbType.Int);
            ProductId.Direction = ParameterDirection.Output;
            string res = DataAccessLayer.ExcuteNoneQueryHasOutput("Product_Insert", "ProductId", ImagePath, ProductName, Gia,
                                                       Description, NoiBat, ProductId, CateId);
            if (!string.IsNullOrEmpty(res))
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    fuLogo.SaveAs(Server.MapPath("/Product/" + guid));
                }
                Response.Redirect("ProductEditor.aspx?action=edit&reload=true&id=" + res + "&cateId=" + id,
                             false);
            }
        }

        private void UpdateProduct()
        {
            string id = Request.QueryString["id"];
            string cateid = Request.QueryString["cateId"];
            SqlParameter ImagePath = new SqlParameter("ImagePath", SqlDbType.VarChar);
            string guid = "";
            if (!string.IsNullOrEmpty(fuLogo.FileName))
            {
                string extention = fuLogo.FileName.Substring(fuLogo.FileName.IndexOf("."));
                guid = Guid.NewGuid() + extention;
            }
            ImagePath.Value = string.IsNullOrEmpty(fuLogo.FileName) ? hdImage.Value : guid;
            SqlParameter ProductName = new SqlParameter("ProductName", SqlDbType.NVarChar);
            ProductName.Value = txtProductName.Text;
            SqlParameter Gia = new SqlParameter("Gia", SqlDbType.Float);
            Gia.Value = txtGia.Text;
            SqlParameter Description = new SqlParameter("Description", SqlDbType.NVarChar);
            Description.Value = hidContent.Value;
            SqlParameter NoiBat = new SqlParameter("NoiBat", SqlDbType.VarChar);
            NoiBat.Value = chkNoiBat.Checked;
            SqlParameter ProductId = new SqlParameter("@ProductId", SqlDbType.Int);
            ProductId.Value = int.Parse(id);
            bool res = DataAccessLayer.ExcuteNoneQuery("Product_Update", ImagePath, ProductName, Gia,
                                                       Description, NoiBat, ProductId);
            if (res)
            {
                if (!string.IsNullOrEmpty(fuLogo.FileName))
                {
                    fuLogo.SaveAs(Server.MapPath("/Product/" + guid));
                    if (File.Exists(Server.MapPath("/Product/" + hdImage.Value)))
                    {
                        File.Delete(Server.MapPath("/Product/" + hdImage.Value));
                    }
                    hdImage.Value = fuLogo.FileName;
                }
                Response.Redirect("ProductEditor.aspx?action=edit&reload=true&id=" + id + "&cateId=" + cateid,
                             false);
            }
           
        }

        private void LoadProduct()
        {
            string id = Request.QueryString["id"];
            SqlParameter ProductId = new SqlParameter("ProductId", SqlDbType.Int);
            ProductId.Value = id;
            DataSet ds = DataAccessLayer.ExecuteDataSet("Product_GetProductById", ProductId);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dr = ds.Tables[0].Rows[0];
                string ImagePath = dr["Image"].ToString();
                string ProductName = dr["ProductName"].ToString();
                string Gia = dr["Price"].ToString();
                string Description = dr["Description"].ToString();
                bool NoiBat = (bool)dr["NoiBat"];
                hdImage.Value = ImagePath;
                imgProduct.ImageUrl = !string.IsNullOrWhiteSpace( ImagePath) ? "/product/" + ImagePath : "/Images/logo.png";
                txtProductName.Text = ProductName;
                txtGia.Text = Gia;
                hidContent.Value = Description;
                chkNoiBat.Checked = NoiBat;
            }
        }
    }
}