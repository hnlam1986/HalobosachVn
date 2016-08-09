using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HaloBoSach.Dal;

namespace HaloBoSach.admin
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
                return;
            if (!AjaxFileUpload1.IsInFileUploadPostBack)
            {
                GetCategory();
                string id = Request.QueryString["id"].ToString();
                string folder = "";
                string title = "";
                string cate = "";
                bool hot = false;
                SqlParameter pID = new SqlParameter("AlbumID", SqlDbType.Int);
                pID.Value = id;
                DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadAlbumByID", pID);
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    folder = ds.Tables[0].Rows[0]["FolderPath"].ToString();
                    title = ds.Tables[0].Rows[0]["AlbumTitle"].ToString();
                    cate =ds.Tables[0].Rows[0]["NewsCateID"]!=null?ds.Tables[0].Rows[0]["NewsCateID"].ToString():"";
                    hot = bool.Parse(ds.Tables[0].Rows[0]["HotAlbum"].ToString() == "" ? "False" : ds.Tables[0].Rows[0]["HotAlbum"].ToString());
                    ListItem selectedListItem = DropDownList1.Items.FindByValue(cate);
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;
                    }
                }
                
                UploadSession up = new UploadSession();
                up.Id = id;
                up.Folder = folder;
                Session["upload"] = up;
                Session["BookPage"] = 0;
                TextBox1.Text = title;
                chkHot.Checked = hot;
            }
        }
        private void GetCategory()
        {
            DataSet ds = DataAccessLayer.ExecuteDataSet("Album_LoadAlbumCategory");
            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    string id = dataRow["NewsCateID"].ToString();
                    string text = dataRow["NewsCateName"].ToString();
                    DropDownList1.Items.Add(new ListItem(text, id, true));
                }
            }
        }
        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string guid = Guid.NewGuid().ToString();
            UploadSession obj = (UploadSession)Session["upload"];
            string filename = Server.MapPath("/Albums/" + obj.Folder + "/" + guid + ".JPG");
            AjaxFileUpload1.SaveAs(filename);
            SqlParameter pID = new SqlParameter("AlbumID", SqlDbType.Int);
            pID.Value = obj.Id;
            int pageIndex = int.Parse(Session["BookPage"].ToString());
            pageIndex++;
            SqlParameter pOrder = new SqlParameter("OrderImage", SqlDbType.Int);
            pOrder.Value = pageIndex;
            Session["BookPage"] = pageIndex;
            SqlParameter pUrl = new SqlParameter("ImageUrl", SqlDbType.NVarChar);
            pUrl.Value = guid+".jpg";
            SqlParameter insertedKey = new SqlParameter("AlbumDetailID", SqlDbType.Int);
            insertedKey.Direction = ParameterDirection.Output;
            string NewsID = DataAccessLayer.ExcuteNoneQueryHasOutput("Album_InsertAlbumDetail", "AlbumDetailID", pID, pUrl, insertedKey, pOrder);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlParameter pID = new SqlParameter("AlbumID", SqlDbType.Int);
            string id = Request.QueryString["id"].ToString();
            pID.Value = id;
            SqlParameter pTitle = new SqlParameter("AlbumTitle", SqlDbType.NVarChar);
            pTitle.Value = TextBox1.Text;
            SqlParameter pHot = new SqlParameter("HotAlbum", SqlDbType.Bit);
            pHot.Value = chkHot.Checked;
            SqlParameter pCate = new SqlParameter("NewsCateID", SqlDbType.Int);
            pCate.Value = DropDownList1.SelectedValue;
            DataAccessLayer.ExcuteNoneQuery("Album_UpdateAlbumTitle", pID, pTitle, pHot, pCate);
        }
    }
    [Serializable()]
    public class UploadSession
    {
        public string Id { get; set; }
        public string Folder { get; set; }
    }
}